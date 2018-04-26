# Fantur

Este es un servicio web escrito en .Net Core, con clientes escritos en Angular.js y (probablemente) Android.

## Dependencias

Para correr el sitio de Fantur (que todavía no tiene nada) tienen que tener

1. [El runtime de .NET Core 2.0](https://www.microsoft.com/net/download/dotnet-core/runtime-2.0.5) para correr el backend
2. [PostgreSQL 10.x](https://www.postgresql.org/download/windows/) como base de datos
3. [NodeJS LTS](https://nodejs.org/es/) para poder compilar con webpack el proyecto Angular.JS

## Configuración

La configuración para conectar a la base de datos se levanta del archivo appsettings.json si el proyecto está corriendo en producción, o de appsettings.Development.json si está corriendo en modo development.

## Development

### Para Windows

* Necesitan una IDE, si usan [Visual Studio Community Edition](https://www.visualstudio.com/es/downloads) en el instalador les ofrece ya instalar el SDK de .NET Core
* Si no van a usar Visual Studio, pueden optar por [Visual Studio Code](https://code.visualstudio.com/) o por una IDE mas polenta como [Rider](https://www.jetbrains.com/rider/) (Aunque ésta última es paga) y van a tener que bajar a parte el [SDK de .NET Core 2.0](https://www.microsoft.com/net/download/windows)

### Para Linux

* Como IDE todo el mundo recomienda [Visual Studio Code](https://code.visualstudio.com/)
* El [SDK](https://www.microsoft.com/net/download/linux/build) si o si lo tienen que bajar, o instalar con el administrador de paquetes de su distribución si es que está en los repos de Microsoft

## Correr por primera vez

1. Clonen el repo
2. En consola de comandos se paran en la carpeta del proyecto de ASP.NET Core y ejecutan `dotnet restore` para bajar las dependencias de .NET Core
3. Luego ejecutan `dotnet ef migrations` para aplicar las migraciones a la base de datos (esto va a crear las tablas en la base de datos)
4. El próximo paso es instalar los paquetes de Node. Para esto, también en consola hay que correr `npm install`. Si algo sale mal, hay que borrar la carpeta node_modules, tratar de solucionar el problema y correr `npm install` de nuevo.
5. Por último, `dotnet run`, y les debería compilar y correr el sitio.