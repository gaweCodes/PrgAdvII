using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace App {
    static class Program {
        static void Main(string[] args) {
            using (var fs = new FileStream(Path, FileMode.Create)) {

                var wheels = new List<Wheel>();
                wheels.Add(new Wheel {Id = 1, Type = "Bridgestone"});
                wheels.Add(new Wheel {Id = 2, Type = "Bridgestone"});
                var car = new Car {Model = 2015, Type = "Chev", IsHatchback = false, Wheels = wheels, Price = 20000};

                var ser = new DataContractSerializer(typeof(Car));
                ser.WriteObject(fs, car);

                fs.Position = 0;

                var car1 = (Car) ser.ReadObject(fs);
                fs.Close();


                Console.ReadLine();
            }
        }

        static string Path {
            get { return Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.LastIndexOf("bin")) + "\\data.xml"; }
        }
    }
}