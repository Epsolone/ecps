USE [ecps_base]
GO
/****** 对象:  Table [dbo].[SystemSetting]    脚本日期: 04/13/2010 01:31:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemSetting](
	[SystemSettingID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[ValueType] [int] NOT NULL,
 CONSTRAINT [PK_SystemSetting] PRIMARY KEY CLUSTERED 
(
	[SystemSettingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[PortalSetting]    脚本日期: 04/13/2010 01:31:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortalSetting](
	[PortalSettingID] [int] IDENTITY(1,1) NOT NULL,
	[PortalID] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[ValueType] [int] NOT NULL,
 CONSTRAINT [PK_PortalSetting] PRIMARY KEY CLUSTERED 
(
	[PortalSettingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[PortalAlias]    脚本日期: 04/13/2010 01:31:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortalAlias](
	[PortalAliasID] [int] IDENTITY(1,1) NOT NULL,
	[PortalID] [int] NOT NULL,
	[Host] [nvarchar](256) NOT NULL,
	[Port] [int] NOT NULL CONSTRAINT [DF_PortalAlias_Port]  DEFAULT ((80)),
	[IsSsl] [bit] NOT NULL,
	[Protocol] [nvarchar](16) NOT NULL CONSTRAINT [DF_PortalAlias_Protocol]  DEFAULT ('http'),
	[AreaName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_PortalAlias] PRIMARY KEY CLUSTERED 
(
	[PortalAliasID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Portal]    脚本日期: 04/13/2010 01:31:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portal](
	[PortalID] [int] IDENTITY(1,1) NOT NULL,
	[PortalName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[DefaultAliasID] [int] NULL,
	[DatabaseConnectionString] [nvarchar](256) NULL,
 CONSTRAINT [PK_Portal] PRIMARY KEY CLUSTERED 
(
	[PortalID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  ForeignKey [FK_Portal_PortalAlias]    脚本日期: 04/13/2010 01:31:05 ******/
ALTER TABLE [dbo].[Portal]  WITH CHECK ADD  CONSTRAINT [FK_Portal_PortalAlias] FOREIGN KEY([DefaultAliasID])
REFERENCES [dbo].[PortalAlias] ([PortalAliasID])
GO
ALTER TABLE [dbo].[Portal] CHECK CONSTRAINT [FK_Portal_PortalAlias]
GO
/****** 对象:  ForeignKey [FK_PortalAlias_Portal]    脚本日期: 04/13/2010 01:31:05 ******/
ALTER TABLE [dbo].[PortalAlias]  WITH CHECK ADD  CONSTRAINT [FK_PortalAlias_Portal] FOREIGN KEY([PortalID])
REFERENCES [dbo].[Portal] ([PortalID])
GO
ALTER TABLE [dbo].[PortalAlias] CHECK CONSTRAINT [FK_PortalAlias_Portal]
GO
/****** 对象:  ForeignKey [FK_PortalSetting_Portal]    脚本日期: 04/13/2010 01:31:05 ******/
ALTER TABLE [dbo].[PortalSetting]  WITH CHECK ADD  CONSTRAINT [FK_PortalSetting_Portal] FOREIGN KEY([PortalID])
REFERENCES [dbo].[Portal] ([PortalID])
GO
ALTER TABLE [dbo].[PortalSetting] CHECK CONSTRAINT [FK_PortalSetting_Portal]
GO
