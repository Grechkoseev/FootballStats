using Microsoft.EntityFrameworkCore;
using FootballStats.Models;

namespace FootballStats.Database;

public class MyDbContext : DbContext
{
    private readonly string _connectionString = ConfigHandler.ConnectionString; 
    public DbSet<PlayerModel> Players { get; set; }
    public DbSet<PlayerStatModel> PlayerStats { get; set; }
    public DbSet<SeasonModel> Seasons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}