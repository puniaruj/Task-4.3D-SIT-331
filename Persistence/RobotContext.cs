using Microsoft.EntityFrameworkCore;
using MoonRobotSimulation.Models;

namespace MoonRobotSimulation.Persistence
{
    public class RobotContext : DbContext
    {
        public RobotContext(DbContextOptions<RobotContext> options)
            : base(options) { }

        public DbSet<Map> Maps { get; set; } = null!;
        public DbSet<RobotCommand> RobotCommands { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Map>().ToTable("map");
            modelBuilder.Entity<Map>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Columns).HasColumnName("columns");
                entity.Property(e => e.Rows).HasColumnName("rows");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.CreatedDate).HasColumnName("createddate");
                entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");
            });

            modelBuilder.Entity<RobotCommand>().ToTable("robotcommand");
            modelBuilder.Entity<RobotCommand>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.IsMoveCommand).HasColumnName("ismovecommand");
                entity.Property(e => e.CreatedDate).HasColumnName("createddate");
                entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");
                entity.Property(e => e.Description).HasColumnName("description");
            });
        }
    }
}
