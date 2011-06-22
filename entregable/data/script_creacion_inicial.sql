-- Seteo la DB
USE [GD1C2011]
GO

-- Genero el Schema
/**
 * Chequeo la existencia del schema antes de crearlo.
 */
IF NOT EXISTS(SELECT 1 FROM information_schema.schemata WHERE
schema_name='MAYUSCULAS_SIN_ESPACIOS')
EXEC ('create schema MAYUSCULAS_SIN_ESPACIOS AUTHORIZATION gd');
GO

--------------------------------------------------------

/*
 * NOTA: los tildes y demas caracteres no ASCII-standar fueron omitidos deliberadamente a fin de
 * evitar problemas de codificacion en el versionado de los archivos
 */

/*
 * La normalizacion de la tabla Maestra de datos genero la aparicion de varias tablas 'simples'
 * como las Provincias, Tipos de Empleados, Tipos de Sucursal y las Sucursales.
 *
 * Los unicos campos que estas tablas poseen son su identificador (generalmente, un codigo
 * numerico autogenerado) y el nombre o descripcion de la entidad modelada.
 *
 * Estas coinciden, ademas, en ser inmutables a lo largo de la vida del sistema.
 */

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

/*
 * Las provincias existentes son los 24 posibles valores del campo SUC_PROVINCIA de la tabla
 * Maestra.
 */

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

/*
 * Existen solo dos tipos de empleados
 */
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

/*
 * Las Sucursales pueden ser de tipo Sucursal o Casa Central
 */
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.TiposSucursal ([Descripcion])
(SELECT DISTINCT SUC_TIPO
FROM gd_esquema.Maestra)

GO

--------------------------------------------------------
PRINT 'Tabla Sucursales'
GO

/*
 * Existe una Sucursal por cada provincia, y cada provincia tiene una unica sucursal, por lo que
 * conocer la Provincia es suficiente para identificar a una Sucursal.
 */
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

/*
 * La vista SucursalProvincia es una version menos normalizada de la relacion
 * Provincia-Sucursal-TipoSucursal que simplifica el acceso a la informacion "de negocio" de las
 * Sucursales.
 */
CREATE VIEW [MAYUSCULAS_SIN_ESPACIOS].[SucursalProvincia] AS
SELECT Provincias.Codigo AS Codigo, Provincias.Nombre AS Provincia, TiposSucursal.Descripcion AS Tipo, Sucursales.Direccion AS Direccion, Sucursales.Telefono AS Telefono
FROM MAYUSCULAS_SIN_ESPACIOS.Provincias LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Sucursales ON Provincias.Codigo = Sucursales.Provincia
		LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.TiposSucursal ON Sucursales.Tipo = TiposSucursal.Codigo
GO

--------------------------------------------------------
PRINT 'Tabla Cliente'
GO

/*
 * Los Clientes del sistema (al igual que varias entidades mas) tienen la propiedad de estar
 * Habilitados o no, y de ello depende la posibilidad de realizar determinadas acciones en el
 * mismo.
 */

/*
 * Se modifico el tipo de datos original del campo DNI (varchar) a un tipo numerico porque los
 * caracteres no-numericos no tienen sentido alguno en un DNI, y unificar el tipo del DNI de los
 * Clientes con el de los Empleados facilita la aplicacion de las restricciones de DNI entre ambos.
 *
 * Cabe destacar que la modificacion es simplemente a nivel de representacion, ya que, si bien
 * eran almacenados como una cadena de caracteres, los DNIs originales de los Clientes eran todos
 * numericos.
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

GO

/*
 * Los datos de la tabla Clientes se cargan mas adelante para verificar las restricciones respecto
 * de la duplicacion de DNIs con los Empleados de la empresa.
 */

--------------------------------------------------------
PRINT 'Indices Clientes'
GO

/*
 * Sobre la tabla Clientes (al igual que para todas las otras tablas de la base de datos) se crean
 * indices para los campos por los cuales se realizaran las busquedas desde el sistema, a fin de
 * acelerar las mismas.
 */
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

/*
 * La tabla Empleados almacena los datos sobre los Empleados del sistema.
 */
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

/*
 * Un modelo alternativo podria haber consistido en una tabla que almacenara datos de Personas,
 * entendiendo como Persona tanto a los Clientes como a los Empleados, dado que los datos de cada
 * uno coinciden en su mayoria, y tablas adicionales que representaran los datos particulares de
 * cada tipo.
 *
 * Esta alternativa simplificaria el control de duplicados de los DNIs entre entidades de una y
 * otra naturaleza, pero como contrapartida complejizaria el manejo de las trancisiones de tipos
 * (los Clientes que se convierten en Empleados, y la prohibición de venta a los Empleados).
 */

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
 * En cada Alta de un Empleado, el trigger NuevoEmpleado se encarga de deshabilitar a todo Cliente
 * que tuviera el mismo DNI que el nuevo Empleado, cumpliendo asi la restriccion de no vender a los
 * empleados de la empresa.
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
 * El trigger LosEmpleadosNoPuedenHacerseClientes deshacen el alta de un Cliente en caso de
 * detectar que el mismo ya es Empleado de la empresa.
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

/*
 * La base Maestra no cuenta con informacion sobre Telefono, Direccion ni Provincia de residencia
 * de los Clientes existentes.
 */
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
 * Los Empleados de tipo Vendedor tienen asignada como Sucursal aquella en la que realizan sus
 * ventas.
 *
 * Los Empleados Analistas, en cambio, tienen como Sucursal asignada aquella cuyo Tipo sea Sede
 * Central.
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

