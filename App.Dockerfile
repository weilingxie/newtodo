# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY src/NewTodo/*.csproj ./src/NewTodo/
COPY NewTodo.DbMigration/*.csproj ./NewTodo.DbMigration/
COPY NewTodo.Domain/*.csproj ./NewTodo.Domain/
COPY NewTodo.IntegrationTest/*.csproj ./NewTodo.IntegrationTest/
COPY NewTodo.Test/*.csproj ./NewTodo.Test/
COPY runTest.sh runTest.sh

RUN chmod +x runTest.sh
RUN dotnet restore

# Copy everything else and build
COPY src/NewTodo/. ./src/NewTodo/
COPY NewTodo.DbMigration/. ./NewTodo.DbMigration/
COPY NewTodo.Domain/. ./NewTodo.Domain/
COPY NewTodo.IntegrationTest/. ./NewTodo.IntegrationTest/
COPY NewTodo.Test/. ./NewTodo.Test/

#Publish
WORKDIR /app/src/NewTodo
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/src/NewTodo/out ./
ENTRYPOINT ["dotnet", "NewTodo.dll"]