USE [HiringTracker]
GO
/****** Object:  UserDefinedTableType [dbo].[TYPE_Position_Skills]    Script Date: 8/21/2021 12:27:47 PM ******/
CREATE TYPE [dbo].[TYPE_Position_Skills] AS TABLE(
	[SkillID] [bigint] NOT NULL,
	[IsMandatory] [bit] NOT NULL,
	[ProficiencyID] [bigint] NOT NULL
)
GO
