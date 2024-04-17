CREATE TABLE [dbo].[Roles](
	[ID] [bigint] NULL,
	[Name] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT [dbo].[Roles] ([ID], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([ID], [Name]) VALUES (2, N'User')
GO