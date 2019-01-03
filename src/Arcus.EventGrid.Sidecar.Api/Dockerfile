FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Arcus.EventGrid.Sidecar.Api/Arcus.EventGrid.Sidecar.Api.csproj", "Arcus.EventGrid.Sidecar.Api/"]
RUN dotnet restore "Arcus.EventGrid.Sidecar.Api/Arcus.EventGrid.Sidecar.Api.csproj"
COPY . .
WORKDIR "/src/Arcus.EventGrid.Sidecar.Api"
RUN dotnet build "Arcus.EventGrid.Sidecar.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Arcus.EventGrid.Sidecar.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Arcus.EventGrid.Sidecar.Api.dll"]