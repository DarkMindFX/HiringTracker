USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_CandidateProperty]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_CandidateProperty]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testProperties AS TABLE (
	[ID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](1000) NULL,
	[CandidateID] [bigint] NOT NULL)

	DECLARE @candidateId1 BIGINT = 100001
	DECLARE @candidateId2 BIGINT = 100002
	DECLARE @candidateId3 BIGINT = 100003
	DECLARE @candidateId4 BIGINT = 100004
	DECLARE @candidateId5 BIGINT = 100005
	DECLARE @candidateId6 BIGINT = 100006
	DECLARE @candidateId7 BIGINT = 100007
	DECLARE @candidateId8 BIGINT = 100008
	DECLARE @candidateId9 BIGINT = 100009

	INSERT INTO @testProperties
	SELECT 100001, 'Notice Period', '3 months', @candidateId1 UNION
	SELECT 100002, 'Salary Expectations', '120K/year', @candidateId1 UNION
	SELECT 100003, 'Notice Period', '1 month', @candidateId2 UNION
	SELECT 100004, 'Preferable work location', 'Remote', @candidateId2 UNION
	SELECT 100005, 'Preferable role', 'Tech Lead', @candidateId2 UNION
	SELECT 100006, 'Type of work', 'No work with legacy code', @candidateId3 UNION
	SELECT 100007, 'Salary Expectations', '180/h', @candidateId4 UNION
	SELECT 100008, 'Availability', '1 month', @candidateId4 UNION
	SELECT 100009, 'Specialization', 'ETL', @candidateId4 UNION
	SELECT 1000010, 'Salary Expectations', '220K/year', @candidateId5 UNION
	SELECT 1000011, 'Notice Period', '2 months', @candidateId6 UNION
	SELECT 1000012, 'Type of work', 'OK to work with legacy', @candidateId6


	SET IDENTITY_INSERT dbo.[CandidateProperty] ON; 

	MERGE dbo.[CandidateProperty] t USING @testProperties s
	ON t.ID = s.ID
	WHEN MATCHED THEN
		UPDATE SET
			t.[Name] = s.[Name],
			t.[Value] = s.[Value],
			t.[CandidateID] = s.[CandidateID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Name], [Value], [CandidateID])
		VALUES (s.[ID], s.[Name], s.[Value], s.[CandidateID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[CandidateProperty] OFF;
END
GO
