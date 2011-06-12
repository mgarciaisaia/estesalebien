-- Seteo la DB
USE [GD1C2011]
GO

-- Genero el Schema
/**
 * Chequeo la existencia del schema antes de crearlo. Deberia pensar en eliminar las tablas y otros objetos tambien.
 */
IF NOT EXISTS(SELECT 1 FROM information_schema.schemata WHERE
schema_name='ESTELOCAMBIAMOS')
EXEC ('create schema ESTELOCAMBIAMOS AUTHORIZATION gd');
GO


PRINT 'Tabla Provincias'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[Provincias] (
	[Codigo] [tinyint] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](53) UNIQUE
)
GO

INSERT INTO ESTELOCAMBIAMOS.Provincias ([Nombre])
(SELECT DISTINCT SUC_PROVINCIA
FROM gd_esquema.Maestra)

GO


PRINT 'Tabla TipoEmpleado'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[TiposEmpleado] (
	[Codigo] [tinyint] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [nvarchar] (8) UNIQUE
)
GO

INSERT INTO ESTELOCAMBIAMOS.TiposEmpleado ([Descripcion])
(SELECT DISTINCT EMPLEADO_TIPO
FROM gd_esquema.Maestra)

GO


PRINT 'Tabla TipoSucursal'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[TiposSucursal] (
	[Codigo] [tinyint] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [nvarchar] (12) UNIQUE
)
GO

INSERT INTO ESTELOCAMBIAMOS.TiposSucursal ([Descripcion])
(SELECT DISTINCT SUC_TIPO
FROM gd_esquema.Maestra)

GO


PRINT 'Tabla Sucursales'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[Sucursales] (
	[Provincia] [tinyint] PRIMARY KEY REFERENCES [ESTELOCAMBIAMOS].[Provincias] (Codigo),
	[Tipo] [tinyint] REFERENCES [ESTELOCAMBIAMOS].[TiposSucursal] (Codigo),
	[Direccion] [nvarchar] (255),
	[Telefono] [nvarchar] (20)
)
GO

INSERT INTO ESTELOCAMBIAMOS.Sucursales ([Provincia], [Tipo], [Direccion], [Telefono])
(SELECT DISTINCT Provincias.Codigo, TiposSucursal.Codigo, SUC_DIR, SUC_TEL
FROM gd_esquema.Maestra LEFT JOIN ESTELOCAMBIAMOS.Provincias ON Maestra.SUC_PROVINCIA = Provincias.Nombre
		LEFT JOIN ESTELOCAMBIAMOS.TiposSucursal ON Maestra.SUC_TIPO = TiposSucursal.Descripcion)

GO



CREATE VIEW [ESTELOCAMBIAMOS].[SucursalProvincia] AS
SELECT Provincias.Codigo AS Codigo, Provincias.Nombre AS Provincia, TiposSucursal.Descripcion AS Tipo, Sucursales.Direccion AS Direccion, Sucursales.Telefono AS Telefono
FROM ESTELOCAMBIAMOS.Provincias LEFT JOIN ESTELOCAMBIAMOS.Sucursales ON Provincias.Codigo = Sucursales.Provincia
		LEFT JOIN ESTELOCAMBIAMOS.TiposSucursal ON Sucursales.Tipo = TiposSucursal.Codigo
GO



PRINT 'Tabla Empleados'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[Empleados] (
	[DNI] [numeric](8, 0) PRIMARY KEY,
	[Nombre] [nvarchar] (30),
	[Apellido] [nvarchar] (30),
	[Mail] [nvarchar] (255) NULL,
	[Direccion] [nvarchar] (255),
	[Telefono] [nvarchar] (20) NULL,
	[Provincia] [tinyint] REFERENCES [ESTELOCAMBIAMOS].[Provincias] (Codigo),
	[Tipo] [tinyint] REFERENCES [ESTELOCAMBIAMOS].[TiposEmpleado] (Codigo),
	[Sucursal] [tinyint] REFERENCES [ESTELOCAMBIAMOS].[Sucursales] (Provincia),
	[Habilitado] [tinyint] DEFAULT 1
)
GO

