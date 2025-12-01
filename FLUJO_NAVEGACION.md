# 🔄 Flujo de Navegación del Sistema ERP

## 📊 Diagrama de Flujo

```
┌─────────────────────────────────────────────────────────────┐
│                      INICIO DE SESIÓN                        │
│                     (MainWindow.xaml)                        │
│                                                              │
│  Credenciales de prueba:                                    │
│  • admin / admin123                                         │
│  • vendedor / vend123                                       │
│                                                              │
│  Botones:                                                   │
│  ├─ [INICIAR SESIÓN] ──────────────────────┐               │
│  └─ [¿Olvidaste tu contraseña?] ───────┐   │               │
└────────────────────────────────────────┼───┼───────────────┘
                                         │   │
                                         │   └──> RecuperarCont.xaml
                                         │
                                         ▼
┌─────────────────────────────────────────────────────────────┐
│                      MENÚ PRINCIPAL                          │
│                    (MenuWindow.xaml)                         │
│                                                              │
│  Barra Superior:                                            │
│  ├─ Usuario actual                                          │
│  ├─ [🔔 Notificaciones] ────────────────────┐              │
│  ├─ [⚙️ Configuración]                      │              │
│  └─ [🚪 Cerrar Sesión] ──> Volver a Login   │              │
│                                              │              │
│  ┌──────────────────────────────────────────┼──────────┐   │
│  │  📈 VENTAS Y CLIENTES                    │          │   │
│  ├──────────────────────────────────────────┼──────────┤   │
│  │  💰 Gestión de Ventas ───────────────────┼────┐     │   │
│  │  📊 Historial de Ventas ─────────────────┼──┐ │     │   │
│  │  👥 Gestión de Clientes ─────────────────┼┐ │ │     │   │
│  └──────────────────────────────────────────┼┼─┼─┼─────┘   │
│                                              ││ │ │         │
│  ┌──────────────────────────────────────────┼┼─┼─┼─────┐   │
│  │  📦 INVENTARIO Y PRODUCTOS               ││ │ │     │   │
│  ├──────────────────────────────────────────┼┼─┼─┼─────┤   │
│  │  📦 Inventarios ──────────────────────────┼┼─┼┼┐    │   │
│  │  📋 Catálogo de Productos ───────────────┼┼─┼┼│┐   │   │
│  │  📅 Calcular Entrega ─────────────────────┼┼─┼┼││   │   │
│  └──────────────────────────────────────────┼┼─┼┼┼┼┼───┘   │
│                                              ││ │││││       │
│  ┌──────────────────────────────────────────┼┼─┼┼┼┼┼───┐   │
│  │  🏢 PROVEEDORES Y PAGOS                  ││ │││││   │   │
│  ├──────────────────────────────────────────┼┼─┼┼┼┼┼───┤   │
│  │  📞 Contactar Proveedores ───────────────┼┼─┼┼┼┼┼┐  │   │
│  │  💳 Gestión de Pagos ─────────────────────┼┼─┼┼┼┼┼┐ │   │
│  └──────────────────────────────────────────┼┼─┼┼┼┼┼┼─┘   │
└─────────────────────────────────────────────┼┼─┼┼┼┼┼┼─────┘
                                              ││ │││││││
                    ┌─────────────────────────┘│ │││││││
                    │  ┌───────────────────────┘ │││││││
                    │  │  ┌──────────────────────┘││││││
                    │  │  │  ┌────────────────────┘│││││
                    │  │  │  │  ┌──────────────────┘││││
                    │  │  │  │  │  ┌────────────────┘│││
                    │  │  │  │  │  │  ┌──────────────┘││
                    │  │  │  │  │  │  │  ┌────────────┘│
                    │  │  │  │  │  │  │  │  ┌──────────┘
                    ▼  ▼  ▼  ▼  ▼  ▼  ▼  ▼  ▼
┌──────────────┐ ┌──────────────┐ ┌──────────────┐
│ GestionVentas│ │  HisVentas   │ │Frm_clientes  │
│    .xaml     │ │    .xaml     │ │    .xaml     │
└──────────────┘ └──────────────┘ └──────────────┘

┌──────────────┐ ┌──────────────┐ ┌──────────────┐
│ Inventarios  │ │   Catalogo   │ │CalcularFecha │
│    .xaml     │ │    .xaml     │ │    .xaml     │
└──────────────┘ └──────────────┘ └──────────────┘

┌──────────────┐ ┌──────────────┐ ┌──────────────┐
│ContactarProv │ │    Pagos     │ │Notificaciones│
│    .xaml     │ │    .xaml     │ │    .xaml     │
└──────────────┘ └──────────────┘ └──────────────┘
```

## ✅ Conexiones Implementadas

### 1. Login → Menú Principal
**Archivo:** `MainWindow.xaml.cs`
```csharp
private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
{
    if (ValidarCredenciales(usuario, contrasena))
    {
        MenuWindow menuWindow = new MenuWindow(usuario);
        menuWindow.Show();
        this.Close();
    }
}
```

### 2. Login → Recuperar Contraseña
**Archivo:** `MainWindow.xaml.cs`
```csharp
private void OlvideContrasena_Click(object sender, RoutedEventArgs e)
{
    RecuperarCont recuperarWindow = new RecuperarCont();
    recuperarWindow.ShowDialog();
}
```

