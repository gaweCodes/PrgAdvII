using System;
using System.Runtime.Serialization;

namespace App {
    [Serializable]
    public class Car {
        public string RentedBy;
        public int RentDays;

        [NonSerialized]
        private int RentalRate; // wird z.B. jeweils von einem Webservice gelesen
        [NonSerialized]
        private int TotalRent; // wird berechnet

        public Car() {
            //Read rate from a web service
            RentalRate = 50;
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context) {
            //For some reason, this is secret stuff!
            RentedBy = Cesar.Encrypt(this.RentedBy, 15);
        }

        [OnSerialized]
        private void OnSerialized(StreamingContext context) {
            RentedBy = Cesar.Decrypt(this.RentedBy, 15);
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context) {
            //Read rate from a web service
            RentalRate = 50;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            TotalRent = RentalRate * RentDays;
        }
    }
}