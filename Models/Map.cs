namespace MoonRobotSimulation.Models
{
    public class Map
    {
        public int Id { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Map(int id, int columns, int rows, DateTime createdDate, DateTime modifiedDate, string? description = null)
        {
            Id = id;
            Columns = columns;
            Rows = rows;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            Description = description;
        }

        public Map() { }
    }
}
