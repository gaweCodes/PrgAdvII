using System;
using System.IO;
using System.Runtime.Serialization;

namespace App {
    static class Program {
        static void Main(string[] args) {
            using (var ms = new MemoryStream()) {

                var car = new Car {Model = 2014, Type = "Chev"};

                var ser = new DataContractSerializer
                (typeof(Car),
                    null, int.MaxValue, false, false,
                    new CarSurrogate());

                ser.WriteObject(ms, car);

                ms.Position = 0;

                Console.WriteLine(new StreamReader(ms).ReadToEnd());

                ms.Position = 0;

                var car1 = (Car) ser.ReadObject(ms);

                Console.ReadLine();
            }
        }
    }
}