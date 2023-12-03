using System.Collections.Generic;
using System.Threading.Tasks;
using FootballStats.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.Database;

public class PlayerRepository
{
    public async Task<List<PlayerModel>> GetAllPlayersAsync()
    {
        await using var dbContext = new MyDbContext();
        var players = await dbContext.Players.ToListAsync();
        return players;
    }

    protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConfigHandler.ConnectionString);
    }
}