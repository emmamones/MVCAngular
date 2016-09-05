using AngularMVCAuthentication.Dtos;
using AutoMapper;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<GenreDto, Genre>().ForMember(m=> m.Id,opt =>opt.Ignore());
        }
    }
}