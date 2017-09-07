using System;
using Com.AlanKwok.Projects.Currency.Classes;

namespace Com.AlanKwok.Projects.Currency
{
    public class Program
    {
        public static void Main()
        {
            var isLoop = true;

            do
            {
                var currency = new CurrencyOperations();

                Console.WriteLine("Please enter a decimal number");

                var inputNumber = Console.ReadLine();

                if (inputNumber == null || inputNumber.Trim() == string.Empty) continue;

                Console.WriteLine(currency.Convert(decimal.Parse(inputNumber)));

                Console.WriteLine("Do you want to enter another decimal number? (Y / N)");

                var inputResponse = Console.ReadLine();

                isLoop = inputResponse != null && string.Equals(inputResponse, "Y", StringComparison.OrdinalIgnoreCase);
            } while (isLoop);
        }
    }
}