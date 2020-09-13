using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorSvrLib;

namespace _05_IUnknown {
    class Program {
        static void Main(string[] args) {
            ISimpleCalculator calc = new Calculator();
            Console.WriteLine(calc.Add(3, 4));
            ITrigCalculator trig = calc as ITrigCalculator;   // Verwendet QueryInterface()
            if (trig != null) {                               // Prüfung, ob das Interface ITrigCalculator vorhanden
                Console.WriteLine(trig.Sin(30));     // Radiant/Bogenmass   (360° -> 2Pi)
                trig.Degrees = true;
                Console.WriteLine(trig.Sin(30));     // Grad
            }


            // zweiter Versuch: DLL Demos\CalculatorSvr.dll ersetzen durch CalculatorSvr.dll_WithoutITrigCalculator (hier wurde das Interface ITrigCalculator auskommentiert)
            // -> nun ist trig == null, d.h. QueryInterface() liefert das Interface nicht zurück
        }
    }
}
