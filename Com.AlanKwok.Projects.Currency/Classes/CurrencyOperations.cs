using System;
using System.Text.RegularExpressions;

namespace Com.AlanKwok.Projects.Currency.Classes
{
    public class CurrencyOperations
    {
        private string[] _zerosAndOnesArray;
        private string[] _tysArray;
        private string[] _unitsArray;

        private void Init()
        {
            _zerosAndOnesArray = new[]
            {
                "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve",
                "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };

            _tysArray = new[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninty" };

            _unitsArray = new[] { "", "Thousand", "Million", "Billion" };
        }

        public string Convert(decimal numbers)
        {
            if (numbers == 0)
            {
                return "Zero Dollars and Zero Cents";
            }

            var currencyNumbers = (numbers.ToString().IndexOf(".") > -1) ? numbers.ToString("0.##") : numbers.ToString("0.00");
            var beforeDecimal = currencyNumbers.Substring(0, currencyNumbers.IndexOf("."));
            var afterDecimal = currencyNumbers.Substring(currencyNumbers.IndexOf(".") + 1);

            Init();

            if (beforeDecimal == "0")
            {
                return ProcessAfterDecimal(afterDecimal);
            }

            var wordAmount = string.Empty;
            var remains = Int32.Parse(beforeDecimal);

            var numberOfThousands = beforeDecimal.Length / 4;
            var numberOfBelowThousands = numberOfThousands == 0 ? beforeDecimal.Length % 4 : (Int32.Parse(beforeDecimal) / (1000 * numberOfThousands)).ToString().Length;

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
                    if (remains == 0)
                    {
                        wordAmount += " " + _unitsArray[i];
                        break;
                    }
                }

                numberOfThousands--;

                if (numberOfThousands >= 0)
                {
                    wordAmount += " " + _unitsArray[numberOfThousands];
                }

            }

            wordAmount = wordAmount.Trim() != string.Empty ? wordAmount += " Dollars and " : string.Empty;

            wordAmount += ProcessAfterDecimal(afterDecimal);

            return Regex.Replace(wordAmount, @"\s+", " ").Trim();
        }

        public string Convert(int numbers)
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
            if (afterDecimal.Length == 1)
            {
                afterDecimal = afterDecimal.PadRight(2, '0');
            }

            var wordTemp = string.Empty;
            var quotient = Int32.Parse(afterDecimal) / 10;

            if (quotient == 1)
            {
                wordTemp += _zerosAndOnesArray[Int32.Parse(afterDecimal) % 100] + " Cents";
            }
            else if (quotient == 0)
            {
                wordTemp += _zerosAndOnesArray[Int32.Parse(afterDecimal)] + " Cents";
            }
            else
            {
                wordTemp += _tysArray[quotient - 2] + " ";

                if (Int32.Parse(afterDecimal) % 10 > 0 || Int32.Parse(afterDecimal) == 0)
                {
                    wordTemp += _zerosAndOnesArray[Int32.Parse(afterDecimal) % 10];
                }

                wordTemp += " Cents";
            }

            return Regex.Replace(wordTemp, @"\s+", " ").Trim();
        }

        public Result ProcessSubThousands(int remains, int numberOfThousands, int level, string wordAmount)
        {
            var value = (int)(remains / Math.Pow(10, 3 * numberOfThousands + 1 - level));

            var isEndOfNumber = true;

            if (value > 0 && level == 1)
            {
                wordAmount += (wordAmount.Trim() == string.Empty) ? " " + _zerosAndOnesArray[value] + " " + _unitsArray[numberOfThousands] + " " : string.Empty;
            }
            else if(value > 0 && level == 2)
            {
                wordAmount += " " + _zerosAndOnesArray[value] + " Hundred";
            }
            else if(value > 1 && level == 3)
            {
                wordAmount += " " + _tysArray[value - 2] + " ";

                if (Int32.Parse(remains.ToString()[1].ToString()) > 0)
                {
                    wordAmount += " " + _zerosAndOnesArray[Int32.Parse(remains.ToString()[1].ToString())] + " ";
                    wordAmount += _unitsArray[numberOfThousands - 1] + " ";
                }

                if (remains.ToString().Length > 2)
                {
                    isEndOfNumber = false;
                }
            }
            else if(value == 1 && level == 3)
            {
                wordAmount += " " + _zerosAndOnesArray[remains % 10 + 10];
            }

            remains = isEndOfNumber ? (int)(remains % Math.Pow(10, 3 * numberOfThousands + 1 - level)) : (int)(remains % Math.Pow(10, 3 * numberOfThousands - level));

            return new Result()
            {
                WordAmount = wordAmount,
                Remains = remains
            };
        }
    }
}
