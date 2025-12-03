# REQUERIMIENTOS COMPLETOS DEL SISTEMA ERP

## TABLA DE REQUERIMIENTOS FUNCIONALES Y NO FUNCIONALES

| ID | Nombre del Requerimiento | Descripción | Funcional/No Funcional | Check |
|----|-------------------------|-------------|------------------------|-------|
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
| R070 | Desactivación de Cliente (Eliminación Lógica) | El sistema debe permitir desactivar clientes cambiando estado a 'Inactivo' sin eliminar físicamente el registro | Funcional | ☐ |
| R071 | Confirmación de Desactivación | El sistema debe solicitar confirmación antes de desactivar un cliente explicando que los datos se conservarán | Funcional | ☐ |
| R072 | Limpiar Formulario | El sistema debe permitir limpiar todos los campos del formulario | Funcional | ☐ |
| R073 | Control de Botones | El sistema debe deshabilitar botones Activar/Desactivar al editar cliente existente | Funcional | ☐ |
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

## RESUMEN DE REQUERIMIENTOS

| Categoría | Cantidad |
|-----------|----------|
| **Autenticación y Seguridad** | 11 |
| **Menú Principal** | 13 |
| **Gestión de Ventas** | 13 |
| **Inventarios** | 12 |
| **Catálogo de Productos** | 11 |
| **Gestión de Clientes** | 15 |
| **Contactar Proveedor** | 11 |
| **Pagos** | 13 |
| **Notificaciones** | 8 |
| **No Funcionales** | 8 |
| **TOTAL** | **115** |

