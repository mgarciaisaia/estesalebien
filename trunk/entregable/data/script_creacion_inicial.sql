-- Seteo la DB
USE [GD1C2011]
GO

-- Genero el Schema
/**
 * Chequeo la existencia del schema antes de crearlo. Deberia pensar en eliminar las tablas y otros objetos tambien.
 */
IF NOT EXISTS(SELECT 1 FROM information_schema.schemata WHERE
schema_name='MAYUSCULAS_SIN_ESPACIOS')
EXEC ('create schema MAYUSCULAS_SIN_ESPACIOS AUTHORIZATION gd');
GO

--------------------------------------------------------
PRINT 'Tabla Provincias'
GO

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Provincias] (
	[Codigo] [tinyint] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](53) UNIQUE
)
GO

--------------------------------------------------------
PRINT 'Insert Provincias'
GO

INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Provincias ([Nombre])
(SELECT DISTINCT SUC_PROVINCIA
FROM gd_esquema.Maestra)

GO

--------------------------------------------------------
PRINT 'Tabla TipoEmpleado'
GO

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[TiposEmpleado] (
	[Codigo] [tinyint] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [nvarchar] (8) UNIQUE
)
GO

--------------------------------------------------------
PRINT 'Insert TipoEmpleado'
GO

INSERT INTO MAYUSCULAS_SIN_ESPACIOS.TiposEmpleado ([Descripcion])
(SELECT DISTINCT EMPLEADO_TIPO
FROM gd_esquema.Maestra)

GO

--------------------------------------------------------
PRINT 'Tabla TipoSucursal'
GO

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[TiposSucursal] (
	[Codigo] [tinyint] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [nvarchar] (12) UNIQUE
)
GO

--------------------------------------------------------
PRINT 'Insert TipoSucursal'
GO

INSERT INTO MAYUSCULAS_SIN_ESPACIOS.TiposSucursal ([Descripcion])
(SELECT DISTINCT SUC_TIPO
FROM gd_esquema.Maestra)

GO

--------------------------------------------------------
PRINT 'Tabla Sucursales'
GO

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Sucursales] (
	[Provincia] [tinyint] PRIMARY KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Provincias] (Codigo),
	[Tipo] [tinyint] REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[TiposSucursal] (Codigo),
	[Direccion] [nvarchar] (255),
	[Telefono] [nvarchar] (20)
)
GO

--------------------------------------------------------
PRINT 'Insert Sucursales'
GO

INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Sucursales ([Provincia], [Tipo], [Direccion], [Telefono])
(SELECT DISTINCT Provincias.Codigo, TiposSucursal.Codigo, SUC_DIR, SUC_TEL
FROM gd_esquema.Maestra LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Provincias ON Maestra.SUC_PROVINCIA = Provincias.Nombre
		LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.TiposSucursal ON Maestra.SUC_TIPO = TiposSucursal.Descripcion)

GO

--------------------------------------------------------
PRINT 'Vista SucursalProvincia'
GO

CREATE VIEW [MAYUSCULAS_SIN_ESPACIOS].[SucursalProvincia] AS
SELECT Provincias.Codigo AS Codigo, Provincias.Nombre AS Provincia, TiposSucursal.Descripcion AS Tipo, Sucursales.Direccion AS Direccion, Sucursales.Telefono AS Telefono
FROM MAYUSCULAS_SIN_ESPACIOS.Provincias LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Sucursales ON Provincias.Codigo = Sucursales.Provincia
		LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.TiposSucursal ON Sucursales.Tipo = TiposSucursal.Codigo
GO

--------------------------------------------------------
PRINT 'Tabla Cliente'
GO

/*
 * DNI como varchar no tiene sentido, por eso la declaro numeric (y, ademas, lo hago compartir tipo con
 * el DNI del Empleado, para la restriccion de los duplicados).
 *
 * Lo de "respetar los tipos de datos" del enunciado pareciera que hablaba de tipos a nivel conceptual
 * en lugar de nivel implementacion, segun el mail de Miguel Lopez del 28 de Mayo
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Clientes] (
	[DNI] [numeric] (8, 0) PRIMARY KEY,
	[Nombre] [nvarchar] (30) not null,
	[Apellido] [nvarchar] (30) not null,
	[Mail] [nvarchar] (255),
	[Telefono] [nvarchar] (20),
	[Direccion] [nvarchar] (255),
	[Provincia] [tinyint] FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Provincias] (Codigo),
	[Habilitado] [tinyint] DEFAULT 1
)

--------------------------------------------------------
PRINT 'Indices Clientes'
GO

CREATE INDEX ClientesPorNombre
    ON MAYUSCULAS_SIN_ESPACIOS.Clientes (Nombre); 
GO

CREATE INDEX ClientesPorApellido
    ON MAYUSCULAS_SIN_ESPACIOS.Clientes (Apellido); 
GO

CREATE INDEX ClientesPorProvincia
    ON MAYUSCULAS_SIN_ESPACIOS.Clientes (Provincia);
GO

--------------------------------------------------------
PRINT 'Tabla Empleados'
GO

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Empleados] (
	[DNI] [numeric](8, 0) PRIMARY KEY,
	[Nombre] [nvarchar] (30),
	[Apellido] [nvarchar] (30),
	[Mail] [nvarchar] (255) NULL,
	[Telefono] [nvarchar] (20) NULL,
	[Direccion] [nvarchar] (255),
	[Provincia] [tinyint] REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Provincias] (Codigo),
	[Tipo] [tinyint] REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[TiposEmpleado] (Codigo),
	[Sucursal] [tinyint] REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Sucursales] (Provincia),
	[Habilitado] [tinyint] DEFAULT 1
)
GO

--------------------------------------------------------
PRINT 'Indices Empleados'
GO

CREATE INDEX EmpleadosPorNombre
    ON MAYUSCULAS_SIN_ESPACIOS.Empleados (Nombre);
GO

CREATE INDEX EmpleadosPorApellido
    ON MAYUSCULAS_SIN_ESPACIOS.Empleados (Apellido);
GO

CREATE INDEX EmpleadosPorProvincia
    ON MAYUSCULAS_SIN_ESPACIOS.Empleados (Provincia);
GO

CREATE INDEX EmpleadosPorSucursal
    ON MAYUSCULAS_SIN_ESPACIOS.Empleados (Sucursal);
GO

CREATE INDEX EmpleadosPorTipo
    ON MAYUSCULAS_SIN_ESPACIOS.Empleados (Tipo);
GO

--------------------------------------------------------
PRINT 'Trigger NuevoEmpleado'
GO

/*
 * Cuando hago un INSERT de Empleado, deshabilito el Cliente con el mismo DNI.
 */

