namespace NumberParserTest
{
    using NumberParserLib;
    using NumberParserLib.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NumberParserTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidNumberException))]
        public void ShouldFailIfNull()
        {
            // When
            NumberParser.Parse(null);

            // Then
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNumberException))]
        public void ShouldFailIfEmptyString()
        {
            // When
            NumberParser.Parse("");

            // Then
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNumberException))]
        public void ShouldFailIfWrongNumber()
        {
            // When
            NumberParser.Parse("12ea");

            // Then
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldParseNegativeNumbers()
        {
            // Then
            Assert.AreEqual(NumberParser.Parse("-1"), -1);
            Assert.AreEqual(NumberParser.Parse("-2"), -2);
            Assert.AreEqual(NumberParser.Parse("-3"), -3);
            Assert.AreEqual(NumberParser.Parse("-9"), -9);
            Assert.AreEqual(NumberParser.Parse("-99"), -99);
            Assert.AreEqual(NumberParser.Parse("-9999"), -9999);
        }

        [TestMethod]
        public void ShouldParseZero()
        {
            Assert.AreEqual(NumberParser.Parse("0"), 0);
            Assert.AreEqual(NumberParser.Parse("-0"), 0);
        }

        [TestMethod]
        public void ShouldParsePositiveNumbersWithPlus()
        {
            // Then
            Assert.AreEqual(NumberParser.Parse("+1"), 1);
            Assert.AreEqual(NumberParser.Parse("+2"), 2);
            Assert.AreEqual(NumberParser.Parse("+3"), 3);
            Assert.AreEqual(NumberParser.Parse("+9"), 9);
            Assert.AreEqual(NumberParser.Parse("+99"), 99);
            Assert.AreEqual(NumberParser.Parse("+9999"), 9999);
        }

        [TestMethod]
        public void ShouldParsePositiveNumbers()
        {
            // Then
            Assert.AreEqual(NumberParser.Parse("1"), 1);
            Assert.AreEqual(NumberParser.Parse("2"), 2);
            Assert.AreEqual(NumberParser.Parse("3"), 3);
            Assert.AreEqual(NumberParser.Parse("9"), 9);
            Assert.AreEqual(NumberParser.Parse("99"), 99);
            Assert.AreEqual(NumberParser.Parse("9999"), 9999);
        }

        [TestMethod]
        public void ShouldParsePositiveNumbersWithWhitespaces()
        {
            // Then
            Assert.AreEqual(NumberParser.Parse("1  "), 1);
            Assert.AreEqual(NumberParser.Parse("   2"), 2);
            Assert.AreEqual(NumberParser.Parse("3   "), 3);
            Assert.AreEqual(NumberParser.Parse("    9"), 9);
            Assert.AreEqual(NumberParser.Parse("99    "), 99);
            Assert.AreEqual(NumberParser.Parse("    9999"), 9999);
        }

        [TestMethod]
        public void ShouldParseNegativeNumbersWithWhitespaces()
        {
            // Then
            Assert.AreEqual(NumberParser.Parse("-1  "), -1);
            Assert.AreEqual(NumberParser.Parse("   -2"), -2);
            Assert.AreEqual(NumberParser.Parse("-3   "), -3);
            Assert.AreEqual(NumberParser.Parse("    -9"), -9);
            Assert.AreEqual(NumberParser.Parse("-99    "), -99);
            Assert.AreEqual(NumberParser.Parse("    -9999"), -9999);
        }

        [TestMethod]
        public void ShouldFalseIfNull()
        {
            // Then
            Assert.AreEqual(NumberParser.TryParse(null, out var i), false);
        }

        [TestMethod]
        public void ShouldFalseIfEmptyString()
        {
            // Then
            Assert.AreEqual(NumberParser.TryParse("", out var i), false);
        }

        [TestMethod]
        public void ShouldFalseIfWrongNumber()
        {
            // Then
            Assert.AreEqual(NumberParser.TryParse("12ea", out var i), false);
        }

        [TestMethod]
        public void ShouldTryParseNegativeNumbers()
        {
            // Then
            Assert.AreEqual(NumberParser.TryParse("-1", out var a), true);            
            Assert.AreEqual(NumberParser.TryParse("-2", out var b), true);            
            Assert.AreEqual(NumberParser.TryParse("-40", out var c), true);            
            Assert.AreEqual(NumberParser.TryParse("-2500", out var d), true);

            Assert.AreEqual(a, -1);
            Assert.AreEqual(b, -2);
            Assert.AreEqual(c, -40);
            Assert.AreEqual(d, -2500);
        }

        [TestMethod]
        public void ShouldTryParseZero()
        {
            Assert.AreEqual(NumberParser.TryParse("0", out var a), true);
            Assert.AreEqual(NumberParser.TryParse("-0", out var b), true);
            Assert.AreEqual(a, 0);
            Assert.AreEqual(b, 0);
        }

        [TestMethod]
        public void ShouldTryParsePositiveNumbersWithPlus()
        {
            // Then
            Assert.AreEqual(NumberParser.TryParse("+1", out var a), true);
            Assert.AreEqual(NumberParser.TryParse("+2", out var b), true);
            Assert.AreEqual(NumberParser.TryParse("+40", out var c), true);
            Assert.AreEqual(NumberParser.TryParse("+2500", out var d), true);

            Assert.AreEqual(a, 1);
            Assert.AreEqual(b, 2);
            Assert.AreEqual(c, 40);
            Assert.AreEqual(d, 2500);
        }

        [TestMethod]
        public void ShouldTryParsePositiveNumbers()
        {
            // Then
            Assert.AreEqual(NumberParser.TryParse("1", out var a), true);
            Assert.AreEqual(NumberParser.TryParse("2", out var b), true);
            Assert.AreEqual(NumberParser.TryParse("40", out var c), true);
            Assert.AreEqual(NumberParser.TryParse("2500", out var d), true);

            Assert.AreEqual(a, 1);
            Assert.AreEqual(b, 2);
            Assert.AreEqual(c, 40);
            Assert.AreEqual(d, 2500);
        }

        [TestMethod]
        public void ShouldTryParsePositiveNumbersWithWhitespaces()
        {
            // Then
            Assert.AreEqual(NumberParser.TryParse("       1", out var a), true);
            Assert.AreEqual(NumberParser.TryParse("2        ", out var b), true);
            Assert.AreEqual(NumberParser.TryParse("       40", out var c), true);
            Assert.AreEqual(NumberParser.TryParse("     2500       ", out var d), true);

            Assert.AreEqual(a, 1);
            Assert.AreEqual(b, 2);
            Assert.AreEqual(c, 40);
            Assert.AreEqual(d, 2500);
        }

        [TestMethod]
        public void ShouldTryParseNegativeNumbersWithWhitespaces()
        {
            // Then
            Assert.AreEqual(NumberParser.TryParse("       -1", out var a), true);
            Assert.AreEqual(NumberParser.TryParse("-2        ", out var b), true);
            Assert.AreEqual(NumberParser.TryParse("       -40", out var c), true);
            Assert.AreEqual(NumberParser.TryParse("     -2500       ", out var d), true);

            Assert.AreEqual(a, -1);
            Assert.AreEqual(b, -2);
            Assert.AreEqual(c, -40);
            Assert.AreEqual(d, -2500);
        }
    }
}
