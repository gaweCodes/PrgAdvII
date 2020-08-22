using System;
using System.IO;
using System.Runtime.Serialization;

namespace App {
    static class Program {
        static void Main(string[] args) {
            using (var ms = new MemoryStream()) {
                var car = new Car {Type = "Chev", Model = 2014};
                car.Radio = new Radio { Type = "Sony" };

                DataContractSerializer ser =
                    new DataContractSerializer(typeof(Automobile));

                // Damit Car als Automobile serialisert werden kann, muss dieser bei Automobile bekannt sein
                // -> Es werden ja keine Assemblies und Typen serialisiert
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
