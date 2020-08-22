using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace Server {
    static class Program {
        static void Main(string[] args) {
            using (var stream = ReceiveMessage()) {
                DeserializeV2(stream);
            }

            Console.WriteLine("press enter to finish");
            Console.ReadLine();
        }

        static Stream ReceiveMessage() {
            using (var s = new NamedPipeServerStream("pipe1",
                PipeDirection.InOut, 1, PipeTransmissionMode.Message)) {
                s.WaitForConnection();

                var counter = 1;
                var message = new StringBuilder();
                var chunk = "";

                var buffer = new byte[30]; // Read in blocks

                var stream = new MemoryStream();

                do {
                    s.Read(buffer, 0, buffer.Length);

                    //pipestream does not support seaking
                    stream.Write(buffer, 0, buffer.Length);

                    chunk = Encoding.ASCII.GetString(buffer);
                    message.Append(chunk);
                    Console.WriteLine($"chunk {counter} read: {chunk}");
                    counter = counter + 1;

                    Array.Clear(buffer, 0, buffer.Length);

                    Thread.Sleep(1000);
                } while (!s.IsMessageComplete);


                stream.Position = 0;
                return stream;
            }
        }

        static void DeserializeV2(Stream stream) {
            var formatter = new BinaryFormatter();

            formatter.Binder = new ServerBinder();

            var car = (LibV2.Car_V2) formatter.Deserialize(stream);
            Console.WriteLine($"Car deserialized: {car.Type}");
        }
    }
}