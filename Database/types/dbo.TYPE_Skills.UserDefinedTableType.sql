USE [HiringTracker]
GO
/****** Object:  UserDefinedTableType [dbo].[TYPE_Skills]    Script Date: 8/21/2021 12:27:47 PM ******/
CREATE TYPE [dbo].[TYPE_Skills] AS TABLE(
	[SkillID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[SkillID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
