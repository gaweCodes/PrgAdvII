using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace App {
    static class Program {
        static void Main(string[] args) {
            using (var ms = new MemoryStream()) {

                var manufacturingAddress = new Address {POBox = "11", PostalCode = "22"};

                var assemblingAddress = manufacturingAddress;

                var car = new Car {isHatchBack = false, Model = 2014, Type = "Chev"};
                car.ManufacturingAddress = manufacturingAddress;
                car.AssemblingAddress = assemblingAddress;

                Console.WriteLine(object.ReferenceEquals(car.ManufacturingAddress, car.AssemblingAddress));

                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, car);

                ms.Position = 0;

                var car1 = (Car) formatter.Deserialize(ms);

                Console.WriteLine(object.ReferenceEquals(car1.ManufacturingAddress, car1.AssemblingAddress));

                Console.ReadLine();
            }
        }
    }
}