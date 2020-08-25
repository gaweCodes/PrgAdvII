using System.Collections.Generic;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.ViewModels
{
    public abstract class BaseViewModel<T> where T : IIdentifiable
    {
        protected IRepositoryBase<T> DataRepository { get; set; }
        public IRelayCommand SaveCurrentItemCommand { get; set; }
        public List<string> Stratgies { get; } = new List<string> { "AdoNet", "LINQ", "EF" };
        protected RepositoryFactory Factory = null;
    }
}
