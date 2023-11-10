using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Database;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration configuration;

    // Models
    public DbSet<User> Users { get; set; }

    public DatabaseContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnectionString"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
