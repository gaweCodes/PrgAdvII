using System;
using System.Diagnostics;

namespace Attributes.Attribute1
{
    internal static class Trace
    {
        [Conditional("tracing")]
        public static void WriteLine(string s) => Console.WriteLine(s);
        [Conditional("tracing")]
        public static void WriteLine() => Console.WriteLine();
        [Conditional("tracing")]
        public static void Write(string s) => Console.Write(s);

    }
}
