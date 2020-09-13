using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MathServerLib;

namespace _04_SimpleCOMInterop {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            //try {
                ICalculator calc = new Calculator();       //  dotPeek/Decompile Sources:   ICalculator instance = (ICalculator) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("8276B052-81CC-451F-9FD5-67B9A0B1BFFD")));


            int result = calc.Add(3, 4);
                Console.WriteLine("result: {0}", result);
            //} catch (COMException ex) {
            //    Console.WriteLine(ex.Message);
            //}
        }
    }
}
