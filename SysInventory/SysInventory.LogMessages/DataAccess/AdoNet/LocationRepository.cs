using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.AdoNet
{
    internal class LocationRepository : AdoNetBaseRepository<Location>
    {
        private static readonly string _orderByBase = "ORDER BY Name";
        public override string TableName { get; protected set; } = "Location";
        protected override string SqlIdField { get; set; } = "LocationId";
        protected virtual string SelectBase { get; } = "select LocationId, Name, PodFk, ParentId from Location";
        public override void Add(Location entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = entity.ParentId.HasValue
                        ? $"INSERT INTO {TableName} (Name, ParentId, PodFk) VALUES (@Name, @ParentId, @PoDId)"
                        : $"INSERT INTO {TableName} (Name, PodFk) VALUES (@Name, @PoDId)";
                    AddParameters(cmd, entity);
                    var result = cmd.ExecuteNonQuery();
                    if (result == -1) MessageBox.Show("The given pod doens't exist");
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
        public override IQueryable<Location> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"{SelectBase} {_orderByBase}";
                    using (var reader = cmd.ExecuteReader()) PopulateLoadedObjectList(reader);
                }
            }
            return LoadedObjects.AsQueryable();
        }
        public override IQueryable<Location> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    if (parameterValues.Count == 0 && string.IsNullOrWhiteSpace(whereCondition)) cmd.CommandText = "select id, pod, location, hostname, severity, timestamp, message from v_logentries order by timestamp";
                    else
                    {
                        cmd.CommandText = $"{SelectBase} WHERE {whereCondition} {_orderByBase}";
                        foreach (var keyValuePair in parameterValues) cmd.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                    }
                    try
                    {
                        using (var reader = cmd.ExecuteReader()) PopulateLoadedObjectList(reader);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return new List<Location>().AsQueryable();
                    }
                }
            }
            return LoadedObjects.AsQueryable();
        }
        public override Location GetSingle<TKey>(TKey pkValue)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"{SelectBase} WHERE LocationId = '{pkValue}'";
                    using (var reader = cmd.ExecuteReader()) return reader.Read() ? BuildLocation(reader) : null;
                }
            }
        }
        public override void Update(Location entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = entity.ParentId.HasValue
                        ? $"UPDATE {TableName} SET Name = @Name, Parentid = @ParentId, PodFk = @PoDId"
                        : $"UPDATE {TableName} SET Name = @Name, PodFk = @PoDId";
                    cmd.CommandText += $" WHERE LocationId = '{entity.Id}'";
                    AddParameters(cmd, entity);
                    var result = cmd.ExecuteNonQuery();
                    if (result == -1) MessageBox.Show("The given pod doens't exist");
                }
            }
        }
        private void PopulateLoadedObjectList(IDataReader reader)
        {
            LoadedObjects.Clear();
            while (reader.Read()) LoadedObjects.Add(BuildLocation(reader));
        }
        private static Location BuildLocation(IDataRecord reader)
        {
            var location = new Location
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                PoDId = reader.GetGuid(2)
            };
            if (!reader.IsDBNull(3)) location.ParentId = reader.GetGuid(3);
            return location;
        }
        private static void AddParameters(SqlCommand cmd, Location entity)
        {
            cmd.Parameters.AddWithValue("Name", entity.Name);
            cmd.Parameters.AddWithValue("PoDId", entity.PoDId);
            if (entity.ParentId.HasValue) cmd.Parameters.AddWithValue("ParentId", entity.ParentId);
        }
    }
}
