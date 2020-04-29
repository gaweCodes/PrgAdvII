using System;

namespace Introduction
{
    internal class ObjectPrinter
    {
        private readonly object _value;
        public ObjectPrinter(object value)
        {
            _value = value;
        }
        public void Print()
        {
            Console.WriteLine($"Der Wert ist {_value}");
        }
        public object GetValue()
        {
            return _value;
        }
    }
}
