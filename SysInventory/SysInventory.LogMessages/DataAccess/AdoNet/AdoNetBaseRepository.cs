using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using SysInventory.LogMessages.Properties;

namespace SysInventory.LogMessages.DataAccess.AdoNet
{
    internal abstract class AdoNetBaseRepository<T> : IRepositoryBase<T>
    {
        protected readonly List<T> LoadedObjects;
        public string TableName { get; }
        protected string ConnectionString { get; }
        protected AdoNetBaseRepository(string tableName)
        {
            ConnectionString = Settings.Default.ConnectionString;
            TableName = tableName;
            LoadedObjects = new List<T>();
        }
        public abstract T GetSingle<TKey>(TKey pkValue);
        public abstract void Add(T entity);
        public abstract void Delete(T entity);
        public abstract void Update(T entity);
        public abstract List<T> GetAll(string whereCondition, Dictionary<string, object> parameterValues);

        public abstract List<T> GetAll();
        public abstract long Count(string whereCondition, Dictionary<string, object> parameterValues);
        public long Count()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select count(*) from " + TableName;
                    if (!long.TryParse(cmd.ExecuteScalar().ToString(), out var numberOfItems))
                        MessageBox.Show("Couldn't count the data of table " + TableName);
                    return numberOfItems;
                }
            }
        }
    }
}
