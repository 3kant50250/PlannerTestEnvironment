# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore PlannerClient.csproj
RUN dotnet publish PlannerClient.csproj -c Release -o /app/publish

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Set the environment to Development
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build /app/publish ./
EXPOSE 80
ENTRYPOINT ["dotnet", "PlannerClient.dll"]