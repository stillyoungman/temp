#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TheBarbershop.Api/TheBarbershop.Api.csproj", "TheBarbershop.Api/"]
COPY ["TheBarbershop.Persistence/TheBarbershop.Persistence.csproj", "TheBarbershop.Persistence/"]
COPY ["TheBarbershop.Core/TheBarbershop.Core.csproj", "TheBarbershop.Core/"]
RUN dotnet restore "TheBarbershop.Api/TheBarbershop.Api.csproj"
COPY . .
WORKDIR "/src/TheBarbershop.Api"
RUN dotnet build "TheBarbershop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheBarbershop.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheBarbershop.Api.dll"]