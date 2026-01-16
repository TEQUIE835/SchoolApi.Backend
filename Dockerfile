# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .

COPY SchoolApp/SchoolApp.csproj SchoolApp/
COPY 1.Application/1.Application.csproj 1.Application/
COPY 2.Domain/2.Domain.csproj 2.Domain/
COPY 3.Infrastructure/3.Infrastructure.csproj 3.Infrastructure/

RUN dotnet restore SchoolApp/SchoolApp.csproj

COPY . .
WORKDIR /src/SchoolApp
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SchoolApp.dll"]
