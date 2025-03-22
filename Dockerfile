FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
ARG REPO_OWNER
ARG REPO_NAME
ARG BRANCH
ARG USER_NAME
ARG PASSWORD
ARG APP_ID
ARG APP_SECRET
ARG DOTNET_ENV

ENV ConfigProvider__RepoOwner=$REPO_OWNER
ENV ConfigProvider__RepoName=$REPO_NAME
ENV ConfigProvider__Branch=$BRANCH
ENV ConfigProvider__UserName=$USER_NAME
ENV ConfigProvider__Password=$PASSWORD
ENV ConfigProvider__AppId=$APP_ID
ENV ConfigProvider__AppSecret=$APP_SECRET

ENV DOTNET_ENVIRONMENT=$DOTNET_ENV

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
