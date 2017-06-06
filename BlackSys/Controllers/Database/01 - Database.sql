PRINT 'INCIANDO EL SCRIPT....'
PRINT 'CREANDO BASE DE DATOS...'
USE master;
GO
IF EXISTS (SELECT * FROM sysdatabases WHERE name='RED_DB')
BEGIN
	ALTER DATABASE RED_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE
END
GO
USE master;
IF EXISTS(SELECT * FROM sysdatabases where name='RED_DB')
BEGIN
	DROP DATABASE RED_DB	
END
GO
CREATE DATABASE RED_DB;
GO
USE [RED_DB];
GO

PRINT 'LA BASE DE DATOS SE CREO CORRECTAMENTE.';
PRINT 'CREANDO TABLAS...';
IF EXISTS(SELECT * FROM sysobjects where name='Permission')
BEGIN
	DROP TABLE Permission
END
ELSE
BEGIN
	CREATE TABLE Permission
	(
		PermissionID INT IDENTITY(1,1) NOT NULL,
		RoleID NVARCHAR(128) NOT NULL,
		MenuID INT NOT NULL,
		CONSTRAINT PK_Permission_PermissionID PRIMARY KEY (PermissionID)		
	)
END
GO

IF EXISTS(SELECT * FROM sysobjects where name='CustomPermission')
BEGIN
	DROP TABLE CustomPermission
END
ELSE
BEGIN
	CREATE TABLE CustomPermission
	(
		CustomPermissionID INT IDENTITY(1,1) NOT NULL,
		UserID NVARCHAR(128) NOT NULL,
		MenuID INT NOT NULL,
		CONSTRAINT PK_CustomPermission_CustomPermissionID PRIMARY KEY (CustomPermissionID)		
	)
END
GO

IF EXISTS(SELECT * FROM sysobjects where name='Menu')
BEGIN
	DROP TABLE Menu
END
ELSE
BEGIN
	CREATE TABLE Menu
	(
		MenuID INT IDENTITY(1,1) NOT NULL,
		DisplayName NVARCHAR(50) NOT NULL,
		ParentMenuID INT NOT NULL,
		OrderNumber INT NOT NULL,
		MenuURL NVARCHAR(100) NULL,
		MenuIcon NVARCHAR(25) NULL,
		CONSTRAINT PK_Menu_MenuID PRIMARY KEY (MenuID)
	)
END
GO

IF EXISTS(SELECT * FROM sysobjects where name='MenuTemp')
BEGIN
	DROP TABLE MenuTemp
END
ELSE
BEGIN
	CREATE TABLE MenuTemp
	(
		MenuID INT NOT NULL,
		DisplayName NVARCHAR(50) NOT NULL,
		ParentMenuID INT NOT NULL,
		OrderNumber INT NOT NULL,
		MenuURL NVARCHAR(100) NULL,
		MenuIcon NVARCHAR(25) NULL	
		
	)
END
GO

IF EXISTS(SELECT * FROM sysobjects where name='UnitMeasure')
BEGIN
	DROP TABLE UnitMeasure
END
ELSE
BEGIN
CREATE TABLE UnitMeasure(
    UomID INT IDENTITY (1,1) NOT NULL,
    Code NVARCHAR (5) NOT NULL, 
    Name NVARCHAR(50) NOT NULL, 
    [ModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_UnitMeasure_ModifiedDate] DEFAULT (GETDATE()),
	CONSTRAINT [PK_UnitMeasure_UomID] PRIMARY KEY (UomID),
	CONSTRAINT [UK_UnitMeasure_Code] UNIQUE (Code)
) ON [PRIMARY];
END
GO

IF EXISTS(SELECT * FROM sysobjects where name='Colors')
BEGIN
	DROP TABLE Colors
END
ELSE
BEGIN
CREATE TABLE Colors(
    ColorID INT IDENTITY (1,1) NOT NULL,    
    Name NVARCHAR(25) NOT NULL, 
    [ModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_Colors_ModifiedDate] DEFAULT (GETDATE()),
	CONSTRAINT [PK_Colors_ColorID] PRIMARY KEY (ColorID),
	CONSTRAINT [UK_Colors_Name] UNIQUE (Name)
) ON [PRIMARY];
END
GO

