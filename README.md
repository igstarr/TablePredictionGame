# TablePredictionGame

A sports league table prediction game. Players predict final league standings (team positions and points) for a season; scores are calculated against actual results.

## Stack

- **Backend:** ASP.NET Core Web API (.NET 10) with Entity Framework Core and SQLite
- **Frontend:** Angular with standalone components

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 22+](https://nodejs.org/)
- [Angular CLI](https://angular.dev/tools/cli) (`npm install -g @angular/cli`)

## Getting Started

### Backend

```bash
cd backend/TablePredictionGame.Api
dotnet run
```

The API runs at `https://localhost:7150` and `http://localhost:5150`. The Angular dev app uses HTTP on port 5150. OpenAPI is available in Development at `/openapi/v1.json`.

### Frontend

```bash
cd frontend/table-prediction-game
npm install
ng serve
```

The app runs at `http://localhost:4200`.

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/leagues` | List leagues with teams |
| GET | `/api/leagues/{id}` | League detail |
| POST | `/api/predictions` | Submit a prediction set |
| GET | `/api/predictions/leaderboard?leagueId=` | Rank players by accuracy |

## Scoring

Per team: `abs(predictedPosition - actualPosition) + abs(predictedPoints - actualPoints)`. Lower total error = better rank.

## Future Enhancements

- User authentication (ASP.NET Identity / JWT)
- Admin UI to enter final standings
- Multiple concurrent seasons per league
- Deployment (Azure App Service + Static Web Apps, or Docker)
- Unit/integration tests
