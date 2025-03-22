using FastMember;
using Npgsql;

namespace MoonRobotSimulation.Persistence
{
    public static class ExtensionMethods
    {
        public static void MapTo<T>(this NpgsqlDataReader reader, T entity) where T : class, new()
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var members = accessor.GetMembers().Select(m => m.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string columnName = reader.GetName(i);
                if (members.Contains(columnName))
                {
                    var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    accessor[entity, columnName] = value;
                }
            }
        }
    }
}
