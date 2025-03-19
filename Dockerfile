FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TokenTeam.ConfigCenter/TokenTeam.ConfigCenter.csproj", "TokenTeam.ConfigCenter/"]
RUN dotnet restore "TokenTeam.ConfigCenter/TokenTeam.ConfigCenter.csproj"
COPY . .
WORKDIR "/src/TokenTeam.ConfigCenter"
RUN dotnet build "TokenTeam.ConfigCenter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TokenTeam.ConfigCenter.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AIHub.Blazor.dll"]
