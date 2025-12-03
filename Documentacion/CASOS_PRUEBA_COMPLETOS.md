# CASOS DE PRUEBA - SISTEMA ERP

## MÓDULO: AUTENTICACIÓN Y SEGURIDAD

### CP-001: Inicio de Sesión Exitoso
- **Requerimiento:** R001, R004
- **Precondiciones:** Usuario existe en BD con credenciales válidas
- **Datos de Entrada:** Usuario: "admin", Contraseña: "12345678"
- **Pasos:**
  1. Abrir aplicación
  2. Ingresar usuario "admin"
  3. Ingresar contraseña "12345678"
  4. Hacer clic en "Iniciar Sesión"
- **Resultado Esperado:** Sistema muestra MenuWindow con nombre de usuario
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-002: Campos Vacíos
- **Requerimiento:** R002, R005
- **Precondiciones:** Aplicación abierta
- **Datos de Entrada:** Usuario: "", Contraseña: ""
- **Pasos:**
  1. Dejar campos vacíos
  2. Hacer clic en "Iniciar Sesión"
- **Resultado Esperado:** Mensaje "Complete los campos, por favor."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-003: Usuario Vacío
- **Requerimiento:** R002, R005
- **Precondiciones:** Aplicación abierta
- **Datos de Entrada:** Usuario: "", Contraseña: "12345678"
- **Pasos:**
  1. Dejar usuario vacío
  2. Ingresar contraseña
  3. Hacer clic en "Iniciar Sesión"
- **Resultado Esperado:** Mensaje "Por favor, ingrese su usuario."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-004: Contraseña Vacía
- **Requerimiento:** R002, R005
- **Precondiciones:** Aplicación abierta
- **Datos de Entrada:** Usuario: "admin", Contraseña: ""
- **Pasos:**
  1. Ingresar usuario
  2. Dejar contraseña vacía
  3. Hacer clic en "Iniciar Sesión"
- **Resultado Esperado:** Mensaje "Por favor ingrese su contraseña."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-005: Contraseña Longitud Incorrecta
- **Requerimiento:** R003, R005
- **Precondiciones:** Aplicación abierta
- **Datos de Entrada:** Usuario: "admin", Contraseña: "123"
- **Pasos:**
  1. Ingresar usuario válido
  2. Ingresar contraseña con menos de 8 caracteres
  3. Hacer clic en "Iniciar Sesión"
- **Resultado Esperado:** Mensaje "La contraseña debe ser exactamente 8 caracteres."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

# CASOS DE PRUEBA - PARTE 2

## MÓDULO: RECUPERACIÓN DE CONTRASEÑA

### CP-010: Recuperación Exitosa
- **Requerimiento:** R006, R010
- **Precondiciones:** Usuario con ID 1 existe en BD
- **Datos de Entrada:** ID: "1", Nueva Contraseña: "abc12345", Confirmar: "abc12345"
- **Pasos:**
  1. Hacer clic en "Olvidé mi contraseña"
  2. Ingresar ID de usuario
  3. Ingresar nueva contraseña
  4. Confirmar contraseña
  5. Hacer clic en "Confirmar"
- **Resultado Esperado:** Mensaje "Contraseña actualizada correctamente"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-011: Campos Vacíos en Recuperación
- **Requerimiento:** R006
- **Datos de Entrada:** ID: "", Nueva Contraseña: "", Confirmar: ""
- **Resultado Esperado:** Mensaje "Llene los campos por favor."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-012: ID No Numérico
- **Requerimiento:** R007
- **Datos de Entrada:** ID: "abc", Nueva Contraseña: "12345678", Confirmar: "12345678"
- **Resultado Esperado:** Mensaje "Por favor ingrese un Id válido (número)."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-013: Contraseñas No Coinciden
- **Requerimiento:** R009
- **Datos de Entrada:** ID: "1", Nueva Contraseña: "abc12345", Confirmar: "xyz12345"
- **Resultado Esperado:** Mensaje "Las contraseñas no coinciden."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-014: Formato Inválido de Contraseña
- **Requerimiento:** R008
- **Datos de Entrada:** ID: "1", Nueva Contraseña: "abc@1234", Confirmar: "abc@1234"
- **Resultado Esperado:** Mensaje "La contraseña debe contener solo letras y números"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-015: ID No Existe
- **Requerimiento:** R006
- **Datos de Entrada:** ID: "9999", Nueva Contraseña: "abc12345", Confirmar: "abc12345"
- **Resultado Esperado:** Mensaje "El Id de usuario no existe."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

