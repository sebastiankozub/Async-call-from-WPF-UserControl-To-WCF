using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interviews.VM.PriceHumanizer.Logic
{
    public class CurrencyHumanizer : ICurrencyHumanizer
    {
        public CurrencyHumanizer(IReadableBuilder currencyBuilder, IFormatParser currencyParser)
        {
            _currencyBuilder = currencyBuilder;
            _currencyParser = currencyParser;
        }

        IReadableBuilder _currencyBuilder;
        IFormatParser _currencyParser;
        
        public string Humanize(string currencyFormatRepresentation)
        {
            if (_currencyParser.CheckFormat(currencyFormatRepresentation))
                return HuminizeCurrencyString(currencyFormatRepresentation);
            else
                throw new FormatException("Input string not properly formatted.");
        }

        private string HuminizeCurrencyString(string currencyFormatRepresentation)
        {
            var parsedData = _currencyParser.Parse(currencyFormatRepresentation);
            return _currencyBuilder.Build(parsedData);
        }
    }
}
