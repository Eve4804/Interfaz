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

