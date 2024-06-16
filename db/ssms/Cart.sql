USE [FullstackDB]
GO

/****** Object:  Table [dbo].[Cart]    Script Date: 6/16/2024 12:20:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cart](
	[CustomerId] [int] NOT NULL,
	[ProductId] [int] NOT NULL
) ON [PRIMARY]
GO


