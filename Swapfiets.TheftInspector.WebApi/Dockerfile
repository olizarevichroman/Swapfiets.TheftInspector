FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Swapfiets.TheftInspector.WebApi/Swapfiets.TheftInspector.WebApi.csproj", "Swapfiets.TheftInspector.WebApi/"]
COPY ["Swapfiets.TheftInspector.BikeIndexAdapter/Swapfiets.TheftInspector.BikeIndexAdapter.csproj", "Swapfiets.TheftInspector.BikeIndexAdapter/"]
RUN dotnet restore "Swapfiets.TheftInspector.WebApi/Swapfiets.TheftInspector.WebApi.csproj"
COPY . .
WORKDIR "/src/Swapfiets.TheftInspector.WebApi"
RUN dotnet build "Swapfiets.TheftInspector.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Swapfiets.TheftInspector.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Swapfiets.TheftInspector.WebApi.dll"]
