using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interviews.VM.PriceHumanizer.Logic
{
    public class CurrencyReadableBuilder : IReadableBuilder
    {
        public CurrencyReadableBuilder(IIntergerHumanizer integerHuminizer)
        {
            _integerHuminizer = integerHuminizer;
        }

        IIntergerHumanizer _integerHuminizer; 
        
        public string Build(CurrencyData data)
        {
            var isMillionGreaterZero = data.millions != 0;
            var isThousandGreaterZero = data.thousands != 0;
            var isDollarGreaterZero = data.dollars != 0;

            string result = "";

            if (isMillionGreaterZero)
            {
                result = HumanizeMillions(data.millions);
            }

            if (isThousandGreaterZero)
            {
                result += HumanizeThousands(data.thousands);
            }

            if (isDollarGreaterZero) 
            {
                result += HumanizeDollars(data.dollars);
            }
            else if (!(isMillionGreaterZero || isThousandGreaterZero))
            {
                result += HumanizeDollars(data.dollars);
            }

            result += (data.millions + data.thousands + data.dollars) == 1 ? "dollar" : "dollars";

            if (data.decimalExists)
                result += HumanizeDecimals(data.decimals);

            return result;
        }

        private string HumanizeMillions(ushort number)
        {
            return number == 0 ? "" : _integerHuminizer.Humanize(number) + " million ";
        }

        private string HumanizeThousands(ushort number)
        {
            return number == 0 ? "" : _integerHuminizer.Humanize(number) + " thousand ";
        }

        private string HumanizeDollars(ushort number)
        {
            return _integerHuminizer.Humanize(number) + " ";
        }

        private string HumanizeDecimals(ushort number)
        {
            return " and " + _integerHuminizer.Humanize(number) + (number == 1 ? " cent" : " cents");
        }
    }
}
