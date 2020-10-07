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
            CreateMap<Data.DbModels.TTask, Domain.Models.TaskDTO>();
            CreateMap<Domain.Models.TaskDTO,  Data.DbModels.TTask>();

            CreateMap<Data.DbModels.TTask, Domain.Models.TaskToCreateDTO>();
            CreateMap<Domain.Models.TaskToCreateDTO, Data.DbModels.TTask>();

            CreateMap<Data.DbModels.TTask, Domain.Models.TaskToUpdateDTO>();
            CreateMap<Domain.Models.TaskToUpdateDTO, Data.DbModels.TTask>();

            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Data.DbModels.TList, Domain.Models.ListDTO>();
            //});

            //var mapper = new Mapper(config);
        }
    }
}