CREATE TRIGGER [MAYUSCULAS_SIN_ESPACIOS].[NuevoEmpleado]
ON MAYUSCULAS_SIN_ESPACIOS.Empleados
AFTER INSERT
AS
	UPDATE MAYUSCULAS_SIN_ESPACIOS.Clientes
	SET Habilitado = 0
	FROM INSERTED
	WHERE Clientes.DNI = INSERTED.DNI;
GO

--------------------------------------------------------
PRINT 'Trigger LosEmpleadosNoPuedenHacerseClientes'
GO

/*
 * Cuando hago INSERT de un Cliente, si ya existe un Empleado con ese DNI, prevengo el INSERT
 */
CREATE TRIGGER [MAYUSCULAS_SIN_ESPACIOS].[LosEmpleadosNoPuedenHacerseClientes]
ON MAYUSCULAS_SIN_ESPACIOS.Clientes
AFTER INSERT
AS
BEGIN
	DECLARE @Existe int
	DECLARE @DNIInsertado int
	SELECT @DNIInsertado = INSERTED.DNI FROM INSERTED
	SELECT @Existe = 1
	FROM MAYUSCULAS_SIN_ESPACIOS.Empleados
	WHERE @DNIInsertado = Empleados.DNI
	IF (@Existe > 0)
		BEGIN
			RAISERROR('Ya existe un empleado con DNI %d (los empleados no pueden ser clientes)', 15, 15, @DNIInsertado);
			ROLLBACK TRANSACTION;
		END
END
GO

--------------------------------------------------------
PRINT 'Insert Clientes'
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Clientes]([DNI],[NOMBRE],[APELLIDO],[MAIL],[Telefono],[Direccion],
											[Provincia],[Habilitado])
(SELECT DISTINCT [CLI_DNI] ,[CLI_NOMBRE] ,[CLI_APELLIDO] ,[CLI_MAIL],null,null,null,1
FROM GD_ESQUEMA.MAESTRA
WHERE CLI_DNI IS NOT NULL)
GO

--------------------------------------------------------
PRINT 'Insert Empleados'
GO

/*
 * Este UNION lo cambiaria por una funcion que relacione empleado con sucursal, o por un CASE en el SELECT, pero el UNION es una locura
 */
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Empleados ([DNI], [Nombre], [Apellido], [Mail], [Direccion], [Telefono], [Provincia], [Tipo], [Sucursal])
((SELECT DISTINCT Maestra.EMPLEADO_DNI, Maestra.EMPLEADO_NOMBRE, Maestra.EMPLEADO_APELLIDO, Maestra.EMPLEADO_MAIL, Maestra.EMPLEADO_DIR, '', Provincias.Codigo, TiposEmpleado.Codigo, SucursalProvincia.Codigo
FROM gd_esquema.Maestra LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Provincias ON Maestra.EMPLEADO_PROVINCIA = Provincias.Nombre
		LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.TiposEmpleado ON Maestra.EMPLEADO_TIPO = TiposEmpleado.Descripcion
		LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.SucursalProvincia ON Maestra.SUC_PROVINCIA = SucursalProvincia.Provincia
WHERE EMPLEADO_TIPO = 'Vendedor')

UNION

(SELECT DISTINCT Maestra.EMPLEADO_DNI, Maestra.EMPLEADO_NOMBRE, Maestra.EMPLEADO_APELLIDO, Maestra.EMPLEADO_MAIL, Maestra.EMPLEADO_DIR, '', Provincias.Codigo, TiposEmpleado.Codigo, SucursalProvincia.Codigo
FROM gd_esquema.Maestra LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Provincias ON Maestra.EMPLEADO_PROVINCIA = Provincias.Nombre
		LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.TiposEmpleado ON Maestra.EMPLEADO_TIPO = TiposEmpleado.Descripcion
		LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.SucursalProvincia ON Maestra.SUC_PROVINCIA = SucursalProvincia.Provincia
WHERE EMPLEADO_TIPO = 'Analista' AND SucursalProvincia.Tipo = 'Sede Central'))

GO

