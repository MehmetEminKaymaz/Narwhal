using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace narwhalLang.Domain
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=narwhalDB;Username=narwhal;Password=narwhal");

        public DbSet<NarwhalNode> Nodes { get; set; }
        public DbSet<NarwhalProperty> Properties { get; set; }
        public DbSet<NarwhalReadOnlyProperty> PropertiesReadOnly { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Repository> Repository { get; set; }
    }
}
