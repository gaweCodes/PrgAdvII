using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using Reflection.PluginInterface;

namespace Reflection.Reflection2 
{
    internal static class Program 
    {
        private static void Main() 
        {
            var files = Directory.GetFiles(@".\plugins\", "*.dll");

            foreach (var file in files.Select(Path.GetFullPath)) 
            {
                try 
                {
                    var assembly = Assembly.LoadFile(file);
                    foreach (var t in assembly.GetTypes().Where(t => t != typeof(IPlugin) && typeof(IPlugin).IsAssignableFrom(t))) 
                    {
                        var plugin = (IPlugin) Activator.CreateInstance(t);
                        var ret = plugin.Execute();
                        Console.WriteLine($"Plugin {plugin.Name} {(ret ? "successfully executed" : "failed")}");
                    }
                } catch (Win32Exception) {
                } catch (ArgumentException) {
                } catch (FileNotFoundException) {
                } catch (PathTooLongException) {
                } catch (BadImageFormatException) {
                } catch (SecurityException) {
                }
            }
            Console.ReadLine();
        }
    }
}
