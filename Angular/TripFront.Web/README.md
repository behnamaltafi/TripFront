# TripFront Web

The Angular frontend for TripFront. It has no database or .NET project dependency; it communicates with the API over HTTP.

## Run locally

1. Start the API from `../TripFront.Api`:

   ```powershell
   dotnet run
   ```

   The default development API address is `http://localhost:5146`.

2. In this directory, start Angular:

   ```powershell
   npm start
   ```

   Open `http://localhost:4200`.

The API permits this development origin and uses an HTTP-only authentication cookie. If the API address changes, update the `apiUrl` value in `src/app/core/auth.service.ts` and `src/app/core/trip.service.ts`.
