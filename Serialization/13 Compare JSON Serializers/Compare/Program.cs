using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

namespace Compare {
    class Program {
        static void Main(string[] args) {
            JS();
            DJS();
            JNET();

            Console.ReadLine();
        }

        static void JS() {
            var car = InstantiateCar();

            var ser = new JavaScriptSerializer();
            var result = ser.Serialize(car);

            Console.Write("JavaScriptSerializer:" + '\n');

            Console.WriteLine(result);

            var car1 = ser.Deserialize<Car>(result);

            Console.WriteLine("Reference Integrity: " +
                              object.ReferenceEquals(car1.AssemblingAdress,
                                  car1.ManufacturingAdress).ToString() + '\n');

        }

        static void DJS() {
            var car = InstantiateCar();

            var ms = new MemoryStream();

            var ser = new
                DataContractJsonSerializer(typeof(Car));

            ser.WriteObject(ms, car);

            ms.Position = 0;

            Console.Write("DataContractJsonSerializer:" + '\n');
            Console.WriteLine(new StreamReader(ms).ReadToEnd());

            ms.Position = 0;

            var car1 = (Car) ser.ReadObject(ms);

            Console.WriteLine("Reference Integrity: " +
                              object.ReferenceEquals(car1.AssemblingAdress,
                                  car1.ManufacturingAdress).ToString() + '\n');
        }

        static void JNET() {
            var car = InstantiateCar();

            var json = JsonConvert.SerializeObject(car, Formatting.Indented,
                new JsonSerializerSettings {
                    PreserveReferencesHandling =
                        PreserveReferencesHandling.Objects
                });

            Console.Write("Json.Net:" + '\n');
            Console.WriteLine(json);

            var car1 = JsonConvert.DeserializeObject<Car>(json);

            Console.WriteLine("Reference Integrity: " +
                              object.ReferenceEquals(car1.AssemblingAdress,
                                  car1.ManufacturingAdress).ToString() + '\n');
        }

        static Car InstantiateCar() {
            var wheels = new List<Wheel>();
            wheels.Add(new Wheel {id = 1, Type = "Bridgestone"});
            wheels.Add(new Wheel {id = 2, Type = "Michelin "});

            var tbl = new Hashtable();
            tbl.Add("key1", "val1");
            tbl.Add("key2", "val2");

            var address = new Address
                {Country = "US", PostalCode = 123};

            return new Car {
                IsHatchback = false,
                ManufacturingDate = DateTime.Now,
                Model = 2014,
                Type = "Chev",
                Wheels = wheels,
                tbl = tbl,
                ManufacturingAdress = address,
                AssemblingAdress = address,
                En = CarConditionEnum.New
            };
        }

    }
}