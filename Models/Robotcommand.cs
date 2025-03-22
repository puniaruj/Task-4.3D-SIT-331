namespace MoonRobotSimulation.Models
{
    public class RobotCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // ✅ Fix: Ensures Name is never null
        public bool IsMoveCommand { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? Description { get; set; }

        public RobotCommand(int id, string name, bool isMoveCommand, DateTime createdDate, DateTime modifiedDate, string? description = null)
        {
            Id = id;
            Name = name;
            IsMoveCommand = isMoveCommand;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            Description = description;
        }

        public RobotCommand() { }
    }
}
