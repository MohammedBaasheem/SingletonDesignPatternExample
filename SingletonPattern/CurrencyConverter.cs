using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    public class CurrencyConverter
    {
        private IEnumerable<ExchangeRate> _exchangeRates;

        private CurrencyConverter()
        {
            LoadExchangeRates();
        }
        private static readonly object _lock = new object();
        private static CurrencyConverter _instance;
        public static CurrencyConverter Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CurrencyConverter();
                        }

                    }
                }
                
                return _instance;
            }
        }
        private void LoadExchangeRates()
        {
            Thread.Sleep(5000);
            _exchangeRates = new[]
{
    new ExchangeRate("YR", "SR", 1m / 600m), 
    new ExchangeRate("YR", "USD", 1m / 2273m),  
    new ExchangeRate("SR", "USD", 1m / 3.75m), 
    new ExchangeRate("US Dollar", "SR", 3.75m),   
};
        }
        public decimal Convert(string baseCurrency, string targetCurrency, decimal amount)
        {
            var rate = _exchangeRates.FirstOrDefault(x => x.BaseCurrency == baseCurrency && x.TargetCurrency == targetCurrency);
            if (rate == null)
            {
                throw new InvalidOperationException($"Exchange rate not found from {baseCurrency} to {targetCurrency}");
            }
            decimal convertedAmount = Math.Round(amount * rate.Rate, 2);
            return convertedAmount;
        }
    }
}
