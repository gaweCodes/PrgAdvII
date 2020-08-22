using System;
using System.Runtime.Serialization;

namespace App {
    [Serializable]
    public class Wheel : ISerializable {
        public int Diameter;

        public Wheel() {
        }

        public void GetObjectData(SerializationInfo info,
            StreamingContext context) {
            info.AddValue("Diameter", Diameter);
        }

        public Wheel(SerializationInfo info, StreamingContext context) {
            Diameter = info.GetInt32("Diameter");
        }
    }
}