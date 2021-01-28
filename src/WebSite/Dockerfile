FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY "WebSite.sln" "WebSite.sln"
COPY "WebSite/WebSite.csproj" "WebSite/WebSite.csproj"
COPY "WebSite.Tests/WebSite.Tests.csproj" "WebSite.Tests/WebSite.Tests.csproj"
COPY "docker-compose.dcproj" "docker-compose.dcproj"
COPY "NuGet.config" "NuGet.config"

RUN dotnet restore "WebSite.sln"
COPY . .

RUN dotnet build "WebSite/WebSite.csproj" -c Release -o /app/build --no-restore
RUN dotnet build "WebSite.Tests/WebSite.Tests.csproj" -c Release --no-restore

FROM build AS test
ARG BuildId
LABEL test=${BuildId}
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results_website.trx" WebSite.Tests/WebSite.Tests.csproj

FROM build AS publish
WORKDIR /src/WebSite
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/pubnlish .
ENTRYPOINT ["dotnet", "WebSite.dll"]