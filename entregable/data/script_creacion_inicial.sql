-- Seteo la DB
USE [GD1C2011]
GO

-- Genero el Schema
/**
 * Chequeo la existencia del schema antes de crearlo. Deberia pensar en eliminar las tablas y otros objetos tambien.
 */
if not exists(select 1 from information_schema.schemata where
schema_name='MAYUSCULAS_SIN_ESPACIOS')
EXEC ('create schema MAYUSCULAS_SIN_ESPACIOS AUTHORIZATION gd');
go


print ' TABLA CLIENTE '
GO
---------------------------------------------------------------------
--------------------CLIENTE------------------------------------------
----------------------140--------------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Cliente](
	[CLI_DNI] [nvarchar](255) PRIMARY KEY,
	[CLI_NOMBRE] [nvarchar](255) NULL,
	[CLI_APELLIDO] [nvarchar](255) NULL,
	[CLI_MAIL] [nvarchar](255) NULL,
	[CLI_TELEFONO] [nvarchar](255) NULL,
	[CLI_DIRECCION] [nvarchar](100) NULL,
	[CLI_PROVINCIA] [nvarchar](255) NULL
) 
GO



INSERT INTO MAYUSCULAS_SIN_ESPACIOS.CLIENTE([CLI_DNI],[CLI_NOMBRE],[CLI_APELLIDO],[CLI_MAIL])
(SELECT DISTINCT [CLI_DNI] ,[CLI_NOMBRE] ,[CLI_APELLIDO] ,[CLI_MAIL]
FROM GD_ESQUEMA.MAESTRA
WHERE CLI_DNI IS NOT NULL)
GO

print ' TABLA SUCURSAL'
GO
---------------------------------------------------------------------
--------------------SUCURSAL-----------------------------------------
----------------------24---------------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Sucursal](
	[SUC_CODIGO] INT PRIMARY KEY IDENTITY,
	[SUC_DIR] [nvarchar](255) NULL,
	[SUC_TEL] [nvarchar](255) NULL,
	[SUC_TIPO] [nvarchar](255) NULL,
	[SUC_PROVINCIA] [nvarchar](255) NULL
)
GO


INSERT INTO MAYUSCULAS_SIN_ESPACIOS.SUCURSAL(
		SUC_TIPO, SUC_DIR, SUC_TEL, SUC_PROVINCIA)
(SELECT SUC_TIPO, SUC_DIR, SUC_TEL, SUC_PROVINCIA
FROM GD_ESQUEMA.MAESTRA
GROUP BY SUC_TIPO, SUC_DIR, SUC_TEL, SUC_PROVINCIA)
GO

print ' TABLA EMPLEADO'
GO
---------------------------------------------------------------------
--------------------EMPLEADO-----------------------------------------
-----------------------53--------------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Empleado](
	[EMPLEADO_DNI] [numeric](8, 0) PRIMARY KEY,
	[EMPLEADO_NOMBRE] [nvarchar](255) NULL,
	[EMPLEADO_APELLIDO] [nvarchar](255) NULL,
	[EMPLEADO_MAIL] [nvarchar](255) NULL,
	[EMPLEADO_DIR] [nvarchar](255) NULL,
	[EMPLEADO_TIPO] [nvarchar](100) NULL,
	[EMPLEADO_PROVINCIA] [nvarchar](255) NULL,
	[EMPLEADO_TELEFONO] [nvarchar](255) NULL,
	[EMPLEADO_SUCURSAL] INT FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.SUCURSAL(SUC_CODIGO)
)
GO


INSERT INTO MAYUSCULAS_SIN_ESPACIOS.EMPLEADO(
		[EMPLEADO_DNI],[EMPLEADO_NOMBRE],[EMPLEADO_APELLIDO],[EMPLEADO_MAIL],
		[EMPLEADO_DIR],[EMPLEADO_TIPO],[EMPLEADO_PROVINCIA],[EMPLEADO_SUCURSAL])
