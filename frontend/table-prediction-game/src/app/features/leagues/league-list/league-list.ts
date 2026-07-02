import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LeagueService } from '../../../core/services/league.service';
import { League } from '../../../core/models/game.models';

@Component({
  selector: 'app-league-list',
  imports: [RouterLink],
  templateUrl: './league-list.html'
})
export class LeagueList implements OnInit {
  private readonly leagueService = inject(LeagueService);

  leagues: League[] = [];
  loading = true;
  error = '';

  ngOnInit(): void {
    this.leagueService.getLeagues().subscribe({
      next: (leagues) => {
        this.leagues = leagues;
        this.loading = false;
      },
      error: () => {
        this.error = 'Could not load leagues. Make sure the API is running.';
        this.loading = false;
      }
    });
  }
}
