using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Interviews.VM.PriceHumanizer.ServiceDefinition.Messages;
using Interviews.VM.PriceHumanizer.Logic;


namespace Interviews.VM.PriceHumanizer.ServiceDefinition.Contracts
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class PriceHumanizerService : IPriceHumanizerService
    {
        public PriceHumanizerResponse HumanizePrice(PriceHumanizerRequest request)
        {
            IIntergerHumanizer _integerHumanaizer = new IntegerHumanizer();
            IReadableBuilder _currencyBuilder = new CurrencyReadableBuilder(_integerHumanaizer);

            var priceHuminizer = new CurrencyHumanizer(_currencyBuilder, new CurrencyFormatParser()); 

            var resp = priceHuminizer.Humanize(request.price);

            return new PriceHumanizerResponse { humanizedPrice = resp };
        }
    }
}
