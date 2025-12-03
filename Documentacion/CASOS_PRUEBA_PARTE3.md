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

