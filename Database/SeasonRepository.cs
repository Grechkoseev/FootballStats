using System.Collections.Generic;
using System.Threading.Tasks;
using FootballStats.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.Database;

public class SeasonRepository
{
    public async Task<List<SeasonModel>> GetAllSeasonsAsync()
    {
        await using var dbContext = new MyDbContext();
        List<SeasonModel> seasons = await dbContext.Seasons.ToListAsync();
        return seasons;
    }

    protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConfigHandler.ConnectionString);
    }
}