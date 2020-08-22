using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App {
    class Program {
        static void Main(string[] args) {
            Car car = InstantiateCar();

            using (FileStream fs = new FileStream(Path, FileMode.Create)) {
                XmlSerializer ser = new XmlSerializer(typeof(Car));
                ser.Serialize(fs, car);
                fs.Close();

                Console.WriteLine("Done");

                Console.ReadLine();
            }
        }

        static Car InstantiateCar() {
            List<Wheel> wheels = new List<Wheel>();
            wheels.Add(new Wheel {Type = "Bridgestone"});
            wheels.Add(new Wheel {Type = "Bridgestone"});

            Radio radio = new Radio {Model = 2015, Type = "Sony"};

            return new Car {
                _Radio = radio,
                Wheels = wheels,
                Model = 2015,
                Type = "Chev",
                Price = 60000
            };
        }

        static string Path {
            get { return Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.LastIndexOf("bin")) + "\\data.xml"; }
        }
    }
}