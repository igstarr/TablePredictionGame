namespace TablePredictionGame.Api.Models;

public class PredictionEntry
{
    public int Id { get; set; }
    public int PredictionId { get; set; }
    public Prediction? Prediction { get; set; }
    public int TeamId { get; set; }
    public Team? Team { get; set; }
    public int PredictedPosition { get; set; }
    public int PredictedPoints { get; set; }
}
