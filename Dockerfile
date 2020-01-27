FROM mcr.microsoft.com/dotnet/core/runtime:3.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["Api/Competition.csproj", "Api/"]
COPY ["Competition.Domain/Competition.Domain.csproj", "Competition.Domain/"]
COPY ["Competition.Dal/Competition.Dal.csproj", "Competition.Dal/"]
COPY ["Competition.Services/Competition.Services.csproj", "Competition.Services/"]
COPY ["Competition.Tests/Competition.Tests.csproj", "Competition.Tests/"]
RUN dotnet restore "Api/Competition.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Competition.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Competition.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Competition.dll"]