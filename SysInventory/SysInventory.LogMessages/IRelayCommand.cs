using System.Windows.Input;

namespace SysInventory.LogMessages
{
    public interface IRelayCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
