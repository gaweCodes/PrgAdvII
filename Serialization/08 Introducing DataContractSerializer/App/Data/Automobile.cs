using System.Runtime.Serialization;

namespace App {
    [DataContract, KnownType(typeof(Car))]
    public class Automobile {
        private string _type;
        [DataMember]
        public string Type {
            get { return _type; }
            set { _type = value; }
        }

        private int _model;
        [DataMember]
        public int Model {
            get { return _model; }
            set { _model = value; }
        }

    }
}
