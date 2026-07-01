using TablePredictionGame.Api.Models;

namespace TablePredictionGame.Api.Services;

public static class ScoringService
{
    public static int CalculateTotalError(Prediction prediction, IReadOnlyDictionary<int, ActualStanding> standingsByTeamId)
    {
        var totalError = 0;

        foreach (var entry in prediction.Entries)
        {
            if (!standingsByTeamId.TryGetValue(entry.TeamId, out var actual))
            {
                continue;
            }

            totalError += Math.Abs(entry.PredictedPosition - actual.FinalPosition);
            totalError += Math.Abs(entry.PredictedPoints - actual.FinalPoints);
        }

        return totalError;
    }
}
