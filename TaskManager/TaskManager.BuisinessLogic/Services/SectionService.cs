using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskManager.BuisinessLogic.Abstraction.Interfaces;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;
using TaskManager.DataAccess.Abstraction.Interfaces.Repositories;
using TaskManager.Shared.Common.DTOs;
using TaskManager.Shared.DataAccess.Abstraction;

namespace TaskManager.BuisinessLogic.Services
{
    public class SectionService : GenericService<SectionDTO, Section, ISectionRepository>, ISectionService
    {
        public SectionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