/*
 * Un metodo mas eficiente de realizar estas inserciones de datos podria haber sido delegando en
 * una funcion auxiliar o un CASE dentro del SELECT la tarea de determinal la Sucursal de un
 * Empleado a partir de su Tipo, evitando asi hacer el UNION entre dos consultas.
 */

--------------------------------------------------------
PRINT 'Tabla Usuarios'
GO


/*
 * Los Empleados se identifican en el sistema mediante un Usuario.
 *
 * Si bien el Nombre de cada Usuario en el sistema es unico e inmodificable, se prefirio usar un
 * codigo autogenerado numerico como identificador por cuestiones de performance en las busquedas
 * y relaciones.
 */
/*
 * El password inicial por defecto de los Usuarios es 'password' (sin las comillas).
 * Los passwords se almacenan codificados en SHA256, en su representacion hexadecimal, con los
 * caracteres alfabeticos en mayusculas, y sin guiones ni separadores entre bytes.
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
 * El Usuario 'admin' es un Usuario especial dentro del sistema que no esta asignado a ningun
 * Empleado. Su password inicial es 'w23e' (sin las comillas).
 */
INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Usuarios] (Nombre, Password, Empleado)
VALUES ('admin', 'E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7', null);
GO

--------------------------------------------------------
PRINT 'Insert Usuarios de los empleados'
GO

/*
 * La migracion de datos tambien genera automaticamente un Usuario para cada Empleado del sistema.
 * El nombre de usuario se genera convirtiendo a minusculas la concatenacion del nombre y apellido
 * de cada Empleado, y eliminando los espacios existentes.
 */
INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Usuarios] (Nombre, Empleado)
(SELECT REPLACE(LOWER(Nombre + Apellido), ' ','') AS Username, DNI
FROM MAYUSCULAS_SIN_ESPACIOS.Empleados)

GO

--------------------------------------------------------
PRINT 'Tabla Roles'
GO

/*
 * Los Roles del sistema son conjuntos de funcionalidades que pueden realizar los Usuarios a
 * quienes estan asignados.
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Roles] (
	[Codigo] [int] IDENTITY(1, 1) PRIMARY KEY,
	[Nombre] [nvarchar] (60),
	[Habilitado] [tinyint] DEFAULT 1
)
GO

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
 * Existe un rol 'Administrador General' que sera asignado al Usuario especial 'admin'
 */
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.Roles ([Nombre])
VALUES ('Administrador General');

GO

/*
 * Tambien se podrian haber creado algunos otros Roles basicos para asignarles a los Usuarios de
 * los Empleados del sistema, posiblemente asignados segun su Tipo.
 */

--------------------------------------------------------
PRINT 'Tabla Asignaciones'
GO

/*
 * Si bien no existe motivo de negocio para que las Asignaciones tengan una Clave Primaria, se
 * decidio usarla en lugar de una restriccion UNIQUE porque esta ultima permite que alguno de
 * los dos valores sea nulo, hecho que no queremos permitir en nuestro sistema.
 *
 * Este mismo criterio se utilizo en otras entidades asociativas.
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Asignaciones] (
	[Usuario] [int],
	[Rol] [int],
	PRIMARY KEY (Usuario, Rol)
)
GO

--------------------------------------------------------
PRINT 'Insert Asignacion del usuario admin'
GO

/*
 * El Usuario especial 'admin' tiene pre-asignado el rol 'Administrador General'
 */

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Asignaciones] (Usuario, Rol)
(SELECT TOP 1 Usuarios.Codigo, Roles.Codigo
FROM MAYUSCULAS_SIN_ESPACIOS.Usuarios, MAYUSCULAS_SIN_ESPACIOS.Roles
WHERE Usuarios.Nombre = 'admin' AND Roles.Nombre = 'Administrador General')
GO


/*
 * Las posibles Funcionalidades que conforman cada Rol del sistema definen a que acciones del
 * sistema tienen acceso los Usuarios que tienen asignado ese Rol.
 *
 * Las distintas Funcionalidades son fijas, definidas por extension en el enunciado del TP.
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
 * FuncionalidadesRol determina que Funcionalidad esta incluida en cada Rol
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[FuncionalidadesRol] (
	[Rol] [int] REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Roles] (Codigo),
	[Funcionalidad] [tinyint] REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Funcionalidades] (Codigo),
	PRIMARY KEY (Rol, Funcionalidad)
)
GO

--------------------------------------------------------
PRINT 'Insert Funcionalidades del Rol Administrador General'
GO

/*
 * El Rol 'Administrador General' incluye todas las Funcionalidades del sistema
 */
INSERT INTO MAYUSCULAS_SIN_ESPACIOS.FuncionalidadesRol ([Rol], [Funcionalidad])
(SELECT Roles.Codigo, Funcionalidades.Codigo
FROM MAYUSCULAS_SIN_ESPACIOS.Roles, MAYUSCULAS_SIN_ESPACIOS.Funcionalidades
WHERE Roles.Nombre = 'Administrador General');

GO

--------------------------------------------------------
PRINT 'Tabla Categorias'
GO

