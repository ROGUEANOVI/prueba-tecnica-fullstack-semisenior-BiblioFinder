/*
================================================================================
SCRIPT DE CREACIÓN DE BASE DE DATOS PARA BIBLIOFINDER
================================================================================
Este script crea la base de datos,  tablas, indices, tipos y procedimientos almacenados
necesarios para el funcionamiento de la aplicación.
*/

-- Creación de la base de datos si no existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BiblioFinderDB')
BEGIN
    CREATE DATABASE BiblioFinderDB;
END
GO

-- Establecer el contexto a la base de datos de BiblioFinder
USE BiblioFinderDB;
GO

--------------------------------------------------------------------------------
-- TABLAS
--------------------------------------------------------------------------------
-- Comprueba si la tabla no existe antes de crearla 
IF OBJECT_ID('dbo.HistorialBusquedas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.HistorialBusquedas (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Autor NVARCHAR(255) NOT NULL,
        Titulo NVARCHAR(255) NOT NULL,
        AnioPublicacion INT NULL,
        Editorial NVARCHAR(255) NULL,
        FechaConsulta DATETIME2 NOT NULL CONSTRAINT DF_HistorialBusquedas_FechaConsulta DEFAULT GETUTCDATE()
    );

    
    PRINT 'Tabla dbo.HistorialBusquedas creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla dbo.HistorialBusquedas ya existe. No se realizaron cambios.';
END
GO

--------------------------------------------------------------------------------
-- ÍNDICES
--------------------------------------------------------------------------------
-- Comprueba si el índice NO existe antes de crearlo
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_HistorialBusquedas_Autor' AND object_id = OBJECT_ID('dbo.HistorialBusquedas'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_HistorialBusquedas_Autor ON dbo.HistorialBusquedas(Autor);
    PRINT 'Índice IX_HistorialBusquedas_Autor creado exitosamente.';
END
ELSE
BEGIN
    PRINT 'El índice IX_HistorialBusquedas_Autor ya existe.';
END
GO

--------------------------------------------------------------------------------
-- TIPOS DE TABLA (TABLE TYPES)
--------------------------------------------------------------------------------
PRINT 'Creando el tipo TipoHistorialBusqueda...';
IF TYPE_ID('dbo.TipoHistorialBusqueda') IS NOT NULL
    DROP TYPE dbo.TipoHistorialBusqueda;
GO

CREATE TYPE dbo.TipoHistorialBusqueda AS TABLE(
    Autor NVARCHAR(255) NOT NULL,
    Titulo NVARCHAR(255) NOT NULL,
    AnioPublicacion INT NULL,
    Editorial NVARCHAR(255) NULL
);
GO
PRINT 'Tipo dbo.TipoHistorialBusqueda creado exitosamente.';
GO

--------------------------------------------------------------------------------
-- PROCEDIMIENTOS ALMACENADOS (STORED PROCEDURES)
--------------------------------------------------------------------------------
PRINT 'Creando Procedimientos Almacenados...';

-- SP para inserción masiva
IF OBJECT_ID('dbo.sp_InsertarHistorialBusqueda', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_InsertarHistorialBusqueda;
GO

CREATE PROCEDURE dbo.sp_InsertarHistorialBusqueda
    @HistoryData dbo.TipoHistorialBusqueda READONLY
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.HistorialBusquedas (Autor, Titulo, AnioPublicacion, Editorial)
    SELECT Autor, Titulo, AnioPublicacion, Editorial FROM @HistoryData;
END
GO
PRINT 'SP dbo.sp_InsertarHistorialBusqueda creado exitosamente.';
GO

-- SP para obtener todo el historial
IF OBJECT_ID('dbo.sp_ObtenerTodoHistorialBusqueda', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerTodoHistorialBusqueda;
GO

CREATE PROCEDURE dbo.sp_ObtenerTodoHistorialBusqueda
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.HistorialBusquedas ORDER BY FechaConsulta DESC;
END
GO
PRINT 'SP dbo.sp_ObtenerTodoHistorialBusqueda creado exitosamente.';
GO

-- SP para obtener el último registro por autor
IF OBJECT_ID('dbo.sp_ObtenerUltimoHistorialBusquedaPorAutor', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerUltimoHistorialBusquedaPorAutor;
GO

CREATE PROCEDURE dbo.sp_ObtenerUltimoHistorialBusquedaPorAutor
    @NombreAutor NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 * FROM dbo.HistorialBusquedas
    WHERE Autor = @NombreAutor
    ORDER BY FechaConsulta DESC;
END
GO
PRINT 'SP dbo.sp_ObtenerUltimoHistorialBusquedaPorAutor creado exitosamente.';
GO

PRINT '=================================================='
PRINT 'Configuración de la base de datos completada.'
PRINT '=================================================='