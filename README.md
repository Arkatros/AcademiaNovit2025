# Academia Novit 2025

## Requisitos

Antes de ejecutar el proyecto, asegúrate de cumplir con los siguientes requisitos:

1. **.NET SDK**: Instala la versión 8.0 o superior del SDK de .NET. Puedes descargarlo desde [dotnet.microsoft.com](https://dotnet.microsoft.com/).
2. **Visual Studio (opcional)**: Si prefieres usar un entorno gráfico, instala Visual Studio 2022 o superior con la carga de trabajo de desarrollo web y de escritorio .NET.
3. Instalar Entity Framework Core Tools [ver documentación](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

```sh
dotnet tool install --global dotnet-ef
```

## Configuración

1. Clona este repositorio en tu máquina local:
```bash
git clone https://github.com/tu-usuario/AcademiaNovit2025.git
cd AcademiaNovit2025
```

2. Restaura las dependencias del proyecto:
```bash
dotnet restore
```

## Base de datos (Postgres con Docker)

Para levantar una instancia de Postgres como container de docker se debe:
- Instalar Docker Desktop (en Windows)
- Desde la terminal/CMD/powershell ejecutar el siguiente comando:

```sh
docker run --name academianovit -p 5432:5432 -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=tivon1234 -d postgres:17.5
```

## Ejecución del Proyecto

Desde la Línea de Comandos
1. Navega al directorio del proyecto principal de la Web API:

```bash
cd AcademiaNovit
```

2. Ejecuta el proyecto:

```bash
dotnet run
```

3. Accede a la aplicación en tu navegador en http://localhost:5286/scalar

Desde Visual Studio

1. Abre el archivo de solución `AcademiaNovit.sln` en Visual Studio.
2. Selecciona el proyecto `AcademiaNovit` como proyecto de inicio.
3. Presiona `F5` o haz clic en el botón de "Iniciar" para ejecutar la aplicación.
4. Accede a la aplicación en tu navegador en http://localhost:5286/scalar

## Pruebas
Para ejecutar las pruebas unitarias, utiliza el siguiente comando en la raíz del proyecto:

```bash
dotnet test
```

Estructura del Proyecto
- **AcademiaNovit/:** Contiene el código fuente principal de la aplicación.
- **AcademiaNovit.Tests/:** Contiene las pruebas unitarias del proyecto.
- **Migrations/:** Contiene las migraciones de la base de datos generadas por Entity Framework.
- **Util/:** Contiene utilidades como validadores.

## Notas
- Si necesitas cambiar la configuración de la base de datos, edita el archivo `appsettings.json` o `appsettings.Development.json`.
- Asegúrate de que el archivo `Data.db` esté en el directorio raíz del proyecto `AcademiaNovit` para que la base de datos funcione correctamente.
