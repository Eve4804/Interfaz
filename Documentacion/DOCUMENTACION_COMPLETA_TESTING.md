# DOCUMENTACIÓN COMPLETA - REQUERIMIENTOS Y CASOS DE PRUEBA
## SISTEMA ERP - GESTIÓN EMPRESARIAL

---

# PARTE 1: TABLA DE REQUERIMIENTOS

| ID | Nombre del Requerimiento | Descripción | Funcional/No Funcional | Check |
|----|-------------------------|-------------|------------------------|-------|
| **MÓDULO: AUTENTICACIÓN Y SEGURIDAD** |
| R001 | Inicio de Sesión | El sistema debe permitir a los administradores iniciar sesión con usuario y contraseña | Funcional | ☐ |
| R002 | Validación de Campos Vacíos | El sistema debe validar que los campos de usuario y contraseña no estén vacíos | Funcional | ☐ |
| R003 | Validación de Longitud de Contraseña | El sistema debe validar que la contraseña tenga exactamente 8 caracteres | Funcional | ☐ |
| R004 | Autenticación con Base de Datos | El sistema debe verificar las credenciales contra la tabla 'administradores' en PostgreSQL | Funcional | ☐ |
| R005 | Mensajes de Error Específicos | El sistema debe mostrar mensajes de error específicos según el tipo de validación fallida | Funcional | ☐ |
| R006 | Recuperación de Contraseña | El sistema debe permitir recuperar/cambiar contraseña mediante ID de usuario | Funcional | ☐ |
| R007 | Validación de ID Numérico | El sistema debe validar que el ID de usuario sea un valor numérico válido | Funcional | ☐ |
| R008 | Validación de Formato de Contraseña | La nueva contraseña debe contener solo letras y números, máximo 8 caracteres | Funcional | ☐ |
| R009 | Confirmación de Contraseña | El sistema debe validar que la contraseña y su confirmación coincidan | Funcional | ☐ |
| R010 | Actualización de Contraseña en BD | El sistema debe actualizar la contraseña en la base de datos correctamente | Funcional | ☐ |
| R011 | Conexión a Base de Datos | El sistema debe establecer conexión con PostgreSQL al iniciar | No Funcional | ☐ |
| **MÓDULO: MENÚ PRINCIPAL** |
| R012 | Dashboard Principal | El sistema debe mostrar un menú principal tipo dashboard con todas las opciones disponibles | Funcional | ☐ |
| R013 | Visualización de Usuario Activo | El sistema debe mostrar el nombre del usuario que inició sesión | Funcional | ☐ |
| R014 | Navegación a Gestión de Ventas | El sistema debe permitir acceder al módulo de gestión de ventas | Funcional | ☐ |
| R015 | Navegación a Historial de Ventas | El sistema debe permitir acceder al historial de ventas | Funcional | ☐ |
| R016 | Navegación a Clientes | El sistema debe permitir acceder al módulo de gestión de clientes | Funcional | ☐ |
| R017 | Navegación a Inventarios | El sistema debe permitir acceder al módulo de inventarios | Funcional | ☐ |
| R018 | Navegación a Catálogo | El sistema debe permitir acceder al catálogo de productos | Funcional | ☐ |
| R019 | Navegación a Contactar Proveedor | El sistema debe permitir acceder al módulo de contacto con proveedores | Funcional | ☐ |
| R020 | Navegación a Pagos | El sistema debe permitir acceder al módulo de registro de pagos | Funcional | ☐ |
| R021 | Navegación a Notificaciones | El sistema debe permitir acceder al módulo de notificaciones | Funcional | ☐ |
| R022 | Cerrar Sesión | El sistema debe permitir cerrar sesión y regresar a la pantalla de login | Funcional | ☐ |
| R023 | Confirmación de Cierre de Sesión | El sistema debe solicitar confirmación antes de cerrar sesión | Funcional | ☐ |
| R024 | Ventanas Modales | Todas las ventanas secundarias deben abrirse como modales (ShowDialog) | No Funcional | ☐ |
| **MÓDULO: GESTIÓN DE VENTAS** |
| R025 | Visualización de Pedidos por Estado | El sistema debe mostrar pedidos organizados en pestañas: Nuevos, Pendientes, Cancelados | Funcional | ☐ |
| R026 | Carga de Ventas desde BD | El sistema debe cargar todas las ventas desde la tabla 'ventas' con JOIN a 'clientes' | Funcional | ☐ |
| R027 | Filtrado de Pedidos Nuevos | El sistema debe filtrar y mostrar pedidos con estado 'Pendiente' | Funcional | ☐ |
| R028 | Filtrado de Pedidos Pendientes | El sistema debe filtrar y mostrar pedidos con estado 'Confirmada' o 'En tránsito' | Funcional | ☐ |
| R029 | Filtrado de Pedidos Cancelados | El sistema debe filtrar y mostrar pedidos con estado 'Cancelada' | Funcional | ☐ |
| R030 | Búsqueda de Ventas | El sistema debe permitir buscar ventas por ID o nombre de cliente | Funcional | ☐ |
| R031 | Confirmar Pedido | El sistema debe permitir cambiar el estado de un pedido a 'Confirmada' | Funcional | ☐ |
| R032 | Cancelar Pedido | El sistema debe permitir cambiar el estado de un pedido a 'Cancelada' | Funcional | ☐ |
| R033 | Confirmación de Acciones | El sistema debe solicitar confirmación antes de confirmar o cancelar pedidos | Funcional | ☐ |
| R034 | Ver Detalles de Pedido | El sistema debe mostrar todos los detalles de un pedido seleccionado | Funcional | ☐ |
| R035 | Actualización de Fecha de Modificación | El sistema debe actualizar automáticamente fecha_modificacion al cambiar estado | Funcional | ☐ |
| R036 | Contador de Pedidos | El sistema debe mostrar el total de pedidos en cada categoría | Funcional | ☐ |
| R037 | Actualización Automática | El sistema debe actualizar las listas después de cada operación | Funcional | ☐ |
| **MÓDULO: INVENTARIOS** |
| R038 | Visualización de Inventario por Categorías | El sistema debe mostrar inventario organizado en pestañas: Cartón, Plásticos, Vehículos | Funcional | ☐ |
| R039 | Carga de Inventario con LEFT JOIN | El sistema debe cargar productos con LEFT JOIN para mostrar productos sin inventario | Funcional | ☐ |
| R040 | Visualización de Productos sin Inventario | El sistema debe mostrar productos con cantidad 0 y estado 'Sin inventario' | Funcional | ☐ |
| R041 | Filtrado por Categoría | El sistema debe filtrar productos según id_categoria (1=Cartón, 2=Plásticos, 3=Vehículos) | Funcional | ☐ |
| R042 | Búsqueda de Productos | El sistema debe permitir buscar productos por ID o nombre | Funcional | ☐ |
| R043 | Búsqueda por ID Numérico | El sistema debe buscar por ID exacto cuando el criterio es numérico | Funcional | ☐ |
| R044 | Búsqueda por Nombre con ILIKE | El sistema debe buscar por nombre usando coincidencia parcial (ILIKE) | Funcional | ☐ |
| R045 | Limpieza de Barra de Búsqueda | El sistema debe limpiar automáticamente la barra de búsqueda después de mostrar resultados | Funcional | ☐ |
| R046 | Mensaje Sin Resultados | El sistema debe mostrar mensaje cuando no se encuentran productos | Funcional | ☐ |
| R047 | Actualizar Inventario | El sistema debe permitir abrir ventana de actualización de inventario | Funcional | ☐ |
| R048 | Contactar Proveedor desde Inventario | El sistema debe permitir acceder a contactar proveedor desde inventarios | Funcional | ☐ |
| R049 | Visualización de Fecha de Actualización | El sistema debe mostrar la fecha de última actualización de cada producto | Funcional | ☐ |

