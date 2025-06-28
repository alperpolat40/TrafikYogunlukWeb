# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TrafikYogunlukWeb.sln ./
COPY TrafikYogunlukWeb/*.csproj ./TrafikYogunlukWeb/
RUN dotnet restore TrafikYogunlukWeb.sln

COPY . ./
WORKDIR /src/TrafikYogunlukWeb
RUN dotnet publish -c Release -o /app/publish

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TrafikYogunlukWeb.dll"]
