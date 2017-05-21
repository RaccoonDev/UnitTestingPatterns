namespace ProductCatalog.Business
{
    public class Price
    {
        public Price(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public decimal Amount { get; }
        public Currency Currency { get; }
    }
}
