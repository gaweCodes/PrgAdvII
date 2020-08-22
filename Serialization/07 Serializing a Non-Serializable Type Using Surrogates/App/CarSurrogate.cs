using System.Runtime.Serialization;
using Lib;

namespace App {
    class CarSurrogate : ISerializationSurrogate {
        public void GetObjectData(object obj, SerializationInfo info,
            StreamingContext context) {
            var car = (Car) obj;

            info.AddValue("Model", car.Model);
            info.AddValue("Type", car.Type);
            info.AddValue("IsHatchback", car.IsHatchback);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {

            // object wird von FormatterServices.GetUninitializedObject erstellt
            // https://referencesource.microsoft.com/#mscorlib/system/runtime/serialization/formatters/binary/binaryobjectreader.cs,b9d0c578b71509b3,references
            // https://stackoverflow.com/questions/4866179/where-does-nativegetuninitializedobject-actually-exist
            // --> WICHTIG: es erfolgt kein Konstruktoraufruf!

            var car = (Car) obj;

            car.Model = info.GetInt32("Model");
            car.Type = info.GetString("Type");
            car.IsHatchback = info.GetInt32("IsHatchback");

            return car;

            //or we could do the following to save "obj" and return null
            //((SurrogateLib.Car)obj).Model = info.GetInt32("Model");
            //((SurrogateLib.Car)obj).Type = info.GetString("Type");
            //((SurrogateLib.Car)obj).IsHatchback = info.GetInt32("IsHatchback");
            //return null;
        }
    }
}