--------------------------------------------------------
PRINT 'Tabla Usuarios'
GO

/*
 * Agregamos un Codigo de usuario porque nos parece poco feliz tener una PRIMARY KEY que sea un String de 30 caracteres
 *
 * El password default es 'password'
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Usuarios] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar] (30) UNIQUE,
	[Password] [nvarchar] (64) DEFAULT '5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8',
	[Empleado] [numeric](8, 0) FOREIGN KEY REFERENCES MAYUSCULAS_SIN_ESPACIOS.Empleados(DNI),
	[Habilitado] [tinyint] DEFAULT 1,
	[Intentos] [tinyint] DEFAULT 0
)
GO

CREATE INDEX UsuariosPorNombre
    ON MAYUSCULAS_SIN_ESPACIOS.Usuarios (Nombre);
GO

CREATE INDEX UsuariosPorEmpleado
    ON MAYUSCULAS_SIN_ESPACIOS.Usuarios (Empleado);
GO

--------------------------------------------------------
PRINT 'Insert Usuario admin'
GO

/*
 * El usuario 'admin' no tiene ningun empleado asignado
 */
INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Usuarios] (Nombre, Password, Empleado)
VALUES ('admin', 'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7', null);

--------------------------------------------------------
PRINT 'Insert Usuarios de los empleados'
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Usuarios] (Nombre, Empleado)
(SELECT REPLACE(LOWER(Nombre + Apellido), ' ','') AS Username, DNI
FROM MAYUSCULAS_SIN_ESPACIOS.Empleados)

GO

--------------------------------------------------------
PRINT 'Tabla Roles'
GO

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Roles] (
	[Codigo] [int] IDENTITY(1, 1) PRIMARY KEY,
	[Nombre] [nvarchar] (60),
	[Habilitado] [tinyint] DEFAULT 1
)

--------------------------------------------------------
PRINT 'Indices Roles'
GO

CREATE INDEX RolesPorNombre
    ON MAYUSCULAS_SIN_ESPACIOS.Roles (Nombre);
GO

--------------------------------------------------------
PRINT 'Insert Rol Administrador General'
GO

/*
 * Rol pre-creado ("Administrador General") para el usuario que crea el script ("admin")
 */
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Roles ([Nombre])
VALUES ('Administrador General');

/*
 * FIXME: tal vez habria que crear algunos Roles mas preseteados (lo decian en algun mail, creo)
 */

GO

--------------------------------------------------------
PRINT 'Tabla Asignaciones'
GO

/*
 * Jamas vamos a hacer una busqueda por la "PRIMARY KEY", pero un UNIQUE compuesto permitiria
 * que alguno de los dos valores sea nulo
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Asignaciones] (
	[Usuario] [int],
	[Rol] [int],
	PRIMARY KEY (Usuario, Rol)
)

--------------------------------------------------------
PRINT 'Insert Asignacion del usuario admin'
GO

/*
 * El usuario admin tiene el rol Administrador General
 */

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Asignaciones] (Usuario, Rol)
(SELECT TOP 1 Usuarios.Codigo, Roles.Codigo
FROM MAYUSCULAS_SIN_ESPACIOS.Usuarios, MAYUSCULAS_SIN_ESPACIOS.Roles
WHERE Usuarios.Nombre = 'admin' AND Roles.Nombre = 'Administrador General')

/*
 * FIXME: podriamos hacer algunas asignaciones mas (un rol por tipo de empleado?)
 */
 
--------------------------------------------------------
/*
 * Funcionalidades: las posibles funcionalidades que se le pueden asignar
 * a los roles del sistema.
 * Son fijas, definidas por extension en el enunciado del TP.
 */

PRINT 'Tabla Funcionalidades'
GO

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Funcionalidades] (
	[Codigo] [tinyint] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [nvarchar](19) UNIQUE
)
GO

--------------------------------------------------------
PRINT 'Inserts Funcionalidades'
GO

INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Empleado');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Rol');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Usuario');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Cliente');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('ABM de Producto');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('Asignacion de stock');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('Facturacion');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('Efectuar Pago');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('Tablero de Control');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('Clientes Premium');
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Funcionalidades ([Descripcion]) VALUES ('Mejores Categorias');

GO

--------------------------------------------------------
PRINT 'Tabla FuncionalidadesRol'
GO

/*
 * Jamas vamos a hacer una busqueda por la "PRIMARY KEY", pero un UNIQUE compuesto permitiria
 * que alguno de los dos valores sea nulo
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[FuncionalidadesRol] (
	[Rol] [int] REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Roles] (Codigo),
	[Funcionalidad] [tinyint] REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Funcionalidades] (Codigo),
	PRIMARY KEY (Rol, Funcionalidad)
)

--------------------------------------------------------
PRINT 'Insert Funcionalidades del Rol Administrador General'
GO

/*
 * Le asignamos todas las funcionalidades al rol pre-creado
 */
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.FuncionalidadesRol ([Rol], [Funcionalidad])
(SELECT Roles.Codigo, Funcionalidades.Codigo
FROM MAYUSCULAS_SIN_ESPACIOS.Roles, MAYUSCULAS_SIN_ESPACIOS.Funcionalidades
WHERE Roles.Nombre = 'Administrador General');

GO

--------------------------------------------------------
PRINT 'Tabla Categorias'
GO
 
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Categorias] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](100) NULL,
	[Padre] [int] NULL DEFAULT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Categorias] (Codigo)
)