## MÓDULO: MENÚ PRINCIPAL

### CP-020: Visualización de Usuario
- **Requerimiento:** R013
- **Precondiciones:** Usuario "admin" ha iniciado sesión
- **Resultado Esperado:** Se muestra "Usuario: admin" en el menú
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-021: Navegación a Gestión de Ventas
- **Requerimiento:** R014
- **Pasos:** Hacer clic en tarjeta "Gestión de Ventas"
- **Resultado Esperado:** Se abre ventana GestionVentas
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-022: Navegación a Inventarios
- **Requerimiento:** R017
- **Pasos:** Hacer clic en tarjeta "Inventarios"
- **Resultado Esperado:** Se abre ventana Inventarios con datos cargados
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-023: Cerrar Sesión con Confirmación
- **Requerimiento:** R022, R023
- **Pasos:**
  1. Hacer clic en "Cerrar Sesión"
  2. Hacer clic en "Sí" en confirmación
- **Resultado Esperado:** Regresa a pantalla de login
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-024: Cancelar Cierre de Sesión
- **Requerimiento:** R023
- **Pasos:**
  1. Hacer clic en "Cerrar Sesión"
  2. Hacer clic en "No" en confirmación
- **Resultado Esperado:** Permanece en menú principal
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

# CASOS DE PRUEBA - PARTE 3

## MÓDULO: GESTIÓN DE VENTAS

### CP-030: Carga de Ventas
- **Requerimiento:** R026
- **Precondiciones:** Existen ventas en la BD
- **Pasos:** Abrir ventana Gestión de Ventas
- **Resultado Esperado:** Se cargan todas las ventas con datos de clientes
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-031: Filtrado de Pedidos Nuevos
- **Requerimiento:** R027
- **Precondiciones:** Existen ventas con estado 'Pendiente'
- **Resultado Esperado:** Pestaña "Nuevos" muestra solo pedidos pendientes
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-032: Búsqueda por ID de Venta
- **Requerimiento:** R030
- **Datos de Entrada:** Criterio: "1"
- **Pasos:**
  1. Ingresar ID de venta en búsqueda
  2. Hacer clic en Buscar
- **Resultado Esperado:** Se muestra solo la venta con ID 1
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-033: Búsqueda por Nombre de Cliente
- **Requerimiento:** R030
- **Datos de Entrada:** Criterio: "Juan"
- **Resultado Esperado:** Se muestran todas las ventas de clientes con "Juan" en el nombre
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-034: Confirmar Pedido
- **Requerimiento:** R031, R033
- **Precondiciones:** Existe pedido con estado 'Pendiente'
- **Pasos:**
  1. Seleccionar pedido en pestaña "Nuevos"
  2. Hacer clic en "Confirmar"
  3. Hacer clic en "Sí" en confirmación
- **Resultado Esperado:** Estado cambia a 'Confirmada', pedido se mueve a "Pendientes"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-035: Cancelar Pedido
- **Requerimiento:** R032, R033
- **Pasos:**
  1. Seleccionar pedido
  2. Hacer clic en "Cancelar"
  3. Confirmar acción
- **Resultado Esperado:** Estado cambia a 'Cancelada', pedido se mueve a "Cancelados"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-036: Ver Detalles de Pedido
- **Requerimiento:** R034
- **Pasos:**
  1. Seleccionar pedido
  2. Hacer clic en "Ver Detalles"
