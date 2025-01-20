#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["BP_API/BP_API.csproj", "BP_API/"]
#RUN dotnet restore "./BP_API/BP_API.csproj"
#COPY . .
#WORKDIR "/src/BP_API"
#RUN dotnet build "./BP_API.csproj" -c %BUILD_CONFIGURATION% -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./BP_API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "BP_API.dll"]


# Etapa 1: Construcción de la aplicación
# Etapa 1: Construcción de la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MiAplicacion/MiAplicacion.csproj", "MiAplicacion/"]
RUN dotnet restore "MiAplicacion/MiAplicacion.csproj"
COPY . .
WORKDIR "/src/MiAplicacion"
RUN dotnet build "MiAplicacion.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiAplicacion.csproj" -c Release -o /app/publish

# Etapa 2: Instalar dependencias de Skia y ejecutar la aplicación
FROM base AS final
WORKDIR /app

# Instalar dependencias para QuestPdfSkia (SkiaSharp)
RUN apt-get update && apt-get install -y \
    libfontconfig1 \
    libfreetype6 \
    libssl1.0.0 \
    libx11-6 \
    libxrender1 \
    libgdiplus\
    libfontconfig1libgdiplus

# Copiar los archivos desde la etapa de publicación
COPY --from=publish /app/publish .

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "MiAplicacion.dll"]
