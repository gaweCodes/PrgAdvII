using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using TypesLib;

namespace App2 {
    class Program {
        static void Main(string[] args) {
            Stream stream = ReceiveMessage();
            Deserialize(stream);


            Console.ReadLine();
        }

        static Stream ReceiveMessage() {
            using (var s = new NamedPipeServerStream("pipe1",
                PipeDirection.InOut, 1, PipeTransmissionMode.Message)) {
                s.WaitForConnection();

                int counter = 1;
                StringBuilder message = new StringBuilder();
                string chunk = "";

                byte[] buffer = new byte[30]; // Read in blocks

                MemoryStream stream = new MemoryStream();

                do {
                    s.Read(buffer, 0, buffer.Length);

                    //pipestream does not support seaking
                    stream.Write(buffer, 0, buffer.Length);

                    chunk = Encoding.ASCII.GetString(buffer);
                    message.Append(chunk);
                    Console.WriteLine("chunk " + counter.ToString() +
                                      " read: " + chunk);
                    counter = counter + 1;

                    Array.Clear(buffer, 0, buffer.Length);

                    Thread.Sleep(500);
                } while (!s.IsMessageComplete);


                stream.Position = 0;
                return stream;
            }
        }

        static void Deserialize(Stream stream) {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.AssemblyFormat = FormatterAssemblyStyle.Full;

            Car car = (Car) formatter.Deserialize(stream);
            Truck truck = (Truck) formatter.Deserialize(stream);

            Console.WriteLine();

            Console.WriteLine("Deserialization done!");
        }

    }
}