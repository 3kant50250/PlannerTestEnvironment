#!/bin/bash

echo "Waiting for SQL Server to be ready..."

for i in {1..30}; do
  /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "Your_password123" -Q "SELECT 1" > /dev/null 2>&1
  if [ $? -eq 0 ]; then
    echo "✅ SQL Server is ready."
    break
  fi
  echo "⏳ Still waiting... ($i)"
  sleep 5
done

exec dotnet PlannerServer.dll