(SELECT DISTINCT [EMPLEADO_DNI],[EMPLEADO_NOMBRE],[EMPLEADO_APELLIDO],[EMPLEADO_MAIL],
		[EMPLEADO_DIR],[EMPLEADO_TIPO],[EMPLEADO_PROVINCIA],[SUC_CODIGO]
FROM GD_ESQUEMA.MAESTRA M JOIN MAYUSCULAS_SIN_ESPACIOS.SUCURSAL S ON (M.[EMPLEADO_PROVINCIA] = S.SUC_PROVINCIA))
GO

print ' TABLA FACTURA'
GO
---------------------------------------------------------------------
--------------------FACTURA------------------------------------------
----------------------26855------------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Factura](
	[FACTURA_NRO] [int] PRIMARY KEY,
	[FACTURA_DESCUENTO] [float] NULL,
	[FACTURA_TOTAL] [float] NULL,
	[FACTURA_TOTAL_DESCU] [float] NULL,
	[FACTURA_FECHA] [datetime] NULL,
	[FACTURA_CANT_COUTAS] [int] NULL,
	[FACTURA_CLIENTE] [nvarchar](255) FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.CLIENTE(CLI_DNI),
	[FACTURA_EMPLEADO] [numeric](8, 0) FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.EMPLEADO(EMPLEADO_DNI)
)
GO


INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Factura]([FACTURA_NRO],[FACTURA_DESCUENTO] ,[FACTURA_TOTAL] ,
		[FACTURA_TOTAL_DESCU],[FACTURA_FECHA],[FACTURA_CANT_COUTAS],[FACTURA_CLIENTE],[FACTURA_EMPLEADO])
(SELECT  distinct [FACTURA_NRO],[FACTURA_DESCUENTO],[FACTURA_TOTAL],[FACTURA_TOTAL_DESCU],[FACTURA_FECHA],
		[FACTURA_CANT_COUTAS],[CLI_DNI],[EMPLEADO_DNI]
FROM GD_ESQUEMA.MAESTRA
WHERE FACTURA_NRO <> '0')
GO

print ' TABLA PAGO'
GO
---------------------------------------------------------------------
--------------------PAGO---------------------------------------------
-------------------142718--------------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Pago](
	[PAGO_CODIGO] [INT] PRIMARY KEY IDENTITY,
	[PAGO_FECHA] [datetime] NULL,
	[PAGO_MONTO] [float] NULL,
	[PAGO_EMPLEADO_DNI] [numeric](8, 0) FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].EMPLEADO(EMPLEADO_DNI),
	[PAGO_FACTURA_NRO] [int] FOREIGN KEY REFERENCES [MAYUSCULAS_SIN_ESPACIOS].FACTURA(FACTURA_NRO)
)
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Pago]([PAGO_FECHA] ,[PAGO_MONTO] ,[PAGO_EMPLEADO_DNI]
			,[PAGO_FACTURA_NRO] )
((SELECT [PAGO_FECHA] ,[PAGO_MONTO] ,[PAGO_EMPLEADO_DNI],[FACTURA_NRO]
FROM GD_ESQUEMA.MAESTRA
WHERE PAGO_FECHA IS NOT NULL)
UNION
(SELECT  DISTINCT [FACTURA_FECHA] ,([FACTURA_TOTAL_DESCU]/[FACTURA_CANT_COUTAS]) AS "PAGO MONTO" ,
		[EMPLEADO_DNI],[FACTURA_NRO]
FROM GD_ESQUEMA.MAESTRA
WHERE [FACTURA_CANT_COUTAS]>'1' AND [PAGO_FECHA] IS NULL))
ORDER BY [FACTURA_NRO]
GO

