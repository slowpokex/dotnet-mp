namespace NumberParserTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NumberParserLib;
    using NumberParserLib.Exceptions;

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
    }
}