### 3. Menú → Gestión de Ventas
**Archivo:** `MenuWindow.xaml.cs`
```csharp
private void BtnGestionVentas_Click(object sender, MouseButtonEventArgs e)
{
    GestionVentas ventana = new GestionVentas();
    ventana.ShowDialog();
}
```

### 4. Menú → Historial de Ventas
```csharp
private void BtnHistorialVentas_Click(object sender, MouseButtonEventArgs e)
{
    HisVentas ventana = new HisVentas();
    ventana.ShowDialog();
}
```

### 5. Menú → Gestión de Clientes
```csharp
private void BtnClientes_Click(object sender, MouseButtonEventArgs e)
{
    Frm_clientes ventana = new Frm_clientes();
    ventana.ShowDialog();
}
```

### 6. Menú → Inventarios
```csharp
private void BtnInventarios_Click(object sender, MouseButtonEventArgs e)
{
    Inventarios ventana = new Inventarios();
    ventana.ShowDialog();
}
```

### 7. Menú → Catálogo de Productos
```csharp
private void BtnCatalogo_Click(object sender, MouseButtonEventArgs e)
{
    Catalogo ventana = new Catalogo();
    ventana.ShowDialog();
}
```

### 8. Menú → Calcular Fecha de Entrega
```csharp
private void BtnCalcularFecha_Click(object sender, MouseButtonEventArgs e)
{
    CalcularFecha ventana = new CalcularFecha();
    ventana.ShowDialog();
}
```

### 9. Menú → Contactar Proveedores
```csharp
private void BtnContactarProveedor_Click(object sender, MouseButtonEventArgs e)
{
    ContactarProv ventana = new ContactarProv();
    ventana.ShowDialog();
}
```

### 10. Menú → Gestión de Pagos
```csharp
private void BtnPagos_Click(object sender, MouseButtonEventArgs e)
{
    Pagos ventana = new Pagos();
    ventana.ShowDialog();
}
```

### 11. Menú → Notificaciones
```csharp
private void BtnNotificaciones_Click(object sender, RoutedEventArgs e)
{
    Notificaciones ventana = new Notificaciones();
    ventana.ShowDialog();
}
```

### 12. Menú → Cerrar Sesión (Volver a Login)
```csharp
private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
{
    var resultado = MessageBox.Show(
        "¿Está seguro que desea cerrar sesión?",
        "Cerrar Sesión",
        MessageBoxButton.YesNo,
        MessageBoxImage.Question);

    if (resultado == MessageBoxResult.Yes)
    {
        MainWindow loginWindow = new MainWindow();
        loginWindow.Show();
        this.Close();
    }
}
```

## 🔄 Tipos de Navegación

### ShowDialog() - Ventanas Modales
Todas las ventanas secundarias se abren como **modales** usando `ShowDialog()`:
- El usuario debe cerrar la ventana modal antes de volver al menú
- El menú principal queda bloqueado hasta que se cierre la ventana
- Mejor control del flujo de la aplicación

### Show() - Ventanas No Modales
Solo se usa en:
- Login → Menú Principal
- Cerrar Sesión → Login

## 📝 Credenciales de Prueba

| Usuario   | Contraseña | Rol       |
|-----------|------------|-----------|
| admin     | admin123   | Admin     |
| vendedor  | vend123    | Vendedor  |

## ✅ Estado de Conexiones

| Ventana              | Estado | Conectada desde    |
|---------------------|--------|--------------------|
| MainWindow          | ✅     | Inicio             |
| MenuWindow          | ✅     | MainWindow         |
| GestionVentas       | ✅     | MenuWindow         |
| HisVentas           | ✅     | MenuWindow         |
| Frm_clientes        | ✅     | MenuWindow         |
| Inventarios         | ✅     | MenuWindow         |
| Catalogo            | ✅     | MenuWindow         |
| CalcularFecha       | ✅     | MenuWindow         |
| ContactarProv       | ✅     | MenuWindow         |
| Pagos               | ✅     | MenuWindow         |
| Notificaciones      | ✅     | MenuWindow         |
| RecuperarCont       | ✅     | MainWindow         |

## 🎯 Flujo de Usuario Típico

1. **Inicio:** Usuario abre la aplicación
2. **Login:** Ingresa credenciales (admin/admin123)
3. **Menú:** Ve el dashboard con todas las opciones
4. **Selección:** Hace clic en "Gestión de Ventas"
5. **Módulo:** Se abre la ventana modal de ventas
6. **Trabajo:** Realiza operaciones en el módulo
7. **Cierre:** Cierra la ventana modal
8. **Retorno:** Vuelve automáticamente al menú
9. **Repetir:** Puede abrir otros módulos
10. **Salir:** Cierra sesión y vuelve al login

## 🔧 Cómo Probar la Navegación

1. Ejecuta la aplicación
2. Ingresa: `admin` / `admin123`
3. Haz clic en cualquier tarjeta del menú
4. Verifica que se abre la ventana correspondiente
5. Cierra la ventana (X o botón cerrar)
6. Verifica que vuelves al menú
7. Prueba el botón "Cerrar Sesión"
8. Verifica que vuelves al login

## ✨ Características

- ✅ Navegación fluida entre ventanas
- ✅ Ventanas modales para mejor control
- ✅ Validación de credenciales
- ✅ Confirmación al cerrar sesión
- ✅ Paso de parámetros (usuario actual)
- ✅ Cierre automático de ventanas anteriores

---

*Sistema ERP v1.0.0 - Navegación Completa*
*Última actualización: 30/nov/2025*
