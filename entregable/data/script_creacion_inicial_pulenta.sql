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

PRINT 'Insert Provincias'
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

PRINT 'Insert TipoEmpleado'
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

PRINT 'Insert TipoSucursal'
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

PRINT 'Insert Sucursales'
GO

INSERT INTO ESTELOCAMBIAMOS.Sucursales ([Provincia], [Tipo], [Direccion], [Telefono])
(SELECT DISTINCT Provincias.Codigo, TiposSucursal.Codigo, SUC_DIR, SUC_TEL
FROM gd_esquema.Maestra LEFT JOIN ESTELOCAMBIAMOS.Provincias ON Maestra.SUC_PROVINCIA = Provincias.Nombre
		LEFT JOIN ESTELOCAMBIAMOS.TiposSucursal ON Maestra.SUC_TIPO = TiposSucursal.Descripcion)

GO


PRINT 'Vista SucursalProvincia'
GO

CREATE VIEW [ESTELOCAMBIAMOS].[SucursalProvincia] AS
SELECT Provincias.Codigo AS Codigo, Provincias.Nombre AS Provincia, TiposSucursal.Descripcion AS Tipo, Sucursales.Direccion AS Direccion, Sucursales.Telefono AS Telefono
FROM ESTELOCAMBIAMOS.Provincias LEFT JOIN ESTELOCAMBIAMOS.Sucursales ON Provincias.Codigo = Sucursales.Provincia
		LEFT JOIN ESTELOCAMBIAMOS.TiposSucursal ON Sucursales.Tipo = TiposSucursal.Codigo
GO


PRINT 'Tabla Cliente'
GO

/*
 * DNI como varchar no tiene sentido, por eso la declaro numeric (y, ademas, lo hago compartir tipo con
 * el DNI del Empleado, para la restriccion de los duplicados).
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


PRINT 'Tabla Empleados'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[Empleados] (
	[DNI] [numeric](8, 0) PRIMARY KEY,
	[Nombre] [nvarchar] (30),
	[Apellido] [nvarchar] (30),
	[Mail] [nvarchar] (255) NULL,
	[Telefono] [nvarchar] (20) NULL,
	[Direccion] [nvarchar] (255),
	[Provincia] [tinyint] REFERENCES [ESTELOCAMBIAMOS].[Provincias] (Codigo),
	[Tipo] [tinyint] REFERENCES [ESTELOCAMBIAMOS].[TiposEmpleado] (Codigo),
	[Sucursal] [tinyint] REFERENCES [ESTELOCAMBIAMOS].[Sucursales] (Provincia),
	[Habilitado] [tinyint] DEFAULT 1
)
GO


PRINT 'Trigger NuevoEmpleado'
GO

/*
 * Cuando hago un INSERT de Empleado, deshabilito el Cliente con el mismo DNI.
 */

CREATE TRIGGER [ESTELOCAMBIAMOS].[NuevoEmpleado]
ON ESTELOCAMBIAMOS.Empleados
AFTER INSERT
AS
	UPDATE ESTELOCAMBIAMOS.Clientes
	SET Habilitado = 0
	FROM INSERTED
	WHERE Clientes.DNI = INSERTED.DNI;
GO


PRINT 'Trigger LosEmpleadosNoPuedenHacerseClientes'
GO

/*
 * Cuando hago INSERT de un Cliente, si ya existe un Empleado con ese DNI, prevengo el INSERT
 */
CREATE TRIGGER [ESTELOCAMBIAMOS].[LosEmpleadosNoPuedenHacerseClientes]
ON ESTELOCAMBIAMOS.Clientes
AFTER INSERT
AS
BEGIN
	DECLARE @Existe int
	DECLARE @DNIInsertado int
	SELECT @DNIInsertado = INSERTED.DNI FROM INSERTED
	SELECT @Existe = 1
	FROM ESTELOCAMBIAMOS.Empleados
	WHERE @DNIInsertado = Empleados.DNI
	IF (@Existe > 0)
		BEGIN
			RAISERROR('Ya existe un empleado con DNI %d (los empleados no pueden ser clientes)', 15, 15, @DNIInsertado);
			ROLLBACK TRANSACTION;
		END
