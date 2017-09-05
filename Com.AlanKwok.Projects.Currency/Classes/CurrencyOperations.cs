using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Com.AlanKwok.Projects.Currency.Classes
{
    public class CurrencyOperations
    {
        private string[] zerosAndOnesArray = null;
        private string[] tysArray = null;
        private string[] unitsArray = null;

        public CurrencyOperations()
        {
        }

        private void Init()
        {
            zerosAndOnesArray = new string[]
            {
                "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve",
                "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };

            tysArray = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Sevenyt", "Eighty", "Ninty" };
            unitsArray = new string[] { "", "Thousand", "Million", "Billion" };
        }

        public string Convert(decimal numbers)
        {
            if (numbers == 0)
            {
                return "Zero Dollars and Zero Cents";
            }

            var currencyNumbers = numbers.ToString("0.##");
            var beforeDecimal = currencyNumbers.Substring(0, currencyNumbers.IndexOf("."));
            var afterDecimal = currencyNumbers.Substring(currencyNumbers.IndexOf(".") + 1);

            Init();

            if (beforeDecimal == "0")
            {
                return ProcessAfterDecimal(afterDecimal);
            }

            var wordAmount = string.Empty;
            var remains = Int32.Parse(beforeDecimal);

            var numberOfThousands = (int) beforeDecimal.Length / 4;
            var numberOfBelowThousands = numberOfThousands > 0 ? (int) beforeDecimal.Length % 3 : (int)beforeDecimal.Length % 4;


            // Take care of the digits before any thousand counts
            if (numberOfBelowThousands > 0)
            {
                for (var i = 1; i <= numberOfBelowThousands; i++)
                {
                    Result result = ProcessSubThousands(remains, numberOfThousands + 1, (i + 3 - numberOfBelowThousands), wordAmount);
                    wordAmount = result.WordAmount;
                    remains = result.Remains;
                }
            }

            // Take care of the digits within the thousand counts
            while(numberOfThousands > 0)
            {
                for (var i = 1; i <= 3; i++)
                {
                    Result result = ProcessSubThousands(remains, numberOfThousands, i, wordAmount);
                    wordAmount = result.WordAmount;
                    remains = result.Remains;
                }

                numberOfThousands--;

                if (numberOfThousands >= 0)
                {
                    wordAmount += " " + unitsArray[numberOfThousands];
                }

            };

            wordAmount = wordAmount.Trim() != string.Empty ? wordAmount += " Dollars and " : String.Empty;

            wordAmount += ProcessAfterDecimal(afterDecimal);

            return Regex.Replace(wordAmount, @"\s+", " ").Trim();
        }

        public string Convert(int numbers)
        {
            var decimalNumbers = decimal.Parse(numbers.ToString());
            return Convert(decimalNumbers);
        }

        public string Convert(double numbers)
        {
            var decimalNumbers = decimal.Parse(numbers.ToString());
            return Convert(decimalNumbers);
        }

        public decimal Convert(string words)
        {
            throw new NotImplementedException();
        }

        public string ProcessAfterDecimal(string afterDecimal)
        {
            var wordTemp = string.Empty;
            var quotient = Int32.Parse(afterDecimal) / 10;

            if (quotient > 0)
            {
                if (quotient > 1)
                {
                    wordTemp += tysArray[quotient - 2] + " ";
                    wordTemp += zerosAndOnesArray[Int32.Parse(afterDecimal) % 10] + " Cents";
                }
                else
                {
                    wordTemp += zerosAndOnesArray[Int32.Parse(afterDecimal) % 100] + " Cents";
                }
            }
            else
            {
                wordTemp += zerosAndOnesArray[Int32.Parse(afterDecimal)] + " Cents";
            }

            return wordTemp;
        }

        public Result ProcessSubThousands(int remains, int numberOfThousands, int level, string wordAmount)
        {
            var value = (int)(remains / Math.Pow(10, 3 * numberOfThousands + 1 - level));

            var isEndOfNumber = true;

            if (value > 0)
            {
                switch (level)
                {
                    case 1:
                    {
                        if (wordAmount.Trim() == string.Empty)
                        {
                            wordAmount += " "  + zerosAndOnesArray[value] + " " + unitsArray[numberOfThousands] + " ";
                        }
                        
                        break;
                    }
                    case 2:
                    {
                        wordAmount += " " + zerosAndOnesArray[value] + " Hundred";
                        break;
                    }
                    case 3:
                    {
                        if (value > 1)
                        {
                            wordAmount += " " + tysArray[value - 2] + " ";
                            if (Int32.Parse(remains.ToString()[1].ToString()) > 0)
                            {
                                wordAmount += " " + zerosAndOnesArray[Int32.Parse(remains.ToString()[1].ToString())] + " ";
                                wordAmount += unitsArray[numberOfThousands - 1] + " ";
                            }

                            if (remains.ToString().Length > 2)
                            {
                                isEndOfNumber = false;
                            }
                        }
                        else
                        {
                            wordAmount += " " + zerosAndOnesArray[remains % 10 + 10];
                        }

                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
            }

            if (isEndOfNumber)
            {
                remains = (int)(remains % Math.Pow(10, 3 * numberOfThousands + 1 - level));
            }
            else
            {
                remains = (int)(remains % Math.Pow(10, 3 * numberOfThousands - level));
            }

            return new Result()
            {
                WordAmount = wordAmount,
                Remains = remains
            };
        }
    }
}
