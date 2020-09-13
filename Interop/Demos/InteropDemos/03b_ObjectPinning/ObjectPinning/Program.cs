//#define DO_PIN

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPinning {
    class Program {
        [DllImport("SampleNativeLib")]
        static extern int SetData([MarshalAs(UnmanagedType.LPArray)] int[] data);

        [DllImport("SampleNativeLib")]
        static extern int DoCalc();

        static void Main(string[] args) {
            var data = Enumerable.Range(0, 10).Select(i => i + 1).ToArray();
#if DO_PIN
			GCHandle h = GCHandle.Alloc(data, GCHandleType.Pinned);
#endif
            // in SampleNativeLib wird ein int* (Pointer auf int) erwartet, was in C# einem int-Array entspricht (Pointer auf erstes Element des Arrays)
            // es handelt sich um ein prozedurales C-Programm. data wird dort in einer globalen Variable gespeichert

            // data wird vor dem Auruf von SetData von der P/Invoke-Engine "gepinned"
            Console.WriteLine(SetData(data));
            Console.WriteLine(DoCalc());  // hier könnten die Daten schon weg sein, weil nach dem Aufruf von SetData() data "unpinned" wurde
            Console.WriteLine("Press ENTER to GC.Collect");
            Console.ReadLine();
            GC.Collect();   // jetzt sind die Daten sicher verschoben worden
            Console.WriteLine("Trying DoWork again");
            Console.WriteLine(DoCalc());   // ohne manuellem pinning verwendet DoCalc nun einen falschen Speicherbereich - d.h. die Daten sind nicht mehr da und wurden von etwas anderem überschrieben
#if DO_PIN
			h.Free();
#endif
        }
    }
}
