using System.Collections.Generic;
using SysInventory.LogMessages.DataAccess;

namespace SysInventory.LogMessages.ViewModels
{
    internal abstract class BaseViewModel<T>
    {
        public string WhereCrit { get; set; }
        public string Values { get; set; }
        protected IRepositoryBase<T> DataRepository { get; set; }
        protected Dictionary<string, object> ParseSearchValues()
        {
            var queryParams = new Dictionary<string, object>();
            foreach (var keyValuePair in Values.Split(';'))
            {
                var keyValuePairParsed = keyValuePair.Split('|');
                if (keyValuePairParsed.Length == 2) queryParams.Add(keyValuePairParsed[0], keyValuePairParsed[1]);
                else return new Dictionary<string, object>();
            }
            return queryParams;
        }
    }
}
