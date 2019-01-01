# ProcessingTools Web Documents (PTWD) for .NET Core

## Needed resources
+ NodeJS [Download](https://nodejs.org/en/)
+ .NET Core + .NET Standard 2 SDK [Download](https://www.microsoft.com/net/download/linux/build)
+ Windows / Linux / OS X

## Build
- ```npm install```
- ```npm run build```
- ```TERM=xterm``` (for Linux only)
- ```dotnet restore```
- ```dotnet build```

## Run
- ```dotnet run```

## Clean before re-build
- ```dotnet clean```
- ```npm run clean```

## Before deploy
- Edit `appsettings.json` with relevant values.

## After deploy
### (When the system is up and running)
+ Register new user account
+ After login go to route `/Admin/Databases/InitializeAll`, which builds indices on dbs
