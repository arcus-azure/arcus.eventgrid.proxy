FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.5-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.302-alpine AS build
WORKDIR /src
COPY ["Arcus.EventGrid.Proxy.Api/Arcus.EventGrid.Proxy.Api.csproj", "Arcus.EventGrid.Proxy.Api/"]
RUN dotnet restore "Arcus.EventGrid.Proxy.Api/Arcus.EventGrid.Proxy.Api.csproj"
COPY . .
WORKDIR "/src/Arcus.EventGrid.Proxy.Api"
RUN dotnet build "Arcus.EventGrid.Proxy.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Arcus.EventGrid.Proxy.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Arcus.EventGrid.Proxy.Api.dll"]