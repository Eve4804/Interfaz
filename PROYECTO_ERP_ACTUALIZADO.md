# 📊 Proyecto ERP - Sistema de Gestión

## ✅ Actualización Completada

El proyecto ha sido actualizado como un módulo ERP profesional basado en el esquema de base de datos PostgreSQL.

## 🎨 Interfaz Actualizada

### 1. Login (MainWindow)
- ✅ Diseño moderno y profesional
- ✅ Logo corporativo
- ✅ Validación de credenciales
- ✅ Opción "Recordar sesión"
- ✅ Enlace "Olvidé contraseña"

**Credenciales de prueba:**
- Usuario: `admin` / Contraseña: `admin123`
- Usuario: `vendedor` / Contraseña: `vend123`

### 2. Menú Principal (MenuWindow)
- ✅ Diseño tipo dashboard
- ✅ Organizado por secciones
- ✅ Tarjetas interactivas con iconos
- ✅ Barra superior con usuario actual
- ✅ Botón de notificaciones
- ✅ Botón de cerrar sesión

## 📋 Módulos del Sistema

### 📈 Ventas y Clientes
1. **Gestión de Ventas** - Crear y administrar ventas
2. **Historial de Ventas** - Consultar ventas realizadas
3. **Gestión de Clientes** - Administrar clientes

### 📦 Inventario y Productos
4. **Inventarios** - Control de stock
5. **Catálogo de Productos** - Gestionar productos
6. **Calcular Entrega** - Fechas de entrega

### 🏢 Proveedores y Pagos
7. **Contactar Proveedores** - Solicitudes a proveedores
8. **Gestión de Pagos** - Pagos a proveedores

### 🔔 Otros
9. **Notificaciones** - Sistema de alertas

## 📊 Modelos de Datos

Todos los modelos están definidos en `Models.cs`:

- ✅ Cliente
- ✅ Categoria
- ✅ Producto
- ✅ Inventario
- ✅ Proveedor
- ✅ SolicitudProveedor
- ✅ Pago
- ✅ Venta
- ✅ VentaItem
- ✅ Usuario
- ✅ Notificacion
- ✅ HistorialInventario

## 🗄️ Base de Datos

El esquema completo está en `Documentacion/database_schema.sql` con:

- 12 tablas principales
- Índices para rendimiento
- Triggers automáticos
- Vistas para reportes
- Datos de prueba

## 🎯 Características

### Diseño
- ✅ Interfaz moderna y profesional
- ✅ Colores corporativos (azul #2196F3)
- ✅ Iconos emoji para mejor UX
- ✅ Sombras y efectos visuales
- ✅ Responsive (se adapta al tamaño de ventana)

### Funcionalidad
- ✅ Sistema de login
- ✅ Navegación entre módulos
- ✅ Ventanas modales (ShowDialog)
- ✅ Cierre de sesión
- ✅ Validaciones de formularios

### Arquitectura
- ✅ Separación de capas (UI, Modelos)
- ✅ Código limpio y comentado
- ✅ Preparado para conectar con PostgreSQL
- ✅ Npgsql 4.1.13 instalado

## 📁 Estructura del Proyecto

```
Interfaz/
├── MainWindow.xaml/.cs          # Login
├── MenuWindow.xaml/.cs          # Menú principal
├── Models.cs                    # Modelos de datos
├── GestionVentas.xaml/.cs       # Gestión de ventas
├── HisVentas.xaml/.cs           # Historial de ventas
├── Frm_clientes.xaml/.cs        # Gestión de clientes
├── Inventarios.xaml/.cs         # Control de inventario
├── Catalogo.xaml/.cs            # Catálogo de productos
├── CalcularFecha.xaml/.cs       # Calcular entregas
├── ContactarProv.xaml/.cs       # Contactar proveedores
├── Pagos.xaml/.cs               # Gestión de pagos
├── Notificaciones.xaml/.cs      # Sistema de notificaciones
└── Documentacion/
    ├── database_schema.sql      # Esquema de BD
    └── README.md                # Documentación

## 🚀 Cómo Usar

### 1. Compilar
Desde Visual Studio 2022 Community:
- Presiona F5 o usa "Iniciar"

### 2. Ejecutar
1. Se abre la ventana de login
2. Ingresa credenciales (admin/admin123)
3. Accede al menú principal
4. Haz clic en cualquier módulo para abrirlo

### 3. Conectar a Base de Datos
Para conectar a PostgreSQL:
1. Crea la base de datos ejecutando `database_schema.sql`
2. Crea una clase `DatabaseConnection.cs`
3. Implementa los métodos de acceso a datos
4. Usa los modelos de `Models.cs`

## 📝 Próximos Pasos

### Implementación Pendiente
- [ ] Conectar login con tabla `usuarios`
- [ ] Implementar CRUD en cada módulo
- [ ] Agregar validaciones de negocio
- [ ] Implementar sistema de notificaciones
- [ ] Agregar reportes y gráficas
- [ ] Implementar permisos por rol

### Mejoras Sugeridas
- [ ] Agregar búsqueda global
- [ ] Implementar filtros avanzados
- [ ] Agregar exportación a Excel/PDF
- [ ] Implementar dashboard con estadísticas
- [ ] Agregar modo oscuro
- [ ] Implementar multi-idioma

## 🎨 Paleta de Colores

- **Primario:** #2196F3 (Azul)
- **Secundario:** #4CAF50 (Verde)
- **Acento:** #FF9800 (Naranja)
- **Error:** #F44336 (Rojo)
- **Fondo:** #F5F5F5 (Gris claro)
- **Texto:** #333333 (Gris oscuro)

## 📚 Tecnologías

- **Framework:** .NET Framework 4.7.2
- **UI:** WPF (Windows Presentation Foundation)
- **Base de Datos:** PostgreSQL
- **Driver:** Npgsql 4.1.13
- **IDE:** Visual Studio 2022 Community

---

*Proyecto actualizado: 30/nov/2025*
*Sistema ERP v1.0.0*
