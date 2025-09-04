#!/bin/bash
# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."

# Execute the SQL file
echo "Running database initialization script..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrongPassword123 -d master -i /init-scripts/database.sql

echo "Database initialization completed."