using FootballStats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.Database;

public class PlayerStatRepository
{
    public async Task<List<PlayerStatModel>> GetPlayerStatsAsync(int playerId)
    {
        await using var dbContext = new MyDbContext();
        var playerStats = dbContext.PlayerStats.Where(ps => ps.PlayerId == playerId).ToList();
        return playerStats;
    }

    protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConfigHandler.ConnectionString);
    }
}