IF EXISTS(SELECT * FROM sysobjects where name='ComponentCategory')
BEGIN
	DROP TABLE ComponentCategory
END
ELSE
BEGIN
CREATE TABLE [ComponentCategory](
    [ComponentCategoryID] [int] IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(200) NOT NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](128) NULL,	
    [rowguid] uniqueidentifier ROWGUIDCOL NOT NULL CONSTRAINT [DF_ComponentCategory_rowguid] DEFAULT (NEWID()),     
	CONSTRAINT [PK_ComponentCategory_ComponentCategoryID] PRIMARY KEY (ComponentCategoryID),
	CONSTRAINT [UK_ComponentCategory_Name] UNIQUE (Name)
) ON [PRIMARY];
END
GO


IF EXISTS(SELECT * FROM sysobjects where name='Component')
BEGIN
	DROP TABLE Component
END
ELSE
BEGIN
CREATE TABLE Component(
    ComponentID INT IDENTITY(1,1) NOT NULL,
    Code NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(200) NOT NULL,
    ComponentCategoryID INT NOT NULL,
    UomID INT NOT NULL,
    CustomerID INT NULL,
    CustomerCode NVARCHAR(50) NULL,
    CountryOfOrigin NVARCHAR(100) NULL,
    SupplierID INT NULL,
    SupplierCode NVARCHAR(50) NULL,
    Description2 NVARCHAR(200) NULL,
    PurchasingPrice MONEY NOT NULL,
    PurchasingTerms NVARCHAR (50) NULL,
	ListPrice MONEY NOT NULL,
    HtsID INT NULL,
    SacID INT NULL,	
	IsActive BIT DEFAULT 1 NOT NULL,
	[DiscontinuedDate] [datetime] NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](128) NULL,
    [rowguid] uniqueidentifier ROWGUIDCOL NOT NULL CONSTRAINT [DF_Component_rowguid] DEFAULT (NEWID())
	CONSTRAINT [PK_Component_ComponentID] PRIMARY KEY (ComponentID),
	CONSTRAINT [UK_Component_Code] UNIQUE (Code, CustomerID),	
	CONSTRAINT [CK_Component_ListPrice] CHECK ([ListPrice] >= 0.00),
	CONSTRAINT [CK_Component_PurchasePrice] CHECK ([PurchasingPrice] >= 0.00)
)
END
GO


IF EXISTS(SELECT * FROM sysobjects where name='Fabric')
BEGIN
	DROP TABLE Fabric
END
ELSE
BEGIN
CREATE TABLE Fabric(
    FabricID INT IDENTITY(1,1) NOT NULL,
    Code NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(200) NOT NULL,
    CategoryID INT NOT NULL,
    UnitOfMeasurementID INT NOT NULL,
    CustomerID INT NULL,
    CustomerCode NVARCHAR(50) NULL,
    CountryOfOrigin NVARCHAR(100) NULL,
    SupplierID INT NULL,
    SupplierCode NVARCHAR(50) NULL,	
    Description2 NVARCHAR(200) NULL,
	Width NVARCHAR (15) NULL,
	UnitWeight NVARCHAR(5) NULL,
	Weight NVARCHAR (10) NULL,
	Blend NVARCHAR (150) NULL,
	Construction NVARCHAR (200) NULL,
	CutDirection NVARCHAR (150) NULL,
	[FabricType] NVARCHAR (15) NULL,
    PurchasingPrice MONEY NOT NULL,
    PurchasingTerms NVARCHAR (50) NULL,
    HtsID INT NULL,
    SacID INT NULL,
	IsActive BIT DEFAULT 1 NOT NULL,
	[DiscontinuedDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](128) NULL,
    [rowguid] uniqueidentifier ROWGUIDCOL NOT NULL CONSTRAINT [DF_Fabric_rowguid] DEFAULT (NEWID()) 
)
END
GO

CREATE TABLE [FabricCategory](
    [FabricCategoryID] [INT] IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(200) NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](128) NULL,
    [rowguid] uniqueidentifier ROWGUIDCOL NOT NULL CONSTRAINT [DF_FabricCategory_rowguid] DEFAULT (NEWID())
) ON [PRIMARY];
GO

