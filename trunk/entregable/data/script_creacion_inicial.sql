-- Seteo la DB
USE [GD1C2011]
GO

-- Genero el Schema
/**CREATE SCHEMA [MAYUSCULAS_SIN_ESPACIOS] AUTHORIZATION [gd]
GO*/

/**
 *
 * Chequeo la existencia del schema antes de crearlo. Deberia pensar en eliminar las tablas y otros objetos tambien.
 *
 */
if not exists(select 1 from information_schema.schemata where
schema_name='MAYUSCULAS_SIN_ESPACIOS')
EXEC ('create schema MAYUSCULAS_SIN_ESPACIOS AUTHORIZATION gd');
go


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


---------------------------------------------------------------------
--------------------CATEGORIA----------------------------------------
-----------------------81--------------------------------------------
CREATE TABLE [MAYUSCULAS_SIN_ESPACIOS].[Categoria](
	[CATE_CODIGO] INT IDENTITY PRIMARY KEY,
	[CATE_NOMBRE] [nvarchar](100) NULL,
	[CATE_PADRE] INT NULL
)
GO
---------------------------------------------------------------------
------------------------FUNCTION-PARSE-------------------------------
---------------------------------------------------------------------
CREATE PROCEDURE [MAYUSCULAS_SIN_ESPACIOS].[PARSE](@CateList varchar(8000))
AS
BEGIN
	DECLARE @Delimeter varchar(1)
	SET @Delimeter = '�'
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

---------------------------------------------------------------------
--------------------USUARIOS----------------------------------------
---------------------------------------------------------------------

CREATE TABLE MAYUSCULAS_SIN_ESPACIOS.USUARIOS(
		US_EMPLEADO NUMERIC(8,0) NULL,
		US_USERNAME NVARCHAR(20) PRIMARY KEY ,
		US_PASSWORD NVARCHAR(50) NULL,
		US_INTENTOS INT
)
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

---------------------------------------------------------------------
----------------------ROLESS-Y-FUNCIONES-----------------------------
---------------------------------------------------------------------

CREATE TABLE MAYUSCULAS_SIN_ESPACIOS.ROLES(
		ROL_NOMBRE NVARCHAR(50) NULL,
		ROL_FUNCION NVARCHAR(20) NULL
)
GO