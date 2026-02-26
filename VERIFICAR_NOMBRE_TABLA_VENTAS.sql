-- ============================================================
-- PROCEDIMIENTOS PARA AMBOS CASOS
-- Ejecuta el que corresponda a tu BD
-- ============================================================

-- OPCIÓN 1: Si tu tabla se llama "venta" (SINGULAR, minúscula)
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
    FROM venta  -- ? MINÚSCULA, SINGULAR
    ORDER BY Fecha_venta DESC
END
GO

-- ============================================================

-- OPCIÓN 2: Si tu tabla se llama "Ventas" (PLURAL, con V mayúscula)
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
    FROM Ventas  -- ? MAYÚSCULA, PLURAL
    ORDER BY Fecha_venta DESC
END
GO

-- ============================================================

-- OPCIÓN 3: Si tu tabla se llama "VENTAS" (TODO MAYÚSCULA)
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
    FROM VENTAS  -- ? TODO MAYÚSCULA
    ORDER BY Fecha_venta DESC
END
GO

-- ============================================================
-- PARA VERIFICAR CUÁL ES EL NOMBRE EXACTO:
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE='BASE TABLE' 
ORDER BY TABLE_NAME
GO
