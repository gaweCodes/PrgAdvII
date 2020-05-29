using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using SysInventory.LogMessages.Models;
using SysInventory.LogMessages.Properties;

namespace SysInventory.LogMessages.DataAccess.AdoNet
{
    internal abstract class AdoNetBaseRepository<T> : IRepositoryBase<T> where T : IIdentifiable
    {
        protected readonly List<T> LoadedObjects;
        protected abstract string SqlIdField { get; }
        public abstract string TableName { get; }
        protected string ConnectionString { get; }
        protected AdoNetBaseRepository()
        {
            ConnectionString = Settings.Default.ConnectionString;
            LoadedObjects = new List<T>();
        }
        public abstract void Add(T entity);
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
        public abstract long Count(string whereCondition, Dictionary<string, object> parameterValues);
        public void Delete(T entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"delete from {TableName} where {SqlIdField} = '{entity.Id}'";
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured while deleting the location: " + ex.Message);
                    }
                }
            }
        }
        public abstract IQueryable<T> GetAll();
        public abstract IQueryable<T> GetAll(string whereCondition, Dictionary<string, object> parameterValues);
        public abstract T GetSingle<TKey>(TKey pkValue);
        public abstract void Update(T entity);
    }
}
