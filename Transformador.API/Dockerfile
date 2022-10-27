#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Transformador.API/Transformador.API.csproj", "Transformador.API/"]
COPY ["Transformador.CrossCutting/Transformador.CrossCutting.csproj", "Transformador.CrossCutting/"]
COPY ["Transformador.Data/Transformador.Data.csproj", "Transformador.Data/"]
COPY ["Transformador.Domain/Transformador.Domain.csproj", "Transformador.Domain/"]
COPY ["Transformador.Services/Transformador.Services.csproj", "Transformador.Services/"]
RUN dotnet restore "Transformador.API/Transformador.API.csproj"
COPY . .
WORKDIR "/src/Transformador.API"
RUN dotnet build "Transformador.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transformador.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transformador.API.dll"]