END
GO


PRINT 'Insert Clientes'
GO

INSERT INTO [ESTELOCAMBIAMOS].[Clientes]([DNI],[NOMBRE],[APELLIDO],[MAIL],[Telefono],[Direccion],
											[Provincia],[Habilitado])
(SELECT DISTINCT [CLI_DNI] ,[CLI_NOMBRE] ,[CLI_APELLIDO] ,[CLI_MAIL],null,null,null,1
FROM GD_ESQUEMA.MAESTRA
WHERE CLI_DNI IS NOT NULL)
GO


PRINT 'Insert Empleados'
GO

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

/*
 * El password default es 'password'
 */
CREATE TABLE [ESTELOCAMBIAMOS].[Usuarios] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar] (30) UNIQUE,
	[Password] [nvarchar] (64) DEFAULT '5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8',
	[Empleado] [numeric](8, 0) FOREIGN KEY REFERENCES ESTELOCAMBIAMOS.Empleados(DNI),
	[Habilitado] [tinyint] DEFAULT 1,
	[Intentos] [tinyint] DEFAULT 0
)
GO


PRINT 'Insert Usuario admin'
GO

/*
 * El usuario 'admin' no tiene ningun empleado asignado
 */
INSERT INTO [ESTELOCAMBIAMOS].[Usuarios] (Nombre, Password, Empleado)
VALUES ('admin', 'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7', null);


PRINT 'Insert Usuarios de los empleados'
GO

INSERT INTO [ESTELOCAMBIAMOS].[Usuarios] (Nombre, Empleado)
(SELECT REPLACE(LOWER(Nombre + Apellido), ' ','') AS Username, DNI
FROM ESTELOCAMBIAMOS.Empleados)

GO

PRINT 'Tabla Roles'
GO

CREATE TABLE [ESTELOCAMBIAMOS].[Roles] (
	[Codigo] [int] IDENTITY(1, 1) PRIMARY KEY,
	[Nombre] [nvarchar] (60),
	[Habilitado] [tinyint] DEFAULT 1
)

PRINT 'Insert Rol Administrador General'
GO

/*
 * Rol pre-creado ("Administrador General") para el usuario que crea el script ("admin")
 */
INSERT INTO ESTELOCAMBIAMOS.Roles ([Nombre])
VALUES ('Administrador General');

/*
 * FIXME: tal vez habria que crear algunos Roles mas preseteados (lo decian en algun mail, creo)
 */

GO


PRINT 'Tabla Asignaciones'
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


PRINT 'Insert Asignacion del usuario admin'
GO

/*
 * El usuario admin tiene el rol Administrador General
 */

INSERT INTO [ESTELOCAMBIAMOS].[Asignaciones] (Usuario, Rol)
(SELECT TOP 1 Usuarios.Codigo, Roles.Codigo
FROM ESTELOCAMBIAMOS.Usuarios, ESTELOCAMBIAMOS.Roles
WHERE Usuarios.Nombre = 'admin' AND Roles.Nombre = 'Administrador General')

/*
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


PRINT 'Inserts Funcionalidades'
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


PRINT 'Insert Funcionalidades del Rol Administrador General'
GO

/*
 * Le asignamos todas las funcionalidades al rol pre-creado
 */
INSERT INTO ESTELOCAMBIAMOS.FuncionalidadesRol ([Rol], [Funcionalidad])
(SELECT Roles.Codigo, Funcionalidades.Codigo
FROM ESTELOCAMBIAMOS.Roles, ESTELOCAMBIAMOS.Funcionalidades
WHERE Roles.Nombre = 'Administrador General');

GO