--------------------------------------------------------
PRINT 'Indices Categoria'
GO

CREATE INDEX CategoriasPorNombre
    ON MAYUSCULAS_SIN_ESPACIOS.Categorias (Nombre);
GO

--------------------------------------------------------
--Garantizamos 'Make it work'. Las otras dos, veremos.
PRINT 'Procedure Parse (categorias)'
GO 

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[PARSE](@CateList varchar(100))
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
		if not @Cate in (select Nombre from [MAYUSCULAS_SIN_ESPACIOS].Categorias WHERE (@CATEPADRECOD = PADRE OR (@CATEPADRECOD IS NULL AND PADRE IS NULL)))
		  BEGIN	
			INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].Categorias(NOMBRE,Padre) VALUES(@Cate,@CatePadreCod)				
		  END
		SET @CatePadre = @Cate
		SELECT @CatePadreCod = CODIGO FROM [MAYUSCULAS_SIN_ESPACIOS].Categorias WHERE ( NOMBRE=@Cate AND (PADRE=@CatePadreCod OR PADRE IS NULL))
	  END
	RETURN @CATEPADRECOD
END
GO

--------------------------------------------------------
PRINT 'Insert Categorias'
GO

/*
 *  Inserta todas las categorias
 */
declare @au_id nvarchar(100)
select distinct @au_id =  min( producto_cate ) from gd_esquema.Maestra
while @au_id is not null
begin
	EXECUTE [MAYUSCULAS_SIN_ESPACIOS].[PARSE] @au_id    
	select @au_id = min( producto_cate ) from gd_esquema.Maestra where producto_cate > @au_id
end
GO

--------------------------------------------------------
PRINT 'TABLA Marcas'
GO 

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Marcas] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar] (30) UNIQUE
 )


CREATE INDEX MarcasPorNombre
    ON MAYUSCULAS_SIN_ESPACIOS.Marcas (Nombre);
GO

--------------------------------------------------------
PRINT 'Insert Marcas'
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Marcas](NOMBRE)
  SELECT DISTINCT PRODUCTO_MARCA
  FROM gd_esquema.Maestra
  WHERE PRODUCTO_MARCA IS NOT NULL

--------------------------------------------------------
PRINT 'TABLA PRODUCTOS'
GO 

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Productos] (
	[Codigo] [INT] IDENTITY PRIMARY KEY,
	[Nombre] [nvarchar] (100),
	[Descripcion] [nvarchar] (100),
	[Categoria] [int] FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Categorias] (Codigo),
	[Precio] [float] CHECK (Precio > 0),
	[Habilitado] [tinyint] DEFAULT 1,
	[Marca] [int] FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Marcas]
 )
GO

SET IDENTITY_INSERT [MAYUSCULAS_SIN_ESPACIOS].[Productos] ON;

--------------------------------------------------------
PRINT 'Indices Productos'
GO

CREATE INDEX ProductosPorNombre
    ON MAYUSCULAS_SIN_ESPACIOS.Productos (Nombre);
GO

CREATE INDEX ProductosPorCategoria
    ON MAYUSCULAS_SIN_ESPACIOS.Productos (Categoria);
GO

CREATE INDEX ProductosPorPrecio
    ON MAYUSCULAS_SIN_ESPACIOS.Productos (Precio);
GO

--------------------------------------------------------
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
				FROM GD_ESQUEMA.MAESTRA JOIN [MAYUSCULAS_SIN_ESPACIOS].[MARCAS] ON (PRODUCTO_MARCA = NOMBRE)
				WHERE PRODUCTO_PRECIO <> '0')
open CURSORITO
fetch next from CURSORITO
into @CODIGO,@NOM,@DESC,@CATE,@PRECIO,@MARCA
while @@fetch_status = 0
  begin
    EXECUTE @COD=[MAYUSCULAS_SIN_ESPACIOS].[PARSE] @CATE
    INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Productos](CODIGO,[NOMBRE],[DESCRIPCION],[CATEGORIA],[PRECIO],[HABILITADO],[MARCA])
    VALUES(@CODIGO,@NOM,@DESC,@COD,@PRECIO,'1',@MARCA)
    fetch next from CURSORITO
    into @CODIGO,@NOM,@DESC,@CATE,@PRECIO,@MARCA
  end
close CURSORITO
deallocate CURSORITO
END

SET IDENTITY_INSERT [MAYUSCULAS_SIN_ESPACIOS].[Productos] OFF;
GO

--------------------------------------------------------
PRINT 'VISTA PRODUCTOS COMPLETOS'
GO 

CREATE VIEW [MAYUSCULAS_SIN_ESPACIOS].[ProductosCompletos] AS
SELECT Productos.Codigo, Productos.Nombre, Productos.Descripcion, Productos.Categoria AS CodigoCategoria, Categorias.Nombre AS Categoria, Productos.Precio, Productos.Marca AS CodigoMarca, Marcas.Nombre AS Marca, Productos.Habilitado
FROM MAYUSCULAS_SIN_ESPACIOS.Productos
	LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Categorias ON Productos.Categoria = Categorias.Codigo
	LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Marcas ON Productos.Marca = Marcas.Codigo

GO

