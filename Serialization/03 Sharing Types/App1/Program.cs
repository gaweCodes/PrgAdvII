//using App1.Types;

using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using TypesLib;

namespace BinarySerialization {
    class Program {
        static void Main(string[] args) {
            Stream stream = Serialize();

            stream.Position = 0;

            TransmitMessage(stream);

            Console.ReadLine();
        }

        private static Stream Serialize() {
            Car car = new Car {
                Type = "Chev",
                Model = 2015,
                isHatchBack = false
            };
            Truck truck = new Truck {
                Type = "Toyota",
                Model = 2015,
                isSemi = false
            };

            MemoryStream stream = new MemoryStream();

            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, car);
            formatter.Serialize(stream, truck);

            stream.Position = 0;

            return stream;
        }



        static void TransmitMessage(Stream stream) {
            using (var s = new NamedPipeClientStream("pipe1")) {
                Console.WriteLine("press enter to write message to pipe");
                Console.ReadLine();

                s.Connect();

                byte[] msg = ((MemoryStream) stream).ToArray();
                s.Write(msg, 0, msg.Length);

                Console.WriteLine("message written to pipe");
            }

        }
    }
}