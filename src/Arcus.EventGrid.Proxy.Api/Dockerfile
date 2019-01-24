FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Arcus.EventGrid.Proxy.Api/Arcus.EventGrid.Proxy.Api.csproj", "Arcus.EventGrid.Proxy.Api/"]
RUN dotnet restore "Arcus.EventGrid.Proxy.Api/Arcus.EventGrid.Proxy.Api.csproj" --source https://www.myget.org/F/arcus/api/v3/index.json --source https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/src/Arcus.EventGrid.Proxy.Api"
RUN dotnet build "Arcus.EventGrid.Proxy.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Arcus.EventGrid.Proxy.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Arcus.EventGrid.Proxy.Api.dll"]