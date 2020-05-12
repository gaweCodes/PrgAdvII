using System;
using System.Collections.Generic;

namespace Generics.Delegates
{
    public delegate void Action(int i);
    public class MyClass 
    {
        public static void PrintValues(int i)  => Console.WriteLine("Value {0}", i);
    }
    public class FunctionParameterTest 
    {
        private static void ForAll(IEnumerable<int> array, Action action) 
        {
            Console.WriteLine("ForAll called...");
            if (action == null)
                return;
            foreach (int t in array) action(t);
        }
        public static void TestPrintValues() 
        {
            var c = new MyClass();
            int[] a1 = {1, 5, 8, 14, 22};
            string[] a2 = {"a", "b", "c"};
            ForAll(a1, MyClass.PrintValues);
            //ForAll(a2, MyClass.PrintValues);  
        }
    }
}