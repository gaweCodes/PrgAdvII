using System;
using System.Runtime.Serialization;

namespace TypesLib {
    [Serializable]
    public abstract class Automobile {
        [OptionalField] public string Type;
        public int Model;

    }
}