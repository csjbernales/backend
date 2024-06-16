USE [FullstackDB]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 6/16/2024 12:21:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL
) ON [PRIMARY]
GO