print ' TABLA PRODUCTO'
GO
---------------------------------------------------------------------
--------------------PRODUCTO-----------------------------------------
-----------------------99--------------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Producto](
	[PRODUCTO_CODIGO] [nvarchar](100) PRIMARY KEY,
	[PRODUCTO_NOMBRE] [nvarchar](100) NULL,
	[PRODUCTO_DESC] [nvarchar](100) NULL,
	[PRODUCTO_PRECIO] [float] NULL,
	[PRODUCTO_MARCA] [nvarchar](100) NULL,
	[PRODUCTO_CATE] [nvarchar](100) NULL
)
GO 

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Producto]([PRODUCTO_CODIGO],[PRODUCTO_NOMBRE] ,[PRODUCTO_DESC] ,
			[PRODUCTO_PRECIO] ,[PRODUCTO_MARCA] ,[PRODUCTO_CATE])
(SELECT  DISTINCT SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10),
			SUBSTRING(PRODUCTO_NOMBRE,1,LEN(PRODUCTO_NOMBRE)-11),[PRODUCTO_DESC],
			[PRODUCTO_PRECIO],[PRODUCTO_MARCA],[PRODUCTO_CATE]
FROM GD_ESQUEMA.MAESTRA
WHERE PRODUCTO_PRECIO <> '0')
GO

print ' TABLA ITEM_FACTURA'
GO
---------------------------------------------------------------------
--------------------ITEM_FACTURA-------------------------------------
----------------------179238-----------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Item_Factura](
	[ITEM_FACTURA] [int] FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.FACTURA(FACTURA_NRO),
	[ITEM_PRODUCTO] [nvarchar](100) FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.PRODUCTO(PRODUCTO_CODIGO),
	[ITEM_PRECIO] [float] NULL,
	[ITEM_CANT] [INT] NULL,
	PRIMARY KEY(ITEM_FACTURA,ITEM_PRODUCTO)
)
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[Item_Factura]([ITEM_FACTURA],[ITEM_PRODUCTO],[ITEM_PRECIO],[ITEM_CANT])
SELECT [FACTURA_NRO],SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10) AS "PRODUCTO_CODIGO",PRODUCTO_PRECIO,COUNT (*)
FROM GD_ESQUEMA.MAESTRA
WHERE PRODUCTO_NOMBRE IS NOT NULL AND CLI_DNI IS NOT NULL
GROUP BY FACTURA_NRO, PRODUCTO_NOMBRE,PRODUCTO_PRECIO
GO

print 'TABLA MOVS_STOCK'
GO
---------------------------------------------------------------------
--------------------MOVS_STOCK---------------------------------------
----------------------353662-----------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Movs_Stock](
	[MOVS_CODIGO] INT PRIMARY KEY IDENTITY,
	[MOVS_PRODUCTO] [nvarchar](100)  FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.PRODUCTO(PRODUCTO_CODIGO),
	[MOVS_FACTURA] [int] FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.FACTURA(FACTURA_NRO),
	[MOVS_SUCURSAL] INT FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.SUCURSAL(SUC_CODIGO),
	[MOVS_TIPO] [nvarchar](100) NULL,
	[MOVS_FECHA] [datetime] NULL,
	[MOVS_CANT] [INT] NULL,
)
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[MOVS_STOCK]([MOVS_PRODUCTO],[MOVS_FACTURA],[MOVS_SUCURSAL],[MOVS_TIPO],[MOVS_FECHA],[MOVS_CANT])
(SELECT SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10), NULL, S.SUC_CODIGO, 'Llegada',LLEGADA_STOCK_FECHA,LLEGADA_STOCK_CANT
FROM GD_ESQUEMA.MAESTRA M JOIN MAYUSCULAS_SIN_ESPACIOS.SUCURSAL S ON (M.[SUC_DIR] = S.SUC_DIR)
WHERE LLEGADA_STOCK_FECHA IS NOT NULL)
UNION
(SELECT SUBSTRING(PRODUCTO_NOMBRE,LEN(PRODUCTO_NOMBRE)-9,10),[FACTURA_NRO],
		 S.SUC_CODIGO, 'Venta',FACTURA_FECHA,COUNT(*)
FROM GD_ESQUEMA.MAESTRA M JOIN MAYUSCULAS_SIN_ESPACIOS.SUCURSAL S ON (M.[SUC_DIR] = S.SUC_DIR)	
WHERE PRODUCTO_NOMBRE IS NOT NULL AND CLI_DNI IS NOT NULL
GROUP BY FACTURA_NRO, PRODUCTO_NOMBRE,PRODUCTO_PRECIO,FACTURA_FECHA,S.SUC_CODIGO)
GO

