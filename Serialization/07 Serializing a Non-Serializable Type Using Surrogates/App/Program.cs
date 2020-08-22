using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Lib;

namespace App {
    static class Program {
        static void Main(string[] args) {
            var car = new Car();
            car.Type = "Chev";
            car.Model = 2015;
            car.IsHatchback = 0;

            using (var stream = new MemoryStream()) {
                var formatter = new BinaryFormatter();

                var carSurrogate = new CarSurrogate();

                var surrogateSelector = new SurrogateSelector();
                surrogateSelector.AddSurrogate(typeof(Car),
                    new StreamingContext(StreamingContextStates.All), carSurrogate);

                // ohne SurrogateSelector tritt eine Exception auf, weil Car nicht serialisierbar ist
                formatter.SurrogateSelector = surrogateSelector;

                formatter.Serialize(stream, car);

                stream.Position = 0;

                var deserializedCar = (Car) formatter.Deserialize(stream);
            }
        }
    }
}