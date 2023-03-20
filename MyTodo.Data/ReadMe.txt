cmd cd projectFolder >> dotnet tool install --global dotnet-ef --version 3.1.3
dotnet ef migrations add initMigration
dotnet ef database update