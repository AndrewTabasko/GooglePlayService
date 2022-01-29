#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GooglePlayService.Agent/GoogleApps.Agent.csproj", "GooglePlayService.Agent/"]
COPY ["EntityFrameworkProvider/EntityFrameworkProvider.csproj", "EntityFrameworkProvider/"]
COPY ["GoogleApps.Interfaces/GoogleApps.Interfaces.csproj", "GoogleApps.Interfaces/"]
RUN dotnet restore "GooglePlayService.Agent/GoogleApps.Agent.csproj"
COPY . .
WORKDIR "/src/GooglePlayService.Agent"
RUN dotnet build "GoogleApps.Agent.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "GoogleApps.Agent.csproj" -c Debug -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoogleApps.Agent.dll"]
