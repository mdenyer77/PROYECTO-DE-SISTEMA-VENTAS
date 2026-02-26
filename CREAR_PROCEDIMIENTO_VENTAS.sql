-- ============================================================
-- PROCEDIMIENTO PARA LISTAR VENTAS CON DATOS DEL CLIENTE
-- EJECUTA ESTO EN TU BASE DE DATOS SQL SERVER
-- ============================================================

-- Primero, VERIFICA si el procedimiento ya existe:
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_ListarVentasConCliente')
BEGIN
    DROP PROCEDURE sp_ListarVentasConCliente
END
GO

-- Ahora CREA el procedimiento:
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

-- VERIFICAR que se creó correctamente:
SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ListarVentasConCliente'
GO

-- Prueba el procedimiento:
EXEC sp_ListarVentasConCliente
GO

-- Si ves resultados, ¡está funcionando!
