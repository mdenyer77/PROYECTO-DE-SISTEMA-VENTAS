-- ============================================================
-- PROCEDIMIENTO PARA LISTAR VENTAS (TABLA: venta)
-- EJECUTA ESTO EN SQL SERVER
-- ============================================================

-- Primero elimina si existe
IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'sp_listar_venta')
    DROP PROCEDURE sp_listar_venta
GO

-- Crea el procedimiento correcto (usando tabla "venta" - SINGULAR)
CREATE PROCEDURE sp_listar_venta
AS
BEGIN
    SELECT 
        ID_venta,
        Fecha_venta,
        ID_cliente,
        Total_general,
        Estado_venta
    FROM venta  -- ? TABLA: venta (SINGULAR, minúscula)
    ORDER BY Fecha_venta DESC
END
GO

-- Verifica que se creó correctamente
SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_listar_venta'
GO

-- Prueba el procedimiento
EXEC sp_listar_venta
GO

-- ============================================================
-- Si ves tus ventas aquí, ¡está funcionando!
-- Ahora cierra SQL Server y abre Form3 de tu aplicación
-- ============================================================
