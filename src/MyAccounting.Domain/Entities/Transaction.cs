using System;
using MyAccounting.Domain.ValueObjects;

namespace MyAccounting.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        
        public Money Money { get; set; }

        public Actor Remitter { get; set; }

        public Actor Beneficiary { get; set; }
        
        public DateTime OccurredAt { get; set; }
    }
}