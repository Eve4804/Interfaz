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

