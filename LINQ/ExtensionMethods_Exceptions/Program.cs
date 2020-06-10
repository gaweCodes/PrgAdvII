using System;

namespace ExtensionMethods_Exceptions
{
    internal static class Program
    {
        private static Exception TestException
        {
            get
            {
                return
                    new ApplicationException(
                        "Top",
                        new NotSupportedException(
                            "Middle",
                            new NotImplementedException("Bottom")
                        )
                    );
            }
        }
        private static void Main()
        {
            Console.WriteLine("---------- \r\nTestGetInnerstException");
            TestGetInnerstException();
            Console.WriteLine("---------- \r\nTestForEachException");
            TestForEachException();
            Console.ReadKey();
        }
        private static void TestGetInnerstException()
        {
            Exception e = TestException.GetInnerstException();
            Console.WriteLine(e.Message);
        }
        private static void TestForEachException()
        {
            TestException.ForEachException(e => Console.WriteLine(e.Message));
        }
    }
}
