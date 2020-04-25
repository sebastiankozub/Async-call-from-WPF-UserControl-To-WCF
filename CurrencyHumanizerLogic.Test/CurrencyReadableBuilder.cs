using NUnit.Framework;
using Interviews.VM.PriceHumanizer.Logic;
using System;
using Moq;

namespace Interviews.VM.PriceHumanizer.Logic.Tests
{
    public class CurrencyReadableBuilder_Build
    {
        
        private IIntergerHumanizer _integerHumanizer;
        private IReadableBuilder _sut;

        [OneTimeSetUp]
        public void Init()
        {
            var mockIntergerHumanizer = new Mock<IIntergerHumanizer>();

            mockIntergerHumanizer.Setup(m => m.Humanize(0)).Returns("zero");
            mockIntergerHumanizer.Setup(m => m.Humanize(1)).Returns("one");
            mockIntergerHumanizer.Setup(m => m.Humanize(11)).Returns("eleven");
            mockIntergerHumanizer.Setup(m => m.Humanize(111)).Returns("one-hunderd eleven");

            _integerHumanizer = mockIntergerHumanizer.Object;
            _sut = new CurrencyReadableBuilder(_integerHumanizer);
        }

        [Test]
        public void GivesProperOutputString_For_InputWithDecimals()
        {            
            var input = new CurrencyData {
                millions = 1,
                thousands = 11,
                dollars = 111,
                decimals = 0,
                decimalExists = true
            };

            var expected = "one million eleven thousand one-hunderd eleven dollars and zero cents";
            var actual = _sut.Build(input);
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutputString_For_InputWithoutDecimals()
        {
            var input = new CurrencyData
            {
                millions = 111,
                thousands = 11,
                dollars = 1,
                decimals = 0,
                decimalExists = false
            };

            var expected = "one-hunderd eleven million eleven thousand one dollars";
            var actual = _sut.Build(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutputString_For_InputWithAllValuesGreaterZero()
        {
            var input = new CurrencyData
            {
                millions = 1,
                thousands = 1,
                dollars = 1,
                decimals = 1,
                decimalExists = true
            };

            var expected = "one million one thousand one dollars and one cent";
            var actual = _sut.Build(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutputString_For_InputWith_AllValuesEqualZero_And_DecimalsDeclared()
        {
            var input = new CurrencyData
            {
                millions = 0,
                thousands = 0,
                dollars = 0,
                decimals = 0,
                decimalExists = true
            };

            var expected = "zero dollars and zero cents";
            var actual = _sut.Build(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutputString_For_InputWith_AllValuesEqualZero_And_NoDecimals()
        {
            var input = new CurrencyData
            {
                millions = 0,
                thousands = 0,
                dollars = 0,
                decimals = 0,
                decimalExists = false
            };

            var expected = "zero dollars";
            var actual = _sut.Build(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutputString_For_InputWith_SingleDollar()
        {
            var input = new CurrencyData
            {
                millions = 0,
                thousands = 0,
                dollars = 1,
                decimals = 0,
                decimalExists = false
            };

            var expected = "one dollar";
            var actual = _sut.Build(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutputString_For_InputWith_OneCent()
        {
            var input = new CurrencyData
            {
                millions = 0,
                thousands = 0,
                dollars = 0,
                decimals = 1,
                decimalExists = true
            };

            var expected = "zero dollars and one cent";
            var actual = _sut.Build(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutputString_For_InputWith_SingleDollarWithCents()
        {

            var input = new CurrencyData
            {
                millions = 0,
                thousands = 0,
                dollars = 1,
                decimals = 11,
                decimalExists = true
            };

            var expected = "one dollar and eleven cents";
            var actual = _sut.Build(input);

            Assert.AreEqual(expected, actual);
        }

    }
}