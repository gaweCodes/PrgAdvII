using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.ViewModels
{
    internal abstract class BaseViewModel<T> where T : IIdentifiable
    {
        protected IRepositoryBase<T> DataRepository { get; set; }
        public IRelayCommand SaveCurrentItemCommand { get; set; }
    }
}
