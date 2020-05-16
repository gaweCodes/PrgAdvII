using SysInventory.LogMessages.DataAccess;

namespace SysInventory.LogMessages.ViewModels
{
    internal abstract class BaseViewModel<T>
    {
        protected IRepositoryBase<T> DataRepository { get; set; }
        public IRelayCommand SaveCurrentItemCommand { get; set; }
    }
}
