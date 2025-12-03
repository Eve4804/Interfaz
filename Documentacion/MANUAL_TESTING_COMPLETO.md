# MANUAL DE TESTING COMPLETO
## SISTEMA ERP - GESTIÓN EMPRESARIAL

**Versión:** 1.0  
**Fecha:** Diciembre 2024  
**Tecnología:** WPF .NET Framework 4.7.2 + PostgreSQL

---

## ÍNDICE

1. [Introducción](#introducción)
2. [Requerimientos del Sistema](#requerimientos-del-sistema)
3. [Casos de Prueba por Módulo](#casos-de-prueba-por-módulo)
4. [Criterios de Aceptación](#criterios-de-aceptación)
5. [Matriz de Trazabilidad](#matriz-de-trazabilidad)

---

## INTRODUCCIÓN

Este documento contiene la especificación completa de requerimientos y casos de prueba para el Sistema ERP de Gestión Empresarial. El sistema está diseñado para administrar ventas, inventarios, clientes, proveedores y pagos de manera integrada.

### Alcance del Testing

- **115 Requerimientos** (107 funcionales, 8 no funcionales)
- **95+ Casos de Prueba** organizados por módulos
- **9 Módulos Principales** del sistema
- **Cobertura:** 100% de funcionalidades críticas

---

## REQUERIMIENTOS DEL SISTEMA

### Resumen por Módulo

| Módulo | Requerimientos | Prioridad |
|--------|---------------|-----------|
| Autenticación y Seguridad | R001-R011 (11) | CRÍTICA |
| Menú Principal | R012-R024 (13) | ALTA |
| Gestión de Ventas | R025-R037 (13) | ALTA |
| Inventarios | R038-R049 (12) | ALTA |
| Catálogo de Productos | R050-R060 (11) | MEDIA |
| Gestión de Clientes | R061-R075 (15) | ALTA |
| Contactar Proveedor | R076-R086 (11) | MEDIA |
| Pagos | R087-R099 (13) | ALTA |
| Notificaciones | R100-R107 (8) | MEDIA |
| No Funcionales | R108-R115 (8) | ALTA |

**Ver documento completo:** `REQUERIMIENTOS_COMPLETOS.md`

---

## CASOS DE PRUEBA POR MÓDULO

### 1. AUTENTICACIÓN Y SEGURIDAD

#### CP-001: Inicio de Sesión Exitoso ✓
- **Prioridad:** CRÍTICA
- **Requerimientos:** R001, R004
- **Precondiciones:** Usuario "admin" con contraseña "12345678" existe en BD
- **Pasos:**
  1. Abrir aplicación
  2. Ingresar usuario "admin"
  3. Ingresar contraseña "12345678"
  4. Clic en "Iniciar Sesión"
- **Resultado Esperado:** Sistema muestra MenuWindow con "Usuario: admin"
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-002: Validación Campos Vacíos ✓
- **Prioridad:** ALTA
- **Requerimientos:** R002, R005
- **Datos:** Usuario: "", Contraseña: ""
- **Resultado Esperado:** Mensaje "Complete los campos, por favor."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-003: Usuario Vacío ✓
- **Requerimientos:** R002, R005
- **Datos:** Usuario: "", Contraseña: "12345678"
- **Resultado Esperado:** Mensaje "Por favor, ingrese su usuario."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-004: Contraseña Vacía ✓
- **Requerimientos:** R002, R005
- **Datos:** Usuario: "admin", Contraseña: ""
- **Resultado Esperado:** Mensaje "Por favor ingrese su contraseña."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-005: Validación Longitud Contraseña ✓
- **Prioridad:** ALTA
- **Requerimientos:** R003, R005
- **Datos:** Usuario: "admin", Contraseña: "123" (menos de 8)
- **Resultado Esperado:** Mensaje "La contraseña debe ser exactamente 8 caracteres."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-006: Credenciales Incorrectas ✓
- **Requerimientos:** R004, R005
- **Datos:** Usuario: "admin", Contraseña: "wrongpwd"
- **Resultado Esperado:** Mensaje "Usuario o contraseña incorrectos."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

---

### 2. RECUPERACIÓN DE CONTRASEÑA

#### CP-010: Recuperación Exitosa ✓
- **Prioridad:** ALTA
- **Requerimientos:** R006, R010
- **Precondiciones:** Usuario ID 1 existe
- **Datos:** ID: "1", Nueva: "abc12345", Confirmar: "abc12345"
- **Resultado Esperado:** 
  - Mensaje "Contraseña actualizada correctamente"
  - Muestra nombre de usuario actualizado
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-011: Campos Vacíos Recuperación
- **Requerimientos:** R006
- **Datos:** Todos los campos vacíos
- **Resultado Esperado:** Mensaje "Llene los campos por favor."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-012: ID No Numérico ✓
- **Requerimientos:** R007
- **Datos:** ID: "abc"
- **Resultado Esperado:** Mensaje "Por favor ingrese un Id válido (número)."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-013: Contraseñas No Coinciden ✓
- **Requerimientos:** R009
- **Datos:** Nueva: "abc12345", Confirmar: "xyz12345"
- **Resultado Esperado:** Mensaje "Las contraseñas no coinciden."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-014: Formato Inválido
- **Requerimientos:** R008
- **Datos:** Contraseña: "abc@1234" (con caracteres especiales)
- **Resultado Esperado:** Mensaje sobre formato válido
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-015: ID No Existe ✓
- **Requerimientos:** R006
- **Datos:** ID: "9999"
- **Resultado Esperado:** Mensaje "El Id de usuario no existe."
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

---

### 3. MENÚ PRINCIPAL

#### CP-020: Visualización Usuario ✓
- **Prioridad:** MEDIA
- **Requerimientos:** R013
- **Precondiciones:** Usuario "admin" logueado
- **Resultado Esperado:** Texto "Usuario: admin" visible
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-021: Navegación Gestión Ventas ✓
- **Requerimientos:** R014
- **Pasos:** Clic en tarjeta "Gestión de Ventas"
- **Resultado Esperado:** Ventana GestionVentas se abre
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-022: Navegación Inventarios ✓
- **Requerimientos:** R017
- **Pasos:** Clic en tarjeta "Inventarios"
- **Resultado Esperado:** Ventana Inventarios con datos cargados
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-023: Cerrar Sesión Confirmado ✓
- **Prioridad:** ALTA
- **Requerimientos:** R022, R023
- **Pasos:**
  1. Clic en "Cerrar Sesión"
  2. Clic en "Sí"
- **Resultado Esperado:** Regresa a MainWindow (login)
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

#### CP-024: Cancelar Cierre Sesión
- **Requerimientos:** R023
- **Pasos:**
  1. Clic en "Cerrar Sesión"
  2. Clic en "No"
- **Resultado Esperado:** Permanece en MenuWindow
- **Estado:** ☐ Pendiente | ☐ Aprobado | ☐ Rechazado

---

