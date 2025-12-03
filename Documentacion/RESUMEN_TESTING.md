# RESUMEN DE REQUERIMIENTOS Y CASOS DE PRUEBA

## SISTEMA ERP - GESTIÓN EMPRESARIAL

### ESTRUCTURA DE DOCUMENTACIÓN

Este sistema de documentación está organizado en los siguientes archivos:

1. **REQUERIMIENTOS_Y_CASOS_PRUEBA.md** - Tabla completa de 115 requerimientos
2. **CASOS_DE_PRUEBA.md** - Casos de prueba para Autenticación (CP-001 a CP-005)
3. **CASOS_PRUEBA_PARTE2.md** - Recuperación de contraseña y Menú (CP-010 a CP-024)
4. **CASOS_PRUEBA_PARTE3.md** - Gestión de Ventas e Inventarios (CP-030 a CP-046)
5. **CASOS_PRUEBA_PARTE4.md** - Catálogo y Clientes (CP-050 a CP-068)
6. **CASOS_PRUEBA_PARTE5.md** - Proveedores, Pagos y Notificaciones (CP-070 a CP-094)

---

## RESUMEN POR MÓDULOS

### 1. AUTENTICACIÓN Y SEGURIDAD (R001-R011)
**Requerimientos:** 11
**Casos de Prueba:** 15 (CP-001 a CP-015)

**Funcionalidades Principales:**
- Inicio de sesión con validación de credenciales
- Validación de campos vacíos y formato
- Recuperación de contraseña por ID
- Conexión a PostgreSQL

**Casos Críticos:**
- CP-001: Login exitoso
- CP-005: Validación de longitud de contraseña
- CP-010: Recuperación exitosa de contraseña
- CP-013: Contraseñas no coinciden

---

### 2. MENÚ PRINCIPAL (R012-R024)
**Requerimientos:** 13
**Casos de Prueba:** 5 (CP-020 a CP-024)

**Funcionalidades Principales:**
- Dashboard con tarjetas de navegación
- Visualización de usuario activo
- Navegación a 8 módulos diferentes
- Cierre de sesión con confirmación

**Casos Críticos:**
- CP-020: Visualización correcta de usuario
- CP-023: Cerrar sesión exitosamente

---

### 3. GESTIÓN DE VENTAS (R025-R037)
**Requerimientos:** 13
**Casos de Prueba:** 8 (CP-030 a CP-037)

**Funcionalidades Principales:**
- Visualización por pestañas (Nuevos, Pendientes, Cancelados)
- Búsqueda por ID o nombre de cliente
- Confirmar y cancelar pedidos
- Ver detalles completos de pedidos

**Casos Críticos:**
- CP-030: Carga correcta de ventas con JOIN
- CP-034: Confirmar pedido y cambio de estado
- CP-035: Cancelar pedido
- CP-036: Ver detalles completos

---

### 4. INVENTARIOS (R038-R049)
**Requerimientos:** 12
**Casos de Prueba:** 7 (CP-040 a CP-046)

**Funcionalidades Principales:**
- Visualización por categorías (Cartón, Plásticos, Vehículos)
- LEFT JOIN para mostrar productos sin inventario
- Búsqueda por ID o nombre
- Limpieza automática de búsqueda

**Casos Críticos:**
- CP-040: LEFT JOIN funciona correctamente
- CP-041: Productos sin inventario se muestran
- CP-045: Limpieza automática de búsqueda

---

### 5. CATÁLOGO DE PRODUCTOS (R050-R060)
**Requerimientos:** 11
**Casos de Prueba:** 7 (CP-050 a CP-056)

**Funcionalidades Principales:**
- Visualización de productos activos
- Búsqueda múltiple (nombre, descripción, categoría)
- Cambio automático de pestaña
- Búsqueda con tecla Enter

**Casos Críticos:**
- CP-051: Solo productos activos
- CP-053: Cambio automático de pestaña
- CP-055: Búsqueda con Enter

---

### 6. GESTIÓN DE CLIENTES (R061-R075)
**Requerimientos:** 15
**Casos de Prueba:** 9 (CP-060 a CP-068)

**Funcionalidades Principales:**
- Búsqueda por RFC o nombre
- Registro de nuevos clientes
- Actualización y eliminación
- Validaciones de RFC (13 caracteres)

**Casos Críticos:**
- CP-062: Registro exitoso con validaciones
- CP-063: Validación de RFC
- CP-066: Eliminación con confirmación
- CP-068: Control de botones al editar

---

