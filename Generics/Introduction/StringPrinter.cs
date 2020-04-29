using System;

namespace Introduction
{
    internal class StringPrinter
    {
        private readonly string _value;
        public StringPrinter(string value)
        {
            _value = value;
        }
        public void Print()
        {
            Console.WriteLine($"Der Wert ist {_value}");
        }
    }
}
