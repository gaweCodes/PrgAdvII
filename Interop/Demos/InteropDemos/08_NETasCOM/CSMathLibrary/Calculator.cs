using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSMathLibrary {
    /*
     * 1. Interface definieren (wir wissen nun, dass COM mit Interfaces arbeitet)
     *    Es wäre auch möglich, nur eine Klasse zu erstellen und der Compiler erstellt das Interface
     *    automatisch - jedoch: Kontrolle ist besser.
     * 2. InterfaceType definieren:
     *       InterfaceIsIUnknown:  Indicates that an interface is exposed to COM as an interface that is derived from IUnknown, which enables only early binding.
     *       InterfaceIsIDispatch: Indicates that an interface is exposed to COM as a dispinterface, which enables late binding only.
     *       InterfaceIsDual:      Indicates that the interface is exposed to COM as a dual interface, which enables both early and late binding. This is the default value.
     * 3. [ComVisible(true)] ist standard für Klassen
     * 4. AssemblyInfo:   [assembly: ComVisible(true)] sowie [assembly: Guid("93ddc74f-2adc-4846-8e4e-20be9f1669b0")]
     * 5. ProjectProperties -> Build -> Register for COM-Interop  =  Visual Studio führt autom. regasm.exe aus und erstellt eine tlb-Datei (diese kann mit OleView inspiziert werden)
     * 6. Nach Build sollte die CLSID 7F0739B7-2645-418C-B151-B159842C1BEB in der Registry unter HKEY_CLASSES_ROOT\Wow6432Node\CLSID eingetragen sein
     *     Unter InprocServer32 ist mscoree.dll registriert - dies ist die einzige COM-dll des .NET-Frameworks und ist verantwortlich mit Hilfe den weiteren Informationen die COM-Klasse (bzw. CCW) zur Verfügung zu stellen
     * 7. File .\CSMathLibrary\bin\Debug\CSMathLibrary.tlb enthält die TypeLibrary welche in OleView geöffnet werden kann
     *     Da gibt es nun die Interfaces ICalculator (von uns erstellt) sowie _Object (stellt die Standardmethoden von Object zur Verfügung)
     * 8. CPPMathClient verwendet CSMathLibrary
     */

    [Guid("BBD28338-F647-4A25-8EAB-94C7EABDDB93")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ICalculator {
		int Multiply(int a, int b);
	}

	[Guid("7F0739B7-2645-418C-B151-B159842C1BEB")]
	[ClassInterface(ClassInterfaceType.None)]    // Wir haben das Interface selber definiert - es soll kein Auto-Interface generiert werden
	public class Calculator : ICalculator {
		public int Multiply(int a, int b) {
			return a * b;
		}
	}
}