--------------------------------------------------------
PRINT 'TABLA FACTURAS'
GO

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Facturas] (
	[Numero] [int] IDENTITY PRIMARY KEY,
	[Fecha] [datetime],
	[Descuento] [float],
	[Cuotas] [tinyint],
	[Sucursal] [tinyint] FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Sucursales] (Provincia),
	[Vendedor] [numeric] (8, 0) FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Empleados] (DNI),
	[Cliente] [numeric] (8,0) FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Clientes] (DNI)
)

SET IDENTITY_INSERT [MAYUSCULAS_SIN_ESPACIOS].[Facturas] ON;

--------------------------------------------------------
PRINT 'Indices Facturas'
GO

CREATE INDEX FacturasPorCliente
    ON MAYUSCULAS_SIN_ESPACIOS.Facturas (Cliente);
GO

CREATE INDEX FacturasPorFecha
    ON MAYUSCULAS_SIN_ESPACIOS.Facturas (Fecha);
GO

/*
 * FIXME: seguramente falten indices para las consultas del final del TP
 */

--------------------------------------------------------
PRINT 'Insert Facturas'
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Facturas] (Numero, Fecha, Descuento, Cuotas, Sucursal, Vendedor, Cliente)
(SELECT DISTINCT FACTURA_NRO, FACTURA_FECHA, FACTURA_DESCUENTO, FACTURA_CANT_COUTAS, Codigo, EMPLEADO_DNI, CONVERT(NUMERIC, CLI_DNI)
FROM GD_ESQUEMA.MAESTRA LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Provincias ON Provincias.Nombre = SUC_PROVINCIA
WHERE FACTURA_NRO <> 0)

GO

--------------------------------------------------------
PRINT 'TABLA MOVIMIENTOS STOCKS'
GO 

/*
 * Un MovimientoStock con Cantidad negativa representa una salida
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[MovimientosStock] (
	[Producto] [int] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Productos] (Codigo),
	[Sucursal] [tinyint] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Sucursales] (Provincia),
	[Auditor] [numeric](8,0) NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Empleados] (DNI), --CHECK Auditor.Tipo.Descripcion = Analista
	[Cantidad] [int] NOT NULL,
	[Fecha] [datetime]
)
GO

--------------------------------------------------------
PRINT 'Indices MovimientosStock'
GO

CREATE INDEX MovimientosPorProducto
    ON MAYUSCULAS_SIN_ESPACIOS.MovimientosStock (Producto);
GO

CREATE INDEX MovimientosPorSucursal
    ON MAYUSCULAS_SIN_ESPACIOS.MovimientosStock (Sucursal);
GO

CREATE INDEX MovimientosPorFecha
    ON MAYUSCULAS_SIN_ESPACIOS.MovimientosStock (Fecha);
GO

--------------------------------------------------------
PRINT 'Insert MovimientosStock (entradas)'
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[MovimientosStock] (Producto, Sucursal, Auditor, Cantidad, Fecha)
(SELECT CONVERT(INT, SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10)), Provincias.Codigo, EMPLEADO_DNI, LLEGADA_STOCK_CANT, LLEGADA_STOCK_FECHA
FROM gd_esquema.Maestra LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Provincias ON Provincias.Nombre = SUC_PROVINCIA
WHERE LLEGADA_STOCK_CANT IS NOT NULL AND LLEGADA_STOCK_CANT <> 0)


--------------------------------------------------------
PRINT 'TABLA ITEM FACTURAS'
GO 

CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[ItemsFactura] (
	[Factura] [int] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Facturas] (Numero),
	[Producto] [int] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Productos] (Codigo),
	[PrecioUnitario] [float] CHECK (PrecioUnitario > 0),
	[Cantidad] [int]
)
GO

--------------------------------------------------------
PRINT 'Indices ItemsFacturas'
GO


CREATE INDEX ItemsPorFactura
    ON MAYUSCULAS_SIN_ESPACIOS.ItemsFactura (Factura);
GO

CREATE INDEX ItemsPorProducto
    ON MAYUSCULAS_SIN_ESPACIOS.ItemsFactura (Producto);
GO

CREATE INDEX ItemsPorPrecio
    ON MAYUSCULAS_SIN_ESPACIOS.ItemsFactura (PrecioUnitario);
GO

--------------------------------------------------------
PRINT 'Trigger ItemVendido'
GO

/*
 * Al vender un producto, registro la salida del stock correspondiente
 */

CREATE TRIGGER [MAYUSCULAS_SIN_ESPACIOS].[ItemVendido]
ON MAYUSCULAS_SIN_ESPACIOS.ItemsFactura
AFTER INSERT
AS
BEGIN
	INSERT INTO MAYUSCULAS_SIN_ESPACIOS.[MovimientosStock] (Producto, Sucursal, Auditor, Cantidad, Fecha)
	SELECT Producto, Sucursales.Provincia, Facturas.Vendedor, (-1) * Cantidad, Facturas.Fecha
	FROM INSERTED	LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Facturas ON Facturas.Numero = INSERTED.Factura
					LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Sucursales ON Sucursales.Provincia = Facturas.Sucursal
END
GO

--------------------------------------------------------
PRINT 'Insert ItemsFactura'
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[ItemsFactura] (Factura, Producto, PrecioUnitario, Cantidad)
(SELECT FACTURA_NRO, CONVERT(int, SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10)), PRODUCTO_PRECIO, PRODUCTO_CANT
FROM gd_esquema.Maestra
WHERE FACTURA_NRO <> 0 AND PRODUCTO_NOMBRE IS NOT NULL)
GO

