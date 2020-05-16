using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.AdoNet
{
    internal class LogRepository : AdoNetBaseRepository<LogEntry>
    {
        private static readonly string _tableName = "Log";
        public LogRepository() : base(_tableName, $"SELECT L.LogId, P.Name, Loc.Name, D.Hostname, L.Severity, L.CreatedAt, L.Message FROM {_tableName} AS L INNER JOIN dbo.Device AS D ON L.DeviceFk = D.DeviceId INNER JOIN dbo.Location AS Loc ON Loc.LocationId = D.LocationFk INNER JOIN dbo.PoD AS P ON P.PodId = Loc.PodFk") { } public override LogEntry GetSingle<TKey>(TKey pkValue)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT L.LogId, P.Name, Loc.Name, D.Hostname, L.Severity, L.CreatedAt, L.Message FROM dbo.[{TableName}] AS L INNER JOIN dbo.Device AS D ON L.DeviceFk = D.DeviceId INNER JOIN dbo.Location AS Loc ON Loc.LocationId = D.LocationFk INNER JOIN dbo.PoD AS P ON P.PodId = Loc.PodFk where L.LogId = '{pkValue}'";
                    using (var reader = cmd.ExecuteReader()) return reader.Read() ? BuildLogEntry(reader) : null;
                }
            }
        }
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
                    var result = cmd.ExecuteNonQuery();
                    if (result == -1) MessageBox.Show("The device or pod could not be found");
                }
            }
        }
        public override void Delete(LogEntry entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"delete from {TableName} where Logid = '{entity.Id}'";
                    cmd.ExecuteNonQuery();
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
        public override List<LogEntry> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
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
                        return new List<LogEntry>();
                    }
                }
            }
            return LoadedObjects;
        }
        public override List<LogEntry> GetAll()
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
            return LoadedObjects;
        }
        public override long Count(string whereCondition, Dictionary<string, object> parameterValues)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT count(*) FROM {TableName} AS L INNER JOIN dbo.Device AS D ON L.DeviceFk = D.DeviceId INNER JOIN dbo.Location AS Loc ON Loc.LocationId = D.LocationFk INNER JOIN dbo.PoD AS P ON P.PodId = Loc.PodFk WHERE {whereCondition}";
                    foreach (var keyValuePair in parameterValues) cmd.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                    long numberOfEntries;
                    try
                    {
                        long.TryParse(cmd.ExecuteScalar().ToString(), out numberOfEntries);
                    }
                    catch(Exception ex)
                    {
                        numberOfEntries = -1;
                        MessageBox.Show(ex.Message);
                    }
                    return numberOfEntries;
                }
            }
        }
        protected override void AddParameters(SqlCommand cmd, LogEntry entity)
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
            if (parameterValues.Count == 0 && string.IsNullOrWhiteSpace(whereCondition)) 
                cmd.CommandText = "select id, pod, location, hostname, severity, timestamp, message from v_logentries order by timestamp";
            else
            {
                cmd.CommandText = $"{SelectBase} WHERE {whereCondition}";
                foreach (var keyValuePair in parameterValues) 
                    cmd.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}
