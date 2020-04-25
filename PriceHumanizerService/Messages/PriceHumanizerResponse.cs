using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interviews.VM.PriceHumanizer.ServiceDefinition.Messages
{
    [DataContract]
    public class PriceHumanizerResponse
    {
        [DataMember]
        public string humanizedPrice;
    }
}
