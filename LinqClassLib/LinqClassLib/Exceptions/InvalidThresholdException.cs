namespace CustomerClassLib.Exceptions
{
    using System;

    public class InvalidThresholdException: Exception
    {
        public InvalidThresholdException(string threshold, double from, double to):
            base($"Invalid thresholds in {threshold}: from {from} to {to}") { }
    }
}
