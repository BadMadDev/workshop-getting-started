FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY "WebSite.sln" "WebSite.sln"

COPY "WebSite/WebSite.csproj" "WebSite/WebSite.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "NuGet.config"

COPY "WebSite.Tests/WebSite.Tests.csproj" "WebSite.Tests/WebSite.Tests.csproj"

RUN dotnet restore "WebSite.sln"

FROM build AS test
ARG BuildId
LABEL test=${BuildId}


FROM build AS publish

COPY . .
WORKDIR /src/WebSite
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebSite.dll"]