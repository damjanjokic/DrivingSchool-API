# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and projects
COPY *.sln ./
COPY BuckleApp.API/BuckleApp.API.csproj BuckleApp.API/
COPY BuckleApp.Application/BuckleApp.Application.csproj BuckleApp.Application/
COPY BuckleApp.Core/BuckleApp.Core.csproj BuckleApp.Core/
COPY BuckleApp.Data/BuckleApp.Data.csproj BuckleApp.Data/
COPY BuckleApp.Infrastructure/BuckleApp.Infrastructure.csproj BuckleApp.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy everything and publish the app
COPY . .
WORKDIR /app/BuckleApp.API
RUN dotnet publish -c Release -o /out

# Stage 2: Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Expose port 5000 for the API
EXPOSE 5000

# Run the app
ENTRYPOINT ["dotnet", "BuckleApp.API.dll"]