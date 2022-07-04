## Initial package --5.0.17

dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package Microsoft.EntityFrameworkCore.Sqlite

dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet tool install --global dotnet-ef

dotnet restore

dotnet build

## Initial Sqlite

dotnet ef

dotnet ef migrations add initial --project Zeiss_webapi

dotnet ef database update --project Zeiss_webapi

## Start
dotnet run --project Zeiss_webapi

## Package

dotnet publish Zeiss_webapi -c Release -o /app/build

## Docker

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Zeiss_webapi/Zeiss_webapi.csproj", "Zeiss_webapi/"]
RUN dotnet restore "Zeiss_webapi/Zeiss_webapi.csproj"
COPY . .
WORKDIR "/src/Zeiss_webapi"
RUN dotnet build "Zeiss_webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zeiss_webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zeiss_webapi.dll"]
