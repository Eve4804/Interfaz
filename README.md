# Sistema de Gestión de Ventas - Interfaz

Proyecto WPF con .NET Framework 4.7.2

## 📦 Paquetes Instalados

- **Npgsql 4.1.13** - Driver de PostgreSQL para .NET
- System.Runtime.CompilerServices.Unsafe 4.5.3
- System.Memory 4.5.4
- System.Buffers 4.5.1
- System.Threading.Tasks.Extensions 4.5.4
- System.ValueTuple 4.5.0
- System.Numerics.Vectors 4.5.0

## 🚀 Compilar y Ejecutar

### Desde Visual Studio 2022 Community
1. Abre `Interfaz.sln`
2. Presiona F5 o usa "Iniciar"

### Desde Línea de Comandos
```powershell
# Compilar
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" Interfaz.sln /t:Build /p:Configuration=Debug

# Ejecutar
.\bin\Debug\Interfaz.exe
```

## 📁 Estructura del Proyecto

```
Interfaz/
├── App.xaml / App.xaml.cs          # Aplicación principal
├── MainWindow.xaml / .cs           # Ventana de login
├── MenuWindow.xaml / .cs           # Menú principal
├── Models.cs                       # Modelos de datos
├── Interfaz.csproj                 # Configuración del proyecto
├── App.config                      # Configuración de la aplicación
├── packages.config                 # Paquetes NuGet
└── Documentacion/
    ├── README.md                   # Documentación general
    ├── database_schema.sql         # Esquema de base de datos
    └── INSTRUCCIONES_NPGSQL.md     # Guía de Npgsql
```

## 🗄️ Uso de Npgsql

Ejemplo básico de conexión a PostgreSQL:

```csharp
using Npgsql;

var connectionString = "Host=localhost;Port=5432;Database=mibd;Username=postgres;Password=mipass";

using (var conn = new NpgsqlConnection(connectionString))
{
    conn.Open();
    
    using (var cmd = new NpgsqlCommand("SELECT version()", conn))
    {
        var version = cmd.ExecuteScalar();
        Console.WriteLine($"PostgreSQL: {version}");
    }
}
```

## 📝 Notas

- El proyecto usa .NET Framework 4.7.2
- Npgsql 4.1.13 es compatible con esta versión
- Los binding redirects están configurados en App.config
- El proyecto compila sin errores

## 🔧 Requisitos

- Visual Studio 2022 Community (o superior)
- .NET Framework 4.7.2
- PostgreSQL (opcional, solo si usas la base de datos)

---

*Proyecto limpio - Listo para desarrollo*