/*
 * FIXME: agregar un TRIGGER que de de baja al Cliente que tenga el mismo DNI cuando hago un INSERT aca
 */

/*
 * Este UNION lo cambiaria por una funcion que relacione empleado con sucursal, o por un CASE en el SELECT, pero el UNION es una locura
 */
INSERT INTO ESTELOCAMBIAMOS.Empleados ([DNI], [Nombre], [Apellido], [Mail], [Direccion], [Telefono], [Provincia], [Tipo], [Sucursal])
((SELECT DISTINCT Maestra.EMPLEADO_DNI, Maestra.EMPLEADO_NOMBRE, Maestra.EMPLEADO_APELLIDO, Maestra.EMPLEADO_MAIL, Maestra.EMPLEADO_DIR, '', Provincias.Codigo, TiposEmpleado.Codigo, SucursalProvincia.Codigo
FROM gd_esquema.Maestra LEFT JOIN ESTELOCAMBIAMOS.Provincias ON Maestra.EMPLEADO_PROVINCIA = Provincias.Nombre
		LEFT JOIN ESTELOCAMBIAMOS.TiposEmpleado ON Maestra.EMPLEADO_TIPO = TiposEmpleado.Descripcion
		LEFT JOIN ESTELOCAMBIAMOS.SucursalProvincia ON Maestra.SUC_PROVINCIA = SucursalProvincia.Provincia
WHERE EMPLEADO_TIPO = 'Vendedor')

UNION

(SELECT DISTINCT Maestra.EMPLEADO_DNI, Maestra.EMPLEADO_NOMBRE, Maestra.EMPLEADO_APELLIDO, Maestra.EMPLEADO_MAIL, Maestra.EMPLEADO_DIR, '', Provincias.Codigo, TiposEmpleado.Codigo, SucursalProvincia.Codigo
FROM gd_esquema.Maestra LEFT JOIN ESTELOCAMBIAMOS.Provincias ON Maestra.EMPLEADO_PROVINCIA = Provincias.Nombre
		LEFT JOIN ESTELOCAMBIAMOS.TiposEmpleado ON Maestra.EMPLEADO_TIPO = TiposEmpleado.Descripcion
		LEFT JOIN ESTELOCAMBIAMOS.SucursalProvincia ON Maestra.SUC_PROVINCIA = SucursalProvincia.Provincia
WHERE EMPLEADO_TIPO = 'Analista' AND SucursalProvincia.Tipo = 'Sede Central'))

GO


PRINT 'Tabla Usuarios'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[Usuarios] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar] (30) UNIQUE,
	[Password] [nvarchar] (64),
	[Empleado] [numeric](8, 0) FOREIGN KEY REFERENCES ESTELOCAMBIAMOS.Empleados(DNI),
	[Habilitado] [tinyint] DEFAULT 1,
	[Intentos] [tinyint] DEFAULT 0
)
GO

/*
 * FIXME: aca falta hacer INSERT de los Usuarios
 * FIXME: el usuario 'admin' tiene pass 'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7'
 * FIXME: que Empleado le asignamos al admin?
 */
 
 /*
  * Para los username podria ser algo asi:
  *
  * SELECT REPLACE(LOWER(Nombre + Apellido), ' ','') AS Username
  * FROM ESTELOCAMBIAMOS.Empleados
  * ORDER BY Username
  */

PRINT 'Tabla Roles'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[Roles] (
	[Codigo] [int] IDENTITY(1, 1) PRIMARY KEY,
	[Nombre] [nvarchar] (60),
	[Habilitado] [tinyint] DEFAULT 1
)

/*
 * Rol pre-creado ("Administrador General") para el usuario que crea el script ("admin")
 */
INSERT INTO ESTELOCAMBIAMOS.Roles ([Nombre])
VALUES ('Administrador General');

/*
 * FIXME: tal vez habria que crear algunos Roles mas preseteados (lo decian en algun mail, creo)
 */

GO


/*
 * Jamas vamos a hacer una busqueda por la "PRIMARY KEY", pero un UNIQUE compuesto permitiria
 * que alguno de los dos valores sea nulo
 */
