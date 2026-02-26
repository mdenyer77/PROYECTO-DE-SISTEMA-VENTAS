-- ============================================================
-- PROCEDIMIENTOS SQL QUE DEBES CREAR EN TU BASE DE DATOS
-- ============================================================
-- Ejecuta TODOS estos procedimientos en SQL Server Management Studio
-- para que los DataGrids funcionen correctamente

-- 1. PROCEDIMIENTO PARA LISTAR VENTAS CON DATOS DEL CLIENTE
CREATE PROCEDURE sp_ListarVentasConCliente
AS
BEGIN
    SELECT 
        v.Fecha_venta,
        c.Nombre AS Cliente_Nombre,
        c.Telefono AS Cliente_Telefono,
        c.Direccion AS Cliente_Direccion,
        v.Total_general,
        CASE WHEN v.Estado_venta = 1 THEN 'Activa' ELSE 'Cancelada' END AS Estado_venta
    FROM Ventas v
    INNER JOIN Cliente c ON v.ID_cliente = c.ID_cliente
    ORDER BY v.Fecha_venta DESC
END
GO

-- 2. PROCEDIMIENTO PARA LISTAR DETALLES DE VENTA COMPLETO
CREATE PROCEDURE sp_ListarDetalleVentaCompleto
    @ID_venta INT
AS
BEGIN
    SELECT 
        p.Nombre_Producto AS Producto_Nombre,
        c.Nombre_Categoria AS Categoria_Nombre,
        dv.Cantidad,
        dv.Precio AS Precio_Unitario,
        (dv.Cantidad * dv.Precio) AS Subtotal
    FROM Detalle_Venta dv
    INNER JOIN Productos p ON dv.ID_producto = p.ID_Producto
    INNER JOIN Categoria c ON p.ID_categoria = c.ID_Categoria
    WHERE dv.ID_venta = @ID_venta
    ORDER BY dv.ID_detalle_venta
END
GO

-- 3. PROCEDIMIENTO PARA LISTAR PRODUCTOS CON CATEGORÍA
CREATE PROCEDURE sp_ListarProductosConCategoria
AS
BEGIN
    SELECT 
        p.Nombre_Producto,
        c.Nombre_Categoria AS Categoria_Nombre,
        p.Precio_Producto,
        p.stock AS Stock
    FROM Productos p
    INNER JOIN Categoria c ON p.ID_categoria = c.ID_Categoria
    ORDER BY p.Nombre_Producto
END
GO

-- ============================================================
-- LISTO! Ya pueden usar los Forms actualizados
-- ============================================================

-- VERIFICAR QUE LOS PROCEDIMIENTOS EXISTEN:
-- SELECT * FROM sys.procedures WHERE name LIKE 'sp_Listar%' OR name LIKE 'sp_ListarProductos%'

-- EJECUTAR PROCEDIMIENTOS DE PRUEBA:
-- EXEC sp_ListarVentasConCliente
-- EXEC sp_ListarProductosConCategoria
-- EXEC sp_ListarDetalleVentaComple