USE [HiringTracker]
GO
/****** Object:  UserDefinedTableType [dbo].[TYPE_Candidate_Property]    Script Date: 8/21/2021 12:27:47 PM ******/
CREATE TYPE [dbo].[TYPE_Candidate_Property] AS TABLE(
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](1000) NULL
)
GO
