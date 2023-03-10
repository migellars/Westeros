#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["backend/Api/Api.csproj", "backend/Api/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["backend/Application/Application.csproj", "backend/Application/"]
COPY ["backend/Domain/Domain.csproj", "backend/Domain/"]
COPY ["backend/Infrastructure/Infrastructure.csproj", "backend/Infrastructure/"]
RUN dotnet restore "backend/Api/Api.csproj"
COPY . .
WORKDIR "/src/backend/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]