using System;

namespace Introduction
{
    internal static class Program
    {
        private static void Main()
        {
            var integerPrinter = new IntegerPrinter(5);
            integerPrinter.Print();
            
            var doublePrinter = new DoublePrinter(double.Parse("5"));
            doublePrinter.Print();

            var stringPrinter = new StringPrinter("5");
            stringPrinter.Print();


            var objPrinter1 = new ObjectPrinter(5);
            objPrinter1.Print();
            var objPrinter2 = new ObjectPrinter(double.Parse("5"));
            objPrinter2.Print();
            var objPrinter3 = new ObjectPrinter("asdf");
            objPrinter3.Print();

            Console.WriteLine((int)objPrinter1.GetValue() + 5);
            Console.WriteLine((double)objPrinter2.GetValue() + 5);

            var genPrinter1 = new GenericPrinter<int>(5);
            genPrinter1.Print();
            var genPrinter2 = new GenericPrinter<double>(double.Parse("5"));
            genPrinter2.Print();
            var genPrinter3 = new GenericPrinter<string>("asdf");
            genPrinter3.Print();

            Console.WriteLine(genPrinter1.GetValue() + 5);
            Console.WriteLine(genPrinter2.GetValue() + 5);

            Console.ReadLine();
        }
    }
}
