using System;
using System.Runtime.Serialization;

namespace App {
    [Serializable]
    public class Car : Automobile, ISerializable {

        public Radio Radio;
        public Wheel Wheel;

        public Car() {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);

            info.AddValue("Radio", Radio);
            info.AddValue("Wheel", Wheel);
        }

        protected Car(SerializationInfo info, StreamingContext context) : base(info, context) {
            Radio = (Radio) info.GetValue("Radio", typeof(Radio));
            Wheel = (Wheel) info.GetValue("Wheel", typeof(Wheel));
        }

    }
}
