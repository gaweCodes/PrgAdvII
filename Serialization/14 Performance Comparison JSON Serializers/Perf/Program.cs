using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

namespace Perf {
    static class Program {
        static void Main(string[] args) {
            JS();
            Console.WriteLine();
            DJS();
            Console.WriteLine();
            JNET();

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void JS() {
            var root = InstantiateRoot();
            var ser = new JavaScriptSerializer();

            var sw = new Stopwatch();
            sw.Start();
            ser.MaxJsonLength = int.MaxValue;
            var result = ser.Serialize(root);

            sw.Stop();

            Console.WriteLine($"JS Serialization: {sw.Elapsed.Seconds}s");

            sw.Start();
            var root1 = ser.Deserialize<Root>(result);
            sw.Stop();
            Console.WriteLine($"JS Deserialization: {sw.Elapsed.Seconds}s");

        }

        static void DJS() {
            var root = InstantiateRoot();
            var ms = new MemoryStream();

            var sw = new Stopwatch();
            sw.Start();
            var ser = new DataContractJsonSerializer(typeof(Root));
            ser.WriteObject(ms, root);
            sw.Stop();
            Console.WriteLine($"DJS Serialization: {sw.Elapsed.Seconds}s");

            ms.Position = 0;

            sw.Start();
            var root1 = (Root) ser.ReadObject(ms);
            sw.Stop();
            Console.WriteLine($"DJS Deserialization: {sw.Elapsed.Seconds}s");
        }

        static void JNET() {
            var root = InstantiateRoot();

            var sw = new Stopwatch();
            sw.Start();

            var json = JsonConvert.SerializeObject(root);
            sw.Stop();
            Console.WriteLine($"JNET Serialization: {sw.Elapsed.Seconds}s");

            sw.Start();
            var root1 = JsonConvert.DeserializeObject<Root>(json);
            sw.Stop();
            Console.WriteLine($"JNET Deserialization: {sw.Elapsed.Seconds}s");

        }

        static Root InstantiateRoot() {
            var Sub2List = new List<Sub2>();
            for (var i = 0; i < 1000; i++) {
                Sub2List.Add(
                    new Sub2 {
                        IsOk = false,
                        CreatedAt = DateTime.Now,
                        Name = Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n")
                    });
            }

            var Sub1List = new List<Sub1>();
            var tbl = new Hashtable();
            for (var i = 0; i < 1200; i++) {
                tbl.Add("key" + i.ToString(), Guid.NewGuid().ToString("n"));
            }

            for (var i = 0; i < 1200; i++) {
                Sub1List.Add(
                    new Sub1 {
                        Id = 1,
                        CreatedAt = DateTime.Now,
                        Tbl = tbl,
                        Sub2List = Sub2List
                    });
            }

            return new Root {Name = "MyRoot", Sub1List = Sub1List};

        }
    }
}