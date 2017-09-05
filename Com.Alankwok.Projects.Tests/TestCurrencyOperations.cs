using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using Com.AlanKwok.Projects.Currency.Classes;

namespace Com.Alankwok.Projects.Tests
{
    [TestFixture]
    public class TestCurrencyOperations
    {
        public CurrencyOperations currency = null;

        [SetUp]
        public void Setup()
        {
            currency = new CurrencyOperations();
        }

        [Test]
        public void TestConvertRegularNumbers()
        {
            // Test regular number
            Assert.AreEqual(currency.Convert(32.45M), "Thirty Two Dollars and Forty Five Cents");
        }

        [Test]
        public void TestConvertZeroNumber()
        {
            // Test zero
            Assert.AreEqual(currency.Convert(0), "Zero Dollars and Zero Cents");
        }

        [Test]
        public void TestConvertAfterDecimal()
        {
            // Test after decimal
            Assert.AreEqual(currency.Convert(0.03M), "Three Cents");
        }

        [Test]
        public void TestConvertWrongTypeNumber()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => currency.Convert(10000));
        }

        [Test]
        public void TestConvertLargeNumbers()
        {
            // Test regular number
            Assert.AreEqual(currency.Convert(32760431.29M),
                "Thirty Two Million Seven Hundred Sixty Thousand Four Hundred Thirty One Dollars and Twenty Nine Cents");
        }

        [Test]
        public void TestConvertNoTyNumbers()
        {
            // Test regular number
            Assert.AreEqual(currency.Convert(4515.12M), "Four Thousand Five Hundred Fifteen Dollars and Twelve Cents");
        }

        [Test]
        public void TestConvertSmallNumbers()
        {
            Assert.AreEqual(currency.Convert(123.45M), "One Hundred Twenty Three Dollars and Forty Five Cents");
        }

        [Test]
        public void TestConvertLengthModThreeZeroBeforeDecimal()
        {
            Assert.AreEqual(currency.Convert(476982.32M), "Four Hundred Seventy Six Thousand Nine Hundred Eighty Two Dollars and Thirty Two Cents");
        }

        [Test]
        public void TestConverNoNumber()
        {
            // Test No Number
            Assert.Throws<NotImplementedException>(() => currency.Convert(null));
        }

    }
}
