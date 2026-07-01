using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TablePredictionGame.Api.Data;
using TablePredictionGame.Api.Dtos;
using TablePredictionGame.Api.Models;
using TablePredictionGame.Api.Services;

namespace TablePredictionGame.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredictionsController(AppDbContext db) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreatePrediction([FromBody] CreatePredictionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.PlayerName))
        {
            return BadRequest("Player name is required.");
        }

        if (dto.Entries.Count == 0)
        {
            return BadRequest("At least one prediction entry is required.");
        }

        var league = await db.Leagues
            .Include(l => l.Teams)
            .FirstOrDefaultAsync(l => l.Id == dto.LeagueId);

        if (league is null)
        {
            return NotFound($"League {dto.LeagueId} was not found.");
        }

        var teamIds = league.Teams.Select(t => t.Id).ToHashSet();
        if (dto.Entries.Any(e => !teamIds.Contains(e.TeamId)))
        {
            return BadRequest("One or more teams do not belong to the selected league.");
        }

        var prediction = new Prediction
        {
            PlayerName = dto.PlayerName.Trim(),
            LeagueId = dto.LeagueId,
            CreatedAt = DateTime.UtcNow,
            Entries = dto.Entries.Select(e => new PredictionEntry
            {
                TeamId = e.TeamId,
                PredictedPosition = e.PredictedPosition,
                PredictedPoints = e.PredictedPoints
            }).ToList()
        };

        db.Predictions.Add(prediction);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLeaderboard), new { leagueId = dto.LeagueId }, new { prediction.Id });
    }

    [HttpGet("leaderboard")]
    public async Task<ActionResult<IEnumerable<LeaderboardEntryDto>>> GetLeaderboard([FromQuery] int leagueId)
    {
        var leagueExists = await db.Leagues.AnyAsync(l => l.Id == leagueId);
        if (!leagueExists)
        {
            return NotFound($"League {leagueId} was not found.");
        }

        var standings = await db.ActualStandings
            .AsNoTracking()
            .Where(s => s.Team!.LeagueId == leagueId)
            .ToDictionaryAsync(s => s.TeamId);

        if (standings.Count == 0)
        {
            return Ok(Array.Empty<LeaderboardEntryDto>());
        }

        var predictions = await db.Predictions
            .AsNoTracking()
            .Include(p => p.Entries)
            .Where(p => p.LeagueId == leagueId)
            .ToListAsync();

        var ranked = predictions
            .Select(p => new
            {
                p.PlayerName,
                p.CreatedAt,
                TotalError = ScoringService.CalculateTotalError(p, standings)
            })
            .OrderBy(p => p.TotalError)
            .ThenBy(p => p.CreatedAt)
            .Select((p, index) => new LeaderboardEntryDto(index + 1, p.PlayerName, p.TotalError, p.CreatedAt))
            .ToList();

        return Ok(ranked);
    }
}
