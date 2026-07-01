import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { League } from '../models/game.models';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class LeagueService {
  private readonly http = inject(HttpClient);
  private readonly api = inject(ApiService);

  getLeagues(): Observable<League[]> {
    return this.http.get<League[]>(`${this.api.baseUrl}/leagues`);
  }

  getLeague(id: number): Observable<League> {
    return this.http.get<League>(`${this.api.baseUrl}/leagues/${id}`);
  }
}
