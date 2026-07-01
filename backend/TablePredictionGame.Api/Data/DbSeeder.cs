using Microsoft.EntityFrameworkCore;
using TablePredictionGame.Api.Models;

namespace TablePredictionGame.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Leagues.AnyAsync())
        {
            return;
        }

        var league = new League
        {
            Name = "Premier League",
            SeasonYear = 2025
        };

        var teamNames = new[]
        {
            "Arsenal", "Manchester City", "Liverpool", "Chelsea",
            "Tottenham", "Manchester United", "Newcastle", "Aston Villa"
        };

        var teams = teamNames.Select(name => new Team { Name = name, League = league }).ToList();
        league.Teams = teams;

        var actualStandings = new (string Name, int Position, int Points)[]
        {
            ("Liverpool", 1, 84),
            ("Arsenal", 2, 74),
            ("Manchester City", 3, 71),
            ("Chelsea", 4, 69),
            ("Newcastle", 5, 66),
            ("Aston Villa", 6, 64),
            ("Tottenham", 7, 62),
            ("Manchester United", 8, 58)
        };

        foreach (var (name, position, points) in actualStandings)
        {
            var team = teams.First(t => t.Name == name);
            team.ActualStanding = new ActualStanding
            {
                FinalPosition = position,
                FinalPoints = points
            };
        }

        db.Leagues.Add(league);
        await db.SaveChangesAsync();
    }
}
