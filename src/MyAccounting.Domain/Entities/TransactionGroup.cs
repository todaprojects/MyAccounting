using System;
using System.Collections.Generic;

namespace MyAccounting.Domain.Entities
{
    public class TransactionGroup
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}