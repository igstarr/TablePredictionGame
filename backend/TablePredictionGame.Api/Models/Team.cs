namespace TablePredictionGame.Api.Models;

public class Team
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int LeagueId { get; set; }
    public League? League { get; set; }
    public ActualStanding? ActualStanding { get; set; }
}