CREATE TABLE [ESTELOCAMBIAMOS].[Asignaciones] (
	[Usuario] [int],
	[Rol] [int],
	PRIMARY KEY (Usuario, Rol)
)

/*
 * FIXME: aca va el insert de la asignacion del admin
 * FIXME: podriamos hacer algunas asignaciones mas (un rol por tipo de empleado?)
 */


/*
 * Funcionalidades: las posibles funcionalidades que se le pueden asignar
 * a los roles del sistema.
 * Son fijas, definidas por extension en el enunciado del TP.
 */
PRINT 'Tabla Funcionalidades'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[Funcionalidades] (
	[Codigo] [tinyint] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [nvarchar](19) UNIQUE
)
GO

INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Empleado');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Rol');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Usuario');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Cliente');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Producto');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('Asignacion de stock');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('Facturacion');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('Efectuar Pago');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('Tablero de Control');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('Clientes Premium');
INSERT INTO ESTELOCAMBIAMOS.Funcionalidades ([Descripcion]) VALUES ('Mejores Categorias');

GO


PRINT 'Tabla FuncionalidadesRol'
GO

/*
 * Jamas vamos a hacer una busqueda por la "PRIMARY KEY", pero un UNIQUE compuesto permitiria
 * que alguno de los dos valores sea nulo
 */
CREATE TABLE [ESTELOCAMBIAMOS].[FuncionalidadesRol] (
	[Rol] [int] REFERENCES [ESTELOCAMBIAMOS].[Roles] (Codigo),
	[Funcionalidad] [tinyint] REFERENCES [ESTELOCAMBIAMOS].[Funcionalidades] (Codigo),
	PRIMARY KEY (Rol, Funcionalidad)
)

/*
 * Le asignamos todas las funcionalidades al rol pre-creado
 */
INSERT INTO ESTELOCAMBIAMOS.FuncionalidadesRol ([Rol], [Funcionalidad])
(SELECT Roles.Codigo, Funcionalidades.Codigo
FROM ESTELOCAMBIAMOS.Roles, ESTELOCAMBIAMOS.Funcionalidades
WHERE Roles.Nombre = 'Administrador General');

GO

PRINT 'Tabla Cliente'
GO

/*
 * DNI como varchar no tiene sentido, por eso la declaro numeric (y, ademas, la hago compartir tipo con
 * el DNI del Empleado, al que le aplica el CONSTRAINT).
 *
 * Lo de "respetar los tipos de datos" del enunciado pareciera que hablaba de tipos a nivel conceptual
 * en lugar de nivel implementacion, segun el mail de Miguel Lopez del 28 de Mayo
 */
CREATE TABLE [ESTELOCAMBIAMOS].[Clientes] (
	[DNI] [numeric] (8, 0) PRIMARY KEY,
	[Nombre] [nvarchar] (30) not null, --index   Lo saco porqque sino rompe!
	[Apellido] [nvarchar] (30) not null, --index
	[Mail] [nvarchar] (255),
	[Telefono] [nvarchar] (20),
	[Direccion] [nvarchar] (255),
	[Provincia] [tinyint] FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Provincias] (Codigo),
	[Habilitado] [tinyint] DEFAULT 1
)

/*
 * FIXME: aca va un constraint para que el dni no se repita con el de Empleados
 */

INSERT INTO [ESTELOCAMBIAMOS].[Clientes]([DNI],[NOMBRE],[APELLIDO],[MAIL],[Telefono],[Direccion],
											[Provincia],[Habilitado])
(SELECT DISTINCT [CLI_DNI] ,[CLI_NOMBRE] ,[CLI_APELLIDO] ,[CLI_MAIL],null,null,null,1
FROM GD_ESQUEMA.MAESTRA
WHERE CLI_DNI IS NOT NULL)
GO


PRINT 'TABLA CATEGORIAS'
GO 

