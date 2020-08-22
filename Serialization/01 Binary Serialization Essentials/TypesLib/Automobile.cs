using System;

namespace TypesLib {
    [Serializable]
    public abstract class Automobile {
        public string Type;
        public int Model { get; set; }

        

        [NonSerialized]
        public string ConfigPassword;

        public Wheel _Wheel;
    }
}