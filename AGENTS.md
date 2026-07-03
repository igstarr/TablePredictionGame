# AGENTS.md

## Cursor Cloud specific instructions

This repo is a sports league table prediction game: an ASP.NET Core Web API (.NET 10, EF Core + SQLite) backend and an Angular 20 frontend. See `README.md` for the product/API overview and the standard local commands.

### Services

| Service | Directory | Run command | URL |
|---------|-----------|-------------|-----|
| Backend API | `backend/TablePredictionGame.Api` | `dotnet run --launch-profile http` | `http://localhost:5150` |
| Frontend | `frontend/table-prediction-game` | `npm start` | `http://localhost:4200` |

Start the backend before (or alongside) the frontend; the Angular app calls the API directly at `http://localhost:5150/api` (configured in `src/environments/environment.ts`), and the backend CORS policy only allows origin `http://localhost:4200`.

### Non-obvious notes

- The .NET 10 SDK is installed at `/usr/local/dotnet` with a symlink at `/usr/local/bin/dotnet` (already on PATH). It is not managed by the update script.
- The backend has no migrations. On startup `Program.cs` calls `EnsureCreatedAsync()` + `DbSeeder.SeedAsync()`, creating and seeding `tablepredictiongame.db` (gitignored). To reset data, stop the API and delete `backend/TablePredictionGame.Api/tablepredictiongame.db*`.
- Use the `http` launch profile for local dev (`dotnet run --launch-profile http`). The `https` profile needs a dev cert; the frontend expects HTTP on 5150.
- Frontend unit tests use Karma + Chrome. Chrome is at `/usr/local/bin/google-chrome`. Run headless once with: `CHROME_BIN=/usr/local/bin/google-chrome npx ng test --watch=false --browsers=ChromeHeadless`.
- The scaffolded `src/app/app.spec.ts` tests FAIL out of the box (they don't provide the router that the root `App` component needs, and still assert the default "Hello, ..." title). These are pre-existing failures in committed code, not an environment problem.
- There is no lint script configured (no ESLint; `package.json` has no `lint` target). Backend "lint" is just `dotnet build`. `dotnet restore/build` emit `NU1903` vulnerability warnings for transitive packages; these are warnings only.
