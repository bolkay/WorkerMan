using AutoMapper;
using System;
using WorkerMan.Contracts.DTOs;
using WorkerMan.CrossCutting.Entities.Identity;
using WorkerMan.CrossCutting.Enums;

namespace WorkerMan.Services.MappingProfile
{
    public class WorkerManProfile : Profile
    {
        public WorkerManProfile()
        {
            CreateMap<UserRegistrationDTO, WorkerManUser>()
                .ReverseMap();

            CreateMap<WorkerManUser, UserDTO>()
                .ForMember(x => x.AccountType, option => { option.MapFrom(x => Enum.GetName(typeof(AccountType), x.AccountType)); })
                .ReverseMap();

        }
    }
}
