USE [ShopApp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Shops](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Slug] [nvarchar](250) NOT NULL,
	[DateOpened] [datetime2](7) NULL
) ON [PRIMARY]
GO