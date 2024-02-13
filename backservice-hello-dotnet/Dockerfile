#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 6001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy NuGet's root certificate
COPY ["certs/1-FZ-Secure-Root-CA.crt", "/usr/local/share/ca-certificates/nuget-root.crt"]

# Install and update CA certificates
RUN apt-get update && \
    apt-get install -y ca-certificates && \
    update-ca-certificates


COPY ["backservice-hello-dotnet.csproj", "."]
RUN dotnet restore "./././backservice-hello-dotnet.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./backservice-hello-dotnet.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./backservice-hello-dotnet.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backservice-hello-dotnet.dll"]