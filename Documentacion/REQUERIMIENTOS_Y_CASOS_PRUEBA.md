# REQUERIMIENTOS Y CASOS DE PRUEBA - SISTEMA ERP

## TABLA DE REQUERIMIENTOS

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
| **MÓDULO: CATÁLOGO DE PRODUCTOS** |
| R050 | Visualización de Catálogo por Categorías | El sistema debe mostrar productos activos organizados en pestañas por categoría | Funcional | ☐ |
| R051 | Filtrado de Productos Activos | El sistema debe mostrar solo productos con estado 'Activo' | Funcional | ☐ |
| R052 | Visualización de Información Completa | El sistema debe mostrar: ID, nombre, descripción, precio, categoría y estado | Funcional | ☐ |
| R053 | Búsqueda en Catálogo | El sistema debe permitir buscar productos por ID, nombre, descripción o categoría | Funcional | ☐ |
| R054 | Búsqueda Múltiple | El sistema debe buscar en múltiples campos (nombre, descripción, categoría) | Funcional | ☐ |
| R055 | Cambio Automático de Pestaña | El sistema debe cambiar a la pestaña del primer resultado encontrado | Funcional | ☐ |
| R056 | Contador de Resultados | El sistema debe mostrar la cantidad de productos encontrados | Funcional | ☐ |
| R057 | Actualizar Catálogo | El sistema debe permitir abrir ventana de actualización de catálogo | Funcional | ☐ |
| R058 | Confirmación de Actualización | El sistema debe solicitar confirmación antes de actualizar catálogo | Funcional | ☐ |
| R059 | Recarga de Catálogo | El sistema debe recargar todos los productos activos al actualizar | Funcional | ☐ |
| R060 | Búsqueda con Enter | El sistema debe permitir buscar presionando la tecla Enter | Funcional | ☐ |
| **MÓDULO: GESTIÓN DE CLIENTES** |
| R061 | Búsqueda de Clientes | El sistema debe permitir buscar clientes por RFC o nombre | Funcional | ☐ |
| R062 | Búsqueda con ILIKE | El sistema debe usar coincidencia parcial para búsqueda de clientes | Funcional | ☐ |
| R063 | Carga de Datos de Cliente | El sistema debe cargar todos los datos del cliente al encontrarlo | Funcional | ☐ |
| R064 | Registro de Nuevo Cliente | El sistema debe permitir registrar nuevos clientes (botón Activar) | Funcional | ☐ |
| R065 | Validación de RFC | El sistema debe validar que el RFC tenga exactamente 13 caracteres | Funcional | ☐ |
| R066 | Validación de Nombre Obligatorio | El sistema debe validar que el nombre del cliente no esté vacío | Funcional | ☐ |
| R067 | Validación de Tipo de Cliente | El sistema debe validar que se seleccione tipo (Persona Física/Moral) | Funcional | ☐ |
| R068 | Validación de Estado | El sistema debe validar que se seleccione estado (Activo/Inactivo) | Funcional | ☐ |
| R069 | Actualización de Cliente | El sistema debe permitir actualizar datos de clientes existentes | Funcional | ☐ |
| R070 | Eliminación de Cliente | El sistema debe permitir eliminar clientes de la base de datos | Funcional | ☐ |
| R071 | Confirmación de Eliminación | El sistema debe solicitar confirmación antes de eliminar un cliente | Funcional | ☐ |
| R072 | Limpiar Formulario | El sistema debe permitir limpiar todos los campos del formulario | Funcional | ☐ |
| R073 | Control de Botones | El sistema debe deshabilitar botones Activar/Eliminar al editar cliente existente | Funcional | ☐ |
| R074 | Campos Opcionales | El sistema debe permitir campos opcionales: email, teléfono, direcciones | Funcional | ☐ |
| R075 | Registro de Fechas | El sistema debe registrar fecha_alta y fecha_modificacion automáticamente | Funcional | ☐ |
| **MÓDULO: CONTACTAR PROVEEDOR** |
| R076 | Autocompletado por ID Producto | El sistema debe autocompletar descripción y categoría al ingresar ID de producto | Funcional | ☐ |
| R077 | Carga de Proveedores por Categoría | El sistema debe cargar proveedores según la categoría del producto | Funcional | ☐ |
| R078 | Selección de Proveedor | El sistema debe permitir seleccionar proveedor de lista desplegable | Funcional | ☐ |
| R079 | Validación de Campos Obligatorios | El sistema debe validar que todos los campos estén completos antes de enviar | Funcional | ☐ |
| R080 | Validación de Duplicados | El sistema debe evitar solicitudes duplicadas (mismo proveedor, producto y fecha) | Funcional | ☐ |
| R081 | Registro de Solicitud | El sistema debe insertar solicitud en tabla 'solicitudes_proveedor' | Funcional | ☐ |
| R082 | Generación de ID de Solicitud | El sistema debe obtener el ID generado automáticamente (RETURNING) | Funcional | ☐ |
| R083 | Confirmación de Envío | El sistema debe mostrar confirmación con todos los detalles de la solicitud | Funcional | ☐ |
| R084 | Creación de Notificación | El sistema debe crear notificación automática al enviar solicitud | Funcional | ☐ |
| R085 | Limpieza de Formulario | El sistema debe limpiar todos los campos después de enviar solicitud | Funcional | ☐ |
| R086 | Cambio de Categoría Manual | El sistema debe permitir cambiar categoría manualmente y actualizar proveedores | Funcional | ☐ |
| **MÓDULO: PAGOS** |
| R087 | Autocompletado por ID Solicitud | El sistema debe autocompletar datos al ingresar ID de solicitud | Funcional | ☐ |
| R088 | Cálculo Automático de Monto | El sistema debe calcular monto automáticamente (cantidad × precio) | Funcional | ☐ |
| R089 | Visualización de Nombre de Proveedor | El sistema debe mostrar nombre del proveedor en lugar de ID | Funcional | ☐ |
| R090 | Campos de Solo Lectura | El sistema debe hacer campos autocompletados de solo lectura | Funcional | ☐ |
| R091 | Validación de Campos Obligatorios | El sistema debe validar que todos los campos estén completos | Funcional | ☐ |
| R092 | Validación de Monto Positivo | El sistema debe validar que el monto sea un número positivo | Funcional | ☐ |
| R093 | Validación de Número de Cuenta | El sistema debe validar que la cuenta tenga entre 10 y 20 dígitos | Funcional | ☐ |
| R094 | Validación de CLABE | El sistema debe validar que la CLABE tenga exactamente 18 dígitos | Funcional | ☐ |
| R095 | Registro de Pago | El sistema debe insertar pago en tabla 'pagos' con todos los datos | Funcional | ☐ |
| R096 | Forma de Pago Fija | El sistema debe registrar forma de pago como 'Transferencia' | Funcional | ☐ |
| R097 | Fecha Automática | El sistema debe establecer fecha actual automáticamente | Funcional | ☐ |
| R098 | Notificación de Pago | El sistema debe crear notificación automática al registrar pago | Funcional | ☐ |
| R099 | Historial de Pagos | El sistema debe permitir acceder al historial de pagos | Funcional | ☐ |
| **MÓDULO: NOTIFICACIONES** |
| R100 | Visualización de Solicitudes | El sistema debe mostrar todas las solicitudes enviadas a proveedores | Funcional | ☐ |
| R101 | Visualización de Respuestas | El sistema debe mostrar respuestas recibidas de proveedores | Funcional | ☐ |
| R102 | Carga desde Base de Datos | El sistema debe cargar notificaciones desde tabla 'notificaciones' | Funcional | ☐ |
| R103 | Filtrado por Tipo | El sistema debe filtrar notificaciones por prefijo [SOLICITUD] y [RESPUESTA] | Funcional | ☐ |
| R104 | Ordenamiento por Fecha | El sistema debe mostrar notificaciones ordenadas por fecha descendente | Funcional | ☐ |
| R105 | Detalle de Notificación | El sistema debe mostrar detalles completos al seleccionar una notificación | Funcional | ☐ |
| R106 | Formato de Visualización | El sistema debe mostrar: fecha, ID solicitud, título y mensaje | Funcional | ☐ |
| R107 | Actualización Automática | El sistema debe actualizar notificaciones al abrir la ventana | Funcional | ☐ |
| **REQUERIMIENTOS NO FUNCIONALES** |
| R108 | Diseño Unificado | El sistema debe mantener diseño consistente con fondo azul en todas las ventanas | No Funcional | ☐ |
| R109 | Manejo de Errores | El sistema debe mostrar mensajes de error descriptivos en caso de fallo | No Funcional | ☐ |
| R110 | Conexión Persistente | El sistema debe mantener conexión estable con PostgreSQL | No Funcional | ☐ |
| R111 | Validación de Entrada | El sistema debe validar todas las entradas de usuario antes de procesarlas | No Funcional | ☐ |
| R112 | Usabilidad | El sistema debe ser intuitivo y fácil de usar para administradores | No Funcional | ☐ |
| R113 | Rendimiento | El sistema debe cargar datos en menos de 3 segundos | No Funcional | ☐ |
| R114 | Integridad de Datos | El sistema debe mantener integridad referencial en todas las operaciones | No Funcional | ☐ |
| R115 | Seguridad | El sistema debe proteger credenciales y datos sensibles | No Funcional | ☐ |

---

