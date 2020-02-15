using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace TaskManager.Shared.DataAccess.EF.Extensions
{
    public static class IncludeExpressionConverter
    {
        private const string includeMethodName = nameof(EntityFrameworkQueryableExtensions.Include);
        private const string thenIncludeName = nameof(EntityFrameworkQueryableExtensions.ThenInclude);
        private const string selectMethodName = nameof(Enumerable.Select);
        private const string invalidMemberMessage = "Invalid member along the include path: {0}";

        private static readonly Type stringType = typeof(string);
        private static readonly Type openIEnumerableType = typeof(IEnumerable<>);
        private static readonly Type lamdaExpressionType = typeof(LambdaExpression);

        private static readonly MethodInfo[] entityFrameworkQueryableExtensionsMethods = typeof(EntityFrameworkQueryableExtensions).GetMethods();
        private static readonly MethodInfo includeMethod = entityFrameworkQueryableExtensionsMethods.Single(m => m.Name == includeMethodName &&
                                                                                                                 !m.GetParameters().Any(p => p.ParameterType == stringType));


        private static readonly MethodInfo thenIncludeForCollection = entityFrameworkQueryableExtensionsMethods.Single(m => m.Name == thenIncludeName &&
                                                                                                                            m.GetParameters()[0].ParameterType.GetGenericArguments()[1].IsGenericType &&
                                                                                                                            m.GetParameters()[0].ParameterType.GetGenericArguments()[1].GetGenericTypeDefinition() == typeof(IEnumerable<>));

        private static readonly MethodInfo thenIncludeForEntity = entityFrameworkQueryableExtensionsMethods.Single(m => m.Name == thenIncludeName &&
                                                                                                                        m != thenIncludeForCollection);

        private static readonly MethodInfo selectMethod = typeof(Enumerable).GetMethods().Single(m => m.Name == selectMethodName &&
                                                                                                      m.GetParameters()[1].ParameterType.GetGenericArguments().Count() == 2);


        private static int CountOf(this string text, string pattern) => (text.Length - text.Replace(pattern, string.Empty).Length) / pattern.Length;

        private static Stack<LambdaExpression> GetExpressionParts(Expression expression)
        {
            var stringExpression = expression.ToString();
            var expressionParts = new Stack<LambdaExpression>(stringExpression.CountOf(".") - stringExpression.CountOf($".{selectMethodName}("));

            if (!(expression is LambdaExpression lambda))
                throw new ArgumentException(nameof(expression), $"Parameter must be of type {lamdaExpressionType}");

            GetExpressionParts(lambda.Body, expressionParts);
            return expressionParts;

        }
        private static void GetExpressionParts(Expression expression, Stack<LambdaExpression> expressionParts)
        {
            if (expression is ParameterExpression)
                return;

            if (expression is MemberExpression memberExpression)
            {
                var member = memberExpression.Member;
                if (!(member is PropertyInfo property))
                    throw new ArgumentException(string.Format(invalidMemberMessage, member));

                var parameterExpression = Expression.Parameter(memberExpression.Expression.Type);
                var propertyExpression = Expression.Property(parameterExpression, property);
                expressionParts.Push(Expression.Lambda(propertyExpression, parameterExpression));
                GetExpressionParts(memberExpression.Expression, expressionParts);
            }
            else if (expression is MethodCallExpression methodCallExpression)
            {
                if (methodCallExpression.Method.GetGenericMethodDefinition() != selectMethod)

                    throw new ArgumentException(string.Format(invalidMemberMessage, methodCallExpression));

                if (!(methodCallExpression.Arguments[1] is LambdaExpression innerExpression))
                    throw new ArgumentException(string.Format(invalidMemberMessage, methodCallExpression.Arguments[1]));

                GetExpressionParts(innerExpression.Body, expressionParts);
                GetExpressionParts(methodCallExpression.Arguments[0], expressionParts);
            }
            else
            {
                throw new ArgumentException(string.Format(invalidMemberMessage, expression));
            }
        }

        public static IQueryable<T> IncludeCore<T, TProperty>(this IQueryable<T> queryable, Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            object query = queryable;
            var includeParts = GetExpressionParts(navigationPropertyPath);

            var firstPart = includeParts.Pop();
            var actualIncludeMethod = includeMethod.MakeGenericMethod(new[] { firstPart.Parameters.Single().Type, firstPart.ReturnType });
            query = actualIncludeMethod.Invoke(null, new[] { query, firstPart });
            while (true)
            {
                if (includeParts.Any() == false)
                    break;

                var includePart = includeParts.Pop();
                var genericArguments = new[] { query.GetType().GetGenericArguments()[0], includePart.Parameters.Single().Type, includePart.ReturnType };
                var includeArguments = new[] { query, includePart };
                if (query.GetType().GenericTypeArguments[1].GetInterfaces().Any(f => f.GetGenericTypeDefinition() == openIEnumerableType))
                {
                    query = thenIncludeForCollection.MakeGenericMethod(genericArguments).Invoke(null, includeArguments);
                }
                else
                {
                    query = thenIncludeForEntity.MakeGenericMethod(genericArguments).Invoke(null, includeArguments);
                }
            }
            return (IQueryable<T>)query;
        }
    }
}

