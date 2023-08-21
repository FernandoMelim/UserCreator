#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserCreator/UserCreator.csproj", "UserCreator/"]
COPY ["UserCreator.Application/UserCreator.Application.csproj", "UserCreator.Application/"]
COPY ["UserCreator.Domain/UserCreator.Domain.csproj", "UserCreator.Domain/"]
COPY ["UserCreator.Infrastructure/UserCreator.Infrastructure.csproj", "UserCreator.Infrastructure/"]
RUN dotnet restore "UserCreator/UserCreator.csproj"
COPY . .
WORKDIR "/src/UserCreator"
RUN dotnet build "UserCreator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserCreator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserCreator.dll"]