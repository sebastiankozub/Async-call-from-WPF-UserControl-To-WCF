using NUnit.Framework;
using Interviews.VM.PriceHumanizer.Logic;
using System;

namespace Interviews.VM.PriceHumanizer.Logic.Tests
{
    public class IntegerHumanizer_Humanize
    {
        [Test]
        public void ThrowsException_When_InputIntegerOutOfRange()
        {
            var sut = new IntegerHumanizer();
            ushort exceptionalInput = 3123;        

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Humanize(exceptionalInput));
        }

        [Test, Sequential]
        public void GivesProperOutputString_When_InputIntegerIsFromRange(   [Values(0, 1, 10, 11, 23, 100, 999, 200, 201)] int input,
                                                                            [Values("zero", "one", "ten", "eleven", "twenty-three", "one hundred", "nine hundred ninety-nine", "two hundred", "two hundred one")] string expected)
        {
            var sut = new IntegerHumanizer();

            string actual = sut.Humanize((ushort)input);

            Assert.AreEqual(expected, actual);
        }
    }
}