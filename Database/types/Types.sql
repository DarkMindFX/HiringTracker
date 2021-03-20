USE [HiringTracker]
GO
/****** Object:  UserDefinedTableType [dbo].[TYPE_Candidate_Property]    Script Date: 3/20/2021 11:11:58 AM ******/
CREATE TYPE [dbo].[TYPE_Candidate_Property] AS TABLE(
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](1000) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TYPE_Position_Skills]    Script Date: 3/20/2021 11:11:58 AM ******/
CREATE TYPE [dbo].[TYPE_Position_Skills] AS TABLE(
	[SkillID] [bigint] NOT NULL,
	[IsMandatory] [bit] NOT NULL,
	[ProficiencyID] [bigint] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TYPE_Skills]    Script Date: 3/20/2021 11:11:58 AM ******/
CREATE TYPE [dbo].[TYPE_Skills] AS TABLE(
	[SkillID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[SkillID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
