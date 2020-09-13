using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimplePInvoke {
    class Program {
        /*
         * user32.dll ist in Windows\System32
         * 
         * Before the system searches for a DLL, it checks the following:
         *     If a DLL with the same module name is already loaded in memory, the system uses the loaded DLL, no matter which directory it is in. The system does not search for the DLL.
         *     If the DLL is on the list of known DLLs for the version of Windows on which the application is running, the system uses its copy of the known DLL (and the known DLL's dependent DLLs, if any). The system does not search for the DLL.
         *
         * SetDllDirectory ermöglicht die Spezifikation weiterer Pfade:
         * http://msdn.microsoft.com/en-us/library/ms686203(VS.85).aspx
         */
        [DllImport("user32")]
        extern static bool MessageBeep(uint sound);          // https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-messagebeep


        static void Main(string[] args) {
            // Sound gemäss Konfiguration in Systemsteuerung
            MessageBeep(0x30);
            //MessageBeep(0x10);
            //MessageBeep(0x0);
        }
    }
}
