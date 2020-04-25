using NUnit.Framework;
using Interviews.VM.PriceHumanizer.Logic;
using System;
using Moq;

namespace Interviews.VM.PriceHumanizer.Logic.Tests
{
    public class CurrencyHumanizer_Humanize
    {
        private ICurrencyHumanizer _sut;

        private CurrencyData exampleProperCurrencyData;
        private string exampleProperInputData; 
        private string exampleProperOutput;
        private string exampleExceptionalInput;

        [OneTimeSetUp]
        public void Init()
        {
            exampleProperCurrencyData = new CurrencyData() { dollars = 22, decimalExists = true, decimals = 10 };
            exampleProperInputData = "22,10";
            exampleProperOutput = "twenty-two dollars and ten cents";

            exampleExceptionalInput = "55555";

            var mockCurrencyFormatParser = new Mock<IFormatParser>();
            mockCurrencyFormatParser.Setup(x => x.CheckFormat(It.IsRegex(@"^\d{1,3}(\s\d{3}){0,2}(,\d{2})?$"))).Returns(true);
            mockCurrencyFormatParser.Setup(x => x.Parse(exampleProperInputData)).Returns(exampleProperCurrencyData);

            var mockCurrencyReadableBuilder = new Mock<IReadableBuilder>();
            mockCurrencyReadableBuilder.Setup(m => m.Build(exampleProperCurrencyData)).Returns(exampleProperOutput);
                       
            _sut = new CurrencyHumanizer(mockCurrencyReadableBuilder.Object, mockCurrencyFormatParser.Object);
        }

        [Test]
        public void ThrowsFormatException_When_InputNotProperlyFormatted()
        {
            Assert.Throws<FormatException>(() => _sut.Humanize(exampleExceptionalInput));
        }

        [Test]
        public void ReturnsReadableString_When_CorrectlyFormattedInputGiven()
        {
            Assert.AreEqual(exampleProperOutput, _sut.Humanize(exampleProperInputData));
        }

    }
}