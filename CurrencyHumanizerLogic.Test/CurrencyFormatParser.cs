using NUnit.Framework;
using Interviews.VM.PriceHumanizer.Logic;
using System;

namespace Interviews.VM.PriceHumanizer.Logic.Tests
{
    public class CurrencyFormatParser_Parse
    {
        private CurrencyFormatParser _sut;

        [OneTimeSetUp]
        public void Init()
        {
            _sut = new CurrencyFormatParser();
        }

        [Test]
        public void ThrowsFormatException_When_InputNotProperlyFormatted()
        {
            var exceptionalInput = "222 22 2,22";

            Assert.Throws<FormatException>(() => _sut.Parse(exceptionalInput));
        }

        [Test]
        public void GivesProperOutput_When_InputProperlyFormatted()
        {
            var expected = new CurrencyData() {
                millions = 222,
                thousands = 222,
                dollars = 222,
                decimalExists = true,
                decimals = 22
            };

            var actual = _sut.Parse("222 222 222,22");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutput_When_NoMillionsGiven()
        {
            var actual = _sut.Parse("222 222,22");

            var expected = new CurrencyData()
            {
                millions = 0,
                thousands = 222,
                dollars = 222,
                decimalExists = true,
                decimals = 22
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutput_When_MillionsEqualZero()
        {
            var actual = _sut.Parse("000 222 222,22");

            var expected = new CurrencyData()
            {
                millions = 0,
                thousands = 222,
                dollars = 222,
                decimalExists = true,
                decimals = 22
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutput_When_NoThousandsGiven()
        {
            var actual = _sut.Parse("222,22");

            var expected = new CurrencyData()
            {
                millions = 0,
                thousands = 0,
                dollars = 222,
                decimalExists = true,
                decimals = 22
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutput_When_ThousandsEqualZero()
        {
            var actual = _sut.Parse("222 000 222,22");

            var expected = new CurrencyData()
            {
                millions = 222,
                thousands = 0,
                dollars = 222,
                decimalExists = true,
                decimals = 22
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutput_When_ZeroDollarGiven()
        {
            var actual = _sut.Parse("0,22");

            var expected = new CurrencyData()
            {
                millions = 0,
                thousands = 0,
                dollars = 0,
                decimalExists = true,
                decimals = 22
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutput_When_DollarsEqualZero()
        {
            var actual = _sut.Parse("222 222 000,22");

            var expected = new CurrencyData()
            {
                millions = 222,
                thousands = 222,
                dollars = 0,
                decimalExists = true,
                decimals = 22
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutput_When_NoCentsGiven()
        {
            var actual = _sut.Parse("222 222 000");

            var expected = new CurrencyData()
            {
                millions = 222,
                thousands = 222,
                dollars = 0,
                decimalExists = false,
                decimals = 0
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivesProperOutput_When_ZeroCentsGiven()
        {
            var actual = _sut.Parse("222 222 000,00");

            var expected = new CurrencyData()
            {
                millions = 222,
                thousands = 222,
                dollars = 0,
                decimalExists = true,
                decimals = 0
            };

            Assert.AreEqual(expected, actual);
        }
    }


    public class CurrencyFormatParser_CheckFormat
    {
        private CurrencyFormatParser _sut;

        [OneTimeSetUp]
        public void Init()
        {
            _sut = new CurrencyFormatParser();
        }

        [TestCase("222 222 222,22")]
        [TestCase("22 222 222,22")]
        [TestCase("2 222 222,22")]
        [TestCase("222 222,22")]
        [TestCase("222,22")]
        [TestCase("222")]
        [TestCase("0,22")]
        [TestCase("0,00")]
        [TestCase("2")]
        [TestCase("0")]
        public void ReturnTrue_When_ProperlyFormatted(string properInputs)
        {
            Assert.IsTrue(_sut.CheckFormat(properInputs));
        }

        [TestCase("22 223 223,322")]
        [TestCase("223 22 22,22")]
        [TestCase("22 223 22,22")]
        [TestCase("22 22 22,22")]
        [TestCase("22 22 2,22")]
        [TestCase("22222,22")]
        [TestCase("")]
        [TestCase("xxx")]
        [TestCase("xxx,xx")]
        [TestCase("xxx xxx")]
        public void ReturnFalse_When_NotProperlyFormatted(string notProperInputs)
        {
            Assert.IsFalse(_sut.CheckFormat(notProperInputs));
        }
    }
}