namespace TablePredictionGame.Api.Models;

public class Prediction
{
    public int Id { get; set; }
    public required string PlayerName { get; set; }
    public int LeagueId { get; set; }
    public League? League { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<PredictionEntry> Entries { get; set; } = [];
}
