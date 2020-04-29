using System;

namespace Introduction
{
    internal class DoublePrinter
    {
        private readonly double _value;
        public DoublePrinter(double value)
        {
            _value = value;
        }
        public void Print()
        {
            Console.WriteLine($"Der Wert ist {_value}");
        }
    }
}
