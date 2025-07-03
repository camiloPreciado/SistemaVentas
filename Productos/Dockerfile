# Imagen base con IIS + .NET Framework 4.8 (compatible con 4.6.1)
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

# Crear directorio para la aplicación
WORKDIR /inetpub/wwwroot

# Copiar los archivos publicados del proyecto al contenedor
COPY . .

# Exponer el puerto del contenedor (usualmente 80 en IIS)
EXPOSE 80