- **Resultado Esperado:** Se muestra ventana con todos los detalles del pedido
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-037: Contador de Pedidos
- **Requerimiento:** R036
- **Resultado Esperado:** Cada pestaña muestra el total correcto de pedidos
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

## MÓDULO: INVENTARIOS

### CP-040: Carga de Inventario con LEFT JOIN
- **Requerimiento:** R039
- **Precondiciones:** Existen productos con y sin inventario
- **Resultado Esperado:** Se muestran todos los productos, incluso sin inventario
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-041: Productos sin Inventario
- **Requerimiento:** R040
- **Resultado Esperado:** Productos sin inventario muestran cantidad 0 y estado "Sin inventario"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-042: Filtrado por Categoría Cartón
- **Requerimiento:** R041
- **Resultado Esperado:** Pestaña "Cartón" muestra solo productos con id_categoria = 1
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-043: Búsqueda por ID de Producto
- **Requerimiento:** R042, R043
- **Datos de Entrada:** Criterio: "5"
- **Resultado Esperado:** Se muestra solo el producto con ID 5
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-044: Búsqueda por Nombre Parcial
- **Requerimiento:** R042, R044
- **Datos de Entrada:** Criterio: "caja"
- **Resultado Esperado:** Se muestran todos los productos con "caja" en el nombre
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-045: Limpieza Automática de Búsqueda
- **Requerimiento:** R045
- **Pasos:**
  1. Buscar producto
  2. Ver resultados
- **Resultado Esperado:** Barra de búsqueda se limpia automáticamente
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-046: Sin Resultados de Búsqueda
- **Requerimiento:** R046
- **Datos de Entrada:** Criterio: "productoInexistente"
- **Resultado Esperado:** Mensaje "No se encontraron productos con el criterio"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

# CASOS DE PRUEBA - PARTE 4

## MÓDULO: CATÁLOGO DE PRODUCTOS

### CP-050: Visualización de Productos Activos
- **Requerimiento:** R051
- **Precondiciones:** Existen productos activos e inactivos en BD
- **Resultado Esperado:** Solo se muestran productos con estado 'Activo'
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-051: Información Completa de Productos
- **Requerimiento:** R052
- **Resultado Esperado:** Se muestra ID, nombre, descripción, precio, categoría y estado
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-052: Búsqueda Múltiple
- **Requerimiento:** R054
- **Datos de Entrada:** Criterio: "plástico"
- **Resultado Esperado:** Busca en nombre, descripción y categoría
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-053: Cambio Automático de Pestaña
- **Requerimiento:** R055
- **Pasos:**
  1. Buscar producto de categoría "Vehículos"
  2. Ver resultados
- **Resultado Esperado:** Sistema cambia automáticamente a pestaña "Vehículos"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-054: Contador de Resultados
- **Requerimiento:** R056
- **Resultado Esperado:** Mensaje muestra "Se encontraron X producto(s)"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-055: Búsqueda con Enter
- **Requerimiento:** R060
- **Pasos:**
  1. Ingresar criterio de búsqueda
  2. Presionar tecla Enter
- **Resultado Esperado:** Se ejecuta búsqueda sin hacer clic en botón
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-056: Actualizar Catálogo
- **Requerimiento:** R057, R058
- **Pasos:**
  1. Hacer clic en "Actualizar Catálogo"
  2. Confirmar acción
- **Resultado Esperado:** Se abre ventana ActualizarCat
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

## MÓDULO: GESTIÓN DE CLIENTES

### CP-060: Búsqueda por RFC
- **Requerimiento:** R061, R062
- **Datos de Entrada:** Criterio: "ABC123456"
- **Resultado Esperado:** Se encuentra cliente con RFC que contenga "ABC123456"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-061: Búsqueda por Nombre
- **Requerimiento:** R061, R062
- **Datos de Entrada:** Criterio: "María"
- **Resultado Esperado:** Se encuentra cliente con nombre que contenga "María"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-062: Registro de Nuevo Cliente
- **Requerimiento:** R064, R065, R066, R067, R068
- **Datos de Entrada:**
  - RFC: "XAXX010101000"
  - Nombre: "Empresa Test SA"
  - Tipo: "Persona Moral"
  - Estado: "Activo"
