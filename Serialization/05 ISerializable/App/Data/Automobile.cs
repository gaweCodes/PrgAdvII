using System;
using System.Runtime.Serialization;

namespace App {
    [Serializable]
    public class Automobile : ISerializable {
        public string Type;
        public int Model;

        public Automobile() {
        }

        public virtual void GetObjectData(SerializationInfo info,
            StreamingContext context) {

            info.AddValue("Type", Type);
            info.AddValue("Model", Model);
        }

        protected Automobile(SerializationInfo info,
            StreamingContext context) {
            Type = info.GetString("Type");
            Model = info.GetInt16("Model");
        }
    }
}
