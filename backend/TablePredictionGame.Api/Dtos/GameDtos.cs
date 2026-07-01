namespace TablePredictionGame.Api.Dtos;

public record TeamDto(int Id, string Name, int? FinalPosition, int? FinalPoints);

public record LeagueDto(int Id, string Name, int SeasonYear, IReadOnlyList<TeamDto> Teams);

public record PredictionEntryDto(int TeamId, int PredictedPosition, int PredictedPoints);

public record CreatePredictionDto(int LeagueId, string PlayerName, IReadOnlyList<PredictionEntryDto> Entries);

public record LeaderboardEntryDto(int Rank, string PlayerName, int TotalError, DateTime SubmittedAt);