print 'TABLA STOCK '
GO
---------------------------------------------------------------------
-------------------------STOCK---------------------------------------
--------------------------2376---------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Stock](
	[STOCK_PRODUCTO] [nvarchar](100)  FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.PRODUCTO(PRODUCTO_CODIGO),
	[STOCK_SUCURSAL] INT FOREIGN KEY 
			REFERENCES MAYUSCULAS_SIN_ESPACIOS.SUCURSAL(SUC_CODIGO),
	[STOCK_CANT] [INT] NULL,
	PRIMARY KEY([STOCK_PRODUCTO],[STOCK_SUCURSAL])
)
GO

INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].[STOCK]([STOCK_PRODUCTO],[STOCK_SUCURSAL],[STOCK_CANT])
SELECT  MOVS_PRODUCTO,MOVS_SUCURSAL,(
		SELECT SUM(MOVS_CANT) 
		FROM MAYUSCULAS_SIN_ESPACIOS.MOVS_STOCK MS2 
		WHERE  MS.MOVS_PRODUCTO=MS2.MOVS_PRODUCTO AND MS.MOVS_SUCURSAL=MS2.MOVS_SUCURSAL AND MOVS_TIPO='Llegada'
		GROUP BY MOVS_PRODUCTO,MOVS_SUCURSAL)-(SELECT ISNULL((
		SELECT SUM(MOVS_CANT)
 		FROM MAYUSCULAS_SIN_ESPACIOS.MOVS_STOCK MS2 
		WHERE MS.MOVS_PRODUCTO=MS2.MOVS_PRODUCTO AND MS.MOVS_SUCURSAL=MS2.MOVS_SUCURSAL AND MOVS_TIPO='Venta'
		GROUP BY MOVS_PRODUCTO,MOVS_SUCURSAL),'0'))
FROM MAYUSCULAS_SIN_ESPACIOS.MOVS_STOCK MS
GROUP BY MOVS_PRODUCTO,MOVS_SUCURSAL
GO

print ' TABLA CATEGORIA '
GO
---------------------------------------------------------------------
--------------------CATEGORIA----------------------------------------
-----------------------81--------------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Categoria](
	[CATE_CODIGO] INT IDENTITY PRIMARY KEY,
	[CATE_NOMBRE] [nvarchar](100) NULL,
	[CATE_PADRE] INT NULL
)
GO

print ' FUNCION PARSE '
GO
---------------------------------------------------------------------
------------------------FUNCTION-PARSE-------------------------------
---------------------------------------------------------------------
CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[PARSE](@CateList varchar(8000))
AS
BEGIN
	DECLARE @Delimeter varchar(1)
	SET @Delimeter = '¦'
	DECLARE @Cate varchar(50)
	DECLARE @CatePadre varchar(50)
	SET @CatePadre = ''
	DECLARE @CatePadreCod INT
	SET @CatePadreCod = '0'
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
		SET @CatePadreCod = (SELECT TOP (1) CATE_CODIGO FROM [MAYUSCULAS_SIN_ESPACIOS].Categoria WHERE @CATEPADRE = CATE_NOMBRE)
		if not @Cate in (select cate_nombre from [MAYUSCULAS_SIN_ESPACIOS].Categoria WHERE (@CATEPADRECOD = CATE_PADRE OR CATE_PADRE IS NULL) AND @CATE <> @CATEPADRE)
		  BEGIN	
			INSERT INTO [MAYUSCULAS_SIN_ESPACIOS].Categoria(Cate_NOMBRE,Cate_Padre) VALUES(@Cate,@CatePadreCod)				
		  END
		SET @CatePadre = @Cate
	  END
	RETURN 
END
GO

