using System;

namespace AdditionalTasks
{
    public static partial class Operators
    {
        public delegate TOut MyConverter<T1, T2, TOut>(T1 elem1, T2 elem2);
        public static TOut[] CombineAll<T1, T2, TOut>(T1[] array1, T2[] array2, MyConverter<T1, T2, TOut> converter)
        {
            var result = new TOut[array1.Length];
            for (var i = 0; i < array1.Length; i++) result[i] = converter(array1[i], array2[i]);
            return result;
        }
        public static void TestCombineAll()
        {
            var res = CombineAll(
                new[] { 2, 3, 4 },
                new[] { 1, 2, 5 },
                (a, b) => a * b
            );
            foreach (int i in res) Console.WriteLine(i);

            var res1 = CombineAll(
                new[] { 2, 3, 4 },
               new[] { "abc", "ef", "uv" },
                (a, b) => b + a.ToString()
            );
            foreach (var s in res1) Console.WriteLine(s);
        }
    }
}
