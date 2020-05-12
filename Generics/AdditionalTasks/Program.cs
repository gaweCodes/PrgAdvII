using System;

namespace AdditionalTasks
{
    internal static class Program
    {
        private static void Main()
        {
            TestBinaryTree();
            ForAll();
            Comparer();
            Combiner();
            Sort();
            FoldR();
            Console.ReadLine();
        }
        private static void TestBinaryTree()
        {
            Console.WriteLine("TestBinaryTree");
            var mTree = new BinaryTre<int>();
            mTree.Add(4, 6, 2, 7, 5, 3, 1);
            Console.WriteLine("Trace");
            mTree.TraceTree();
        }
        private static void ForAll()
        {
            Console.WriteLine("TestForAll");
            Operators.TestForAll();
        }
        private static void Comparer()
        {
            Console.WriteLine("TestComparer");
            Operators.TestComparer();
        }
        private static void Combiner()
        {
            Console.WriteLine("Combiner");
            Operators.TestCombineAll();
        }
        private static void Sort()
        {
            Console.WriteLine("sort");
            Operators.TestSort();
        }
        private static void FoldR()
        {
            Console.WriteLine("FoldR");
            Operators.TestFold();
        }
    }
}
