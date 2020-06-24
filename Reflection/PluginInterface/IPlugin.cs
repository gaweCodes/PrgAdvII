namespace Reflection.PluginInterface 
{
    public interface IPlugin 
    {
        string Name { get; }
        bool Execute();
    }
}
