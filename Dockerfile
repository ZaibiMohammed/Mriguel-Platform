FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Mriguel.sln", "./"]
COPY ["src/Core/Domain/Mriguel.Domain.csproj", "src/Core/Domain/"]
COPY ["src/Core/Application/Mriguel.Application.csproj", "src/Core/Application/"]
COPY ["src/Infrastructure/Persistence/Mriguel.Persistence.csproj", "src/Infrastructure/Persistence/"]
COPY ["src/Infrastructure/Identity/Mriguel.Identity.csproj", "src/Infrastructure/Identity/"]
COPY ["src/Infrastructure/Infrastructure/Mriguel.Infrastructure.csproj", "src/Infrastructure/Infrastructure/"]
COPY ["src/Presentation/API/Mriguel.API.csproj", "src/Presentation/API/"]
COPY ["src/Presentation/SignalR/Mriguel.SignalR.csproj", "src/Presentation/SignalR/"]
COPY ["tests/Mriguel.UnitTests/Mriguel.UnitTests.csproj", "tests/Mriguel.UnitTests/"]
COPY ["tests/Mriguel.IntegrationTests/Mriguel.IntegrationTests.csproj", "tests/Mriguel.IntegrationTests/"]

RUN dotnet restore "Mriguel.sln"

COPY . .
WORKDIR "/src/src/Presentation/API"
RUN dotnet build "Mriguel.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mriguel.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mriguel.API.dll"]
