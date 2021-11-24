using System;
using System.Linq;
using System.Threading.Tasks;
using MyAccounting.Domain.Entities;
using MyAccounting.Domain.ValueObjects;
using Type = MyAccounting.Domain.ValueObjects.Type;

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
                        Amount = 100,
                        Currency = Currency.Eur
                    },
                    Type = Type.Income,
                    OccurredAt = DateTime.UtcNow
                });

                await context.SaveChangesAsync();
            }
        }
    }
}