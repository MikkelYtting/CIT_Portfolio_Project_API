
Kør altid på htts og port 7098 (https://localhost:7098/index.html):
dotnet run --launch-profile https

integration tests:
dotnet test .\\test\\CIT_Portfolio_Project_API.IntegrationTests\\CIT_Portfolio_Project_API.IntegrationTests.csproj 

unit tests:
dotnet test .\\test\\CIT_Portfolio_Project_API.UnitTests\\CIT_Portfolio_Project_API.UnitTests.csproj 