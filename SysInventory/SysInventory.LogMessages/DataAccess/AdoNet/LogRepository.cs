using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.AdoNet
{
    internal class LogRepository : AdoNetBaseRepository<LogEntry>
    {
        public override string TableName { get; protected set; } = "v_LogEntries";
        protected override string SqlIdField { get; set; } = "id";
        protected virtual string SelectBase { get; } = "SELECT * FROM v_logEntries ";
        public override void Add(LogEntry entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "LogMessageAdd";
                    cmd.CommandType = CommandType.StoredProcedure;
                    AddParameters(cmd, entity);
                    var res = cmd.ExecuteNonQuery();
                    if(res == -1)
                        throw new ArgumentException("The given pod or device couldn't be found.");
                }
            }
        }
        public override long Count(string whereCondition, Dictionary<string, object> parameterValues)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT count(*) FROM {TableName} WHERE {whereCondition}";
                    foreach (var keyValuePair in parameterValues) cmd.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                    long numberOfEntries;
                    try
                    {
                        long.TryParse(cmd.ExecuteScalar().ToString(), out numberOfEntries);
                    }
                    catch (Exception ex)
                    {
                        numberOfEntries = -1;
                        MessageBox.Show(ex.Message);
                    }
                    return numberOfEntries;
                }
            }
        }
        public override void Delete(LogEntry entity)
        {
            TableName = "Log";
            SqlIdField = "LogId";
            base.Delete(entity);
        }
        public override IQueryable<LogEntry> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"{SelectBase}";
                    using (var reader = cmd.ExecuteReader()) PopulateLoadedObjectList(reader);
                }
            }
            return LoadedObjects.AsQueryable();
        }
        public override IQueryable<LogEntry> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    try
                    {
                        BuildCommand(cmd, whereCondition, parameterValues);
                        using (var reader = cmd.ExecuteReader()) PopulateLoadedObjectList(reader);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return new List<LogEntry>().AsQueryable();
                    }
                }
            }
            return LoadedObjects.AsQueryable();
        }
        public override LogEntry GetSingle<TKey>(TKey pkValue)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"{SelectBase} WHERE id = '{pkValue}'";
                    using (var reader = cmd.ExecuteReader()) return reader.Read() ? BuildLogEntry(reader) : null;
                }
            }
        }
        public override void Update(LogEntry entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "LogClear";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static void AddParameters(SqlCommand cmd, LogEntry entity)
        {
            cmd.Parameters.AddWithValue("@podName", entity.PoD);
            cmd.Parameters.AddWithValue("@hostname", entity.Hostname);
            cmd.Parameters.AddWithValue("@lvl", entity.Severity);
            cmd.Parameters.AddWithValue("@msg", entity.Message);
        }
        private void PopulateLoadedObjectList(IDataReader reader)
        {
            LoadedObjects.Clear();
            while (reader.Read()) LoadedObjects.Add(BuildLogEntry(reader));
        }
        private static LogEntry BuildLogEntry(IDataRecord reader) => new LogEntry
        {
            Id = Guid.Parse(reader.GetValue(0).ToString()),
            PoD = reader.GetValue(1).ToString(),
            Location = reader.GetValue(2).ToString(),
            Hostname = reader.GetValue(3).ToString(),
            Severity = int.Parse(reader.GetValue(4).ToString()),
            Timestamp = DateTime.Parse(reader.GetValue(5).ToString()),
            Message = reader.GetValue(6).ToString()
        };
        private void BuildCommand(SqlCommand cmd, string whereCondition, Dictionary<string, object> parameterValues)
        {
            cmd.CommandText = $"{SelectBase} WHERE {whereCondition}";
            foreach (var keyValuePair in parameterValues) 
                cmd.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