--------------------------------------------------------
PRINT 'Vista FacturasCompletas'
GO

/*
 * FacturasCompletas muestra informacion "procesada" de las Facturas: Importe Base, Importe Final (incluye Descuento) y Valor de cada cuota
 */
CREATE VIEW [MAYUSCULAS_SIN_ESPACIOS].[FacturasCompletas] AS
SELECT Numero, Fecha, Descuento, Descuento * 100 AS PorcentajeDescuento, Cuotas, SUM(PrecioUnitario * Cantidad) AS MontoBase, SUM(PrecioUnitario * Cantidad) * (1 - Descuento) AS Importe, (SUM(PrecioUnitario * Cantidad) * (1 - Descuento)) / Cuotas AS ValorCuota, Sucursal
FROM MAYUSCULAS_SIN_ESPACIOS.Facturas LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.ItemsFactura ON ItemsFactura.Factura = Facturas.Numero
GROUP BY Numero, Sucursal, Fecha, Descuento, Cuotas

GO

--------------------------------------------------------
PRINT 'VISTA STOCKS'
GO 

CREATE VIEW [MAYUSCULAS_SIN_ESPACIOS].[Stocks] AS
SELECT Producto, Sucursal, SUM(Cantidad) AS Cantidad
FROM MAYUSCULAS_SIN_ESPACIOS.MovimientosStock
GROUP BY Producto, Sucursal
GO

--------------------------------------------------------
PRINT 'TABLA PAGOS'
GO


CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Pagos] (
	[Factura] [int] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Facturas] (Numero),
	[Sucursal] [tinyint] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Sucursales] (Provincia),
	[Cuotas] [tinyint],
	[Fecha] [datetime],
	[Cobrador] [numeric] (8, 0) FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Empleados] (DNI)
)
GO


CREATE TRIGGER [MAYUSCULAS_SIN_ESPACIOS].[NoPagarMasCuotasDeLasQueHay]
ON MAYUSCULAS_SIN_ESPACIOS.Pagos
AFTER INSERT
AS
BEGIN
	DECLARE @FacturaQueSePaga int
	DECLARE @CuotasQueSePagan tinyint
	SELECT @FacturaQueSePaga = Factura, @CuotasQueSePagan = Cuotas
	FROM INSERTED;
	
	DECLARE @CuotasFactura tinyint
	SELECT @CuotasFactura = Cuotas
	FROM MAYUSCULAS_SIN_ESPACIOS.Facturas
	WHERE Facturas.Numero = @FacturaQueSePaga;
	
	DECLARE @CuotasPagadas tinyint
	SELECT @CuotasPagadas = SUM(Cuotas)
	FROM MAYUSCULAS_SIN_ESPACIOS.Pagos
	WHERE Factura = @FacturaQueSePaga;
	
	IF (@CuotasPagadas > @CuotasFactura)
		BEGIN
			DECLARE @CuotasExtra tinyint
			DECLARE @CuotasPrevias tinyint
			SET @CuotasExtra = @CuotasPagadas - @CuotasFactura
			SET @CuotasPrevias = @CuotasPagadas - @CuotasQueSePagan
			RAISERROR('Se estan pagando %d cuotas de mas (ya se pagaron %d de las %d)', 15, 15, @CuotasExtra, @CuotasPrevias, @CuotasFactura);
			ROLLBACK TRANSACTION;
		END
END
GO


INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Pagos] (Factura, Sucursal, Cuotas, Fecha, Cobrador)
(SELECT FACTURA_NRO, Provincias.Codigo, PAGO_MONTO / FacturasCompletas.ValorCuota, PAGO_FECHA, PAGO_EMPLEADO_DNI
FROM gd_esquema.Maestra	LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Provincias ON Provincias.Nombre = SUC_PROVINCIA
						LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.FacturasCompletas ON FacturasCompletas.Numero = FACTURA_NRO
WHERE PAGO_MONTO IS NOT NULL AND PAGO_MONTO <> 0)

GO

/*
 * FIXME: mirar por si sirve
 */

/*
SELECT Facturas.Numero, Facturas.Cuotas, SUM(Pagos.Cuotas) AS CuotasPagas, Facturas.Cuotas - SUM(Pagos.Cuotas) AS CuotasPendientes
FROM MAYUSCULAS_SIN_ESPACIOS.Facturas LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Pagos ON Pagos.Factura = Facturas.Numero
GROUP BY Facturas.Numero, Facturas.Cuotas
ORDER BY CuotasPendientes DESC
*/

--------------------------------------------------------
PRINT 'PROCEDURE MODIFINTENTOS '
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_MODIFINTENTOS] (@USERNAME NVARCHAR(20), @INTENTOS INT)
AS
BEGIN
	UPDATE 	[MAYUSCULAS_SIN_ESPACIOS].USUARIOS
	SET INTENTOS = @INTENTOS
	WHERE NOMBRE = @USERNAME
END

GO

