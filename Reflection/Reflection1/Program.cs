using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Reflection.Reflection1 
{
    internal static class Program {
        static void Main(string[] args) {
            if (args.Length < 1)
                return;
            var path = args[0];
            if (!File.Exists(path)) return;

            var assembly = Assembly.LoadFile(path);
            Console.WriteLine(assembly.FullName);
            foreach (var module in assembly.GetModules(true))
            {
                Console.WriteLine($"    Modul: {module.Name}");
                foreach (var methodInfo in module.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    Console.WriteLine($"    Method: {methodInfo.Name}");

                foreach (var fieldInfo in module.GetFields(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
                    Console.WriteLine($"    Field: {fieldInfo.Name}");

                foreach (var type in module.GetTypes())
                {
                    Console.WriteLine($"    Class: {type.Name}");
                    foreach (var propertyInfo in type.GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                        Console.WriteLine($"        Property: {propertyInfo.Name}");
                    
                    foreach (var fieldInfo in type.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                        Console.WriteLine($"        Property: {fieldInfo.Name}");

                    foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    {
                        var parameter = string.Join(", ", methodInfo.GetParameters().Select(x => x.ParameterType.Name + " " + x.Name));
                        Console.WriteLine($"        Method: {methodInfo.Name}({parameter})");
                    }

                    foreach (var constructorInfo in type.GetConstructors(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    {
                        var parameter = string.Join(", ", constructorInfo.GetParameters().Select(x => x.ParameterType.Name + " " + x.Name));
                        Console.WriteLine($"        Constructor: {constructorInfo.Name}({parameter})");
                    }

                    foreach (var eventInfo in type.GetEvents(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                        Console.WriteLine($"        Event: {eventInfo.Name}");
                }
            }
            Console.ReadLine();
        }
    }
}
