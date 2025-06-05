# PlannerTestEnvironment
First set up sql-server with this command:

docker run -d --name sqlserver -e ACCEPT_EULA=Y -e MSSQL_SA_PASSWORD=Your_password123 -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest

Then navigate to program directory and run:

docker compose up -–build
