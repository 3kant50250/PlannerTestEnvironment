FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/PlannerClient/PlannerClient.csproj", "src/PlannerClient/"]
RUN dotnet restore "src/PlannerClient/PlannerClient.csproj"
COPY . .
WORKDIR "/src/src/PlannerClient"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PlannerClient.dll"]
