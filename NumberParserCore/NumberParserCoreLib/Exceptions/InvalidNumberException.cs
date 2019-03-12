namespace NumberParserLib.Exceptions
{
    using System;

    public class InvalidNumberException : Exception
    {
        public InvalidNumberException(string message) : base(message) { }

        public InvalidNumberException(string message, Exception e) : base(message, e) { }
    }
}
