using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interviews.VM.PriceHumanizer.Logic
{
    public class IntegerHumanizer : IIntergerHumanizer
    {
        public string Humanize(ushort inputInteger)
        {
            if (inputInteger >= 1000 || inputInteger < 0)
                throw new ArgumentOutOfRangeException("Argument must be from 0 to 999 range.");
            
            return HumanizeIntegerFromArray(UInt16ToArray(inputInteger));
        }

        private ushort[] UInt16ToArray(ushort inputInteger)
        {
            ushort numberOfHundreds = (ushort)Math.Floor(inputInteger / 100.0);
            ushort restFromHundrets = (ushort)(inputInteger - (numberOfHundreds * 100));
            ushort numberOfTens = (ushort)Math.Floor(restFromHundrets / 10.0);
            ushort numberOfOnes = (ushort)(restFromHundrets - (numberOfTens * 10));

            return new ushort[] { numberOfOnes, numberOfTens, numberOfHundreds };
        }

        private string HumanizeIntegerFromArray(ushort[] integerArrayRepresentation)
        {
            var humanizedResult = new StringBuilder();
            var integer = (ushort)(integerArrayRepresentation[2] * 100 + integerArrayRepresentation[1] * 10 + integerArrayRepresentation[0]);
            var restFromHundrets = (ushort)(integer - (integerArrayRepresentation[2] * 100));

            if (integerArrayRepresentation[2] > 0)
                humanizedResult.Append(HumanizeHundreds(integerArrayRepresentation[2]) + " ");

            if (integerArrayRepresentation[1] > 0)
                humanizedResult.Append(HumanizeTens(restFromHundrets, integerArrayRepresentation[1]));

            if (restFromHundrets >= 20 && integerArrayRepresentation[0] > 0)
                // 'ones' does not get "-" before when tens lower than 20 eg. nieteen, twenty-one
                humanizedResult.Append("-");

            if (integerArrayRepresentation[0] > 0 && (restFromHundrets < 10 || restFromHundrets > 20))
                // from 10 to 20 range resolved with 'ones' by humanizeTens() above
                humanizedResult.Append(HumanizeOnes(integerArrayRepresentation[0]));

            if (restFromHundrets == 0 && integerArrayRepresentation[2] == 0)
                // ones result with 'zero' only when tens and hundreds equal 0
                humanizedResult.Append(HumanizeOnes(integerArrayRepresentation[0]));

            // simulating for service delays
            Thread.Sleep(500);

            return humanizedResult.ToString().Trim();
        }

        private string HumanizeHundreds(ushort numberOfHundreds)
        {
            return _numbersRepresentationTable[numberOfHundreds] + " hundred";
        }

        private string HumanizeTens(ushort restFromHundrets, ushort numberOfTens)
        {
            if (restFromHundrets < 20)
                return _numbersRepresentationTable[restFromHundrets];
            else
                return _numbersRepresentationTable[(ushort)(numberOfTens * 10)];
        }

        private string HumanizeOnes(ushort numberOfOnes)
        {
            return _numbersRepresentationTable[(numberOfOnes)];
        }

        private readonly Dictionary<ushort, string> _numbersRepresentationTable = new Dictionary<ushort, string> {
            { 0, "zero" }, { 1, "one"}, { 2, "two"}, { 3, "three"}, { 4, "four"},
            { 5, "five"}, { 6, "six"}, { 7, "seven"}, { 8, "eight"}, { 9, "nine"},
            { 10, "ten"}, { 11, "eleven"}, { 12, "twelwe"}, { 13, "thirteen"}, { 14, "fourteen"},
            { 15, "fivteen"}, { 16, "sixteen"}, { 17, "seventeen"}, { 18, "eighteen"}, { 19, "nineteen"},
            { 20, "twenty"}, { 30, "thirty"}, { 40, "fourty"}, { 50, "fifty"}, { 60, "sixty"},
            { 70, "sevety"}, { 80, "eighty"}, { 90, "ninety"}
        };
    }
}
