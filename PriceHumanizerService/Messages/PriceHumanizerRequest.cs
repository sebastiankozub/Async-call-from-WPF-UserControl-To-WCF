using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Interviews.VM.PriceHumanizer.ServiceDefinition.Messages
{
    [DataContract]
    public class PriceHumanizerRequest
    {
        [DataMember]
        public string price;
    }
}
