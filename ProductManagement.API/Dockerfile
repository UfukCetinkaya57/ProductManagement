# .NET 8.0 SDK kullanarak uygulamayı inşa ediyoruz
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyalarını kopyalayıp restore ediyoruz
COPY *.sln .
COPY ProductManagement.API/*.csproj ./ProductManagement.API/
COPY ProductManagement.Application/*.csproj ./ProductManagement.Application/
COPY ProductManagement.Core/*.csproj ./ProductManagement.Core/
COPY ProductManagement.Infrastructure/*.csproj ./ProductManagement.Infrastructure/
COPY ProductManagement.Persistence/*.csproj ./ProductManagement.Persistence/

RUN dotnet restore

# Tüm proje dosyalarını kopyalıyoruz ve uygulamayı inşa ediyoruz
COPY . .
WORKDIR /app/ProductManagement.API
RUN dotnet publish -c Release -o out

# Çalıştırılabilir bir imaj oluşturuyoruz
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/ProductManagement.API/out .
ENTRYPOINT ["dotnet", "ProductManagement.API.dll"]
