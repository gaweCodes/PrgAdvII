using System;

namespace Introduction
{
    internal class IntegerPrinter
    {
        private readonly int _value;
        public IntegerPrinter(int value)
        {
            _value = value;
        }
        public void Print()
        {
            Console.WriteLine($"Der Wert ist {_value}");
        }
    }
}
