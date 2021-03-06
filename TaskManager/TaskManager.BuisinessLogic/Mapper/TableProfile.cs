﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DataAccess.Abstraction.DBOs.Basics;
using TaskManager.Shared.Common.DTOs;
using TaskManager.Shared.Common.DTOs.Details;

namespace TaskManager.BuisinessLogic.Mapper
{
    public class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<Table, TableDTO>().ReverseMap();
            CreateMap<Table, TableDetailsDTO>().ReverseMap();
        }
    }
}
