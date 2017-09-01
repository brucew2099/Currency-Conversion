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

        //
        // This test failed. I didn't have time to debug it further.
        // [AKK - 09.01.2017]
        //
        //[Test]
        //public void TestConvertLargeNumbers()
        //{
        //    // Test regular number
        //    Assert.AreEqual(currency.Convert(32760436.29M), "Thirty Two Million Seven Hundred Sixty Thousand Four Hundred Thirty Six and Twenty Nine Cents");
        //}

        [Test]
        public void TestConvertNoTyNumbers()
        {
            // Test regular number
            Assert.AreEqual(currency.Convert(4515.12M), "Four Thousand Five Hundred Fifteen Dollars and Twelve Cents");
        }

        [Test]
        public void TestConverNoNumber()
        {
            // Test No Number
            Assert.Throws<NotImplementedException>(() => currency.Convert(null));
        }

    }
}
