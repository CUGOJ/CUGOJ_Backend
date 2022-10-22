#! /bin/bash
# arg1 migration name

dotnet ef migrations add $1 --output-dir ./UpdateSqlSnapshot/Migrations/$1

echo "`dotnet ef migrations script --idempotent`" > ./UpdateSqlSnapshot/SQLs/update-sql-$1.sql

sed -i '1,2d' ./UpdateSqlSnapshot/SQLs/update-sql-$1.sql