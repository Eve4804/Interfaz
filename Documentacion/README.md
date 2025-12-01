# 📚 Documentación del Sistema de Gestión de Ventas

Bienvenido a la documentación del proyecto. Aquí encontrarás toda la información necesaria para trabajar con PostgreSQL y Npgsql.

## 📋 Índice de Documentos

### 1. 🔧 [INSTRUCCIONES_NPGSQL.md](INSTRUCCIONES_NPGSQL.md)
**Instalación de Npgsql**
- Cómo instalar Npgsql 4.1.13 en Visual Studio
- Versiones compatibles con .NET Framework 4.7.2
- Solución de problemas comunes
- Ejemplo básico de clase de conexión

### 2. 🗄️ [database_schema.sql](database_schema.sql)
**Script de Base de Datos**
- Script completo para crear todas las tablas en PostgreSQL
- Incluye:
  - Tablas (clientes, productos, ventas, inventarios, etc.)
  - Índices para mejorar rendimiento
  - Triggers automáticos
  - Vistas útiles
  - Funciones de utilidad
  - Datos iniciales de ejemplo

### 3. 📖 [GUIA_USO_DATABASE.md](GUIA_USO_DATABASE.md)
**Guía Completa de Uso**
- Configuración inicial de la conexión
- Uso de las clases Repository (ClienteRepository, VentaRepository)
- Ejemplos de uso en Frm_clientes.xaml.cs y HisVentas.xaml.cs
- Métodos disponibles para cada entidad
- Mejores prácticas

### 4. 💡 [EJEMPLO_USO_CONEXION.md](EJEMPLO_USO_CONEXION.md)
**Ejemplos Prácticos**
- 9 ejemplos completos de código
- Consultas SELECT, INSERT, UPDATE, DELETE
- Uso de transacciones
- Manejo de errores
- Diferentes formas de usar la conexión

## 🚀 Inicio Rápido

### Paso 1: Instalar Npgsql
Sigue las instrucciones en [INSTRUCCIONES_NPGSQL.md](INSTRUCCIONES_NPGSQL.md)

### Paso 2: Configurar la Conexión
Abre `DatabaseConnection.cs` y actualiza la cadena de conexión:

```csharp
private static string connectionString = 
    "Host=localhost;" +
    "Port=5432;" +
    "Database=gestion_ventas;" +
    "Username=postgres;" +
    "Password=TU_PASSWORD_AQUI";  // ⚠️ CAMBIA ESTO
```

### Paso 3: Crear la Base de Datos
Ejecuta el script [database_schema.sql](database_schema.sql) en PostgreSQL:

```bash
psql -U postgres -f database_schema.sql
```

O desde pgAdmin:
1. Crea una base de datos llamada `gestion_ventas`
2. Abre el Query Tool
3. Carga y ejecuta el archivo `database_schema.sql`

### Paso 4: Probar la Conexión
Agrega este código en cualquier ventana:

```csharp
if (DatabaseConnection.TestConnection())
{
    MessageBox.Show("✅ Conexión exitosa!");
}
```

### Paso 5: Usar los Ejemplos
Consulta [EJEMPLO_USO_CONEXION.md](EJEMPLO_USO_CONEXION.md) para ver ejemplos de:
- Consultar datos
- Insertar registros
- Actualizar información
- Eliminar/desactivar registros
- Usar transacciones

## 📊 Estructura de la Base de Datos

### Tablas Principales
- **clientes** - Información de clientes
- **productos** - Catálogo de productos
- **categorias** - Categorías de productos
- **inventarios** - Control de stock
- **ventas** - Encabezado de ventas
- **venta_items** - Detalle de productos vendidos
- **proveedores** - Información de proveedores
- **solicitudes_proveedor** - Solicitudes de compra
- **pagos** - Registro de pagos
- **usuarios** - Sistema de login
- **notificaciones** - Sistema de notificaciones
- **historial_inventario** - Auditoría de movimientos

