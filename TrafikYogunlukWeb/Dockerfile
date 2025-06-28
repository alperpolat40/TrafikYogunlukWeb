# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözüm ve proje dosyalarını kopyala
COPY TrafikYogunlukWeb.sln ./
COPY TrafikYogunlukWeb/*.csproj ./TrafikYogunlukWeb/

# NuGet restore
RUN dotnet restore TrafikYogunlukWeb.sln

# Tüm kaynakları kopyala
COPY . .

# Build ve publish
WORKDIR /src/TrafikYogunlukWeb
RUN dotnet publish -c Release -o /app/publish

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TrafikYogunlukWeb.dll"]
