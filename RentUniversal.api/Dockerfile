# ============================================
#          BASE IMAGE (runtime)
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ============================================
#          BUILD IMAGE (SDK)
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["RentUniversal.api/RentUniversal.api.csproj", "RentUniversal.api/"]
RUN dotnet restore "RentUniversal.api/RentUniversal.api.csproj"

# Copy the rest of the source code
COPY . .

# Build project
WORKDIR "/src/RentUniversal.api"
RUN dotnet build "RentUniversal.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ============================================
#          PUBLISH IMAGE
# ============================================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RentUniversal.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ============================================
#          FINAL IMAGE (production)
# ============================================
FROM base AS final
WORKDIR /app

# Copy published output
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "RentUniversal.api.dll"]