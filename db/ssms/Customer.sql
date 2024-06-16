USE [FullstackDB]
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 6/16/2024 12:21:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [varchar](50) NOT NULL,
	[Middlename] [varchar](50) NULL,
	[Lastname] [varchar](50) NULL,
	[Age] [int] NULL,
	[Sex] [char](1) NOT NULL,
	[IsEmployed] [bit] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


