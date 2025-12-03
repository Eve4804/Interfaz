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

