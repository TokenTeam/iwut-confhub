FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

ENV ConfigProvider__RepoOwner=""
ENV ConfigProvider__RepoName=""
ENV ConfigProvider__Branch=""
ENV ConfigProvider__UserName=""
ENV ConfigProvider__Password=""
ENV ConfigProvider__AppId=""
ENV ConfigProvider__AppSecret=""

ENV DOTNET_ENVIRONMENT=""

WORKDIR /app
EXPOSE 80

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
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "TokenTeam.ConfigCenter.dll"]
