namespace CustomerClassLib.Utils
{
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public class UtilService
    {
        public static bool CheckWellBracketsFormed(string input)
        {
            var stack = new Stack<char>();
            var allowedChars = new Dictionary<char, char>() { { '(', ')' } };

            var wellFormated = true;
            foreach (var chr in input)
            {
                if (allowedChars.ContainsKey(chr))
                {
                    stack.Push(chr);
                }
                else if (allowedChars.ContainsValue(chr))
                {
                    wellFormated = stack.Any();
                    if (wellFormated)
                    {
                        var startingChar = stack.Pop();
                        wellFormated = allowedChars.Contains(new KeyValuePair<char, char>(startingChar, chr));
                    }
                    if (!wellFormated)
                    {
                        break;
                    }
                }
            }
            return wellFormated;
        }

        public static void ShowResult<T>(IEnumerable<T> results)
        {
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