CREATE PROCEDURE [ESTELOCAMBIAMOS].[PARSE](@CateList varchar(100))
AS
BEGIN
	DECLARE @Delimeter varchar(1)
	SET @Delimeter = '¦'
	DECLARE @Cate varchar(50)
	DECLARE @CatePadre varchar(50)
	SET @CatePadre = ''
	DECLARE @CatePadreCod INT
	SET @CatePadreCod = NULL
	DECLARE @StartPos int, @Length int
	WHILE LEN(@CateList) > 0
	  BEGIN
		SET @StartPos = CHARINDEX(@Delimeter, @CateList)
		IF @StartPos < 0 SET @StartPos = 0
		SET @Length = LEN(@CateList) - @StartPos - 1
		IF @Length < 0 SET @Length = 0
		IF @StartPos > 0
		  BEGIN
			SET @Cate = SUBSTRING(@CateList, 1, @StartPos - 1)
			SET @CateList = SUBSTRING(@CateList, @StartPos + 1, LEN(@CateList) - @StartPos)
		  END
		ELSE
		  BEGIN
			SET @Cate = @CateList
			SET @CateList = ''
		  END
		if not @Cate in (select nombre from [ESTELOCAMBIAMOS].Categorias WHERE (@CATEPADRECOD = PADRE OR (@CATEPADRECOD IS NULL AND PADRE IS NULL)))
		  BEGIN	
			INSERT INTO [ESTELOCAMBIAMOS].Categorias(NOMBRE,Padre) VALUES(@Cate,@CatePadreCod)				
		  END
		SET @CatePadre = @Cate
		SELECT @CatePadreCod = CODIGO FROM [ESTELOCAMBIAMOS].Categorias WHERE ( NOMBRE=@Cate AND (PADRE=@CatePadreCod OR PADRE IS NULL))
	  END
	RETURN @CATEPADRECOD
END
GO
 
 CREATE TABLE [ESTELOCAMBIAMOS].[Categorias] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](100) NULL,
	[Padre] [int] NULL DEFAULT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Categorias] (Codigo)
 )
 /*
 *  Inserta todas las categorias
 */
declare @au_id nvarchar(100)
select distinct @au_id =  min( producto_cate ) from gd_esquema.Maestra
while @au_id is not null
begin
	EXECUTE [ESTELOCAMBIAMOS].[PARSE] @au_id    
	select @au_id = min( producto_cate ) from gd_esquema.Maestra where producto_cate > @au_id
end
GO
 

CREATE TABLE [ESTELOCAMBIAMOS].[Marcas] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar] (30) UNIQUE
 )
 
INSERT INTO [ESTELOCAMBIAMOS].[Marcas](NOMBRE)
  SELECT DISTINCT PRODUCTO_MARCA
  FROM gd_esquema.Maestra
  WHERE PRODUCTO_MARCA IS NOT NULL
 
 nombre
 
 /*
  * FIXME: aca falta agregarle INDEX a Precio y Nombre
  * En la base maestra en el campo nombre los productos tienen un codigo al final, antes yo habia parseado ese codigo y lo puse como codigo de producto, me aprece que lo mejor es seguir haciendo eso y no generar otro codigo mas.
  * si me das el OK lo hago como antes sino lo sigo como lo planteas vos
  */
CREATE TABLE [ESTELOCAMBIAMOS].[Productos] (
	[Codigo] [INT] IDENTITY(1248681716,1) PRIMARY KEY,
	[Nombre] [nvarchar] (100),
	[Descripcion] [nvarchar] (100),
	[Categoria] [int] FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Categorias] (Codigo),
	[Precio] [float] CHECK (Precio > 0),
	[Habilitado] [tinyint] DEFAULT 1,
	[Marca] [int] FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Marcas]
 )
GO
SET IDENTITY_INSERT [ESTELOCAMBIAMOS].[Productos] ON;
BEGIN
declare @cod int
DECLARE @CODIGO INT
DECLARE @NOM [nvarchar] (100)
DECLARE @DESC [nvarchar] (100)
DECLARE @CATE [nvarchar] (100)
DECLARE @PRECIO [float]
DECLARE @MARCA [int]
declare CURSORITO cursor for (SELECT  DISTINCT SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10),
						SUBSTRING(PRODUCTO_NOMBRE,1,LEN(PRODUCTO_NOMBRE)-11),PRODUCTO_DESC,PRODUCTO_CATE,[PRODUCTO_PRECIO],CODIGO
				FROM GD_ESQUEMA.MAESTRA JOIN [ESTELOCAMBIAMOS].[MARCAS] ON (PRODUCTO_MARCA = NOMBRE)
				WHERE PRODUCTO_PRECIO <> '0')
