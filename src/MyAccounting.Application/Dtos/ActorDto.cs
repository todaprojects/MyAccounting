using System;
using AutoMapper;
using MyAccounting.Application.Common.Mapping;
using MyAccounting.Domain.Entities;

namespace MyAccounting.Application.Dtos
{
    public class ActorDto : IMapFrom<Actor>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Actor, ActorDto>().ReverseMap();
        }
    }
}