using AutoMapper;
using MyAccounting.Application.Common.Mapping;
using MyAccounting.Domain.ValueObjects;

namespace MyAccounting.Application.Dtos
{
    public class MoneyDto : IMapFrom<Money>
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Money, MoneyDto>().ReverseMap();
        }
    }
}