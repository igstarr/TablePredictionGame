import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { CreatePrediction, LeaderboardEntry } from '../models/game.models';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class PredictionService {
  private readonly http = inject(HttpClient);
  private readonly api = inject(ApiService);

  submitPrediction(prediction: CreatePrediction): Observable<unknown> {
    return this.http.post(`${this.api.baseUrl}/predictions`, prediction);
  }

  getLeaderboard(leagueId: number): Observable<LeaderboardEntry[]> {
    return this.http.get<LeaderboardEntry[]>(
      `${this.api.baseUrl}/predictions/leaderboard`,
      { params: { leagueId } }
    );
  }
}
