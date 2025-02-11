namespace SingletonPattern
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write( "Enter Base Currency: " );
                var baseCurrency = Console.ReadLine();
                Console.Write("Enter Target Currency: ");
                var targetCurrency = Console.ReadLine();
                Console.Write("Enter Amount: ");
                var amount = decimal.Parse(Console.ReadLine());
                var result = CurrencyConverter.Instance.Convert(baseCurrency, targetCurrency, amount);
                Console.WriteLine($"{amount} {baseCurrency}= {result} {targetCurrency}");
                Console.WriteLine("----------------------------------------------");
            }
        }
    }
}
