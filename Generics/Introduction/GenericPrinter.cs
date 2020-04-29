using System;

namespace Introduction
{
    internal class GenericPrinter<T>
    {
        private readonly T _value;
        public GenericPrinter(T value)
        {
            _value = value;
        }
        public void Print()
        {
            Console.WriteLine($"Der Wert ist {_value}");
        }
        public T GetValue()
        {
            return _value;
        }
    }
}
