//#define WithException

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace _03a_ErrorHandling {
    class Program {
        const int SYNCHRONIZE = 0x00100000;

#if WithException
        [DllImport("kernel32", EntryPoint = "OpenThread", SetLastError = true)]
        static extern IntPtr OpenThreadInternal(uint access, bool inheritHandle, int id);

        static IntPtr OpenThread(uint access, int id) {
            var handle = OpenThreadInternal(access, false, id);
            if (handle != IntPtr.Zero) {
                return handle;
            }

            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return IntPtr.Zero;
        }


        static void Main(string[] args) {
            Console.Write("Enter thread ID: ");
            int id = int.Parse(Console.ReadLine());
            try {
                var hThread = OpenThread(SYNCHRONIZE, id);
                Console.WriteLine("Opened handle successfully.");
            } catch (Exception ex) {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }
#else
        [DllImport("kernel32", SetLastError = true)]
        static extern IntPtr OpenThread(uint access, bool inheritHandle, int id);


        static void Main(string[] args) {
            Console.Write("Enter thread ID: ");
            int id = int.Parse(Console.ReadLine());
            var hThread = OpenThread(SYNCHRONIZE, false, id);
            if (hThread != IntPtr.Zero) {
                Console.WriteLine("Opened handle successfully.");
            } else {
                Console.WriteLine("Error: {0}", Marshal.GetLastWin32Error());    // Tools -> ErrorLookup für Fehlertext
            }
        }
#endif
    }
}
