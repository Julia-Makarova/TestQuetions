using System;

namespace WebApplication4
{
    public class Product
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PriceUSD { get; set; }
        public decimal PriceRUB { 
            get {
                var currency = GetPrice.USD(DateTime.Now);

                return currency.Value * this.PriceUSD;
            } 
        }

    }
}