/*
 * La tabla Categorias representa las diferentes Categorias de los Productos del sistema, junto a
 * su jerarquia (establecida a traves de la relacion mediante el campo Padre).
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Categorias] (
	[Codigo] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](100) NULL,
	[Padre] [int] NULL DEFAULT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Categorias] (Codigo)
)
GO

--------------------------------------------------------
PRINT 'Indices Categoria'
GO

CREATE INDEX CategoriasPorNombre
    ON MAYUSCULAS_SIN_ESPACIOS.Categorias (Nombre);
GO

--------------------------------------------------------
/*
 * El procedimiento Parse es el encargado de procesar las cadenas de caracteres que representan
 * a las Categorias en la tabla Maestra y almacenarlas de forma normalizada en el nuevo modelo de
 * datos.
 */
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
 * Se ejecuta la insercion de la Categoria de cada Producto de la tabla Maestra.
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
GO


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
GO
--------------------------------------------------------
PRINT 'TABLA PRODUCTOS'
GO 

/*
 * Se garantiza que el Precio de cada Producto sea mayor a 0 como determinan las reglas del
 * negocio.
 */
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

/*
 * Activar IDENTITY_INSERT permite determinar el identificador del Producto en las altas en que se
 * desee, mientras que en el resto el mismo sera autogenerado.
 */
SET IDENTITY_INSERT [MAYUSCULAS_SIN_ESPACIOS].[Productos] ON;
GO

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

/*
 * Para dar de alta los Productos del sistema se recorre la tabla Maestra mediante un cursor, y se
 * utilizan los procedimientos creados previamente para determinar las entidades con las que deben
 * establecerse las relaciones.
 *
 * El codigo de los Productos migrados se extrae del antiguo Nombre del Producto.
 */
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
GO

/*
 * Se desactiva la insercion de Productos con identificadores arbitrarios
 */
SET IDENTITY_INSERT [MAYUSCULAS_SIN_ESPACIOS].[Productos] OFF;
GO

--------------------------------------------------------
PRINT 'VISTA PRODUCTOS COMPLETOS'
GO 

/*
 * La vista ProductosCompletos muestra una version menos normalizada pero mas completa de los datos
 * de negocio de los Productos, mediante el aprovechamiento de las relaciones establecidas con las
 * Categorias y Marcas.
 */
CREATE VIEW [MAYUSCULAS_SIN_ESPACIOS].[ProductosCompletos] AS
SELECT Productos.Codigo, Productos.Nombre, Productos.Descripcion, Productos.Categoria AS CodigoCategoria, Categorias.Nombre AS Categoria, Productos.Precio, Productos.Marca AS CodigoMarca, Marcas.Nombre AS Marca, Productos.Habilitado
FROM MAYUSCULAS_SIN_ESPACIOS.Productos
	LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Categorias ON Productos.Categoria = Categorias.Codigo
	LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Marcas ON Productos.Marca = Marcas.Codigo

GO

--------------------------------------------------------
PRINT 'TABLA FACTURAS'
GO

/*
 * Los Numeros de Factura se generan automaticamente, y la numeracion es compartida por todas las
 * Sucursales del sistema.
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Facturas] (
	[Numero] [int] IDENTITY PRIMARY KEY,
	[Fecha] [datetime],
	[Descuento] [float],
	[Cuotas] [tinyint],
	[Sucursal] [tinyint] FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Sucursales] (Provincia),
	[Vendedor] [numeric] (8, 0) FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Empleados] (DNI),
	[Cliente] [numeric] (8,0) FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Clientes] (DNI)
)
GO

SET IDENTITY_INSERT [MAYUSCULAS_SIN_ESPACIOS].[Facturas] ON;
GO

--------------------------------------------------------
PRINT 'Indices Facturas'
GO

CREATE INDEX FacturasPorCliente
    ON MAYUSCULAS_SIN_ESPACIOS.Facturas (Cliente);
GO

CREATE INDEX FacturasPorFecha
    ON MAYUSCULAS_SIN_ESPACIOS.Facturas (Fecha);
GO

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
 * MovmientosStock almacena el historial de entradas y salidas de Stock realizadas en la empresa.
 *
 * Las Entradas de Stock tienen una Cantidad positvia, con el Analista responsable en su campo
 * Auditor, mientras que las Salidas tienen Cantidad negativa, y su Auditor es el Vendedor que
 * realizo la venta.
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[MovimientosStock] (
	[Producto] [int] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Productos] (Codigo),
	[Sucursal] [tinyint] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Sucursales] (Provincia),
	[Auditor] [numeric](8,0) NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Empleados] (DNI),
	[Cantidad] [int] NOT NULL,
	[Fecha] [datetime]
)
GO

/*
 * Probablemente hubiera sido mas conveniente llamar 'Empleado' al campo Auditor, ya que
 * es mas representativo para ambos tipos de Movimiento.
 */

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

/*
 * Se insertan todas las Llegadas de Stock de la tabla Maestra como Entradas de Stock.
 */
INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[MovimientosStock] (Producto, Sucursal, Auditor, Cantidad, Fecha)
(SELECT CONVERT(INT, SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10)), Provincias.Codigo, EMPLEADO_DNI, LLEGADA_STOCK_CANT, LLEGADA_STOCK_FECHA
FROM gd_esquema.Maestra LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Provincias ON Provincias.Nombre = SUC_PROVINCIA
WHERE LLEGADA_STOCK_CANT IS NOT NULL AND LLEGADA_STOCK_CANT <> 0)
GO


--------------------------------------------------------
PRINT 'TABLA ITEM FACTURAS'
GO 

/*
 * ItemsFactura representa el detalle de las Facturas existentes en el sistema.
 */
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
 * El trigger ItemVendido genera la Salida de Stock correspondiente para cada Venta realizada
 * en el sistema.
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

/*
 * Se cargan todos los detalles de facturacion de la tabla Maestra en el nuevo modelo.
 */
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
/*
 * La vista FacturasCompletas muestra informacion de conveniencia ya procesada de las Facturas
 * (como ser su Importe Base, Importe Final o Valor de cada Cuota) a fin de simplificar las
 * consultas.
 */
