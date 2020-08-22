using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using LibV1;

namespace Client {
    static class Program {
        static void Main(string[] args) {
            var stream = Serialize();
            TransmitMessage(stream);

            Console.ReadLine();
        }

        private static Stream Serialize() {
            Console.WriteLine("Enter Cartype: ");
            var type = Console.ReadLine();
            var car = new Car_V1 {
                Type = type,
                Model = 2015,
                IsHatchback = false
            };

            var stream = new MemoryStream();

            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, car);

            stream.Position = 0;

            return stream;
        }

        static void TransmitMessage(Stream stream) {
            using (var s = new NamedPipeClientStream("pipe1")) {
                Console.WriteLine("press enter to write message to pipe");
                Console.ReadLine();

                s.Connect();

                var msg = ((MemoryStream) stream).ToArray();
                s.Write(msg, 0, msg.Length);

                Console.WriteLine("message written to pipe");
            }

        }
    }
}