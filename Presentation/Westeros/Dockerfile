#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/Westeros/Westeros.csproj", "Presentation/Westeros/"]
COPY ["Core/Application/Application.csproj", "Core/Application/"]
COPY ["Shared/SharedKernel/SharedKernel.csproj", "Shared/SharedKernel/"]
COPY ["Core/Domain/Domain.csproj", "Core/Domain/"]
COPY ["Persistence/Infrastructure/Infrastructure.csproj", "Persistence/Infrastructure/"]
RUN dotnet restore "Presentation/Westeros/Westeros.csproj"
COPY . .
WORKDIR "/src/Presentation/Westeros"
RUN dotnet build "Westeros.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Westeros.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Westeros.dll"]