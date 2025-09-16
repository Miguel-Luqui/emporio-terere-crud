# Imagem base para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagem do SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EmporioTerere.Api.csproj", "./"]
RUN dotnet restore "./EmporioTerere.Api.csproj"
COPY . .
RUN dotnet build "EmporioTerere.Api.csproj" -c Release -o /app/build

# Publicação
FROM build AS publish
RUN dotnet publish "EmporioTerere.Api.csproj" -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmporioTerere.Api.dll"]