open CURSORITO
fetch next from CURSORITO
into @CODIGO,@NOM,@DESC,@CATE,@PRECIO,@MARCA
while @@fetch_status = 0
  begin
    EXECUTE @COD=[ESTELOCAMBIAMOS].[PARSE] @CATE
    INSERT INTO [ESTELOCAMBIAMOS].[Productos](CODIGO,[NOMBRE],[DESCRIPCION],[CATEGORIA],[PRECIO],[HABILITADO],[MARCA])
    VALUES(@CODIGO,@NOM,@DESC,@COD,@PRECIO,'1',@MARCA)
    fetch next from CURSORITO
    into @CODIGO,@NOM,@DESC,@CATE,@PRECIO,@MARCA
  end
close CURSORITO
deallocate CURSORITO
END
SET IDENTITY_INSERT [ESTELOCAMBIAMOS].[Productos] OFF;
GO


CREATE TABLE [ESTELOCAMBIAMOS].[Facturas] (
	[Numero] [int] IDENTITY PRIMARY KEY,
	[Fecha] [datetime],
	[Descuento] [float],
	[Cuotas] [tinyint],
	[Sucursal] [tinyint] FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Sucursales] (Provincia),
	[Vendedor] [numeric] (8, 0) FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Empleados] (DNI),
	[Cliente] [numeric] (8,0) FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Clientes] (DNI)
)

SET IDENTITY_INSERT [ESTELOCAMBIAMOS].[Facturas] ON;

INSERT INTO [ESTELOCAMBIAMOS].[Facturas] (Numero, Fecha, Descuento, Cuotas, Sucursal, Vendedor, Cliente)
(SELECT DISTINCT FACTURA_NRO, FACTURA_FECHA, FACTURA_DESCUENTO, FACTURA_CANT_COUTAS, Codigo, EMPLEADO_DNI, CONVERT(NUMERIC, CLI_DNI)
FROM GD_ESQUEMA.MAESTRA LEFT JOIN ESTELOCAMBIAMOS.Provincias ON Provincias.Nombre = SUC_PROVINCIA
WHERE FACTURA_NRO <> 0)


 /*
  * FIXME: agregar indices y esas cosas
  */
CREATE TABLE [ESTELOCAMBIAMOS].[ItemFactura] (
	[Factura] [int] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Facturas] (Numero),
	[Producto] [int] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Productos] (Codigo),
	[PrecioUnitario] [float] CONSTRAINT CHECK PrecioUnitario > 0,
	[Cantidad] [int]
)


CREATE TABLE [ESTELOCAMBIAMOS].[Pago] (
	[Factura] [int] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Facturas] (Numero),
	[Sucursal] [tinyint] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Sucursales] (Provincia),
	[Cuotas] [tinyint], --CONSTRAINT menor a pendientes
	[Fecha] [datetime],
	[Cobrador] [numeric] (8, 0) FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Empleados] (DNI)
)


/*
 * Un MovimientoStock con Cantidad negativa representa una salida
 */
CREATE TABLE [ESTELOCAMBIAMOS].[MovimientosStock] (
	[Producto] [int] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Productos] (Codigo),
	[Sucursal] [tinyint] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Sucursales] (Provincia),
	[Auditor] [numeric](8,0) NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Empleados] (DNI), --CHECK Auditor.Tipo.Descripcion = Analista
	[Cantidad] [int] NOT NULL,
	[Fecha] [datetime]
)



CREATE VIEW [ESTELOCAMBIAMOS].[Stocks] AS
SELECT Producto, Sucursal, SUM(Cantidad)
FROM ESTELOCAMBIAMOS.MovimientosStock
GROUP BY Producto, Sucursal