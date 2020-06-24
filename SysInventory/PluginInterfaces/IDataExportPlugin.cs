using System.Collections.Generic;

namespace PluginInterfaces
{
    /// <summary>
    /// Plugin-Interface für DataExporter
    /// </summary>
    public interface IDataExportPlugin
    {
        /// <summary>
        /// Name des DataExporters
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Ausführung des Export.
        /// </summary>
        /// <param name="data">Collection, die exportiert werden soll</param>
        /// <param name="destinationPath">Zielpfad des Exports
        /// (Verzeichnis und Dateiname)</param>
        void Export<T>(IEnumerable<T> data, string destinationPath);
    }
}
