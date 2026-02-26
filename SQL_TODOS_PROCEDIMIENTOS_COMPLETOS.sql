-- ============================================================
-- TODOS LOS PROCEDIMIENTOS NECESARIOS PARA EL PROYECTO
-- COPIA Y EJECUTA ESTOS EN SQL SERVER MANAGEMENT STUDIO
-- ============================================================

-- ============================================================
-- 1. LISTAR VENTAS CON DATOS DEL CLIENTE (CRÍTICO PARA FORM3)
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_ListarVentasConCliente')
    DROP PROCEDURE sp_ListarVentasConCliente
GO

CREATE PROCEDURE sp_ListarVentasConCliente
AS
BEGIN
    SELECT 
        v.ID_venta,
        v.Fecha_venta,
        c.ID_cliente,
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

-- ============================================================
-- 2. LISTAR TODAS LAS VENTAS (básico)
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_listar_venta')
    DROP PROCEDURE sp_listar_venta
GO

CREATE PROCEDURE sp_listar_venta
AS
BEGIN
    SELECT 
        ID_venta,
        Fecha_venta,
        ID_cliente,
        Total_general,
        Estado_venta
    FROM Ventas
    ORDER BY Fecha_venta DESC
END
GO

-- ============================================================
-- 3. INSERTAR VENTA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_insertar_venta')
    DROP PROCEDURE sp_insertar_venta
GO

CREATE PROCEDURE sp_insertar_venta
    @ID_cliente INT,
    @Total_general DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Ventas (Fecha_venta, ID_cliente, Total_general, Estado_venta)
    VALUES (GETDATE(), @ID_cliente, @Total_general, 1)
END
GO

-- ============================================================
-- 4. ACTUALIZAR VENTA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_actualizar_venta')
    DROP PROCEDURE sp_actualizar_venta
GO

CREATE PROCEDURE sp_actualizar_venta
    @ID_venta INT,
    @ID_cliente INT,
    @Total_general DECIMAL(10,2),
    @Estado_venta BIT
AS
BEGIN
    UPDATE Ventas
    SET ID_cliente = @ID_cliente,
        Total_general = @Total_general,
        Estado_venta = @Estado_venta
    WHERE ID_venta = @ID_venta
END
GO

-- ============================================================
-- 5. DESACTIVAR VENTA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_desactivar_venta')
    DROP PROCEDURE sp_desactivar_venta
GO

CREATE PROCEDURE sp_desactivar_venta
    @ID_venta INT
AS
BEGIN
    UPDATE Ventas
    SET Estado_venta = 0
    WHERE ID_venta = @ID_venta
END
GO

-- ============================================================
-- 6. LISTAR CLIENTES
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_ListarClientes')
    DROP PROCEDURE sp_ListarClientes
GO

CREATE PROCEDURE sp_ListarClientes
AS
BEGIN
    SELECT 
        ID_cliente,
        Nombre,
        Direccion,
        Telefono,
        Correo
    FROM Cliente
    ORDER BY Nombre
END
GO

-- ============================================================
-- 7. INSERTAR CLIENTE
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertarCliente')
    DROP PROCEDURE sp_InsertarCliente
GO

CREATE PROCEDURE sp_InsertarCliente
    @Nombre NVARCHAR(100),
    @Direccion NVARCHAR(200),
    @Telefono NVARCHAR(20),
    @Correo NVARCHAR(100)
AS
BEGIN
    INSERT INTO Cliente (Nombre, Direccion, Telefono, Correo)
    VALUES (@Nombre, @Direccion, @Telefono, @Correo)
END
GO

-- ============================================================
-- 8. ACTUALIZAR CLIENTE
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_ActualizarCliente')
    DROP PROCEDURE sp_ActualizarCliente
GO

CREATE PROCEDURE sp_ActualizarCliente
    @ID_cliente INT,
    @Nombre NVARCHAR(100),
    @Direccion NVARCHAR(200),
    @Telefono NVARCHAR(20),
    @Correo NVARCHAR(100)
AS
BEGIN
    UPDATE Cliente
    SET Nombre = @Nombre,
        Direccion = @Direccion,
        Telefono = @Telefono,
        Correo = @Correo
    WHERE ID_cliente = @ID_cliente
END
GO

-- ============================================================
-- 9. ELIMINAR CLIENTE
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_EliminarCliente')
    DROP PROCEDURE sp_EliminarCliente
GO

CREATE PROCEDURE sp_EliminarCliente
    @ID_cliente INT
AS
BEGIN
    DELETE FROM Cliente WHERE ID_cliente = @ID_cliente
END
GO

-- ============================================================
-- 10. LISTAR PRODUCTOS
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_listar_productos')
    DROP PROCEDURE sp_listar_productos
GO

CREATE PROCEDURE sp_listar_productos
AS
BEGIN
    SELECT 
        ID_Producto,
        Nombre_Producto,
        Precio_Producto,
        stock,
        ID_categoria
    FROM Productos
    ORDER BY Nombre_Producto
END
GO

-- ============================================================
-- 11. INSERTAR PRODUCTO
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_insertar_productos')
    DROP PROCEDURE sp_insertar_productos
GO

CREATE PROCEDURE sp_insertar_productos
    @Nombre_Producto NVARCHAR(100),
    @Precio_Producto DECIMAL(10,2),
    @stock INT,
    @ID_categoria INT