- **Pasos:**
  1. Llenar todos los campos obligatorios
  2. Hacer clic en "Activar"
- **Resultado Esperado:** Mensaje "Cliente registrado correctamente"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-063: RFC Inválido
- **Requerimiento:** R065
- **Datos de Entrada:** RFC: "ABC123" (menos de 13 caracteres)
- **Resultado Esperado:** Mensaje "El RFC debe tener exactamente 13 caracteres"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-064: Nombre Vacío
- **Requerimiento:** R066
- **Datos de Entrada:** RFC válido, Nombre: ""
- **Resultado Esperado:** Mensaje "El nombre es obligatorio"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-065: Actualización de Cliente
- **Requerimiento:** R069
- **Precondiciones:** Cliente existe y está cargado
- **Pasos:**
  1. Modificar datos del cliente
  2. Hacer clic en "Actualizar"
- **Resultado Esperado:** Mensaje "Cliente actualizado correctamente"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-066: Desactivación de Cliente (Eliminación Lógica)
- **Requerimiento:** R070, R071
- **Precondiciones:** Cliente existe con estado 'Activo'
- **Pasos:**
  1. Buscar cliente activo
  2. Hacer clic en "Desactivar Cliente"
  3. Confirmar desactivación
- **Resultado Esperado:** 
  - Mensaje "Cliente desactivado correctamente"
  - Estado del cliente cambia a 'Inactivo'
  - Registro permanece en base de datos
  - fecha_modificacion se actualiza
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-067: Limpiar Formulario
- **Requerimiento:** R072
- **Pasos:** Hacer clic en "Limpiar"
- **Resultado Esperado:** Todos los campos se limpian y botones se habilitan
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-068: Control de Botones al Editar
- **Requerimiento:** R073
- **Pasos:** Buscar y cargar cliente existente
- **Resultado Esperado:** Botones "Activar" y "Desactivar" se deshabilitan
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-069: Verificación de Eliminación Lógica
- **Requerimiento:** R070
- **Precondiciones:** Cliente desactivado en CP-066
- **Pasos:**
  1. Consultar base de datos directamente
  2. Verificar registro del cliente desactivado
- **Resultado Esperado:** 
  - Registro existe en tabla clientes
  - Campo estado = 'Inactivo'
  - Todos los demás datos permanecen intactos
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

# CASOS DE PRUEBA - PARTE 5

## MÓDULO: CONTACTAR PROVEEDOR

### CP-070: Autocompletado por ID Producto
- **Requerimiento:** R076
- **Datos de Entrada:** ID Producto: "5"
- **Pasos:**
  1. Ingresar ID de producto
  2. Hacer clic fuera del campo (LostFocus)
- **Resultado Esperado:** Se autocompleta descripción y categoría del producto
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-071: Carga de Proveedores por Categoría
- **Requerimiento:** R077
- **Precondiciones:** Producto con ID 5 es de categoría "Cartón"
- **Resultado Esperado:** ComboBox de proveedores se llena con proveedores de categoría Cartón
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-072: Envío de Solicitud Completa
- **Requerimiento:** R081, R082, R083
- **Datos de Entrada:**
  - Producto: "5"
  - Proveedor: "Proveedor A"
  - Cantidad: "100"
  - Fecha: "2024-12-15"
- **Pasos:**
  1. Llenar todos los campos
  2. Hacer clic en "Enviar Solicitud"
