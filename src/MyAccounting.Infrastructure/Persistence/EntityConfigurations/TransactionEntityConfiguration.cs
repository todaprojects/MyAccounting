using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAccounting.Domain.Entities;

namespace MyAccounting.Infrastructure.Persistence.EntityConfigurations
{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.OwnsOne(transaction => transaction.Money, o =>
            {
                o.WithOwner();
                
                o.Property(money => money.Amount).
                    HasPrecision(18, 2)
                    .HasColumnName("Amount")
                    .IsRequired();
                
                o.Property(money => money.Currency)
                    .HasColumnName("Currency")
                    .IsRequired();
                
            }).Navigation(transaction => transaction.Money).IsRequired();;
        }
    }
}