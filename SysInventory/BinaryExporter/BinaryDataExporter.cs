using System.Collections.Generic;
using PluginInterfaces;

namespace BinaryExporter
{
    public class BinaryDataExporter : IDataExportPlugin
    {
        public string Name { get; } = "Binary";
        public void Export<T>(IEnumerable<T> data, string destinationPath)
        {
            // TODO
        }
    }
}