--Garantizamos 'Make it work'. Las otras dos, veremos.
PRINT 'Procedure Parse (categorias)'
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
		if not @Cate in (select Nombre from [ESTELOCAMBIAMOS].Categorias WHERE (@CATEPADRECOD = PADRE OR (@CATEPADRECOD IS NULL AND PADRE IS NULL)))
		  BEGIN	
			INSERT INTO [ESTELOCAMBIAMOS].Categorias(NOMBRE,Padre) VALUES(@Cate,@CatePadreCod)				
		  END
		SET @CatePadre = @Cate
		SELECT @CatePadreCod = CODIGO FROM [ESTELOCAMBIAMOS].Categorias WHERE ( NOMBRE=@Cate AND (PADRE=@CatePadreCod OR PADRE IS NULL))
	  END
	RETURN @CATEPADRECOD
END
GO


PRINT 'Tabla Categorias'
GO
 
CREATE TABLE [ESTELOCAMBIAMOS].[Categorias] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](100) NULL,
	[Padre] [int] NULL DEFAULT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Categorias] (Codigo)
)

PRINT 'Insert Categorias'
GO

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
 
PRINT 'TABLA Marcas'
GO 

CREATE TABLE [ESTELOCAMBIAMOS].[Marcas] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar] (30) UNIQUE
 )
 

PRINT 'Insert Marcas'
GO

INSERT INTO [ESTELOCAMBIAMOS].[Marcas](NOMBRE)
  SELECT DISTINCT PRODUCTO_MARCA
  FROM gd_esquema.Maestra
  WHERE PRODUCTO_MARCA IS NOT NULL


PRINT 'TABLA PRODUCTOS'
GO 

 /*
  * FIXME: aca falta agregarle INDEX a Precio y Nombre
  */
CREATE TABLE [ESTELOCAMBIAMOS].[Productos] (
	[Codigo] [INT] IDENTITY PRIMARY KEY,
	[Nombre] [nvarchar] (100),
	[Descripcion] [nvarchar] (100),
	[Categoria] [int] FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Categorias] (Codigo),
	[Precio] [float] CHECK (Precio > 0),
	[Habilitado] [tinyint] DEFAULT 1,
	[Marca] [int] FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Marcas]
 )
GO

SET IDENTITY_INSERT [ESTELOCAMBIAMOS].[Productos] ON;

PRINT 'INSERT Productos'
GO

BEGIN
DECLARE @cod int
DECLARE @CODIGO INT
DECLARE @NOM [nvarchar] (100)
DECLARE @DESC [nvarchar] (100)
DECLARE @CATE [nvarchar] (100)
DECLARE @PRECIO [float]
DECLARE @MARCA [int]
DECLARE CURSORITO cursor for (SELECT  DISTINCT SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10),
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

PRINT 'TABLA FACTURAS'
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

PRINT 'Insert Facturas'
GO

INSERT INTO [ESTELOCAMBIAMOS].[Facturas] (Numero, Fecha, Descuento, Cuotas, Sucursal, Vendedor, Cliente)
(SELECT DISTINCT FACTURA_NRO, FACTURA_FECHA, FACTURA_DESCUENTO, FACTURA_CANT_COUTAS, Codigo, EMPLEADO_DNI, CONVERT(NUMERIC, CLI_DNI)
FROM GD_ESQUEMA.MAESTRA LEFT JOIN ESTELOCAMBIAMOS.Provincias ON Provincias.Nombre = SUC_PROVINCIA
WHERE FACTURA_NRO <> 0)

GO


PRINT 'TABLA MOVIMIENTOS STOCKS'
GO 

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
GO

PRINT 'Insert MovimientosStock (entradas)'
GO

INSERT INTO [ESTELOCAMBIAMOS].[MovimientosStock] (Producto, Sucursal, Auditor, Cantidad, Fecha)
(SELECT CONVERT(INT, SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10)), Provincias.Codigo, EMPLEADO_DNI, LLEGADA_STOCK_CANT, LLEGADA_STOCK_FECHA
FROM gd_esquema.Maestra LEFT JOIN ESTELOCAMBIAMOS.Provincias ON Provincias.Nombre = SUC_PROVINCIA
WHERE LLEGADA_STOCK_CANT IS NOT NULL AND LLEGADA_STOCK_CANT <> 0)



PRINT 'TABLA ITEM FACTURAS'
GO 

 /*
  * FIXME: agregar indices y esas cosas
  */
