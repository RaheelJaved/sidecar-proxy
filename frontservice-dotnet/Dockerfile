#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy NuGet's root certificate
COPY ["certs/fz-root-ca.crt", "/usr/local/share/ca-certificates/fz-root-ca.crt"]

# Install and update CA certificates
RUN apt-get update && \
    apt-get install -y ca-certificates && \
    update-ca-certificates


COPY ["frontservice-dotnet.csproj", "."]
RUN dotnet restore "./././frontservice-dotnet.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./frontservice-dotnet.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./frontservice-dotnet.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "frontservice-dotnet.dll"]