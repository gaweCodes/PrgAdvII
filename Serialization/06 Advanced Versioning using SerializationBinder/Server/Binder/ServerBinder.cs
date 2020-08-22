using System;
using System.Runtime.Serialization;

namespace Server {
    class ServerBinder : SerializationBinder {
        public override Type BindToType(string assemblyName, string typeName) {
            var newAssemblyName = assemblyName;
            var newTypeName = typeName;

            if (assemblyName == "LibV1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" && typeName == "LibV1.Car_V1") {
                newAssemblyName = "LibV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
                newTypeName = "LibV2.Car_V2";
            }

            return Type.GetType(newTypeName + ", " + newAssemblyName);
        }
    }
}