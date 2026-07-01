namespace TablePredictionGame.Api.Models;

public class ActualStanding
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public Team? Team { get; set; }
    public int FinalPosition { get; set; }
    public int FinalPoints { get; set; }
}
