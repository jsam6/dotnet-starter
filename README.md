ASP.NET Core Empty
dotnet build
dotnet run

go nuget.org to download packages
dotnet add package MinimalApis.Extensions --version 0.11.0
It will then add to GameStore.Api.csproj


2:09:30

Migrations 

dotnet ef migrations add InitialCreate --output-dir Data/Migrations

dotnet ef database update