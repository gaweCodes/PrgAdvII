using System.Runtime.Serialization;

namespace App {
    [DataContract]
    class Car : Automobile {
        [DataMember]
        public Radio Radio { get; set; }
    }
}