- **Resultado Esperado:** 
  - Solicitud se guarda en BD
  - Se muestra ID generado
  - Se crea notificación
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-073: Validación de Campos Obligatorios
- **Requerimiento:** R079
- **Datos de Entrada:** Campos incompletos
- **Resultado Esperado:** Mensaje "Por favor completa todos los campos"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-074: Validación de Duplicados
- **Requerimiento:** R080
- **Precondiciones:** Ya existe solicitud con mismo proveedor, producto y fecha
- **Resultado Esperado:** Mensaje "Ya existe una solicitud para este proveedor, producto y fecha"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-075: Producto No Encontrado
- **Requerimiento:** R076
- **Datos de Entrada:** ID Producto: "9999"
- **Resultado Esperado:** Mensaje "No se encontró un producto con ese ID"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-076: Limpieza Después de Envío
- **Requerimiento:** R085
- **Precondiciones:** Solicitud enviada exitosamente
- **Resultado Esperado:** Todos los campos se limpian automáticamente
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-077: Cambio Manual de Categoría
- **Requerimiento:** R086
- **Pasos:**
  1. Seleccionar categoría "Plásticos" manualmente
  2. Observar ComboBox de proveedores
- **Resultado Esperado:** Se cargan proveedores de categoría Plásticos
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

## MÓDULO: PAGOS

### CP-080: Autocompletado por ID Solicitud
- **Requerimiento:** R087, R088, R089
- **Datos de Entrada:** ID Solicitud: "1"
- **Pasos:**
  1. Ingresar ID de solicitud
  2. Hacer clic fuera del campo
- **Resultado Esperado:**
  - Se muestra nombre del proveedor
  - Se calcula monto automáticamente
  - Campos se hacen de solo lectura
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-081: Registro de Pago Completo
- **Requerimiento:** R095, R096, R097
- **Datos de Entrada:**
  - ID Solicitud: "1"
  - Número Cuenta: "1234567890123456"
  - CLABE: "012345678901234567"
- **Pasos:**
  1. Autocompletar con ID solicitud
  2. Ingresar datos bancarios
  3. Hacer clic en "Guardar"
- **Resultado Esperado:**
  - Pago se registra en BD
  - Se crea notificación
  - Mensaje de confirmación
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-082: Validación de Monto Positivo
- **Requerimiento:** R092
- **Datos de Entrada:** Monto: "-100"
- **Resultado Esperado:** Mensaje "El monto debe ser un número positivo"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-083: Validación de Número de Cuenta
- **Requerimiento:** R093
- **Datos de Entrada:** Número Cuenta: "123" (menos de 10 dígitos)
- **Resultado Esperado:** Mensaje "El número de cuenta debe tener entre 10 y 20 dígitos"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-084: Validación de CLABE
- **Requerimiento:** R094
- **Datos de Entrada:** CLABE: "12345" (menos de 18 dígitos)
- **Resultado Esperado:** Mensaje "La CLABE interbancaria debe tener exactamente 18 dígitos"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-085: Solicitud No Encontrada
- **Requerimiento:** R087
- **Datos de Entrada:** ID Solicitud: "9999"
- **Resultado Esperado:** Mensaje "No se encontró la solicitud con ese ID"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-086: Acceso a Historial de Pagos
- **Requerimiento:** R099
- **Pasos:** Hacer clic en "Historial de Pagos"
- **Resultado Esperado:** Se abre ventana HistorialPagos
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

## MÓDULO: NOTIFICACIONES

### CP-090: Visualización de Solicitudes
- **Requerimiento:** R100, R102
- **Precondiciones:** Existen solicitudes en BD
- **Pasos:** Abrir ventana Notificaciones
- **Resultado Esperado:** Se muestran todas las solicitudes enviadas
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-091: Visualización de Respuestas
- **Requerimiento:** R101, R103
- **Precondiciones:** Existen respuestas en BD
- **Resultado Esperado:** Se muestran solo notificaciones con prefijo [RESPUESTA]
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-092: Ordenamiento por Fecha
- **Requerimiento:** R104
- **Resultado Esperado:** Notificaciones se muestran de más reciente a más antigua
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-093: Detalle de Notificación
- **Requerimiento:** R105
- **Pasos:**
  1. Seleccionar una notificación
  2. Observar ventana de detalles
- **Resultado Esperado:** Se muestra ventana con información completa
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

### CP-094: Formato de Visualización
- **Requerimiento:** R106
- **Resultado Esperado:** Cada notificación muestra: fecha, ID solicitud, título y mensaje
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