## 🔑 Clases Disponibles

### DatabaseConnection
Clase principal para manejar la conexión a PostgreSQL.

**Métodos:**
- `Conexion` - Propiedad para obtener la conexión
- `AbrirConexion()` - Abre la conexión
- `CerrarConexion()` - Cierra la conexión
- `TestConnection()` - Prueba la conexión
- `GetConnection()` - Nueva conexión para usar con `using`
- `SetConnectionString()` - Configura la cadena de conexión

### ClienteRepository
Operaciones CRUD para clientes.

**Métodos:**
- `ObtenerTodos()` - Lista todos los clientes activos
- `BuscarPorNombreORFC()` - Busca un cliente
- `Activar()` - Activa un cliente
- `Desactivar()` - Desactiva un cliente
- `Insertar()` - Registra un nuevo cliente

### VentaRepository
Operaciones para ventas.

**Métodos:**
- `ObtenerTodas()` - Lista todas las ventas
- `Buscar()` - Busca ventas por cliente y producto

## 💻 Ejemplos de Código

### Ejemplo 1: Consultar Clientes
```csharp
var clientes = ClienteRepository.ObtenerTodos();
dgClientes.ItemsSource = clientes;
```

### Ejemplo 2: Buscar un Cliente
```csharp
var cliente = ClienteRepository.BuscarPorNombreORFC("Juan");
if (cliente != null)
{
    txtNombre.Text = cliente.Nombre;
}
```

### Ejemplo 3: Insertar Cliente
```csharp
var nuevoCliente = new Cliente
{
    RFC = "ABC123",
    Nombre = "Juan Pérez",
    Tipo = "Regular"
};

if (ClienteRepository.Insertar(nuevoCliente))
{
    MessageBox.Show("Cliente guardado!");
}
```

### Ejemplo 4: Consulta Personalizada
```csharp
using (var conexion = DatabaseConnection.GetConnection())
{
    conexion.Open();
    var cmd = new NpgsqlCommand("SELECT * FROM productos", conexion);
    var reader = cmd.ExecuteReader();
    
    while (reader.Read())
    {
        // Procesar datos
    }
}
```

## ⚠️ Notas Importantes

1. **Seguridad**: Nunca subas tu contraseña de base de datos al repositorio
2. **Parámetros**: Siempre usa parámetros (@parametro) en las consultas SQL
3. **Conexiones**: Cierra las conexiones después de usarlas
4. **Excepciones**: Maneja los errores con try-catch
5. **Validación**: Valida los datos antes de insertarlos

## 🆘 Solución de Problemas

### Error: "No se pudo conectar"
- Verifica que PostgreSQL esté corriendo
- Revisa la cadena de conexión (host, puerto, usuario, contraseña)
- Verifica que la base de datos exista

### Error: "Tabla no existe"
- Ejecuta el script `database_schema.sql`
- Verifica que estés conectado a la base de datos correcta

### Error: "Npgsql no encontrado"
- Reinstala Npgsql desde el Administrador de paquetes NuGet
- Usa la versión 4.1.13 (no la 10.0.0)

## 📞 Recursos Adicionales

- [Documentación de Npgsql](https://www.npgsql.org/doc/)
- [Tutorial de PostgreSQL](https://www.postgresql.org/docs/)
- [Ejemplos de SQL](https://www.postgresqltutorial.com/)

## 📝 Estructura del Proyecto

```
Interfaz/
├── Documentacion/
│   ├── README.md (este archivo)
│   ├── INSTRUCCIONES_NPGSQL.md
│   ├── GUIA_USO_DATABASE.md
│   ├── EJEMPLO_USO_CONEXION.md
│   └── database_schema.sql
├── DatabaseConnection.cs
├── Models.cs
└── [Archivos XAML y code-behind]
```

---

**¡Éxito con tu proyecto!** 🚀

Si tienes dudas, consulta los archivos de documentación específicos o revisa los ejemplos de código.
