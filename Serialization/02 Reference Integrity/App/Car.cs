using System;

namespace App {
    [Serializable]
    public class Car {
        public bool isHatchBack;

        public string Type;

        public int Model;

        public Address ManufacturingAddress;
        public Address AssemblingAddress;
    }
}