# -------------------------
# BUILD STAGE
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Go into project folder (IMPORTANT: must match exact folder name)
WORKDIR /src/Th_Dpt

# Restore dependencies
RUN dotnet restore

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# -------------------------
# RUNTIME STAGE
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Render uses port 10000 automatically
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Start application
ENTRYPOINT ["dotnet", "Th_Dpt.dll"]
