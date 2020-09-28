using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace WebAPI.Profiles
{
    public class ListProfile : Profile
    {
        public ListProfile()
        {
            CreateMap<Data.DbModels.TList, Domain.Models.ListDTO>();

            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Data.DbModels.TList, Domain.Models.ListDTO>();
            //});

            //var mapper = new Mapper(config);
        }
    }
}