CREATE TABLE [ESTELOCAMBIAMOS].[ItemsFactura] (
	[Factura] [int] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Facturas] (Numero),
	[Producto] [int] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Productos] (Codigo),
	[PrecioUnitario] [float] CHECK (PrecioUnitario > 0),
	[Cantidad] [int]
)
GO


PRINT 'Trigger ItemVendido'
GO

/*
 * Al vender un producto, registro la salida del stock correspondiente
 */

CREATE TRIGGER [ESTELOCAMBIAMOS].[ItemVendido]
ON ESTELOCAMBIAMOS.ItemsFactura
AFTER INSERT
AS
BEGIN
	INSERT INTO ESTELOCAMBIAMOS.[MovimientosStock] (Producto, Sucursal, Auditor, Cantidad, Fecha)
	SELECT Producto, Sucursales.Provincia, Facturas.Vendedor, (-1) * Cantidad, Facturas.Fecha
	FROM INSERTED	LEFT JOIN ESTELOCAMBIAMOS.Facturas ON Facturas.Numero = INSERTED.Factura
					LEFT JOIN ESTELOCAMBIAMOS.Sucursales ON Sucursales.Provincia = Facturas.Sucursal
END
GO


PRINT 'Insert ItemsFactura'
GO

INSERT INTO [ESTELOCAMBIAMOS].[ItemsFactura] (Factura, Producto, PrecioUnitario, Cantidad)
(SELECT FACTURA_NRO, CONVERT(int, SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10)), PRODUCTO_PRECIO, PRODUCTO_CANT
FROM gd_esquema.Maestra
WHERE FACTURA_NRO <> 0 AND PRODUCTO_NOMBRE IS NOT NULL)
GO


PRINT 'Vista FacturasCompletas'
GO

/*
 * FacturasCompletas muestra informacion "procesada" de las Facturas: Importe Base, Importe Final (incluye Descuento) y Valor de cada cuota
 */
CREATE VIEW [ESTELOCAMBIAMOS].[FacturasCompletas] AS
SELECT Numero, Fecha, Descuento, Descuento * 100 AS PorcentajeDescuento, Cuotas, SUM(PrecioUnitario * Cantidad) AS MontoBase, SUM(PrecioUnitario * Cantidad) * (1 - Descuento) AS Importe, (SUM(PrecioUnitario * Cantidad) * (1 - Descuento)) / Cuotas AS ValorCuota
FROM ESTELOCAMBIAMOS.Facturas LEFT JOIN ESTELOCAMBIAMOS.ItemsFactura ON ItemsFactura.Factura = Facturas.Numero
GROUP BY Numero, Fecha, Descuento, Cuotas

GO

PRINT 'VISTA STOCKS'
GO 

CREATE VIEW [ESTELOCAMBIAMOS].[Stocks] AS
SELECT Producto, Sucursal, SUM(Cantidad) AS Cantidad
FROM ESTELOCAMBIAMOS.MovimientosStock
GROUP BY Producto, Sucursal
GO


PRINT 'TABLA PAGOS'
GO

/*
 * FIXME: CONSTRAINT para que la sumatoria de cuotas pagadas no supere las cuotas de la factura (si un constraint no alcanza, tirarse por un trigger)
 */
CREATE TABLE [ESTELOCAMBIAMOS].[Pagos] (
	[Factura] [int] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Facturas] (Numero),
	[Sucursal] [tinyint] NOT NULL FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Sucursales] (Provincia),
	[Cuotas] [tinyint], --CONSTRAINT menor a pendientes
	[Fecha] [datetime],
	[Cobrador] [numeric] (8, 0) FOREIGN KEY REFERENCES [ESTELOCAMBIAMOS].[Empleados] (DNI)
)
GO


