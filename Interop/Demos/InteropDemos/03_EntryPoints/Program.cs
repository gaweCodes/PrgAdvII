using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _03_EntryPoints {
    class Program {

        [DllImport("kernel32", EntryPoint = "Sleep")]
        static extern void DoNothing(uint msec);

        // btw: mit CreateJobObject kann ein Job erzeugt werden. 
        //      Grundsätzlich: Operationen auf einem JobObject betreffen alle zugehörigen Prozesse. z.B.
        //                     - Priorität ändern
        //                     - Prozesse beenden
        [DllImport("kernel32", ExactSpelling = true)]
        static extern IntPtr CreateJobObjectW(IntPtr securityAttributes, string name);

        [DllImport("kernel32")]
        static extern bool CloseHandle(IntPtr handle);

        static void Main(string[] args) {
            try {
                Console.WriteLine("waiting for a while...");
                DoNothing(2000);
                var newJob = CreateJobObjectW(IntPtr.Zero, "myjob");
                Console.WriteLine("Job handle: {0}", newJob);
                CloseHandle(newJob);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
