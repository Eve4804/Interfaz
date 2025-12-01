# Instrucciones para instalar Npgsql en tu proyecto

## Problema
Tu proyecto usa .NET Framework 4.7.2 y necesitas **Npgsql versión 4.1.13** (no la 10.0.0 que es para .NET 8+).

## Solución: Instalar desde Visual Studio

### Opción 1: Usando el Administrador de Paquetes NuGet (Recomendado)

1. Abre tu proyecto en **Visual Studio 2022**
2. Haz clic derecho en el proyecto "Interfaz" en el Explorador de soluciones
3. Selecciona **"Administrar paquetes NuGet..."**
4. Ve a la pestaña **"Examinar"**
5. Busca **"Npgsql"**
6. Selecciona la versión **4.1.13** (NO la 10.0.0)
7. Haz clic en **"Instalar"**
8. Acepta las licencias

### Opción 2: Usando la Consola del Administrador de Paquetes

1. En Visual Studio, ve a **Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes**
2. Ejecuta este comando:
   ```
   Install-Package Npgsql -Version 4.1.13
   ```

## Verificar la instalación

Después de instalar, deberías ver:
- Una carpeta `packages` en la raíz de tu solución
- Referencias a Npgsql en tu proyecto
- El archivo `packages.config` actualizado

## Versiones compatibles con .NET Framework 4.7.2

- ✅ Npgsql 4.1.x (Recomendado)
- ✅ Npgsql 5.0.x (También funciona)
- ❌ Npgsql 6.0+ (Requiere .NET 6+)
- ❌ Npgsql 10.0+ (Requiere .NET 8+)

## Próximo paso: Crear clase de conexión

Una vez instalado Npgsql, puedes crear una clase para manejar la conexión a PostgreSQL.

Ejemplo básico:

```csharp
using Npgsql;
using System;

namespace Interfaz
{
    public class DatabaseConnection
    {
        private string connectionString = "Host=localhost;Port=5432;Database=gestion_ventas;Username=postgres;Password=tu_password";

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de conexión: {ex.Message}");
                return false;
            }
        }
    }
}
```

## Configurar la cadena de conexión

Actualiza estos valores en la cadena de conexión:
- **Host**: Dirección del servidor PostgreSQL (localhost si es local)
- **Port**: Puerto (por defecto 5432)
- **Database**: gestion_ventas (o el nombre que le hayas puesto)
- **Username**: Tu usuario de PostgreSQL
- **Password**: Tu contraseña de PostgreSQL

## Recursos adicionales

- Documentación de Npgsql: https://www.npgsql.org/doc/
- Ejemplos de uso: https://www.npgsql.org/doc/basic-usage.html
