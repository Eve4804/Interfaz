-- ============================================
-- SCRIPT DE CREACIÓN DE BASE DE DATOS POSTGRESQL
-- Sistema de Gestión de Ventas e Inventarios
-- ============================================

-- Crear la base de datos (ejecutar como superusuario)
-- CREATE DATABASE gestion_ventas;
-- \c gestion_ventas;

-- ============================================
-- TABLA: clientes
-- ============================================
CREATE TABLE clientes (
    id_cliente SERIAL PRIMARY KEY,
    rfc VARCHAR(13) NOT NULL UNIQUE,
    nombre VARCHAR(200) NOT NULL,
    tipo VARCHAR(50) NOT NULL CHECK (tipo IN ('Regular', 'Premium', 'Mayorista')),
    email VARCHAR(100),
    telefono VARCHAR(20),
    direccion_fiscal TEXT,
    direccion_envio TEXT,
    metodo_pago VARCHAR(50),
    fecha_alta TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    activo BOOLEAN DEFAULT TRUE,
    fecha_modificacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: categorias
-- ============================================
CREATE TABLE categorias (
    id_categoria SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE,
    descripcion TEXT,
    activo BOOLEAN DEFAULT TRUE
);

-- ============================================
-- TABLA: productos
-- ============================================
CREATE TABLE productos (
    id_producto SERIAL PRIMARY KEY,
    nombre VARCHAR(200) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10, 2) NOT NULL CHECK (precio >= 0),
    id_categoria INTEGER REFERENCES categorias(id_categoria),
    imagen_url VARCHAR(500),
    activo BOOLEAN DEFAULT TRUE,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_modificacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: inventarios
-- ============================================
CREATE TABLE inventarios (
    id_inventario SERIAL PRIMARY KEY,
    id_producto INTEGER NOT NULL REFERENCES productos(id_producto),
    cantidad INTEGER NOT NULL DEFAULT 0 CHECK (cantidad >= 0),
    estado VARCHAR(50) NOT NULL CHECK (estado IN ('Disponible', 'Agotado', 'En pedido', 'Descontinuado')),
    ubicacion VARCHAR(100),
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: proveedores
-- ============================================
CREATE TABLE proveedores (
    id_proveedor SERIAL PRIMARY KEY,
    nombre VARCHAR(200) NOT NULL,
    contacto VARCHAR(100),
    telefono VARCHAR(20),
    email VARCHAR(100),
    direccion TEXT,
    activo BOOLEAN DEFAULT TRUE,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: solicitudes_proveedor
-- ============================================
CREATE TABLE solicitudes_proveedor (
    id_solicitud SERIAL PRIMARY KEY,
    id_proveedor INTEGER NOT NULL REFERENCES proveedores(id_proveedor),
    id_producto INTEGER NOT NULL REFERENCES productos(id_producto),
    descripcion TEXT,
    cantidad INTEGER NOT NULL CHECK (cantidad > 0),
    fecha_solicitud DATE NOT NULL,
    estado VARCHAR(50) DEFAULT 'Pendiente' CHECK (estado IN ('Pendiente', 'Aprobada', 'Rechazada', 'Completada')),
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: pagos
-- ============================================
CREATE TABLE pagos (
    id_pago SERIAL PRIMARY KEY,
    id_proveedor INTEGER NOT NULL REFERENCES proveedores(id_proveedor),
    id_solicitud INTEGER REFERENCES solicitudes_proveedor(id_solicitud),
    monto_total DECIMAL(10, 2) NOT NULL CHECK (monto_total >= 0),
    forma_pago VARCHAR(50) NOT NULL CHECK (forma_pago IN ('Transferencia', 'Tarjeta', 'Depósito', 'Efectivo', 'Cheque')),
    fecha_pago DATE NOT NULL,
    estado_pago VARCHAR(50) DEFAULT 'Pendiente' CHECK (estado_pago IN ('Pendiente', 'Pagado', 'Cancelado')),
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: ventas
-- ============================================
CREATE TABLE ventas (
    id_venta SERIAL PRIMARY KEY,
    fecha TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_cliente INTEGER NOT NULL REFERENCES clientes(id_cliente),
    estado VARCHAR(50) DEFAULT 'Pendiente' CHECK (estado IN ('Pendiente', 'Confirmada', 'Cancelada', 'En tránsito', 'Entregado')),
    metodo_pago VARCHAR(50) NOT NULL CHECK (metodo_pago IN ('Efectivo', 'Tarjeta', 'Transferencia', 'Crédito')),
    subtotal DECIMAL(10, 2) NOT NULL DEFAULT 0,
    impuestos DECIMAL(10, 2) DEFAULT 0,
    total DECIMAL(10, 2) NOT NULL DEFAULT 0,
    notas TEXT,
    fecha_entrega_estimada DATE,
    fecha_entrega_real DATE,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_modificacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: venta_items (detalle de ventas)
-- ============================================
CREATE TABLE venta_items (
    id_venta_item SERIAL PRIMARY KEY,
    id_venta INTEGER NOT NULL REFERENCES ventas(id_venta) ON DELETE CASCADE,
    id_producto INTEGER NOT NULL REFERENCES productos(id_producto),
    cantidad INTEGER NOT NULL CHECK (cantidad > 0),
    precio_unitario DECIMAL(10, 2) NOT NULL CHECK (precio_unitario >= 0),
    subtotal DECIMAL(10, 2) GENERATED ALWAYS AS (cantidad * precio_unitario) STORED,
    descuento DECIMAL(10, 2) DEFAULT 0,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: usuarios (para el sistema de login)
-- ============================================
CREATE TABLE usuarios (
    id_usuario SERIAL PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL, -- Debe estar hasheada
    nombre_completo VARCHAR(200),
    email VARCHAR(100),
    rol VARCHAR(50) DEFAULT 'Usuario' CHECK (rol IN ('Admin', 'Usuario', 'Vendedor', 'Almacenista')),
    activo BOOLEAN DEFAULT TRUE,
    ultimo_acceso TIMESTAMP,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: notificaciones
-- ============================================
CREATE TABLE notificaciones (
    id_notificacion SERIAL PRIMARY KEY,
    tipo VARCHAR(50) NOT NULL CHECK (tipo IN ('Solicitud', 'Respuesta', 'Alerta', 'Información')),
    titulo VARCHAR(200) NOT NULL,
    mensaje TEXT NOT NULL,
    id_usuario INTEGER REFERENCES usuarios(id_usuario),
    leida BOOLEAN DEFAULT FALSE,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- TABLA: historial_inventario (auditoría)
-- ============================================
CREATE TABLE historial_inventario (
    id_historial SERIAL PRIMARY KEY,
    id_producto INTEGER NOT NULL REFERENCES productos(id_producto),
    cantidad_anterior INTEGER NOT NULL,
    cantidad_nueva INTEGER NOT NULL,
    tipo_movimiento VARCHAR(50) NOT NULL CHECK (tipo_movimiento IN ('Entrada', 'Salida', 'Ajuste', 'Venta', 'Devolución')),
    id_usuario INTEGER REFERENCES usuarios(id_usuario),
    motivo TEXT,
    fecha_movimiento TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- ÍNDICES PARA MEJORAR RENDIMIENTO
-- ============================================
CREATE INDEX idx_clientes_nombre ON clientes(nombre);
CREATE INDEX idx_clientes_rfc ON clientes(rfc);
CREATE INDEX idx_productos_nombre ON productos(nombre);
CREATE INDEX idx_productos_categoria ON productos(id_categoria);
CREATE INDEX idx_ventas_cliente ON ventas(id_cliente);
CREATE INDEX idx_ventas_fecha ON ventas(fecha);
CREATE INDEX idx_venta_items_venta ON venta_items(id_venta);
CREATE INDEX idx_venta_items_producto ON venta_items(id_producto);
CREATE INDEX idx_inventarios_producto ON inventarios(id_producto);
CREATE INDEX idx_pagos_proveedor ON pagos(id_proveedor);
CREATE INDEX idx_notificaciones_usuario ON notificaciones(id_usuario);

-- ============================================
-- TRIGGERS PARA ACTUALIZACIÓN AUTOMÁTICA
-- ============================================

-- Trigger para actualizar fecha_modificacion en clientes
CREATE OR REPLACE FUNCTION actualizar_fecha_modificacion()
RETURNS TRIGGER AS $$
BEGIN
    NEW.fecha_modificacion = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trigger_clientes_modificacion
BEFORE UPDATE ON clientes
FOR EACH ROW
EXECUTE FUNCTION actualizar_fecha_modificacion();

CREATE TRIGGER trigger_productos_modificacion
BEFORE UPDATE ON productos
FOR EACH ROW
EXECUTE FUNCTION actualizar_fecha_modificacion();

CREATE TRIGGER trigger_ventas_modificacion
BEFORE UPDATE ON ventas
FOR EACH ROW
EXECUTE FUNCTION actualizar_fecha_modificacion();

-- Trigger para actualizar total de venta automáticamente
CREATE OR REPLACE FUNCTION calcular_total_venta()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE ventas
    SET subtotal = (
        SELECT COALESCE(SUM(subtotal - descuento), 0)
        FROM venta_items
        WHERE id_venta = NEW.id_venta
    ),
    total = (
        SELECT COALESCE(SUM(subtotal - descuento), 0) + COALESCE(ventas.impuestos, 0)
        FROM venta_items
        WHERE id_venta = NEW.id_venta
    )
    WHERE id_venta = NEW.id_venta;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trigger_actualizar_total_venta
AFTER INSERT OR UPDATE OR DELETE ON venta_items
FOR EACH ROW
EXECUTE FUNCTION calcular_total_venta();

-- Trigger para registrar movimientos de inventario
CREATE OR REPLACE FUNCTION registrar_movimiento_inventario()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'UPDATE' AND OLD.cantidad != NEW.cantidad THEN
        INSERT INTO historial_inventario (id_producto, cantidad_anterior, cantidad_nueva, tipo_movimiento, motivo)
        VALUES (NEW.id_producto, OLD.cantidad, NEW.cantidad, 'Ajuste', 'Actualización manual');
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trigger_historial_inventario
AFTER UPDATE ON inventarios
FOR EACH ROW
EXECUTE FUNCTION registrar_movimiento_inventario();

-- ============================================
-- DATOS INICIALES
-- ============================================

-- Insertar usuario administrador por defecto
INSERT INTO usuarios (nombre_usuario, contrasena, nombre_completo, rol)
VALUES ('admin', '12345678', 'Administrador del Sistema', 'Admin');
-- NOTA: En producción, la contraseña debe estar hasheada (usar bcrypt, SHA256, etc.)

-- Insertar categorías iniciales
INSERT INTO categorias (nombre, descripcion) VALUES
('Cartón', 'Productos de cartón y embalaje'),
('Plástico', 'Productos plásticos y burbujas'),
('Vehículos', 'Vehículos de transporte'),
('Otros', 'Otros productos');

-- Insertar productos de ejemplo
INSERT INTO productos (nombre, descripcion, precio, id_categoria) VALUES
('Caja de cartón pequeña', 'Caja de cartón 30x30x30 cm', 25.00, 1),
('Caja de cartón mediana', 'Caja de cartón 50x50x50 cm', 45.00, 1),
('Caja de cartón grande', 'Caja de cartón 80x80x80 cm', 75.00, 1),
('Plástico burbuja rollo', 'Rollo de plástico burbuja 1m x 50m', 36.00, 2),
('Cinta adhesiva', 'Cinta adhesiva transparente 48mm x 100m', 15.00, 4),
('Papel kraft', 'Papel kraft para embalaje 1m x 100m', 120.00, 4);

-- Insertar inventarios iniciales
INSERT INTO inventarios (id_producto, cantidad, estado, ubicacion) VALUES
(1, 500, 'Disponible', 'Almacén A - Estante 1'),
(2, 300, 'Disponible', 'Almacén A - Estante 2'),
(3, 150, 'Disponible', 'Almacén A - Estante 3'),
(4, 200, 'Disponible', 'Almacén B - Estante 1'),
(5, 1000, 'Disponible', 'Almacén B - Estante 2'),
(6, 80, 'Disponible', 'Almacén B - Estante 3');

-- Insertar clientes de ejemplo
INSERT INTO clientes (rfc, nombre, tipo, email, telefono, direccion_fiscal, direccion_envio, metodo_pago) VALUES
('ABC123456789', 'Juan Pérez', 'Regular', 'juan@example.com', '555-1234', 'Calle Falsa 123', 'Av. Siempre Viva 742', 'Tarjeta'),
('XYZ987654321', 'María López', 'Premium', 'maria@example.com', '555-9876', 'Centro 505', 'Centro 505', 'Efectivo'),
('DEF456789123', 'Carlos Ramírez', 'Mayorista', 'carlos@example.com', '555-5555', 'Industrial 100', 'Industrial 100', 'Transferencia');

-- Insertar proveedores de ejemplo
INSERT INTO proveedores (nombre, contacto, telefono, email, direccion) VALUES
('Proveedor Cartón SA', 'Roberto García', '555-1111', 'ventas@cartonsa.com', 'Zona Industrial 200'),
('Plásticos del Norte', 'Ana Martínez', '555-2222', 'contacto@plasticosnorte.com', 'Parque Industrial 300'),
('Distribuidora General', 'Luis Hernández', '555-3333', 'info@distgeneral.com', 'Centro Comercial 400');

-- ============================================
-- VISTAS ÚTILES
-- ============================================

-- Vista de inventario con información de producto
CREATE VIEW vista_inventario_completo AS
SELECT 
    i.id_inventario,
    p.id_producto,
    p.nombre AS producto,
    c.nombre AS categoria,
    i.cantidad,
    i.estado,
    i.ubicacion,
    p.precio,
    (i.cantidad * p.precio) AS valor_total,
    i.fecha_actualizacion
FROM inventarios i
JOIN productos p ON i.id_producto = p.id_producto
LEFT JOIN categorias c ON p.id_categoria = c.id_categoria
WHERE p.activo = TRUE;

-- Vista de ventas con detalles
CREATE VIEW vista_ventas_completas AS
SELECT 
    v.id_venta,
    v.fecha,
    c.nombre AS cliente,
    c.rfc,
    v.estado,
    v.metodo_pago,
    v.subtotal,
    v.impuestos,
    v.total,
    v.fecha_entrega_estimada,
    v.fecha_entrega_real,
    COUNT(vi.id_venta_item) AS cantidad_items
FROM ventas v
JOIN clientes c ON v.id_cliente = c.id_cliente
LEFT JOIN venta_items vi ON v.id_venta = vi.id_venta
GROUP BY v.id_venta, c.nombre, c.rfc;

-- Vista de productos más vendidos
CREATE VIEW vista_productos_mas_vendidos AS
SELECT 
    p.id_producto,
    p.nombre AS producto,
    c.nombre AS categoria,
    SUM(vi.cantidad) AS total_vendido,
    SUM(vi.subtotal) AS ingresos_totales,
    COUNT(DISTINCT vi.id_venta) AS numero_ventas
FROM venta_items vi
JOIN productos p ON vi.id_producto = p.id_producto
LEFT JOIN categorias c ON p.id_categoria = c.id_categoria
GROUP BY p.id_producto, p.nombre, c.nombre
ORDER BY total_vendido DESC;

-- ============================================
-- FUNCIONES ÚTILES
-- ============================================

-- Función para obtener stock disponible de un producto
CREATE OR REPLACE FUNCTION obtener_stock_disponible(p_id_producto INTEGER)
RETURNS INTEGER AS $$
DECLARE
    v_cantidad INTEGER;
BEGIN
    SELECT cantidad INTO v_cantidad
    FROM inventarios
    WHERE id_producto = p_id_producto AND estado = 'Disponible';
    
    RETURN COALESCE(v_cantidad, 0);
END;
$$ LANGUAGE plpgsql;

-- Función para verificar si hay stock suficiente
CREATE OR REPLACE FUNCTION verificar_stock(p_id_producto INTEGER, p_cantidad_requerida INTEGER)
RETURNS BOOLEAN AS $$
DECLARE
    v_stock_disponible INTEGER;
BEGIN
    v_stock_disponible := obtener_stock_disponible(p_id_producto);
    RETURN v_stock_disponible >= p_cantidad_requerida;
END;
$$ LANGUAGE plpgsql;

-- ============================================
-- PERMISOS (ajustar según necesidades)
-- ============================================
-- GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO usuario_app;
-- GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO usuario_app;

-- ============================================
-- FIN DEL SCRIPT
-- ============================================
