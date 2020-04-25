using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interviews.VM.PriceHumanizer.Logic
{
    public class CurrencyFormatParser : IFormatParser
    {
        public CurrencyFormatParser()
        {
            formatChecker = new Regex(@"^\d{1,3}(\s\d{3}){0,2}(,\d{2})?$"); // fixed currency format {{DDD }DDD }DDD{,DD}
        }

        Regex formatChecker;

        public bool CheckFormat(string currencyValue)
        {
            if (formatChecker.IsMatch(currencyValue))
                return true;
            else
                return false;
        }
        
        public CurrencyData Parse(string currencyValue)
        {
            if (!CheckFormat(currencyValue))
                throw new FormatException("Input string not properly formatted.");

            var decimalValueSplits = currencyValue.Split(',');
            var integralValueGroupSplits = decimalValueSplits[0].Split(' ');

            return ParseValues(integralValueGroupSplits, decimalValueSplits);
        }

        private CurrencyData ParseValues(string[] integralValueGroupSplits, string[] decimalValueSplits)
        {
            var millionsExists = integralValueGroupSplits.Count() == 3;
            var thousandsExists = integralValueGroupSplits.Count() >= 2;
            var dollarsExists = integralValueGroupSplits.Count() >= 1;

            var decimalExists = decimalValueSplits.Count() == 2;

            var result = new CurrencyData();

            if (millionsExists)
            {
                result.millions = ushort.Parse(integralValueGroupSplits[0]);
                result.thousands = ushort.Parse(integralValueGroupSplits[1]);
                result.dollars = ushort.Parse(integralValueGroupSplits[2]);
            }
            else if (thousandsExists)
            {
                result.thousands = ushort.Parse(integralValueGroupSplits[0]);
                result.dollars = ushort.Parse(integralValueGroupSplits[1]);
            }
            else if (dollarsExists)
            {
                result.dollars = ushort.Parse(integralValueGroupSplits[0]);
            }

            if (decimalExists)
                result.decimals = ushort.Parse(decimalValueSplits[1]);

            result.decimalExists = decimalExists;

            return result;
        }
    }
}
