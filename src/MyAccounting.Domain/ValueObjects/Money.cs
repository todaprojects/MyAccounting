namespace MyAccounting.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }
    };
}