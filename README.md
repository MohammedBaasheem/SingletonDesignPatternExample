# Singleton Design Pattern Example: Currency Converter

This project demonstrates the Singleton design pattern in C# for a currency converter. The Singleton pattern ensures that a class has only one instance, and provides a global point of access to it. This is useful for managing resources like database connections or, as in this case, a set of exchange rates that should be loaded only once.

## Project Structure

The project consists of the following classes:

*   `CurrencyConverter`: The class that implements the Singleton pattern. It holds the exchange rates and provides a method to convert currencies.
*   `ExchangeRate`: A simple class to represent an exchange rate between two currencies.
*   `Program`: Contains the `Main` method, which demonstrates how to use the `CurrencyConverter` singleton.

## How the Singleton Pattern is Applied

1.  **Private Constructor:** The `CurrencyConverter` class has a private constructor: `private CurrencyConverter()`. This prevents direct instantiation of the class from outside.

2.  **Static Instance:** A static field `_instance` of type `CurrencyConverter` is created to hold the single instance of the class. It's initialized to `null`.

3.  **Static Lock Object:** A static `_lock` object is created to ensure thread safety during the creation of the singleton instance.

4.  **Static Instance Property:** The `Instance` property provides a global point of access to the singleton instance. The `get` accessor of this property implements the logic to create and return the instance:

    ```csharp
    public static CurrencyConverter Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock) // Thread safety
                {
                    if (_instance == null) // Double-checked locking
                    {
                        _instance = new CurrencyConverter();
                    }
                }
            }
            return _instance;
        }
    }
    ```

    *   **Double-checked locking:** The `if (_instance == null)` check is performed twice – once outside the lock and again inside. This optimization reduces the amount of time the lock is held.
    *   **Thread Safety:** The `lock (_lock)` block ensures that only one thread can create the instance at a time. This prevents race conditions and ensures that only one instance is created, even in a multithreaded environment.

5.  **Loading Exchange Rates:** The `LoadExchangeRates()` method is called within the private constructor. This method simulates loading exchange rates (in a real application, this might involve reading from a file or a database).  Because the constructor is private and only called once by the `Instance` property, the exchange rates are loaded only once.

6.  **Usage:** The `Main` method in `Program.cs` uses the `CurrencyConverter.Instance` property to access the singleton instance.  It then calls the `Convert` method to perform currency conversions.

## How to Run

1.  Make sure you have the .NET SDK installed.
2.  Navigate to the project directory in your terminal.
3.  Run `dotnet run`.

## Example Usage (from Program.cs)

The `Main` method demonstrates how to use the `CurrencyConverter` singleton:

```csharp
while (true)
{
    Console.Write("Enter Base Currency: ");
    var baseCurrency = Console.ReadLine();
    Console.Write("Enter Target Currency: ");
    var targetCurrency = Console.ReadLine();
    Console.Write("Enter Amount: ");
    var amount = decimal.Parse(Console.ReadLine());
    var result = CurrencyConverter.Instance.Convert(baseCurrency, targetCurrency, amount); // Accessing the singleton instance
    Console.WriteLine($"{amount} {baseCurrency}= {result} {targetCurrency}");
    Console.WriteLine("----------------------------------------------");
}
