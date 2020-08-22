//using App1.Types;
using TypesLib;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace BinarySerialization {
    class Program {
        static void Main(string[] args) {
            Car car = new Car {
                Type = "Chev",
                Model = 2015,
                IsHatchBack = false,
                ConfigPassword = "Abacadabra"
            };
            car._Wheel = new Wheel {Diameter = 40};

            Truck truck = new Truck {
                Type = "Toyota",
                Model = 2015,
                IsSemi = false,
                ConfigPassword = "Geheim"            
            };
            truck._Wheel = new Wheel {Diameter = 80};

            using (MemoryStream stream = new MemoryStream()) {
                var formatter = new BinaryFormatter();
                //var formatter = new SoapFormatter();
                formatter.Serialize(stream, car);
                formatter.Serialize(stream, truck);
                
                stream.Position = 0;

                Console.WriteLine(new StreamReader(stream).ReadToEnd());  // nicht wirklich lesbar -> BinaryFormat - jedoch Assembly und Types sichtbar
                                                                          // -> SoapFormatter

                stream.Position = 0;

                Car car2 = (Car) formatter.Deserialize(stream);
                Truck truck2 = (Truck) formatter.Deserialize(stream);

                var eq = car == car2;  // true oder false?
            }



            Console.ReadLine();
        }
    }
}