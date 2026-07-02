import { DatePipe } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { LeagueService } from '../../../core/services/league.service';
import { PredictionService } from '../../../core/services/prediction.service';
import { LeaderboardEntry, League } from '../../../core/models/game.models';

@Component({
  selector: 'app-leaderboard',
  imports: [RouterLink, DatePipe],
  templateUrl: './leaderboard.html'
})
export class Leaderboard implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly leagueService = inject(LeagueService);
  private readonly predictionService = inject(PredictionService);

  league: League | null = null;
  entries: LeaderboardEntry[] = [];
  loading = true;
  error = '';

  ngOnInit(): void {
    const leagueId = Number(this.route.snapshot.paramMap.get('id'));

    this.leagueService.getLeague(leagueId).subscribe({
      next: (league) => {
        this.league = league;
      },
      error: () => {
        this.error = 'Could not load league.';
        this.loading = false;
      }
    });

    this.predictionService.getLeaderboard(leagueId).subscribe({
      next: (entries) => {
        this.entries = entries;
        this.loading = false;
      },
      error: () => {
        this.error = 'Could not load leaderboard.';
        this.loading = false;
      }
    });
  }
}
