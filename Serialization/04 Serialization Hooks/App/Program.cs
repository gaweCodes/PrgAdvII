using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace App {
    class Program {
        static void Main(string[] args) {
            var car = new Car();
            car.RentDays = 5;
            car.RentedBy = "Thomas";

            using (MemoryStream ms = new MemoryStream()) {

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, car);

                ms.Position = 0;

                Car car1 = (Car) formatter.Deserialize(ms);
            }

            Console.ReadLine();
        }
    }
}