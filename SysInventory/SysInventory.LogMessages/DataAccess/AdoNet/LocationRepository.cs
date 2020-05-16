using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.AdoNet
{
    internal class LocationRepository : AdoNetBaseRepository<Location>
    {
        private readonly string _selectBase;
        private readonly string _orderByBase = "ORDER BY Location.Name";
        public LocationRepository() : base("Location") => _selectBase =
            $"select LocationId, Location.Name, PoD.Name, PodId, ParentId from {TableName} INNER JOIN PoD on (PoDFk = PodId)";
        public override Location GetSingle<TKey>(TKey pkValue)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"{_selectBase} WHERE LocationId = '{pkValue}'";
                    using (var reader = cmd.ExecuteReader()) return reader.Read() ? BuildLocation(reader) : null;
                }
            }
        }
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
        public override void Delete(Location entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"delete from {TableName} where Locationid = '{entity.Id}'";
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
        public override void Update(Location entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = entity.ParentId.HasValue
                        ? $"UPDATE {TableName} SET Name = @Name, Parentid = @ParentId, PodFk = @PoDId WHERE LocationId = '{entity.Id}'"
                        : $"UPDATE {TableName} sET Name = @Name, PodFk = @PoDId)";
                    AddParameters(cmd, entity);
                    var result = cmd.ExecuteNonQuery();
                    if (result == -1) MessageBox.Show("The given pod doens't exist");
                }
            }
        }
        public override List<Location> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    if (parameterValues.Count == 0 && string.IsNullOrWhiteSpace(whereCondition)) cmd.CommandText = "select id, pod, location, hostname, severity, timestamp, message from v_logentries order by timestamp";
                    else
                    {
                        cmd.CommandText = $"{_selectBase} WHERE {whereCondition} {_orderByBase}";
                        foreach (var keyValuePair in parameterValues) cmd.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                    }
                    try
                    {
                        using (var reader = cmd.ExecuteReader()) PopulateLoadedObjectList(reader);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return new List<Location>();
                    }
                }
            }
            return LoadedObjects;
        }
        public override List<Location> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"{_selectBase} {_orderByBase}";
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
                    cmd.CommandText = $"SELECT count(*) FROM {TableName} WHERE {whereCondition}";
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
                PoD = reader.GetString(2),
                PoDId = reader.GetGuid(3)
            };
            if (!reader.IsDBNull(4)) location.ParentId = reader.GetGuid(4);
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
