using System;
using AutoMapper;
using MyAccounting.Application.Common.Mapping;
using MyAccounting.Domain.Entities;

namespace MyAccounting.Application.Dtos
{
    public class TransactionDto : IMapFrom<Transaction>
    {
        public Guid Id { get; set; }
        
        public MoneyDto Money { get; set; }
        
        public DateTime OccurredAt { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Transaction, TransactionDto>().ReverseMap();
        }
    }
}