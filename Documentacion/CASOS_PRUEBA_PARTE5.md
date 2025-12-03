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