print ' CARGA TABLA CATEGORIA '
GO
---------------------------------------------------------------------
--------------------CATEGORIA----------------------------------------
---------------------------------------------------------------------

declare @au_id nvarchar(100)
select @au_id =  min( producto_cate ) from [MAYUSCULAS_SIN_ESPACIOS].producto
while @au_id is not null
begin
	EXECUTE [MAYUSCULAS_SIN_ESPACIOS].[PARSE] @au_id    
	select @au_id = min( producto_cate ) from [MAYUSCULAS_SIN_ESPACIOS].producto where producto_cate > @au_id
end
GO

print ' TABLA USUARIOS'
GO
---------------------------------------------------------------------
--------------------USUARIOS----------------------------------------
---------------------------------------------------------------------

CREATE TABLE MAYUSCULAS_SIN_ESPACIOS.USUARIOS(
		US_EMPLEADO NUMERIC(8,0) NULL,
		US_USERNAME NVARCHAR(20) PRIMARY KEY ,
		US_PASSWORD NVARCHAR(65) NULL,
		US_INTENTOS INT NULL,
		US_ROLES NVARCHAR(100) NULL
)
GO

-- User: 'admin'; Pass: 'w23e'
INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[USUARIOS]([US_EMPLEADO],[US_USERNAME],
						[US_PASSWORD],[US_INTENTOS],[US_ROLES])
VALUES(NULL,'admin','E6B87050BFCB8143FCB8DB0170A4DC9ED00D904DDD3E2A4AD1B1E8DC0FDC9BE7',0,'Administrador General');

-- User: 'prueba'; Pass: 'prueba'
INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[USUARIOS]([US_EMPLEADO],[US_USERNAME],
						[US_PASSWORD],[US_INTENTOS],[US_ROLES])
VALUES(NULL,'prueba','655E786674D9D3E77BC05ED1DE37B4B6BC89F788829F9F3C679E7687B410C89B',0,'ABM');

-- User: 'otro'; Pass: 'otro'
INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[USUARIOS]([US_EMPLEADO],[US_USERNAME],
						[US_PASSWORD],[US_INTENTOS],[US_ROLES])
VALUES(NULL,'otro','C63A855FCFEC62CC64A47280A4F51D052B67930873906AF30AA92DE2CE160C06',0,'Otros');

GO

print ' StockProcedure sp_MODIFINTENTOS'
GO
---------------------------------------------------------------------
--------------------PROCEDURE-MODIF-INTENTOS-------------------------
---------------------------------------------------------------------

CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].sp_MODIFINTENTOS (	@USERNAME NVARCHAR(20),
								@INTENTOS INT)
AS
BEGIN
	UPDATE 	[MAYUSCULAS_SIN_ESPACIOS].USUARIOS
	SET US_INTENTOS = @INTENTOS
	WHERE US_USERNAME = @USERNAME
END
GO

print ' TABLA ROLES Y FUNCIONES'
GO
---------------------------------------------------------------------
-----------------------ROLES-Y-FUNCIONES-----------------------------
---------------------------------------------------------------------

CREATE TABLE MAYUSCULAS_SIN_ESPACIOS.ROLES(
		ROL_NOMBRE NVARCHAR(50) NULL,
		ROL_FUNCION NVARCHAR(20) NULL
)
GO

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','ABM de Empleado')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','ABM de Rol')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','ABM de Usuario')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','ABM de Cliente')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','ABM de Producto')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','Asignacion de stock')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','Facturacion')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','Efectuar Pago')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','Tablero de Control')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','Clientes Premium')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Administrador General','Mejores Categorias')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('ABM','ABM de Empleado')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('ABM','ABM de Rol')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('ABM','ABM de Usuario')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('ABM','ABM de Cliente')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('ABM','ABM de Producto')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Otros','Asignacion de stock')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Otros','Facturacion')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Otros','Efectuar Pago')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Otros','Tablero de Control')

INSERT INTO [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[ROLES]([ROL_NOMBRE],[ROL_FUNCION])
VALUES('Otros','Clientes Premium')

GO
