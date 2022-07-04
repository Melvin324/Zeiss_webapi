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
