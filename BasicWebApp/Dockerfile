FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BasicWebApp/BasicWebApp.csproj", "BasicWebApp/"]
RUN dotnet restore "BasicWebApp/BasicWebApp.csproj"
COPY . .
WORKDIR "/src/BasicWebApp"
RUN dotnet build "BasicWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BasicWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasicWebApp.dll"]
