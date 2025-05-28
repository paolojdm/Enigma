# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia tutto e ripristina i pacchetti
COPY . ./
RUN dotnet restore

# Compila in modalità Release
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia solo l'output pubblicato dal build stage
COPY --from=build /app/out .

# Espone la porta (modifica se usi porte diverse)
EXPOSE 80

# Comando di avvio
ENTRYPOINT ["dotnet", "Enigma_Protocol.dll"]
