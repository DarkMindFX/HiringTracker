USE [SACHiringTracker]
GO
/****** Object:  UserDefinedTableType [dbo].[TYPE_Position_Skills]    Script Date: 3/14/2021 11:46:00 AM ******/
CREATE TYPE [dbo].[TYPE_Position_Skills] AS TABLE(
	[SkillID] [bigint] NOT NULL,
	[IsMandatory] [bit] NOT NULL,
	[ProficiencyID] [bigint] NOT NULL
)
GO