CREATE VIEW [MAYUSCULAS_SIN_ESPACIOS].[FacturasCompletas] AS
SELECT Numero, Fecha, Descuento, Descuento * 100 AS PorcentajeDescuento, Cuotas, SUM(PrecioUnitario * Cantidad) AS MontoBase, SUM(PrecioUnitario * Cantidad) * (1 - Descuento) AS Importe, (SUM(PrecioUnitario * Cantidad) * (1 - Descuento)) / Cuotas AS ValorCuota, Sucursal, Vendedor, Cliente
FROM MAYUSCULAS_SIN_ESPACIOS.Facturas LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.ItemsFactura ON ItemsFactura.Factura = Facturas.Numero
GROUP BY Sucursal, Fecha, Vendedor, Cliente, Numero, Descuento, Cuotas
GO

--------------------------------------------------------
PRINT 'VISTA STOCKS'
GO 

/*
 * La vista Stocks es un resumen que muestra el Stock actual de cada Producto del sistema para
 * cada Sucursal.
 */
CREATE VIEW [MAYUSCULAS_SIN_ESPACIOS].[Stocks] AS
SELECT Producto, Sucursal, SUM(Cantidad) AS Cantidad
FROM MAYUSCULAS_SIN_ESPACIOS.MovimientosStock
GROUP BY Producto, Sucursal
GO

--------------------------------------------------------
PRINT 'TABLA PAGOS'
GO


/*
 * Cada Pago del sistema esta relacionado con una Factura, y se almacena la cantidad de Cuotas que
 * se abonan en cada operacion.
 *
 * Dado que no existen elementos que dependan de los Pagos, estos no poseen Clave Primaria.
 */
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Pagos] (
	[Factura] [int] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Facturas] (Numero),
	[Sucursal] [tinyint] NOT NULL FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Sucursales] (Provincia),
	[Cuotas] [tinyint],
	[Fecha] [datetime],
	[Cobrador] [numeric] (8, 0) FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].[Empleados] (DNI)
)
GO


/*
 * El trigger NoPagarMasCuotasDeLasQueHay evita que se cobren mas Cuotas de las que corresponde a
 * cada Factura, deshaciendo la operacion en caso de error.
 */
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


/*
 * Se dan de alta los Pagos previamente registrados en el sistema anterior.
 */
INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Pagos] (Factura, Sucursal, Cuotas, Fecha, Cobrador)
(SELECT FACTURA_NRO, Provincias.Codigo, PAGO_MONTO / FacturasCompletas.ValorCuota, PAGO_FECHA, PAGO_EMPLEADO_DNI
FROM gd_esquema.Maestra	LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Provincias ON Provincias.Nombre = SUC_PROVINCIA
						LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.FacturasCompletas ON FacturasCompletas.Numero = FACTURA_NRO
WHERE PAGO_MONTO IS NOT NULL AND PAGO_MONTO <> 0)

GO

--------------------------------------------------------
PRINT 'PROCEDURE MODIFINTENTOS '
GO

/*
 * sp_MODIFINTENTOS actualiza la cantidad de intentos de inicio de sesion consecutivos de un
 * Usuario que fallaron.
 */
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

/*
 * sp_LOGIN es el procedimiento encargado de verificar los datos de inicio de sesion provistos por
 * el Usuario del sistema.
 *
 * En caso de un intento de identificacion erroneo, se recurre al procedimiento anterior para
 * actualizar la cantidad de intentos fallidos realizados y, de haber alcanzado el limite maximo,
 * deshabilita el Usuario.
 *
 * En caso de exito, el procedimiento reinicia el contador de intentos fallidos, y devuelve el
 * listado de Funcionalidades a las que puede acceder el Usuario.
 */
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

/*
 * El procedimiento sp_altaEmpleado genera el nuevo Empleado en la tabla correspondiente.
 */

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

/*
 * Modificacion de los Empleados existentes.
 */

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

/*
 * Generacion de nuevos Clientes.
 */
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

/*
 * Modificacion de los Clientes existentes.
 */
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

/*
 * La eliminacion de Empleados (asi como la de otras entidades del modelo) se realiza de forma
 * logica a traves del campo Habilitado, para evitar perder la informacion registrada del mismo.
 */
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

/*
 * Se genera la asignacion de un Rol a un determinado Usuario, verificando que la asignacion no
 * este duplicada, y se controla el alta y eliminacion de las Asignaciones existentes.
 */
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

-------------------------------------------------------
PRINT 'PROCEDURE Facturar'
GO

/*
 * Al Facturar se genera la Factura correspondiente en el sistema.
 */
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

/*
 * fun_faltanCuotas calcula la cantidad de Cuotas pendientes de Pago para una determinada Factura
 */
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

/*
 * sp_Pagar registra el Pago de una o mas Cuotas para una Factura determinada.
 */
CREATE PROCEDURE mayusculas_sin_espacios.sp_Pagar(@factura int, @sucursal int,@cuotas int,
						 @fecha datetime, @cobrador int)
AS
BEGIN
insert into mayusculas_sin_espacios.pagos (factura,sucursal,cuotas,fecha,cobrador) 
values (@factura,@sucursal,@cuotas,@factura,@cobrador)
END
GO

--------------------------------------------------------
PRINT 'VISTA DeudasPorAnio'
GO

/*
 * La vista DeudasPorAnio resume el endeudamiento de cada Cliente en cada Sucursal a lo largo de
 * los Anios.
 */
