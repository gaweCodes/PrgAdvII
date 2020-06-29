using System;

namespace Attributes.Attribute1
{
    internal static class Program
    {
        private static void Main()
        {
            var stack = new Stack();
            for (var i = 0; i < 5; i++) stack.Push(i);

            while (stack.Size > 1)
                stack.Push(stack.Pop() + stack.Pop());
            Console.ReadLine();
        }
    }
}
