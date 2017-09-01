using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.AlanKwok.Projects.Currency.Classes;

namespace Com.AlanKwok.Projects.Currency
{
    public class Program
    {
        public static void Main()
        {
            CurrencyOperations currency = new CurrencyOperations();

            Console.WriteLine(currency.Convert(32760436.29M));
        }
    }
}