CREATE VIEW MAYUSCULAS_SIN_ESPACIOS.DeudasPorAnio AS
SELECT Cliente, FacturasCompletas.Sucursal, YEAR(FacturasCompletas.Fecha) AS Anio, SUM(Importe) - SUM(Pagos.Cuotas * FacturasCompletas.ValorCuota) AS DeudaTotal
FROM MAYUSCULAS_SIN_ESPACIOS.FacturasCompletas
	LEFT JOIN MAYUSCULAS_SIN_ESPACIOS.Pagos ON FacturasCompletas.Numero = Pagos.Factura
GROUP BY FacturasCompletas.Sucursal, YEAR(FacturasCompletas.Fecha), Cliente
GO

--------------------------------------------------------
PRINT 'VISTA VendedoresPorAnio'
GO

/*
 * VendedoresPorAnio sumariza el Importe facturado por cada Empleado en cada Anio, discriminado por
 * cada Sucursal.
 */
CREATE VIEW MAYUSCULAS_SIN_ESPACIOS.VendedoresPorAnio AS
SELECT Vendedor, FacturasCompletas.Sucursal, YEAR(FacturasCompletas.Fecha) AS Anio, SUM(Importe) AS VentaTotal
FROM MAYUSCULAS_SIN_ESPACIOS.FacturasCompletas
GROUP BY FacturasCompletas.Sucursal, YEAR(FacturasCompletas.Fecha), Vendedor
GO

--------------------------------------------------------
PRINT 'VISTA ProductosPorAnio'
GO

/*
 * ProductosPorAnio muestra la cantidad de Unidades vendidas de cada Producto anualmente, por cada
 * Sucursal.
 */
CREATE VIEW MAYUSCULAS_SIN_ESPACIOS.ProductosPorAnio AS
SELECT Producto, Sucursal, YEAR(Fecha) AS Anio, SUM(Cantidad) AS Vendidos
FROM MAYUSCULAS_SIN_ESPACIOS.ItemsFactura
	JOIN MAYUSCULAS_SIN_ESPACIOS.Facturas ON ItemsFactura.Factura = Facturas.Numero
GROUP BY Producto, Sucursal, YEAR(Fecha)
GO

--------------------------------------------------------
PRINT 'Funcion Dias sin stock'
GO

/*
 * DiasSinStock es una funcion que calcula la cantidad de Dias que un Producto estuvo falto de
 * Stock en un determinado Anio, para una Sucursal dada.
 *
 * El resultado se obtiene efectuando ordenadamente el calculo del Stock acumulado para cada
 * Movimiento, y almacenando la fecha de inicio de faltante cuando corresponde.
 *
 * Al detectar un paso a Stock positivo tras un faltante, se acumula la cantidad de dias
 * transcurridos entre el comienzo del faltante y la entrada que lo compenso, y ese valor
 * es el que se devuelve al finalizar.
 *
 * Si tras recorrer todos los movimientos se detecta que el Producto termino con Stock faltante,
 * se contabilizan tambien todos los dias restantes.
 */
CREATE FUNCTION [MAYUSCULAS_SIN_ESPACIOS].[DiasSinStock](@CodigoProducto int, @Anio int, @Sucursal tinyint)
RETURNS INT
AS
BEGIN
	DECLARE @Stock INT
	DECLARE @Dias INT
	DECLARE @FechaFaltante DATETIME
	DECLARE @UltimaFecha DATETIME
	DECLARE @Cantidad INT
	DECLARE @Fecha DATETIME
	DECLARE Movimientos CURSOR FOR
		SELECT Cantidad, Fecha
		 FROM MAYUSCULAS_SIN_ESPACIOS.MovimientosStock
		 WHERE Sucursal = @Sucursal AND Producto = @CodigoProducto AND YEAR(Fecha) = @Anio
		 ORDER BY Fecha ASC

	SELECT @Stock = SUM(Cantidad) FROM MAYUSCULAS_SIN_ESPACIOS.MovimientosStock WHERE Sucursal = @Sucursal AND Producto = @CodigoProducto AND YEAR(Fecha) < @Anio
	IF @Stock IS NULL BEGIN
		SET @Stock = 0
	END
	SET @Dias = 0
	
	OPEN Movimientos
	SET @FechaFaltante = NULL
	SET @UltimaFecha = NULL
	FETCH NEXT FROM Movimientos INTO @Cantidad, @Fecha
		
	WHILE @@FETCH_STATUS = 0 BEGIN
		SET @Stock = @Stock + @Cantidad
		IF @Stock <= 0 AND @FechaFaltante IS NULL BEGIN
			SET @FechaFaltante = @Fecha
		END
		IF @Stock > 0 AND @FechaFaltante IS NOT NULL BEGIN
			SET @Dias = @Dias + DATEDIFF(DAY, @FechaFaltante, @Fecha)
			SET @FechaFaltante = NULL
		END
		SET @UltimaFecha = @Fecha
		FETCH NEXT FROM Movimientos INTO @Cantidad, @Fecha
	END

	IF @FechaFaltante IS NOT NULL BEGIN
		SET @Dias = @Dias + DATEDIFF(DAY, @FechaFaltante, @UltimaFecha)
	END

	CLOSE Movimientos
	DEALLOCATE Movimientos

	RETURN @Dias
END
GO

--------------------------------------------------------
PRINT 'VISTA FALTANTES DE STOCK'
GO

/*
 * FaltantesDeStock relaciona cada Producto con la cantidad de DiasSinStock del mismo en cada
 * Sucursal en cada Anio.
 */