--------------------------------------------------------
PRINT 'PROCEDURE LOGIN'
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_LOGIN] (@USERNAME NVARCHAR(20), @PASS NVARCHAR(65))
AS
BEGIN

	DECLARE @INTENTOS INT
	DECLARE @USER NVARCHAR(20)
	SET @USER = @USERNAME
	
	SET @INTENTOS = (SELECT INTENTOS FROM [MAYUSCULAS_SIN_ESPACIOS].USUARIOS WHERE NOMBRE=@USERNAME)
	IF (@USERNAME IN (SELECT NOMBRE FROM [MAYUSCULAS_SIN_ESPACIOS].USUARIOS))
		IF ((SELECT NOMBRE FROM [MAYUSCULAS_SIN_ESPACIOS].USUARIOS 
			WHERE NOMBRE=@USERNAME AND INTENTOS >2)IS NOT NULL)
			BEGIN
				SELECT 'USUARIO BLOQUEADO'
			END
		ELSE
			BEGIN
				IF ((SELECT NOMBRE FROM [MAYUSCULAS_SIN_ESPACIOS].USUARIOS 
					WHERE NOMBRE=@USERNAME AND PASSWORD=@PASS )IS NOT NULL)
					BEGIN
						IF (@INTENTOS <> 3)
							BEGIN
								SET @INTENTOS = 0
								EXEC [MAYUSCULAS_SIN_ESPACIOS].sp_MODIFINTENTOS @USER, @INTENTOS
							END
						(SELECT DISTINCT FUN.DESCRIPCION 
							FROM [MAYUSCULAS_SIN_ESPACIOS].FUNCIONALIDADES AS FUN JOIN  [MAYUSCULAS_SIN_ESPACIOS].FUNCIONALIDADESROL AS FUNROL ON (FUN.CODIGO = FUNROL.FUNCIONALIDAD)
								JOIN  [MAYUSCULAS_SIN_ESPACIOS].ROLES AS ROL ON (FUNROL.ROL = ROL.CODIGO)
								JOIN  [MAYUSCULAS_SIN_ESPACIOS].ASIGNACIONES AS ASIG ON (ASIG.ROL = ROL.CODIGO)
								JOIN  [MAYUSCULAS_SIN_ESPACIOS].USUARIOS AS US ON (ASIG.USUARIO = US.CODIGO	)
							WHERE US.NOMBRE = @USERNAME)
					END
				ELSE
					BEGIN
						SET @INTENTOS = @INTENTOS + '1'
						EXEC [MAYUSCULAS_SIN_ESPACIOS].sp_MODIFINTENTOS @USERNAME, @INTENTOS
						SELECT 'CONTRASEÑA INVALIDA',@intentos,@pass
					END
			END
		ELSE
		BEGIN
			SELECT 'NO EXISTE EN LA BASE'
		END
END
GO

--------------------------------------------------------
PRINT ' Procedure Alta Empleado'
GO

CREATE PROCEDURE mayusculas_sin_espacios.sp_altaEmpleado (@DNI numeric(8,0), @Nombre nvarchar(30),
					@Apellido nvarchar(30), @Mail nvarchar(255), @Telefono nvarchar(20),
					@Direccion nvarchar(255), @Provincia tinyint, @Tipo tinyint,@Sucursal tinyint,@habilitado tinyint)
as
begin

INSERT INTO mayusculas_sin_espacios.Empleados(DNI, Nombre, Apellido, Mail, Telefono, Direccion, Provincia, Tipo, Sucursal,Habilitado)
VALUES (@DNI,@Nombre,@Apellido,@Mail,@Telefono,@Direccion,@Provincia,@Tipo,@Sucursal,@habilitado)

end
GO

--------------------------------------------------------
PRINT ' Procedure Modificacion Empleado'
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_ModifEmpleados](@DNI numeric(8,0), @Nombre nvarchar(30),
					@Apellido nvarchar(30), @Mail nvarchar(255), @Telefono nvarchar(20),
					@Direccion nvarchar(255), @habilitado tinyint)
AS
begin
UPDATE  mayusculas_sin_espacios.Empleados
SET nombre=@Nombre, apellido=@Apellido, mail=@Mail, telefono=@Telefono, direccion=@Direccion, habilitado = @habilitado
WHERE dni=@dni
end
GO

--------------------------------------------------------
PRINT ' Procedure Alta Cliente'
GO

CREATE PROCEDURE mayusculas_sin_espacios.sp_altaCliente (@DNI numeric(8,0), @Nombre nvarchar(30),
					@Apellido nvarchar(30), @Mail nvarchar(255), @Telefono nvarchar(20),
					@Direccion nvarchar(255), @Provincia tinyint, @habilitado tinyint)
AS
begin

INSERT INTO mayusculas_sin_espacios.Clientes(DNI, Nombre, Apellido, Mail, Telefono, Direccion, Provincia, Habilitado)
VALUES (@DNI,@Nombre,@Apellido,@Mail,@Telefono,@Direccion,@Provincia,@habilitado)

end
GO

--------------------------------------------------------
PRINT ' Procedure Modificacion Cliente'
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_ModifClientes](@DNI numeric(8,0), @Nombre nvarchar(30),
					@Apellido nvarchar(30), @Mail nvarchar(255), @Telefono nvarchar(20),
					@Direccion nvarchar(255), @habilitado tinyint)
AS
begin
UPDATE  mayusculas_sin_espacios.Clientes
SET nombre=@Nombre, apellido=@Apellido, mail=@Mail, telefono=@Telefono, direccion=@Direccion, habilitado = @habilitado
WHERE dni=@dni
end
GO

--------------------------------------------------------
PRINT 'PROCEDURE Baja Empleado'
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_BajaEmpleados](@DNI numeric(8,0), @habilitado tinyint)
as
begin
UPDATE  mayusculas_sin_espacios.Empleados
SET  habilitado = @habilitado
WHERE dni=@dni
end
go

