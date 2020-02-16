using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.BuisinessLogic;
using TaskManager.BuisinessLogic.Abstraction.Interfaces;
using TaskManager.BuisinessLogic.Services;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;
using TaskManager.DataAccess.Abstraction.Interfaces.Repositories;
using TaskManager.DataAccess.MsSql;
using TaskManager.DataAccess.MsSql.Context;
using TaskManager.DataAccess.MsSql.Repositories;
using TaskManager.Shared.DataAccess.Abstraction;

namespace TaskManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private Assembly[] GetAssemblyAutoMapper()
        {
            return new Assembly[]
            {
                Assembly.GetAssembly(typeof(ProfileAssembly))
            };
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DbContext>(sp =>
            {
                return new TaskManagerContext(new DbContextOptionsBuilder<TaskManagerContext>().UseInMemoryDatabase("TaskManagerDatabase")
                    .Options);
            });
            services.AddDbContext<TaskManagerContext>(option => option.UseInMemoryDatabase("TaskManagerDatabase"));

            //services.AddDbContext<TaskManagerContext>(option => option.UseSqlServer(Configuration.GetConnectionString("TaskManagerContext")));
            //services.AddScoped<DbContext>(sp =>
            //{
            //    return new TaskManagerContext(new DbContextOptionsBuilder<TaskManagerContext>().UseSqlServer(Configuration.GetConnectionString("TaskManagerContext"))
            //        .Options);
            //});

            services.AddControllers().AddNewtonsoftJson();
            services.AddScoped<IDbContextContainer, DbContextContainer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISectionTypeRepository, SectionTypeRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ISectionTypeService, SectionTypeService>();
            services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); }, GetAssemblyAutoMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TaskManagerContext>();
                context.Database.EnsureCreated();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