CREATE VIEW MAYUSCULAS_SIN_ESPACIOS.FaltantesDeStock AS
SELECT Producto, Sucursal, YEAR(Fecha) AS Anio, MAYUSCULAS_SIN_ESPACIOS.DiasSinStock(Producto, YEAR(Fecha), Sucursal) AS Dias
FROM MAYUSCULAS_SIN_ESPACIOS.MovimientosStock
GROUP BY Sucursal, YEAR(Fecha), Producto
GO

--------------------------------------------------------
PRINT 'Procedure Clientes Premium'
GO

/*
 * El procedimiento sp_ClientesPremium recupera la informacion requerida para la ventana homonima.
 * 
 * Es simplemente un conjunto de subconsultas unidas que muestra los 30 Clientes con mayor importe
 * comprado en un anio y sucursal determinados.
 */
CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[sp_ClientesPremium] (@sucursal int, @año int)
AS
select top(30) clientes.nombre as 'Nombre Cliente',
				clientes.apellido as 'Apellido Cliente', 
				clientes.dni as 'DNI Cliente', 
				sum(importe) as 'Importe Total',
				(select sum(itemsfactura.cantidad) 
				from mayusculas_sin_espacios.itemsfactura join mayusculas_sin_espacios.facturascompletas 
						on (itemsfactura.factura=facturascompletas.numero) join mayusculas_sin_espacios.facturas
						on(facturas.numero=facturascompletas.numero)
				where year(facturas.fecha)=@año and facturas.sucursal=@sucursal and clientes.dni= facturas.cliente
				) as 'Total Productos',
				(select top (1) facturas.fecha
				from mayusculas_sin_espacios.facturas
				where year(facturas.fecha)=@año and facturas.sucursal=@sucursal and clientes.dni= facturas.cliente
				order by facturas.fecha desc
				) as 'Fecha Ultima Compra',
				(select top (1) facturas.vendedor
				from mayusculas_sin_espacios.facturas
				where year(facturas.fecha)=@año and facturas.sucursal=@sucursal and clientes.dni= facturas.cliente
				order by facturas.fecha desc
				) as 'Empleado Ultima Compra'		
from mayusculas_sin_espacios.facturascompletas join mayusculas_sin_espacios.facturas
			on(facturas.numero=facturascompletas.numero) join mayusculas_sin_espacios.clientes
			on(clientes.dni=facturas.cliente) 
where year(facturas.fecha)=@año and facturas.sucursal=@sucursal
group by clientes.dni,clientes.nombre,clientes.apellido
order by sum(importe)desc
GO

--------------------------------------------------------
PRINT 'Funcion Categorias Hijas'
GO

/*
 * La funcion CatHijas devuelve una subtabla que incluye todos los datos de las Categorias
 * descendientes de una Categoria dada (tanto las categorias del nivel inmediato inferior como
 * las de los siguientes inferiores.
 */
CREATE FUNCTION mayusculas_sin_espacios.CatHijas(@id INT)
RETURNS @Table TABLE(codigo INT,nombre VARCHAR(50),padre INT)
AS
BEGIN 
;WITH ret AS(
        SELECT  * FROM mayusculas_sin_espacios.categorias WHERE codigo = @ID
        UNION ALL
        SELECT  t.* FROM    mayusculas_sin_espacios.categorias t INNER JOIN ret r ON t.padre = r.codigo)
insert into @table(codigo,nombre,padre)
SELECT * FROM ret
return 
end
GO

--------------------------------------------------------
PRINT 'FUNCION TOTAL FACTURADO POR CATEGORIA PADRE'
GO

/*
 * Partiendo de la seleccion de una Categoria y todas sus descendientes, TotalFacturado calcula la
 * Facturacion total de todos los Productos pertenecientes a esas Categorias en un determinado anio
 * y sucursal.
 */
CREATE FUNCTION MAYUSCULAS_SIN_ESPACIOS.TOTALFACTURADO(@CODIGO INT,@AÑO INT, @SUCURSAL INT)
RETURNS DECIMAL 
AS
BEGIN
DECLARE @TABLE TABLE(ID INT, NOMBRE NVARCHAR(50), PADRE INT)
INSERT INTO @TABLE(ID,NOMBRE,PADRE)
SELECT * FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[CatHijas] (@CODIGO)

DECLARE @TOTAL INT
SELECT  @TOTAL = SUM(PRECIOUNITARIO*(1-DESCUENTO))
FROM @TABLE JOIN MAYUSCULAS_SIN_ESPACIOS.PRODUCTOS ON (PRODUCTOS.CATEGORIA=ID) JOIN
				MAYUSCULAS_SIN_ESPACIOS.ITEMSFACTURA ON (ITEMSFACTURA.PRODUCTO=PRODUCTOS.CODIGO) JOIN
			MAYUSCULAS_SIN_ESPACIOS.FACTURAS ON (ITEMSFACTURA.FACTURA=FACTURAS.NUMERO)
WHERE YEAR(FACTURAS.FECHA)=@AÑO AND FACTURAS.SUCURSAL=@SUCURSAL
RETURN @TOTAL
END
GO

--------------------------------------------------------
PRINT 'PROCEDURE PRODUCTO MAS VENDIDO EN CANTIDAD'
GO

/*
 * sp_PRODMASVENDIDO determina el Codigo y Nombre del Producto mas vendido en una familia de
 * Categorias para un Anio particular en una Sucursal.
 */
