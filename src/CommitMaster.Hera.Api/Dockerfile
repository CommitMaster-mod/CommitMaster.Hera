FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CommitMaster.Hera.Api/CommitMaster.Hera.Api.csproj", "CommitMaster.Hera.Api/"]
RUN dotnet restore "src/CommitMaster.Hera.Api/CommitMaster.Hera.Api.csproj"
COPY . .
WORKDIR "/src/CommitMaster.Hera.Api"
RUN dotnet build "CommitMaster.Hera.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommitMaster.Hera.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommitMaster.Hera.Api.dll"]
