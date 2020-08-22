using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace App {
    class Program {
        static void Main(string[] args) {
            using (MemoryStream ms = new MemoryStream()) {

                Car car = new Car {Model = 2014, Type = "Chev"};
                car.Radio = new Radio {Type = "Sony"};
                car.Wheel = new Wheel {Diameter = 50};


                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, car);

                ms.Position = 0;

                Car car1 = (Car) formatter.Deserialize(ms);
            }

            Console.ReadLine();
        }

    }
}