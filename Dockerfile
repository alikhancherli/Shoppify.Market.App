#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shoppify,Market.App.Api/Shoppify.Market.App.Api.csproj", "Shoppify,Market.App.Api/"]
COPY ["Shoppify,Market.App.Infrastructure/Shoppify.Market.App.Infrastructure.csproj", "Shoppify,Market.App.Infrastructure/"]
COPY ["Shoppify,Market.App.Identity/Shoppify.Market.App.Identity.csproj", "Shoppify,Market.App.Identity/"]
COPY ["Shoppify,Market.App.Service/Shoppify.Market.App.Service.csproj", "Shoppify,Market.App.Service/"]
COPY ["Shoppify,Market.App.Persistence/Shoppify.Market.App.Persistence.csproj", "Shoppify,Market.App.Persistence/"]
COPY ["Shoppify,Market.App.Domain/Shoppify.Market.App.Domain.csproj", "Shoppify,Market.App.Domain/"]
RUN dotnet restore "Shoppify,Market.App.Api/Shoppify.Market.App.Api.csproj"
COPY . .
WORKDIR "/src/Shoppify,Market.App.Api"
RUN dotnet build "Shoppify.Market.App.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shoppify.Market.App.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_HTTPS_PORT=https://+:7107
ENV ASPNETCORE_URLS=http://+:5107

EXPOSE 5107
EXPOSE 7107

ENTRYPOINT ["dotnet", "Shoppify.Market.App.Api.dll"]