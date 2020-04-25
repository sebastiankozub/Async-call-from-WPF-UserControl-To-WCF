using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Interviews.VM.PriceHumanizer.ServiceDefinition.Messages;

namespace Interviews.VM.PriceHumanizer.ServiceDefinition.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPriceHumanizerService
    {
        [OperationContract]
        PriceHumanizerResponse HumanizePrice(PriceHumanizerRequest request);
    }
}

