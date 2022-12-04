using System.Runtime.Serialization;
using System.ServiceModel;

namespace Classes {
    [DataContract]
    public class User {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Nickname { get; set; }
        public string Password { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
