using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;

namespace _07_IDispatch {
    class Program {
        static void Main(string[] args) {
            //Shell();

            // ohne dynamic könnte object verwendet werden. Anschliessend wäre Reflection nötig, um die Properties, Methoden aufzurufen.

            //var t = Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046"));
            var t = Type.GetTypeFromProgID("Excel.Application");
            dynamic xl = Activator.CreateInstance(t);   // nun ist Excel als Prozess gestartet.
            dynamic books = xl.Workbooks;
            dynamic book = books.Add();
            dynamic sheets = book.Worksheets;
            dynamic sheet = sheets[1];   // 1-basiert

            sheet.Cells["C1"].Formula = "=SUM(B2:B4)";
            dynamic range = sheet.Range["C1"];
            range.Formula = "=SUM(B2:B4)";
            range = sheet.Range["B2"];
            range.Value = 5;
            range = sheet.Range["B3"];
            range.Value = 10;
            range = sheet.Range["B4"];
            range.Value = 15;

            range = sheet.Range["C1"];
            var v = range.Value;
            Console.WriteLine(v);

            book.SaveAs("c:\\temp\\test.xlsx");

            xl.Quit();
        }

        private static void Shell() {
            // Windows Script Host Object Model
            // CoClass FileSystemObject, Interface IFileSystem3
            dynamic shell = Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("0D43FE01-F093-11CF-8940-00A0C9054228")));
            foreach (var drive in shell.Drives) {
                Console.Write("{0}", drive.Path);
                if (drive.IsReady) {
                    Console.WriteLine(" Free Space: {0} MB", (long)drive.FreeSpace >> 20);   // Shift 20 Bits nach rechts ergibt - 1024 = 0100 0000 0000, 1024 x 1024 = 0001 0000 0000 0000 0000 0000
                } else {
                    Console.WriteLine(" (Not Ready)");
                }
            }
        }
    }
}
