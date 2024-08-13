#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TelegramSellerBot.TelegramBot/TelegramSellerBot.TelegramBot.csproj", "src/TelegramSellerBot.TelegramBot/"]
COPY ["src/TelegramSellerBot.Application/TelegramSellerBot.Application.csproj", "src/TelegramSellerBot.Application/"]
COPY ["src/TelegramSellerBot.Core/TelegramSellerBot.Core.csproj", "src/TelegramSellerBot.Core/"]
COPY ["src/TelegramSellerBot.Persistense/TelegramSellerBot.Persistense.csproj", "src/TelegramSellerBot.Persistense/"]
RUN dotnet nuget add source https://nuget.voids.site/v3/index.json
RUN dotnet restore "./src/TelegramSellerBot.TelegramBot/TelegramSellerBot.TelegramBot.csproj"
COPY . .
WORKDIR "/src/src/TelegramSellerBot.TelegramBot"
RUN dotnet build "./TelegramSellerBot.TelegramBot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TelegramSellerBot.TelegramBot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TelegramSellerBot.TelegramBot.dll"]