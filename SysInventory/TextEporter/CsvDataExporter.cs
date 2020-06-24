using System.Collections.Generic;
using PluginInterfaces;

namespace TextEporter
{
    public class CsvDataExporter : IDataExportPlugin
    {
        public string Name { get; } = "CSV";
        public void Export<T>(IEnumerable<T> data, string destinationPath)
        {
            // TODO
        }
    }
}
