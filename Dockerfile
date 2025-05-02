# Imagen de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar el archivo csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto del proyecto y compilar
COPY . ./
RUN dotnet publish -c Release -o out

# Imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Nombre de tu app compilada
ENV APP_NET_CORE PC2proyecto.dll

# Comando de inicio (Render usará $PORT automáticamente)
CMD ASPNETCORE_URLS=http://*:$PORT dotnet $APP_NET_CORE