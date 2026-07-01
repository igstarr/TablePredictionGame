import { Routes } from '@angular/router';
import { LeagueList } from './features/leagues/league-list/league-list';
import { PredictionForm } from './features/predictions/prediction-form/prediction-form';
import { Leaderboard } from './features/leaderboard/leaderboard/leaderboard';

export const routes: Routes = [
  { path: '', component: LeagueList },
  { path: 'leagues/:id/predict', component: PredictionForm },
  { path: 'leagues/:id/leaderboard', component: Leaderboard },
  { path: '**', redirectTo: '' }
];
