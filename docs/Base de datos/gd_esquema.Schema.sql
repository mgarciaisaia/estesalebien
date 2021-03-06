USE [GD1C2011]
GO
/****** Object:  Schema [gd_esquema]    Script Date: 04/04/2011 02:07:49 ******/
CREATE SCHEMA [gd_esquema] AUTHORIZATION [gd]
GO

SET NOCOUNT ON
GO

/****** Object:  Table [gd_esquema].[Maestra]    Script Date: 04/05/2011 00:35:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [gd_esquema].[Maestra](
	[CLI_DNI] [nvarchar](255) NULL,
	[CLI_NOMBRE] [nvarchar](255) NULL,
	[CLI_APELLIDO] [nvarchar](255) NULL,
	[CLI_MAIL] [nvarchar](255) NULL,
	[SUC_DIR] [nvarchar](255) NULL,
	[SUC_TEL] [nvarchar](255) NULL,
	[SUC_TIPO] [nvarchar](255) NULL,
	[SUC_PROVINCIA] [nvarchar](255) NULL,
	[EMPLEADO_NOMBRE] [nvarchar](255) NULL,
	[EMPLEADO_APELLIDO] [nvarchar](255) NULL,
	[EMPLEADO_DNI] [numeric](8, 0) NULL,
	[EMPLEADO_MAIL] [nvarchar](255) NULL,
	[EMPLEADO_DIR] [nvarchar](255) NULL,
	[EMPLEADO_TIPO] [nvarchar](100) NULL,
	[EMPLEADO_PROVINCIA] [nvarchar](255) NULL,
	[FACTURA_NRO] [int] NULL,
	[FACTURA_DESCUENTO] [float] NULL,
	[FACTURA_TOTAL] [float] NULL,
	[FACTURA_TOTAL_DESCU] [float] NULL,
	[FACTURA_FECHA] [datetime] NULL,
	[FACTURA_CANT_COUTAS] [int] NULL,
	[PAGO_FECHA] [datetime] NULL,
	[PAGO_MONTO] [float] NULL,
	[PAGO_EMPLEADO_NOMBRE] [nvarchar](100) NULL,
	[PAGO_EMPLEADO_APELLIDO] [nvarchar](100) NULL,
	[PAGO_EMPLEADO_DNI] [numeric](8, 0) NULL,
	[PAGO_EMPLEADO_MAIL] [nvarchar](100) NULL,
	[PAGO_EMPLEADO_DIR] [nvarchar](100) NULL,
	[PAGO_EMPLEADO_TIPO] [nvarchar](100) NULL,
	[PAGO_EMPLEADO_PROVINCIA] [nvarchar](255) NULL,
	[PRODUCTO_NOMBRE] [nvarchar](100) NULL,
	[PRODUCTO_DESC] [nvarchar](100) NULL,
	[PRODUCTO_PRECIO] [float] NULL,
	[PRODUCTO_MARCA] [nvarchar](100) NULL,
	[PRODUCTO_CATE] [nvarchar](100) NULL,
	[PRODUCTO_CANT] [int] NULL,
	[LLEGADA_STOCK_FECHA] [datetime] NULL,
	[LLEGADA_STOCK_CANT] [int] NULL
) ON [PRIMARY]
GO


