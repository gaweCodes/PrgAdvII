using System;

namespace Attributes.Attribute2 
{
    internal static class Program 
    {
        private static void Main() 
        {
            var stack = new Stack();
            var t = stack.GetType();
            foreach (var m in t.GetMethods())
            {
                var attr = m.GetCustomAttributes(typeof(AbbreviationAttribute), true);
                if (attr.Length <= 0) continue;
                var abbr = (AbbreviationAttribute)attr[0];
                Console.WriteLine(abbr.Text);
            }
            Console.ReadLine();
        }
    }
}