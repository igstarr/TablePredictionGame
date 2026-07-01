namespace TablePredictionGame.Api.Models;

public class League
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int SeasonYear { get; set; }
    public ICollection<Team> Teams { get; set; } = [];
    public ICollection<Prediction> Predictions { get; set; } = [];
}
