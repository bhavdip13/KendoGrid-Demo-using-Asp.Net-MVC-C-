USE [master]
GO
/****** Object:  Database [PMS]    Script Date: 2020-06-25 8.52.09 AM ******/
CREATE DATABASE [PMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PMS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PMS] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [PMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PMS] SET RECOVERY FULL 
GO
ALTER DATABASE [PMS] SET  MULTI_USER 
GO
ALTER DATABASE [PMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PMS] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PMS', N'ON'
GO
ALTER DATABASE [PMS] SET QUERY_STORE = OFF
GO
USE [PMS]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 2020-06-25 8.52.10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mst_Product]    Script Date: 2020-06-25 8.52.11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_Product](
	[PID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [float] NULL,
	[CreationDate] [datetime] NULL,
 CONSTRAINT [PK_Mst_Product] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Menus] ON 

INSERT [dbo].[Menus] ([Id], [Name]) VALUES (1, N'Home')
INSERT [dbo].[Menus] ([Id], [Name]) VALUES (2, N'About')
INSERT [dbo].[Menus] ([Id], [Name]) VALUES (3, N'Contact')
SET IDENTITY_INSERT [dbo].[Menus] OFF
SET IDENTITY_INSERT [dbo].[Mst_Product] ON 

INSERT [dbo].[Mst_Product] ([PID], [Name], [Description], [Price], [CreationDate]) VALUES (1, N'Mobile', N'iphone 10', 59999, CAST(N'2020-06-17T18:14:30.117' AS DateTime))
INSERT [dbo].[Mst_Product] ([PID], [Name], [Description], [Price], [CreationDate]) VALUES (2, N'Laptop HP', N'hp pavalion', 49999, CAST(N'2020-06-17T19:27:03.293' AS DateTime))
INSERT [dbo].[Mst_Product] ([PID], [Name], [Description], [Price], [CreationDate]) VALUES (3, N'Laptop', N'HO', 49900, CAST(N'2020-06-25T08:33:13.467' AS DateTime))
INSERT [dbo].[Mst_Product] ([PID], [Name], [Description], [Price], [CreationDate]) VALUES (4, N'Washing machine Samsung', N'Samsung washing machine front load', 25000, CAST(N'2020-06-25T08:33:51.730' AS DateTime))
INSERT [dbo].[Mst_Product] ([PID], [Name], [Description], [Price], [CreationDate]) VALUES (5, N'Fridge ', N'Panasonic ', 20000, CAST(N'2020-06-25T08:34:16.703' AS DateTime))
INSERT [dbo].[Mst_Product] ([PID], [Name], [Description], [Price], [CreationDate]) VALUES (6, N'Computer PC', N'Samsung Monitor & HP CPU', 29000, CAST(N'2020-06-25T08:35:16.420' AS DateTime))
SET IDENTITY_INSERT [dbo].[Mst_Product] OFF
ALTER TABLE [dbo].[Mst_Product] ADD  CONSTRAINT [DF_Mst_Product_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_PRODUCTS]    Script Date: 2020-06-25 8.52.11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bhavdip Talaviya
-- =============================================
CREATE PROCEDURE [dbo].[SP_SELECT_PRODUCTS]

AS
BEGIN
	
	SET NOCOUNT ON;
	
SELECT CONVERT(varchar,PID) as PID
      ,[Name]
      ,[Description]
      ,CONVERT(varchar,Price) as [Price]
      , CONVERT(varchar, CreationDate, 101) as CreationDate
  FROM [dbo].[Mst_Product]

  SELECT count(*) as Total
  FROM [dbo].[Mst_Product]

END
GO
USE [master]
GO
ALTER DATABASE [PMS] SET  READ_WRITE 
GO
