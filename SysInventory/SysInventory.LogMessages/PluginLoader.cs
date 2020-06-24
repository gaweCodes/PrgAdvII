using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Windows;
using PluginInterfaces;

namespace SysInventory.LogMessages
{
    public class PluginLoader
    {
        private const string AssembliesFolder = @".\plugins\";
        private List<IDataExportPlugin> Exporters { get; }
        public PluginLoader() => Exporters = new List<IDataExportPlugin>();
        public List<string> LoadExporters()
        {
            var files = Directory.GetFiles(AssembliesFolder, "*.dll");
            try
            {
                foreach (var file in files.Select(Path.GetFullPath))
                {
                    var assembly = Assembly.LoadFile(file);
                    foreach (var t in assembly.GetTypes().Where(t =>
                        t != typeof(IDataExportPlugin) && typeof(IDataExportPlugin).IsAssignableFrom(t)))
                    {
                        var plugin = (IDataExportPlugin) Activator.CreateInstance(t);
                        Exporters.Add(plugin);
                        MessageBox.Show(plugin.Name);
                    }
                }
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BadImageFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Exporters.Select(e => e.Name).ToList();
        }
        public void ExportData<T>(List<string> selectedExporterNames, List<T> data, string exportPath)
        {
            selectedExporterNames
                .SelectMany(selectedExporterName => Exporters.Where(x => x.Name == selectedExporterName)).ToList()
                .ForEach(x => x.Export(data, exportPath));
        }
    }
}
