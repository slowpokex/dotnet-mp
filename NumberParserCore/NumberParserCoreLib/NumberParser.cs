namespace NumberParserLib
{
    using System;
    using NumberParserLib.Exceptions;

    public class NumberParser
    {
        public static bool TryParse(string number, out int res)
        {
            try
            {
                res = Parse(number);
                return true;
            }
            catch
            {
                res = 0;
                return false;
            }
        }

        public static int Parse(string number)
        {           
            try
            {
                if (string.IsNullOrEmpty(number))
                {
                    throw new NullReferenceException("Empty number");
                }
                var rawData = number.Trim();
                var hasPositiveSymbol = rawData[0].Equals('+');
                var hasNegativeSymbol = rawData[0].Equals('-');

                if (hasPositiveSymbol || hasNegativeSymbol)
                {
                    rawData = rawData.Substring(1);
                }

                var digit = 0;
                var step = 1;

                for (var i = rawData.Length - 1; i >= 0; i--)
                {
                    if (rawData[i].Equals('.'))
                    {
                        break;
                    }
                    if (!char.IsDigit(rawData[i]))
                    {
                        throw new InvalidNumberException("Invalid number");
                    }

                    digit += ConvertFromCharToInt(rawData[i]) * step;
                    step *= 10;
                }

                return hasNegativeSymbol ? digit * -1 : digit;
            }
            catch (NullReferenceException e)
            {
                throw new InvalidNumberException("Empty number", e);
            }
            catch (InvalidCastException e)
            {
                throw new InvalidNumberException("Wrong number format", e);
            }
            catch (Exception e)
            {
                throw new InvalidNumberException("Unknown number", e);
            }
        }

        private static int ConvertFromCharToInt(char digit)
        {
            var result = -1;           ;

            switch (digit)
            {
                case '0':
                    result = 0;
                    break;
                case '1':
                    result = 1;
                    break;
                case '2':
                    result = 2;
                    break;
                case '3':
                    result = 3;
                    break;
                case '4':
                    result = 4;
                    break;
                case '5':
                    result = 5;
                    break;
                case '6':
                    result = 6;
                    break;
                case '7':
                    result = 7;
                    break;
                case '8':
                    result = 8;
                    break;
                case '9':
                    result = 9;
                    break;
                default:
                    throw new InvalidCastException();
            }

            return result;
        }
    }
}
