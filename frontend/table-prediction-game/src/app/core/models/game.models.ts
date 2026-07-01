export interface Team {
  id: number;
  name: string;
  finalPosition: number | null;
  finalPoints: number | null;
}

export interface League {
  id: number;
  name: string;
  seasonYear: number;
  teams: Team[];
}

export interface PredictionEntry {
  teamId: number;
  predictedPosition: number;
  predictedPoints: number;
}

export interface CreatePrediction {
  leagueId: number;
  playerName: string;
  entries: PredictionEntry[];
}

export interface LeaderboardEntry {
  rank: number;
  playerName: string;
  totalError: number;
  submittedAt: string;
}
