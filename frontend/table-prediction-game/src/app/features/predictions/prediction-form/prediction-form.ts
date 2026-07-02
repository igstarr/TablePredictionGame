import { Component, OnInit, inject } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { LeagueService } from '../../../core/services/league.service';
import { PredictionService } from '../../../core/services/prediction.service';
import { League } from '../../../core/models/game.models';

@Component({
  selector: 'app-prediction-form',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './prediction-form.html'
})
export class PredictionForm implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly fb = inject(FormBuilder);
  private readonly leagueService = inject(LeagueService);
  private readonly predictionService = inject(PredictionService);

  league: League | null = null;
  loading = true;
  submitting = false;
  error = '';
  success = '';

  form = this.fb.group({
    playerName: ['', [Validators.required, Validators.maxLength(100)]],
    entries: this.fb.array<FormGroup>([])
  });

  get entries(): FormArray<FormGroup> {
    return this.form.controls.entries;
  }

  ngOnInit(): void {
    const leagueId = Number(this.route.snapshot.paramMap.get('id'));

    this.leagueService.getLeague(leagueId).subscribe({
      next: (league) => {
        this.league = league;
        league.teams.forEach((team) => {
          this.entries.push(this.fb.group({
            teamId: [team.id],
            teamName: [{ value: team.name, disabled: true }],
            predictedPosition: [1, [Validators.required, Validators.min(1)]],
            predictedPoints: [0, [Validators.required, Validators.min(0)]]
          }));
        });
        this.loading = false;
      },
      error: () => {
        this.error = 'Could not load league details.';
        this.loading = false;
      }
    });
  }

  submit(): void {
    if (!this.league || this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.error = '';
    this.success = '';

    const value = this.form.getRawValue();

    this.predictionService.submitPrediction({
      leagueId: this.league.id,
      playerName: value.playerName!.trim(),
      entries: value.entries!.map((entry) => ({
        teamId: entry['teamId'] as number,
        predictedPosition: Number(entry['predictedPosition']),
        predictedPoints: Number(entry['predictedPoints'])
      }))
    }).subscribe({
      next: () => {
        this.success = 'Prediction submitted successfully.';
        this.submitting = false;
        setTimeout(() => {
          void this.router.navigate(['/leagues', this.league!.id, 'leaderboard']);
        }, 800);
      },
      error: () => {
        this.error = 'Failed to submit prediction.';
        this.submitting = false;
      }
    });
  }
}
