#How to make migration

##Migration

`dotnet ef migrations add "Initial" -s "..\API\API.csproj"`

##Update database

`dotnet ef database update -s "..\API\API.csproj"`

##Description

Go to the project directory with DataContext.

The first part as usual.

`dotnet ef migrations add "migrationName"`

Next, use the -s parameter, and specify the directory with your connection string.

`-s "..\API\API.csproj"`

With update the same.