# ÍNDICE DE DOCUMENTACIÓN - SISTEMA ERP

## 📚 GUÍA DE DOCUMENTOS

Este proyecto contiene documentación completa de requerimientos y casos de prueba organizados en los siguientes archivos:

---

## 📋 DOCUMENTOS PRINCIPALES

### 1. **REQUERIMIENTOS_COMPLETOS.md** ⭐
**Contenido:** Tabla completa de 115 requerimientos organizados por módulos
- ID del requerimiento
- Nombre descriptivo
- Descripción detallada
- Clasificación (Funcional/No Funcional)
- Columna Check para seguimiento

**Uso:** Documento de referencia principal para validar funcionalidades

---

### 2. **CASOS_PRUEBA_COMPLETOS.md** ⭐
**Contenido:** Consolidación de todos los casos de prueba (95+)
- Casos CP-001 a CP-005: Autenticación
- Casos CP-010 a CP-024: Recuperación y Menú
- Casos CP-030 a CP-046: Ventas e Inventarios
- Casos CP-050 a CP-068: Catálogo y Clientes
- Casos CP-070 a CP-094: Proveedores, Pagos y Notificaciones

**Uso:** Ejecución secuencial de pruebas

---

### 3. **MANUAL_TESTING_COMPLETO.md** ⭐
**Contenido:** Manual profesional con formato estructurado
- Introducción y alcance
- Resumen de requerimientos
- Casos de prueba detallados
- Criterios de aceptación
- Matriz de trazabilidad

**Uso:** Presentación formal para stakeholders

---

### 4. **RESUMEN_TESTING.md**
**Contenido:** Vista ejecutiva del proyecto de testing
- Estadísticas generales
- Resumen por módulos
- Flujo de pruebas recomendado
- Criterios de aceptación/rechazo

**Uso:** Referencia rápida para gerentes de proyecto

---

## 📂 DOCUMENTOS POR MÓDULO (Detallados)

### CASOS_DE_PRUEBA.md
- CP-001 a CP-005: Autenticación básica

### CASOS_PRUEBA_PARTE2.md
- CP-010 a CP-024: Recuperación de contraseña y Menú principal

### CASOS_PRUEBA_PARTE3.md
- CP-030 a CP-046: Gestión de Ventas e Inventarios

### CASOS_PRUEBA_PARTE4.md
- CP-050 a CP-069: Catálogo de Productos y Gestión de Clientes
- **ACTUALIZADO:** Incluye eliminación lógica de clientes (CP-066, CP-069)

### CASOS_PRUEBA_PARTE5.md
- CP-070 a CP-094: Contactar Proveedor, Pagos y Notificaciones

---

## 🎯 DOCUMENTOS TÉCNICOS ADICIONALES

### README.md
Documentación general del proyecto

### database_schema.sql
Esquema completo de la base de datos PostgreSQL

### INSTRUCCIONES_NPGSQL.md
Guía de instalación y configuración de Npgsql

---

## 📊 ESTADÍSTICAS DEL PROYECTO

| Métrica | Valor |
|---------|-------|
| **Total Requerimientos** | 115 |
| **Requerimientos Funcionales** | 107 |
| **Requerimientos No Funcionales** | 8 |
| **Total Casos de Prueba** | 95+ |
| **Módulos del Sistema** | 9 |
| **Ventanas Principales** | 10 |
| **Tablas Base de Datos** | 12+ |

---

## 🔄 CAMBIOS RECIENTES

### Versión 1.1 - Diciembre 2024
✅ **Implementada Eliminación Lógica en Gestión de Clientes**
- Botón "Eliminar" cambiado a "Desactivar Cliente"
- Los clientes se marcan como 'Inactivo' en lugar de eliminarse
- Actualizado R070: Desactivación de Cliente (Eliminación Lógica)
- Actualizado CP-066: Desactivación en lugar de eliminación física
- Nuevo CP-069: Verificación de eliminación lógica
- Código actualizado en `Frm_clientes.xaml` y `Frm_clientes.xaml.cs`

---

## 🚀 FLUJO DE TRABAJO RECOMENDADO

### Para Desarrolladores:
1. Consultar **REQUERIMIENTOS_COMPLETOS.md** para entender funcionalidades
2. Implementar código según especificaciones
3. Ejecutar casos de prueba de **CASOS_PRUEBA_COMPLETOS.md**
4. Marcar checks en requerimientos completados

### Para Testers:
1. Revisar **MANUAL_TESTING_COMPLETO.md** para contexto
2. Ejecutar casos de prueba por módulo
3. Documentar resultados (Pendiente/Aprobado/Rechazado)
4. Reportar defectos con referencia a ID de caso

### Para Project Managers:
1. Consultar **RESUMEN_TESTING.md** para vista general
2. Revisar estadísticas y cobertura
3. Validar criterios de aceptación
4. Aprobar releases basados en checks completados

---

## 📞 SOPORTE

Para dudas sobre la documentación:
- Revisar archivos en carpeta `/Documentacion/`
- Consultar código fuente en archivos `.xaml.cs`
- Verificar esquema de BD en `database_schema.sql`

---

## ✅ CHECKLIST DE DOCUMENTOS

Marca los documentos que has revisado:

- ☐ REQUERIMIENTOS_COMPLETOS.md
- ☐ CASOS_PRUEBA_COMPLETOS.md
- ☐ MANUAL_TESTING_COMPLETO.md
- ☐ RESUMEN_TESTING.md
- ☐ INDICE_DOCUMENTACION.md (este archivo)
- ☐ database_schema.sql
- ☐ README.md

---

**Última actualización:** Diciembre 2024  
**Versión de documentación:** 1.1  
**Estado del proyecto:** ✅ Documentación Completa

