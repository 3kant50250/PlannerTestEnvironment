FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/PlannerServer/PlannerServer.csproj", "src/PlannerServer/"]
RUN dotnet restore "src/PlannerServer/PlannerServer.csproj"
COPY . .
WORKDIR "/src/src/PlannerServer"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY wait-for-db.sh .
RUN chmod +x wait-for-db.sh
ENTRYPOINT ["./wait-for-db.sh"]
