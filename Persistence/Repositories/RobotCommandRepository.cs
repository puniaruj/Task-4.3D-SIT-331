using MoonRobotSimulation.Models;
using MoonRobotSimulation.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Npgsql;

namespace MoonRobotSimulation.Persistence.Repositories
{
    public class RobotCommandRepository : IRobotCommandDataAccess
    {
        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=mypassword;Database=postgres;Port=5433";

        public List<RobotCommand> GetRobotCommands()
        {
            var commands = new List<RobotCommand>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                commands.Add(new RobotCommand(
                    (int)reader["id"],
                    (string)reader["name"],
                    (bool)reader["ismovecommand"],
                    (DateTime)reader["createddate"],
                    (DateTime)reader["modifieddate"],
                    reader["description"] as string
                ));
            }
            return commands;
        }

        public RobotCommand? GetRobotCommandById(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? new RobotCommand(
                (int)reader["id"],
                (string)reader["name"],
                (bool)reader["ismovecommand"],
                (DateTime)reader["createddate"],
                (DateTime)reader["modifieddate"],
                reader["description"] as string
            ) : null;
        }

        public void AddRobotCommand(RobotCommand command)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("INSERT INTO robotcommand (name, ismovecommand, createddate, modifieddate, description) VALUES (@name, @ismovecommand, @createddate, @modifieddate, @description)", conn);
            cmd.Parameters.AddWithValue("@name", command.Name);
            cmd.Parameters.AddWithValue("@ismovecommand", command.IsMoveCommand);
            cmd.Parameters.AddWithValue("@createddate", command.CreatedDate);
            cmd.Parameters.AddWithValue("@modifieddate", command.ModifiedDate);
            cmd.Parameters.AddWithValue("@description", command.Description ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public bool UpdateRobotCommand(int id, RobotCommand command)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("UPDATE robotcommand SET name = @name, ismovecommand = @ismovecommand, modifieddate = @modifieddate, description = @description WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", command.Name);
            cmd.Parameters.AddWithValue("@ismovecommand", command.IsMoveCommand);
            cmd.Parameters.AddWithValue("@modifieddate", command.ModifiedDate);
            cmd.Parameters.AddWithValue("@description", command.Description ?? (object)DBNull.Value);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteRobotCommand(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("DELETE FROM robotcommand WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<RobotCommand> GetMoveCommands()
        {
            var commands = new List<RobotCommand>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand WHERE ismovecommand = TRUE", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                commands.Add(new RobotCommand(
                    (int)reader["id"],
                    (string)reader["name"],
                    (bool)reader["ismovecommand"],
                    (DateTime)reader["createddate"],
                    (DateTime)reader["modifieddate"],
                    reader["description"] as string
                ));
            }
            return commands;
        }
    }
}
