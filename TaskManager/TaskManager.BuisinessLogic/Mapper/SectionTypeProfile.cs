using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;
using TaskManager.Shared.Common.DTOs;
using TaskManager.Shared.Common.DTOs.Details;

namespace TaskManager.BuisinessLogic.Mapper
{
    public class SectionTypeProfile : Profile
    {
        public SectionTypeProfile()
        {
            CreateMap<SectionType, SectionTypeDTO>().ReverseMap();
            CreateMap<SectionType, SectionTypeDetailsDTO>().ReverseMap();
        }
    }
}