AS
BEGIN
    INSERT INTO Productos (Nombre_Producto, Precio_Producto, stock, ID_categoria)
    VALUES (@Nombre_Producto, @Precio_Producto, @stock, @ID_categoria)
END
GO

-- ============================================================
-- 12. ACTUALIZAR PRODUCTO
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_actualizar_productos')
    DROP PROCEDURE sp_actualizar_productos
GO

CREATE PROCEDURE sp_actualizar_productos
    @ID_Producto INT,
    @Nombre_Producto NVARCHAR(100),
    @Precio_Producto DECIMAL(10,2),
    @stock INT,
    @ID_categoria INT
AS
BEGIN
    UPDATE Productos
    SET Nombre_Producto = @Nombre_Producto,
        Precio_Producto = @Precio_Producto,
        stock = @stock,
        ID_categoria = @ID_categoria
    WHERE ID_Producto = @ID_Producto
END
GO

-- ============================================================
-- 13. DESACTIVAR PRODUCTO
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_desactivar_productos')
    DROP PROCEDURE sp_desactivar_productos
GO

CREATE PROCEDURE sp_desactivar_productos
    @ID_Producto INT
AS
BEGIN
    DELETE FROM Productos WHERE ID_Producto = @ID_Producto
END
GO

-- ============================================================
-- 14. LISTAR CATEGORÍAS
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_listar_categoria')
    DROP PROCEDURE sp_listar_categoria
GO

CREATE PROCEDURE sp_listar_categoria
AS
BEGIN
    SELECT 
        ID_categoria,
        Nombre_Categoria,
        Descripcion
    FROM Categoria
    ORDER BY Nombre_Categoria
END
GO

-- ============================================================
-- 15. CREAR CATEGORÍA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_Crear_Categoria')
    DROP PROCEDURE sp_Crear_Categoria
GO

CREATE PROCEDURE sp_Crear_Categoria
    @NOMBRE_CAT NVARCHAR(100)
AS
BEGIN
    INSERT INTO Categoria (Nombre_Categoria, Estado)
    VALUES (@NOMBRE_CAT, 1)
END
GO

-- ============================================================
-- 16. ACTUALIZAR CATEGORÍA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_actualizar_categoria')
    DROP PROCEDURE sp_actualizar_categoria
GO

CREATE PROCEDURE sp_actualizar_categoria
    @ID_categoria INT,
    @Nombre_categoria NVARCHAR(100),
    @Descripcion NVARCHAR(500)
AS
BEGIN
    UPDATE Categoria
    SET Nombre_Categoria = @Nombre_categoria,
        Descripcion = @Descripcion
    WHERE ID_categoria = @ID_categoria
END
GO

-- ============================================================
-- 17. DESACTIVAR CATEGORÍA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_desactivar_categoria')
    DROP PROCEDURE sp_desactivar_categoria
GO

CREATE PROCEDURE sp_desactivar_categoria
    @ID_categoria INT
AS
BEGIN
    DELETE FROM Categoria WHERE ID_categoria = @ID_categoria
END
GO

-- ============================================================
-- 18. LISTAR DETALLES DE VENTA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_listar_detalle_venta')
    DROP PROCEDURE sp_listar_detalle_venta
GO

CREATE PROCEDURE sp_listar_detalle_venta
AS
BEGIN
    SELECT 
        ID_detalle_venta,
        ID_venta,
        ID_producto,
        Cantidad,
        Precio,
        Estado
    FROM Detalle_Venta
    ORDER BY ID_detalle_venta
END
GO

-- ============================================================
-- 19. INSERTAR DETALLE DE VENTA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_insertar_detalle_venta')
    DROP PROCEDURE sp_insertar_detalle_venta
GO

CREATE PROCEDURE sp_insertar_detalle_venta
    @ID_venta INT,
    @ID_producto INT,
    @Cantidad INT,
    @Precio DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Detalle_Venta (ID_venta, ID_producto, Cantidad, Precio, Estado)
    VALUES (@ID_venta, @ID_producto, @Cantidad, @Precio, 1)
END
GO

-- ============================================================
-- 20. ACTUALIZAR DETALLE DE VENTA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_actualizar_detalle_venta')
    DROP PROCEDURE sp_actualizar_detalle_venta
GO

CREATE PROCEDURE sp_actualizar_detalle_venta
    @ID_detalle_venta INT,
    @Cantidad INT,
    @Precio DECIMAL(10,2)
AS
BEGIN
    UPDATE Detalle_Venta
    SET Cantidad = @Cantidad,
        Precio = @Precio
    WHERE ID_detalle_venta = @ID_detalle_venta
END
GO

-- ============================================================
-- 21. DESACTIVAR DETALLE DE VENTA
-- ============================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_desactivar_detalle_venta')
    DROP PROCEDURE sp_desactivar_detalle_venta
GO

CREATE PROCEDURE sp_desactivar_detalle_venta
    @ID_detalle_venta INT
AS
BEGIN
    DELETE FROM Detalle_Venta WHERE ID_detalle_venta = @ID_detalle_venta
END
GO

-- ============================================================
-- VERIFICACIÓN FINAL
-- ============================================================
-- Ejecuta esto para verificar que todos los procedimientos se crearon:
SELECT * FROM sys.objects WHERE type = 'P' ORDER BY name
GO

-- ============================================================
-- LISTO!
-- ============================================================
-- Ahora puedes cerrar SQL Server y abrir Form3 de nuevo
-- Deberías ver todas las ventas cargadas correctamente
