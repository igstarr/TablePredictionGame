using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TablePredictionGame.Api.Data;
using TablePredictionGame.Api.Dtos;

namespace TablePredictionGame.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaguesController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeagueDto>>> GetLeagues()
    {
        var leagues = await db.Leagues
            .AsNoTracking()
            .Include(l => l.Teams)
            .ThenInclude(t => t.ActualStanding)
            .OrderBy(l => l.Name)
            .ToListAsync();

        return Ok(leagues.Select(ToLeagueDto));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LeagueDto>> GetLeague(int id)
    {
        var league = await db.Leagues
            .AsNoTracking()
            .Include(l => l.Teams)
            .ThenInclude(t => t.ActualStanding)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (league is null)
        {
            return NotFound();
        }

        return Ok(ToLeagueDto(league));
    }

    private static LeagueDto ToLeagueDto(Models.League league) =>
        new(
            league.Id,
            league.Name,
            league.SeasonYear,
            league.Teams
                .OrderBy(t => t.Name)
                .Select(t => new TeamDto(
                    t.Id,
                    t.Name,
                    t.ActualStanding?.FinalPosition,
                    t.ActualStanding?.FinalPoints))
                .ToList());
}
