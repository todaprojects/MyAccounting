using System;
using System.Linq;
using System.Threading.Tasks;
using MyAccounting.Domain.Entities;
using MyAccounting.Domain.ValueObjects;

namespace MyAccounting.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Transactions.Any())
            {
                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Money = new Money
                    {
                        Amount = 500,
                        Currency = Currency.Eur
                    },
                    Beneficiary = new Actor
                    {
                        Id = Guid.NewGuid(),
                        Name = "Coding school"
                    },
                    OccurredAt = DateTime.UtcNow
                });
                
                context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    Money = new Money
                    {
                        Amount = 2000,
                        Currency = Currency.Eur
                    },
                    Remitter = new Actor
                    {
                        Id = Guid.NewGuid(),
                        Name = ".NET project"
                    },
                    OccurredAt = DateTime.UtcNow
                });

                await context.SaveChangesAsync();
            }
        }
    }
}