--------------------------------------------------------
PRINT 'PROCEDURE Baja Cliente '
go

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_BajaClientes](@DNI numeric(8,0), @habilitado tinyint)
as
begin
UPDATE  mayusculas_sin_espacios.Clientes
SET  habilitado = @habilitado
WHERE dni=@dni
end
go

--------------------------------------------------------
PRINT 'PROCEDURE Asignacion Roles'
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_asignacion] 
						(@user nvarchar(30),@rol int, @habilitado tinyint)
as
begin
DECLARE @CODIGO INT
DECLARE @codUser INT
select @codUser=codigo from [MAYUSCULAS_SIN_ESPACIOS].[Usuarios] where @user =nombre

if (not @rol in (select distinct Rol from [MAYUSCULAS_SIN_ESPACIOS].[Asignaciones]
	 where usuario =@coduser))
  begin
  if (@habilitado>0)
	insert into [MAYUSCULAS_SIN_ESPACIOS].[Asignaciones](Usuario,Rol)values(@coduser,@rol)
  end
else
  begin
  if (@habilitado=0)
	delete from [MAYUSCULAS_SIN_ESPACIOS].[Asignaciones] where @rol=rol and @codUser=usuario
  end
end
GO

--------------------------------------------------------
PRINT 'PROCEDURE Alta  Usuarios '
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_altaUsuario] (@Nombre nvarchar(30),
							@password nvarchar(64),@empleado numeric(8,0), @habilitado tinyint)
as
BEGIN
INSERT INTO mayusculas_sin_espacios.Usuarios(Nombre, password, empleado, Habilitado,intentos)
VALUES (@Nombre,@password,@empleado,@habilitado,'0')
END
GO

--------------------------------------------------------
PRINT 'PROCEDURE Modificacion Usuarios '
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_ModifUsuarios] (@Nombre nvarchar(30),
							@password nvarchar(64), @habilitado tinyint)
AS
BEGIN
if @password ='null'
begin
	UPDATE mayusculas_sin_espacios.Usuarios
	SET habilitado=@habilitado, intentos='0'
	where nombre=@nombre
end
else
begin
	UPDATE mayusculas_sin_espacios.Usuarios
	SET password=@Password, habilitado=@habilitado, intentos='0'
	where nombre=@nombre
end
END
GO

--------------------------------------------------------
PRINT 'PROCEDURE Baja Usuarios '
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_BajaUsuarios](@Nombre nvarchar(30), @habilitado tinyint)
AS
BEGIN
UPDATE  mayusculas_sin_espacios.Usuarios
SET  habilitado = @habilitado
WHERE nombre = @Nombre
END
GO

--------------------------------------------------------
-------------------------------------------------------
PRINT 'PROCEDURE Facturar'
GO

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_facturar](@fecha datetime, @descuento decimal,@cuotas int,
										 @sucursal int, @vendedor int, @cliente int)
as
begin

insert into mayusculas_sin_espacios.facturas (fecha,descuento,cuotas,sucursal,vendedor,cliente) 
values (@fecha,@descuento/100,@cuotas,@sucursal,@vendedor,@cliente)

select max(numero)
from mayusculas_sin_espacios.facturas

end
GO


PRINT 'PROCEDURE Efectuar Pago'
GO

CREATE PROCEDURE mayusculas_sin_espacios.sp_facturar(@fecha datetime, @descuento decimal,@cuotas int,
						 @sucursal int, @vendedor int, @cliente int)
AS
BEGIN
insert into mayusculas_sin_espacios.facturas (fecha,descuento,cuotas,sucursal,vendedor,cliente) 
values (@fecha,@descuento/100,@cuotas,@sucursal,@vendedor,@cliente)

return (select numero
from mayusculas_sin_espacios.facturas
where (@fecha=fecha and @descuento=descuento and @sucursal=sucursal and @vendedor =vendedor and @cliente =cliente))
END

--------------------------------------------------------
PRINT 'PROCEDURE FACTURAS PENDIENTES'
GO

CREATE PROCEDURE mayusculas_sin_espacios.sp_FACTURASPENDIENTES(@cliente int)
AS
BEGIN
SELECT numero
FROM mayusculas_sin_espacios.facturas
where CLIENTE = @CLIENTE AND (mayusculas_sin_espacios.fun_faltancuotas (numero)) >0
END
GO

--------------------------------------------------------
PRINT 'FUNCION Cuotas Faltantes'
GO

CREATE FUNCTION [MAYUSCULAS_SIN_ESPACIOS].[fun_faltanCuotas](@factura int)
returns int
AS
BEGIN

declare @pagas int
declare @total int
set @pagas = (select isnull(sum(cuotas),0)from mayusculas_sin_espacios.pagos where factura = @factura)
set @total = (select cuotas FROM mayusculas_sin_espacios.facturascompletas where numero=@factura)

return (@total-@pagas)
END
GO

--------------------------------------------------------
PRINT 'PROCEDURE Efectuar Pago'
GO

CREATE PROCEDURE mayusculas_sin_espacios.sp_Pagar(@factura int, @sucursal int,@cuotas int,
						 @fecha datetime, @cobrador int)
AS
BEGIN

insert into mayusculas_sin_espacios.pagos (factura,sucursal,cuotas,fecha,cobrador) 
values (@factura,@sucursal,@cuotas,@factura,@cobrador)

END