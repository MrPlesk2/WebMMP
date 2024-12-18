FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebMMP/WebMMP.csproj", "WebMMP/"]
RUN dotnet restore "WebMMP/WebMMP.csproj"
COPY . .
WORKDIR "/src/WebMMP"
RUN dotnet build "WebMMP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebMMP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebMMP.dll"]