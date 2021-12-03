#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM  mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GooglePlayService.Backend/GoogleApps.Backend.csproj", "GooglePlayService.Backend/"]
COPY ["GoogleApps.Interfaces/GoogleApps.Interfaces.csproj", "GoogleApps.Interfaces/"]
COPY ["EntityFrameworkProvider/EntityFrameworkProvider.csproj", "EntityFrameworkProvider/"]
RUN dotnet restore "GooglePlayService.Backend/GoogleApps.Backend.csproj"
COPY . .
WORKDIR "/src/GooglePlayService.Backend"
RUN dotnet build "GoogleApps.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoogleApps.Backend.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoogleApps.Backend.dll"]