CREATE TABLE [Product](
    [ProductID] [int] IDENTITY (1, 1) NOT NULL,
    [Style] NVARCHAR(50) NOT NULL,
    [ProductNumber] [nvarchar](25) NOT NULL, 
    [MakeFlag] BIT NOT NULL CONSTRAINT [DF_Product_MakeFlag] DEFAULT (1),
    [FinishedGoodsFlag] BIT NOT NULL CONSTRAINT [DF_Product_FinishedGoodsFlag] DEFAULT (1),
    [Color] [NVARCHAR](25) NULL, 
    [SafetyStockLevel] [SMALLINT] NOT NULL,    
    [StandardCost] [MONEY] NOT NULL,
    [ListPrice] [MONEY] NOT NULL,        
    [WeightUnitMeasureCode] [nchar](5) NULL, 
    [Weight] [decimal](8, 2) NULL,
    [DaysToManufacture] [INT] NOT NULL,
    [ProductLineID] [INT] NULL, 
    [ProductCategory] INT NULL,             
    [SellStartDate] [datetime] NOT NULL,
    [SellEndDate] [datetime] NULL,
	IsActive BIT DEFAULT 1 NOT NULL,
    [DiscontinuedDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](128) NULL,
    [rowguid] uniqueidentifier ROWGUIDCOL NOT NULL CONSTRAINT [DF_Product_rowguid] DEFAULT (NEWID()), 	
    CONSTRAINT [CK_Product_SafetyStockLevel] CHECK ([SafetyStockLevel] > 0),    
    CONSTRAINT [CK_Product_StandardCost] CHECK ([StandardCost] >= 0.00),
    CONSTRAINT [CK_Product_ListPrice] CHECK ([ListPrice] >= 0.00),
    CONSTRAINT [CK_Product_Weight] CHECK ([Weight] > 0.00),
    CONSTRAINT [CK_Product_DaysToManufacture] CHECK ([DaysToManufacture] >= 0),
    CONSTRAINT [CK_Product_SellEndDate] CHECK (([SellEndDate] >= [SellStartDate]) OR ([SellEndDate] IS NULL)),
) ON [PRIMARY];
GO

CREATE TABLE [ProductCategory](
    [ProductCategoryID] [int] IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(200) NOT NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](128) NULL,
    [rowguid] uniqueidentifier ROWGUIDCOL NOT NULL CONSTRAINT [DF_ProductCategory_rowguid] DEFAULT (NEWID()),     
) ON [PRIMARY];
GO

CREATE TABLE [ProductPhoto](
    [ProductPhotoID] [int] IDENTITY (1, 1) NOT NULL,
    [ThumbNailPhoto] [varbinary](max) NULL,
    [ThumbnailPhotoFileName] [nvarchar](50) NULL,
    [LargePhoto] [varbinary](max) NULL,
    [LargePhotoFileName] [nvarchar](50) NULL, 
    [ModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_ProductPhoto_ModifiedDate] DEFAULT (GETDATE()) 
) ON [PRIMARY];
GO

CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY (1, 1) NOT NULL,		
    [rowguid] uniqueidentifier ROWGUIDCOL NOT NULL CONSTRAINT [DF_Customer_rowguid] DEFAULT (NEWID()), 
    [Name] NVARCHAR(25) NOT NULL, 
	[Code] INT NULL, 
	[CompanyName] NVARCHAR(125) NOT NULL, 
	[NRC] NVARCHAR(25) NULL, 
	[NIT] NVARCHAR(18) NULL, 
	[CreationDate] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](128) NULL,
	CONSTRAINT [PK_Customer_CustomerID] PRIMARY KEY (CustomerID)
) ON [PRIMARY];
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierID] [INT] IDENTITY (1, 1) NOT NULL,		
    [rowguid] uniqueidentifier ROWGUIDCOL NOT NULL CONSTRAINT [DF_Supplier_rowguid] DEFAULT (NEWID()), 
    [Name] NVARCHAR(25) NOT NULL, 
	[Code] INT NULL, 
	[CompanyName] NVARCHAR(125) NOT NULL, 
	[NRC] NVARCHAR(25) NULL, 
	[NIT] NVARCHAR(18) NULL, 
	[CreationDate] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](128) NULL,
	CONSTRAINT [PK_Supplier_SupplierID] PRIMARY KEY (SupplierID)
) ON [PRIMARY];
GO


PRINT 'FINALIZANDO SCRIPT...'