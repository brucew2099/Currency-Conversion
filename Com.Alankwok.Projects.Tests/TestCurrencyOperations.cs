using System;
using Com.AlanKwok.Projects.Currency.Classes;
using NUnit.Framework;

namespace Com.Alankwok.Projects.Tests
{
    [TestFixture]
    public class TestCurrencyOperations
    {
        [SetUp]
        public void Setup()
        {
            Currency = new CurrencyOperations();
        }

        public CurrencyOperations Currency;

        [Test]
        public void TestConvertRegularNumbers()
        {
            // Test regular number
            Assert.AreEqual(Currency.Convert(32.45M), "Thirty Two Dollars and Forty Five Cents");
        }

        [Test]
        public void TestConvertLargeNumbers()
        {
            // Test regular number
            Assert.AreEqual(Currency.Convert(32760431.29M),
                "Thirty Two Million Seven Hundred Sixty Thousand Four Hundred Thirty One Dollars and Twenty Nine Cents");
        }

        [Test]
        public void TestConvertAfterDecimal()
        {
            // Test after decimal
            Assert.AreEqual(Currency.Convert(0.03M), "Three Cents");
        }

        [Test]
        public void TestConvertAfterDecimalSingleNumber()
        {
            // Test after decimal
            Assert.AreEqual(Currency.Convert(0.3M), "Thirty Cents");
        }

        [Test]
        public void TestConvertLengthModThreeZeroBeforeDecimal()
        {
            Assert.AreEqual(Currency.Convert(476982.32M),
                "Four Hundred Seventy Six Thousand Nine Hundred Eighty Two Dollars and Thirty Two Cents");
        }

        [Test]
        public void TestConvertNoTyNumbers()
        {
            // Test regular number
            Assert.AreEqual(Currency.Convert(4515.12M), "Four Thousand Five Hundred Fifteen Dollars and Twelve Cents");
        }

        [Test]
        public void TestConvertLessThanThousandNumbers()
        {
            Assert.AreEqual(Currency.Convert(123.45M), "One Hundred Twenty Three Dollars and Forty Five Cents");
        }

        [Test]
        public void TestConvertInteger()
        {
            Assert.AreEqual(Currency.Convert(10000), "Ten Thousand Dollars and Zero Cents");
        }

        [Test]
        public void TestConvertZeroNumber()
        {
            // Test zero
            Assert.AreEqual(Currency.Convert(0), "Zero Dollars and Zero Cents");
        }

        [Test]
        public void TestConverNoNumber()
        {
            // Test No Number
            Assert.Throws<NotImplementedException>(() => Currency.Convert(null));
        }
    }
}