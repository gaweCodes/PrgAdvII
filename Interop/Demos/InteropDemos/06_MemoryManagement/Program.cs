using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorSvrLib;

namespace _06_MemoryManagement {
    class Program {
        static void Main(string[] args) {
            // Programm muss mit Ctrl+F5 gestartet werden
            // CalculatorSrv.dll hat zwei Ausgaben:
            //  - 1x im Constructor
            //  - 1x im Desctructor
            // Diese sind in DbgView sichtbar (https://docs.microsoft.com/en-us/sysinternals/downloads/debugview)
            var calc = new Calculator();
            Console.WriteLine(calc.Add(3, 4));

            Console.WriteLine("Before setting to null");
            Console.ReadLine();
            calc = null;   // setzt den Referenz-Counter -=1

            Console.WriteLine("Before calling GC.Collect");
            Console.ReadLine();
            GC.Collect();  // Weil Referenz-Counter == 0 wird das COM-Objekt entfernt

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
