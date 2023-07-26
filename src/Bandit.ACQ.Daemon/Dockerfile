#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bandit.ACQ.Daemon/Bandit.ACQ.Daemon.csproj", "src/Bandit.ACQ.Daemon/"]
RUN dotnet restore "src/Bandit.ACQ.Daemon/Bandit.ACQ.Daemon.csproj"
COPY . .
WORKDIR "/src/src/Bandit.ACQ.Daemon"
RUN dotnet build "Bandit.ACQ.Daemon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bandit.ACQ.Daemon.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bandit.ACQ.Daemon.dll"]