CREATE PROCEDURE MAYUSCULAS_SIN_ESPACIOS.sp_PRODMASVENDIDO(@CODIGO INT,@AÑO INT, @SUCURSAL INT, @PROD INT OUTPUT,@NOMBRE NVARCHAR(50) OUTPUT)
AS
BEGIN
DECLARE @TABLE TABLE(ID INT, NOMBRE NVARCHAR(50), PADRE INT)
INSERT INTO @TABLE(ID,NOMBRE,PADRE)
SELECT * FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[CatHijas] (@CODIGO)
SELECT  TOP (1) @PROD=CODIGO,@NOMBRE=PRODUCTOS.NOMBRE
FROM @TABLE JOIN MAYUSCULAS_SIN_ESPACIOS.PRODUCTOS ON (PRODUCTOS.CATEGORIA=ID) JOIN
				MAYUSCULAS_SIN_ESPACIOS.ITEMSFACTURA ON (ITEMSFACTURA.PRODUCTO=PRODUCTOS.CODIGO) JOIN
			MAYUSCULAS_SIN_ESPACIOS.FACTURAS ON (ITEMSFACTURA.FACTURA=FACTURAS.NUMERO)
WHERE YEAR(FACTURAS.FECHA)=@AÑO AND FACTURAS.SUCURSAL=@SUCURSAL
GROUP BY CODIGO,PRODUCTOS.NOMBRE
ORDER BY SUM(CANTIDAD) DESC
END
GO

--------------------------------------------------------
PRINT 'PROCEDURE PRODUCTO MAS PAGADO EN FACTURACION'
GO

/*
 * sp_PRODMASPAGADO determina el Producto con mayor Precio de Venta (incluyendo Descuento) en una
 * familia de Categorias para una determinada Sucursal y Anio.
 */
CREATE PROCEDURE MAYUSCULAS_SIN_ESPACIOS.sp_PRODMASPAGADO(@CODIGO INT,@AÑO INT, @SUCURSAL INT, @PROD INT OUTPUT, @NOMBRE NVARCHAR(50) OUTPUT, @MONTO DECIMAL OUTPUT)
AS
BEGIN
DECLARE @TABLE TABLE(ID INT, NOMBRE NVARCHAR(50), PADRE INT)
INSERT INTO @TABLE(ID,NOMBRE,PADRE)
SELECT * FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[CatHijas] (@CODIGO)
DECLARE @MASPAGADO INT
SELECT  TOP (1) @PROD=CODIGO, @NOMBRE=PRODUCTOS.NOMBRE, @MONTO=SUM(PRECIO*(1-DESCUENTO))
FROM @TABLE JOIN MAYUSCULAS_SIN_ESPACIOS.PRODUCTOS ON (PRODUCTOS.CATEGORIA=ID) JOIN
				MAYUSCULAS_SIN_ESPACIOS.ITEMSFACTURA ON (ITEMSFACTURA.PRODUCTO=PRODUCTOS.CODIGO) JOIN
			MAYUSCULAS_SIN_ESPACIOS.FACTURAS ON (ITEMSFACTURA.FACTURA=FACTURAS.NUMERO)
WHERE YEAR(FACTURAS.FECHA)=@AÑO AND FACTURAS.SUCURSAL=@SUCURSAL
GROUP BY CODIGO,PRODUCTOS.NOMBRE
ORDER BY SUM(PRECIO*(1-DESCUENTO)) DESC
END
GO

--------------------------------------------------------
PRINT 'PROCEDURE PRODUCTO MAS CARO'
GO

/*
 * sp_PRODMASCARO determina el Producto con mayor Precio Unitario en una familia de Categorias para
 * una determinada Sucursal y Anio.
 */
CREATE PROCEDURE MAYUSCULAS_SIN_ESPACIOS.sp_PRODMASCARO(@CODIGO INT,@AÑO INT, @SUCURSAL INT, @PROD INT OUTPUT, @NOMBRE NVARCHAR(50) OUTPUT, @PRECIO DECIMAL OUTPUT)
AS
BEGIN
DECLARE @TABLE TABLE(ID INT, NOMBRE NVARCHAR(50), PADRE INT)
INSERT INTO @TABLE(ID,NOMBRE,PADRE)
SELECT * FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[CatHijas] (@CODIGO)
SELECT  TOP (1) @PROD=CODIGO, @NOMBRE=PRODUCTOS.NOMBRE, @PRECIO=MAX(PRECIO)
FROM @TABLE JOIN MAYUSCULAS_SIN_ESPACIOS.PRODUCTOS ON (PRODUCTOS.CATEGORIA=ID) JOIN
				MAYUSCULAS_SIN_ESPACIOS.ITEMSFACTURA ON (ITEMSFACTURA.PRODUCTO=PRODUCTOS.CODIGO) JOIN
			MAYUSCULAS_SIN_ESPACIOS.FACTURAS ON (ITEMSFACTURA.FACTURA=FACTURAS.NUMERO)
WHERE YEAR(FACTURAS.FECHA)=@AÑO AND FACTURAS.SUCURSAL=@SUCURSAL
GROUP BY CODIGO,PRODUCTOS.NOMBRE
ORDER BY MAX(PRECIO) DESC
END
GO

--------------------------------------------------------
PRINT 'PROCEDURE MEJOR EMPLEADO'
GO

/*
 * Calcula el Empleado que mas cantidad de Productos vendio de la familia de Categorias en ese Anio
 * y Sucursal.
 */
