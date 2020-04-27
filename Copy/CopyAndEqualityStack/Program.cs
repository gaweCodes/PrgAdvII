using System;

namespace CopyAndEqualityStack
{
    internal static class Program
    {
        private static void Main()
        {
            TwoEmptyStacksWithSameCapacityShouldBeEqual();
            TwoEmptyStacksWithDifferentCapacitiesShouldBeEqual();
            StackIsEqualToSelf();
            StacksWithEqualElementsShouldBeEqual();
            CloningShouldResultInEqualStacks();
            CloningAndChangingAStackShouldNotBeEqual();
            Console.ReadLine();
        }
        private static void TwoEmptyStacksWithSameCapacityShouldBeEqual()
        {
            var stack1 = new Stack(5);
            var stack2 = new Stack(5);
            Console.WriteLine(stack1 == stack2);
        }
        private static void TwoEmptyStacksWithDifferentCapacitiesShouldBeEqual()
        {
            var stack1 = new Stack(1);
            var stack2 = new Stack(2);
            Console.WriteLine(stack1 == stack2);
        }
        private static void StackIsEqualToSelf()
        {
            var stack1 = new Stack(1);
            Console.WriteLine(stack1 == stack1);
        }
        private static void StacksWithEqualElementsShouldBeEqual()
        {
            var stack1 = new Stack(1);
            stack1.Push("ZbW");
            var stack2 = new Stack(1);
            stack2.Push("ZbW");
            Console.WriteLine(stack1 == stack2);
        }
        private static void CloningShouldResultInEqualStacks()
        {
            var stack1 = new Stack(2);
            stack1.Push("Hello");
            stack1.Push("ZbW");
            var stack2 = (Stack)stack1.Clone();
            Console.WriteLine(stack1 == stack2);
        }
        private static void CloningAndChangingAStackShouldNotBeEqual()
        {
            var stack1 = new Stack(2);
            stack1.Push("Ciao");
            var stack2 = (Stack)stack1.Clone();
            stack1.Push("Hello");
            Console.WriteLine(stack1 != stack2);
        }
    }
}
