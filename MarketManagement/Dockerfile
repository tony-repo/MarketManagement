#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MarketManagement/MarketManagement.csproj", "MarketManagement/"]
RUN dotnet restore "MarketManagement/MarketManagement.csproj"
COPY . .
WORKDIR "/src/MarketManagement"
RUN dotnet build "MarketManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketManagement.dll"]