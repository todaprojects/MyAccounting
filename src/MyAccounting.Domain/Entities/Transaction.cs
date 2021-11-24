using System;
using MyAccounting.Domain.ValueObjects;
using Type = MyAccounting.Domain.ValueObjects.Type;

namespace MyAccounting.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        
        public Money Money { get; set; }

        public Type Type { get; set; }
        
        public DateTime OccurredAt { get; set; }
    }
}