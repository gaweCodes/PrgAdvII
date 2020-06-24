using System.Collections.Generic;
using PluginInterfaces;

namespace TextEporter
{
    public class JsonExporter : IDataExportPlugin
    {
        public string Name { get; } = "JSON";
        public void Export<T>(IEnumerable<T> data, string destinationPath)
        {
            // TODO
        }
    }
}
