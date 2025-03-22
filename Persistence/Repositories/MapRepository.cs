using MoonRobotSimulation.Models;
using MoonRobotSimulation.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Npgsql;

namespace MoonRobotSimulation.Persistence.Repositories
{
    public class MapRepository : IMapDataAccess
    {
        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=mypassword;Database=postgres;Port=5433";

        public List<Map> GetMaps()
        {
            var maps = new List<Map>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM map", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                maps.Add(new Map(
                    (int)reader["id"],
                    (int)reader["columns"],
                    (int)reader["rows"],
                    (DateTime)reader["createddate"],
                    (DateTime)reader["modifieddate"],
                    reader["description"] as string
                ));
            }
            return maps;
        }

        public Map? GetMapById(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM map WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? new Map(
                (int)reader["id"],
                (int)reader["columns"],
                (int)reader["rows"],
                (DateTime)reader["createddate"],
                (DateTime)reader["modifieddate"],
                reader["description"] as string
            ) : null;
        }

        public void AddMap(Map map)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("INSERT INTO map (columns, rows, createddate, modifieddate, description) VALUES (@columns, @rows, @createddate, @modifieddate, @description)", conn);
            cmd.Parameters.AddWithValue("@columns", map.Columns);
            cmd.Parameters.AddWithValue("@rows", map.Rows);
            cmd.Parameters.AddWithValue("@createddate", map.CreatedDate);
            cmd.Parameters.AddWithValue("@modifieddate", map.ModifiedDate);
            cmd.Parameters.AddWithValue("@description", map.Description ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public bool UpdateMap(int id, Map map)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("UPDATE map SET columns = @columns, rows = @rows, modifieddate = @modifieddate, description = @description WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@columns", map.Columns);
            cmd.Parameters.AddWithValue("@rows", map.Rows);
            cmd.Parameters.AddWithValue("@modifieddate", map.ModifiedDate);
            cmd.Parameters.AddWithValue("@description", map.Description ?? (object)DBNull.Value);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteMap(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("DELETE FROM map WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