CREATE PROCEDURE MAYUSCULAS_SIN_ESPACIOS.sp_MEJOREMPLEADO(@CODIGO INT,@AÑO INT, @SUCURSAL INT, @NOMBRE NVARCHAR(50) OUTPUT, @APELLIDO NVARCHAR(50) OUTPUT)
AS
BEGIN
DECLARE @TABLE TABLE(ID INT, NOMBRE NVARCHAR(50), PADRE INT)
INSERT INTO @TABLE(ID,NOMBRE,PADRE)
SELECT * FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[CatHijas] (@CODIGO)
SELECT  TOP (1) @NOMBRE=EMPLEADOS.NOMBRE,@APELLIDO=EMPLEADOS.APELLIDO
FROM @TABLE JOIN MAYUSCULAS_SIN_ESPACIOS.PRODUCTOS ON (PRODUCTOS.CATEGORIA=ID) JOIN
				MAYUSCULAS_SIN_ESPACIOS.ITEMSFACTURA ON (ITEMSFACTURA.PRODUCTO=PRODUCTOS.CODIGO) JOIN
			MAYUSCULAS_SIN_ESPACIOS.FACTURAS ON (ITEMSFACTURA.FACTURA=FACTURAS.NUMERO)JOIN
			MAYUSCULAS_SIN_ESPACIOS.EMPLEADOS ON (VENDEDOR=DNI)
WHERE YEAR(FACTURAS.FECHA)=@AÑO AND FACTURAS.SUCURSAL=@SUCURSAL
GROUP BY EMPLEADOS.NOMBRE,EMPLEADOS.APELLIDO
ORDER BY SUM(CANTIDAD) DESC
END
GO

--------------------------------------------------------
PRINT 'PROCEDURE MEJORES CATEGORIAS'
GO

/*
 * sp_MEJORESCATEGORIAS es el procedimiento que recopila toda la informacion a mostrar en la
 * pantalla de Mejores Categorias.
 *
 * Utilizando un cursor para recorrer las Categorias de nivel superior, el procedimiento usa los
 * procedimientos previamente definidos para recolectar los resultados y finalmente ordenarlos
 */
CREATE PROCEDURE MAYUSCULAS_SIN_ESPACIOS.sp_MEJORESCATEGORIAS(@SUCURSAL INT, @AÑO INT)
AS
BEGIN
DECLARE @CODIGO INT
DECLARE @CATE NVARCHAR(50)
DECLARE @PADRE INT
DECLARE @SUBCOUNT INT
DECLARE @FACTURADO DECIMAL
DECLARE @MASVENDIDO INT
DECLARE @MASVENDIDODESC NVARCHAR(50)
DECLARE @MASPAGADO INT
DECLARE @MASPAGADOPRECIO DECIMAL
DECLARE @MASPAGADODESC NVARCHAR(50)
DECLARE @MASCARO INT
DECLARE @MASCAROPRECIO DECIMAL
DECLARE @MASCARODESC NVARCHAR(50)
DECLARE @MEJORNOMBRE NVARCHAR(50)
DECLARE @MEJORAPELLIDO NVARCHAR(50)
DECLARE @TABLE TABLE(NOMBRE NVARCHAR(50),SUBCAT INT, TOTAL DECIMAL, MASVENDIDO INT, MASVENDIDODESC NVARCHAR(50),
						MASPAGADO INT, MASPAGADODESC NVARCHAR(50), MASPAGADOPRECIO DECIMAL,
						MASCARO INT, MASCARODESC NVARCHAR(50), MASCAROPRECIO DECIMAL,
						MEJORNOMBRE NVARCHAR(50), MEJORAPELLIDO NVARCHAR(50))
DECLARE curPadres CURSOR for (SELECT  * from mayusculas_sin_espacios.categorias where padre is null)
open curPadres
fetch next from curPadres
into @CODIGO,@CATE,@PADRE
while @@fetch_status = 0
  begin
	SELECT @SUBCOUNT =COUNT(*) FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[CatHijas] (@CODIGO)
	SELECT @FACTURADO = [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[TOTALFACTURADO] (@CODIGO,@AÑO,@SUCURSAL)
	EXEC [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[sp_PRODMASVENDIDO] @CODIGO,@AÑO,@SUCURSAL,@MASVENDIDO OUTPUT, @MASVENDIDODESC OUTPUT
	EXEC [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[sp_PRODMASPAGADO] @CODIGO,@AÑO,@SUCURSAL,@MASPAGADO OUTPUT, @MASPAGADODESC OUTPUT, @MASPAGADOPRECIO OUTPUT
    EXEC [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[sp_PRODMASCARO] @CODIGO,@AÑO,@SUCURSAL,@MASCARO OUTPUT, @MASCARODESC OUTPUT, @MASCAROPRECIO OUTPUT
    EXEC [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[sp_MEJOREMPLEADO] @CODIGO,@AÑO,@SUCURSAL,@MEJORNOMBRE OUTPUT, @MEJORAPELLIDO OUTPUT
    INSERT INTO @TABLE(NOMBRE,SUBCAT, TOTAL, MASVENDIDO, MASVENDIDODESC,MASPAGADO,MASPAGADODESC,MASPAGADOPRECIO,MASCARO,MASCARODESC,MASCAROPRECIO,MEJORNOMBRE,MEJORAPELLIDO)
	VALUES (@CATE,@SUBCOUNT-1,@FACTURADO, @MASVENDIDO, @MASVENDIDODESC,@MASPAGADO,@MASPAGADODESC,@MASPAGADOPRECIO,@MASCARO,@MASCARODESC,@MASCAROPRECIO,@MEJORNOMBRE,@MEJORAPELLIDO)
	fetch next from curPadres
	into @CODIGO,@CATE,@PADRE
  end
close curPadres
deallocate curPadres
SELECT * FROM @TABLE ORDER BY 3 DESC
END
GO
