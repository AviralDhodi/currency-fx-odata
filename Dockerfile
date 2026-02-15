# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and publish
COPY . ./
RUN dotnet publish -c Release -o /out

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Render listens on port 10000 by default
ENV ASPNETCORE_URLS=http://0.0.0.0:10000

COPY --from=build /out .

ENTRYPOINT ["dotnet", "CurrencyFxOData.dll"]