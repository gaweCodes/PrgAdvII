using System;

namespace Generics.Delegates.Generic {
    public delegate void Action<T>(T i);

    public class MyClass {
        public static void PrintValues<T>(T i) {
            Console.WriteLine("Value {0}", i);
        }
    }

    public class FunctionParameterTest {
        private static void ForAll<T>(T[] array, Action<T> action) {
            Console.WriteLine("ForAll called...");
            if (action == null) {
                return;
            }

            foreach (T t in array) {
                action(t);
            }
        }

        public static void TestPrintValues() {
            MyClass c = new MyClass();
            int[] array = {1, 5, 8, 14, 22};

            ForAll(array, MyClass.PrintValues);
        }
    }
}