### 7. CONTACTAR PROVEEDOR (R076-R086)
**Requerimientos:** 11
**Casos de Prueba:** 8 (CP-070 a CP-077)

**Funcionalidades Principales:**
- Autocompletado por ID de producto
- Carga de proveedores por categoría
- Validación de duplicados
- Creación automática de notificaciones

**Casos Críticos:**
- CP-070: Autocompletado funciona
- CP-072: Envío completo de solicitud
- CP-074: Validación de duplicados
- CP-076: Limpieza después de envío

---

### 8. PAGOS (R087-R099)
**Requerimientos:** 13
**Casos de Prueba:** 7 (CP-080 a CP-086)

**Funcionalidades Principales:**
- Autocompletado por ID de solicitud
- Cálculo automático de monto
- Validación de datos bancarios
- Registro con notificación

**Casos Críticos:**
- CP-080: Autocompletado completo
- CP-081: Registro exitoso
- CP-083: Validación de cuenta (10-20 dígitos)
- CP-084: Validación de CLABE (18 dígitos)

---

### 9. NOTIFICACIONES (R100-R107)
**Requerimientos:** 8
**Casos de Prueba:** 5 (CP-090 a CP-094)

**Funcionalidades Principales:**
- Visualización de solicitudes y respuestas
- Filtrado por tipo [SOLICITUD] / [RESPUESTA]
- Ordenamiento por fecha descendente
- Detalles completos al seleccionar

**Casos Críticos:**
- CP-090: Carga de solicitudes
- CP-091: Filtrado de respuestas
- CP-092: Ordenamiento correcto

---

### 10. REQUERIMIENTOS NO FUNCIONALES (R108-R115)
**Requerimientos:** 8
**Casos de Prueba:** Transversales a todos los módulos

**Aspectos Cubiertos:**
- Diseño unificado con fondo azul
- Manejo de errores descriptivos
- Validación de todas las entradas
- Rendimiento (carga < 3 segundos)
- Seguridad de credenciales
- Integridad referencial

---

## ESTADÍSTICAS GENERALES

| Métrica | Cantidad |
|---------|----------|
| **Total de Requerimientos** | 115 |
| **Requerimientos Funcionales** | 107 |
| **Requerimientos No Funcionales** | 8 |
| **Total de Casos de Prueba** | 94+ |
| **Módulos del Sistema** | 9 |
| **Ventanas Principales** | 10 |
| **Tablas de Base de Datos** | 12+ |

---

## FLUJO DE PRUEBAS RECOMENDADO

### Fase 1: Autenticación (Crítica)
1. CP-001 a CP-005: Login
2. CP-010 a CP-015: Recuperación de contraseña
3. CP-020 a CP-024: Menú principal

### Fase 2: Módulos de Consulta
4. CP-030 a CP-037: Gestión de ventas
5. CP-040 a CP-046: Inventarios
6. CP-050 a CP-056: Catálogo

### Fase 3: Módulos de Gestión
7. CP-060 a CP-068: Clientes
8. CP-070 a CP-077: Proveedores
9. CP-080 a CP-086: Pagos

### Fase 4: Notificaciones
10. CP-090 a CP-094: Sistema de notificaciones

---

## CRITERIOS DE ACEPTACIÓN

### Para Aprobar un Caso de Prueba:
✅ El resultado obtenido coincide exactamente con el resultado esperado
✅ No se generan errores en consola o logs
✅ La interfaz responde en menos de 3 segundos
✅ Los datos se persisten correctamente en la base de datos
✅ Los mensajes de error son claros y descriptivos

### Para Rechazar un Caso de Prueba:
❌ El resultado no coincide con lo esperado
❌ Se generan excepciones no controladas
❌ La aplicación se congela o crashea
❌ Los datos no se guardan correctamente
❌ La experiencia de usuario es confusa

---

## NOTAS IMPORTANTES

1. **Conexión a Base de Datos:** Todos los casos requieren conexión activa a PostgreSQL
2. **Datos de Prueba:** Se recomienda tener datos de prueba en todas las tablas
3. **Orden de Ejecución:** Algunos casos dependen de datos creados en casos anteriores
4. **Limpieza:** Después de pruebas de eliminación, restaurar datos de prueba
5. **Validaciones:** Todas las validaciones deben probarse con datos válidos e inválidos

---

## CONTACTO Y SOPORTE

Para dudas sobre los casos de prueba o requerimientos, consultar:
- Documentación técnica en `/Documentacion/`
- Esquema de base de datos en `database_schema.sql`
- README principal del proyecto