INSERT INTO [ESTELOCAMBIAMOS].[Pagos] (Factura, Sucursal, Cuotas, Fecha, Cobrador)
(SELECT FACTURA_NRO, Provincias.Codigo, PAGO_MONTO / FacturasCompletas.ValorCuota, PAGO_FECHA, PAGO_EMPLEADO_DNI
FROM gd_esquema.Maestra	LEFT JOIN ESTELOCAMBIAMOS.Provincias ON Provincias.Nombre = SUC_PROVINCIA
						LEFT JOIN ESTELOCAMBIAMOS.FacturasCompletas ON FacturasCompletas.Numero = FACTURA_NRO
WHERE PAGO_MONTO IS NOT NULL AND PAGO_MONTO <> 0)

GO

/*
 * FIXME: mirar por si sirve
 */

/*
SELECT Facturas.Numero, Facturas.Cuotas, SUM(Pagos.Cuotas) AS CuotasPagas, Facturas.Cuotas - SUM(Pagos.Cuotas) AS CuotasPendientes
FROM ESTELOCAMBIAMOS.Facturas LEFT JOIN ESTELOCAMBIAMOS.Pagos ON Pagos.Factura = Facturas.Numero
GROUP BY Facturas.Numero, Facturas.Cuotas
ORDER BY CuotasPendientes DESC
*/


PRINT 'PROCEDURE MODIFINTENTOS '
GO

CREATE PROCEDURE [ESTELOCAMBIAMOS].[sp_MODIFINTENTOS] (@USERNAME NVARCHAR(20), @INTENTOS INT)
AS
BEGIN
	UPDATE 	[ESTELOCAMBIAMOS].USUARIOS
	SET INTENTOS = @INTENTOS
	WHERE NOMBRE = @USERNAME
END

GO


PRINT 'PROCEDURE LOGIN'
GO

CREATE PROCEDURE [ESTELOCAMBIAMOS].[sp_LOGIN] (@USERNAME NVARCHAR(20), @PASS NVARCHAR(65))
AS
BEGIN

	DECLARE @INTENTOS INT
	DECLARE @USER NVARCHAR(20)
	SET @USER = @USERNAME
	
	SET @INTENTOS = (SELECT INTENTOS FROM [ESTELOCAMBIAMOS].USUARIOS WHERE NOMBRE=@USERNAME)
	IF (@USERNAME IN (SELECT NOMBRE FROM [ESTELOCAMBIAMOS].USUARIOS))
		IF ((SELECT NOMBRE FROM [ESTELOCAMBIAMOS].USUARIOS 
			WHERE NOMBRE=@USERNAME AND INTENTOS >2)IS NOT NULL)
			BEGIN
				SELECT 'USUARIO BLOQUEADO'
			END
		ELSE
			BEGIN
				IF ((SELECT NOMBRE FROM [ESTELOCAMBIAMOS].USUARIOS 
					WHERE NOMBRE=@USERNAME AND PASSWORD=@PASS )IS NOT NULL)
					BEGIN
						IF (@INTENTOS <> 3)
							BEGIN
								SET @INTENTOS = 0
								EXEC [ESTELOCAMBIAMOS].sp_MODIFINTENTOS @USER, @INTENTOS
							END
						(SELECT FUN.DESCRIPCION 
							FROM [ESTELOCAMBIAMOS].FUNCIONALIDADES AS FUN JOIN  [ESTELOCAMBIAMOS].FUNCIONALIDADESROL AS FUNROL ON (FUN.CODIGO = FUNROL.FUNCIONALIDAD)
								JOIN  [ESTELOCAMBIAMOS].ROLES AS ROL ON (FUNROL.ROL = ROL.CODIGO)
								JOIN  [ESTELOCAMBIAMOS].ASIGNACIONES AS ASIG ON (ASIG.ROL = ROL.CODIGO)
								JOIN  [ESTELOCAMBIAMOS].USUARIOS AS US ON (ASIG.USUARIO = US.CODIGO	)
							WHERE US.NOMBRE = @USERNAME)
					END
				ELSE
					BEGIN
						SET @INTENTOS = @INTENTOS + '1'
						EXEC [ESTELOCAMBIAMOS].sp_MODIFINTENTOS @USERNAME, @INTENTOS
						SELECT 'CONTRASEÑA INVALIDA',@intentos,@pass
					END
			END
		ELSE
		BEGIN
			SELECT 'NO EXISTE EN LA BASE'
		END
END