using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _02_PInvoke {
    class Program {
        // *** .NET stellt keine Möglichkeit zur Verfügung, zu prüfen, ob es sich bei einem Prozess um einen 32bit oder 64bit Prozess handelt


        // WoW64 = Windows on Windows 64 - emuliert 32bit-Windows auf einer 64Bit-Umgebung
        // https://docs.microsoft.com/en-us/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process
        [DllImport("kernel32")]
        static extern bool IsWow64Process(IntPtr hProcess, out bool isWow64);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724340(v=vs.85).aspx
        [DllImport("kernel32", SetLastError = true)]
        static extern void GetNativeSystemInfo(out SystemInfo si);

        // der Name des Structs ist irrelevant
        // WORD = 16bit
        // DWORD = 32bit
        [StructLayout(LayoutKind.Sequential)]
        struct SystemInfo {
            public short wProcessorArchitecture;
            public short wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public short wProcessorLevel;
            public short wProcessorRevision;
        }

        static bool Is32BitProcess(Process process) {
            SystemInfo si;
            GetNativeSystemInfo(out si);
            if(si.wProcessorArchitecture == 9) {
                bool isWow64;
                IsWow64Process(process.Handle, out isWow64);
                return isWow64;
            }
            return si.wProcessorArchitecture == 0;
        }

        static void Main(string[] args) {
            // Explorer ist bspw. ein 64-bit Prozess
            while (true) {
                try {
                    Console.Write("Enter process ID: ");
                    int pid = int.Parse(Console.ReadLine());
                    var process = Process.GetProcessById(pid);
                    bool is32bit = Is32BitProcess(process);
                    Console.WriteLine("Process {0} ({1}) is 32 bit: {2}", process.ProcessName, pid, is32bit);
                } catch {
                    break;
                }
            }
        }
    }
}
