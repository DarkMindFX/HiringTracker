USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_CandidateComment]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_CandidateComment] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @CandidateComment AS TABLE (
	[CandidateID] [bigint],
	[CommentID] [bigint])

	DECLARE @candidateId1 BIGINT = 100001
	DECLARE @candidateId2 BIGINT = 100002
	DECLARE @candidateId3 BIGINT = 100003
	DECLARE @candidateId4 BIGINT = 100004
	DECLARE @candidateId5 BIGINT = 100005
	DECLARE @candidateId6 BIGINT = 100006
	DECLARE @candidateId7 BIGINT = 100007
	DECLARE @candidateId8 BIGINT = 100008
	DECLARE @candidateId9 BIGINT = 100009

	DECLARE @id1 BIGINT = 100001
	DECLARE @id2 BIGINT = 100002
	DECLARE @id3 BIGINT = 100003
	DECLARE @id4 BIGINT = 100004
	DECLARE @id5 BIGINT = 100005
	DECLARE @id6 BIGINT = 100006
	DECLARE @id7 BIGINT = 100007
	DECLARE @id8 BIGINT = 100008
	DECLARE @id9 BIGINT = 100009

	INSERT INTO @CandidateComment 
	SELECT @candidateId1, @id1 UNION
	SELECT @candidateId2, @id2 UNION
	SELECT @candidateId3, @id3 UNION
	SELECT @candidateId4, @id4 UNION
	SELECT @candidateId5, @id5 UNION
	SELECT @candidateId6, @id6 UNION
	SELECT @candidateId7, @id7 UNION
	SELECT @candidateId8, @id8 UNION
	SELECT @candidateId9, @id9
	


	MERGE dbo.CandidateComment as t USING @CandidateComment as s ON
	(
		t.CandidateID = s.CandidateID AND
		t.CommentID = s.CommentID
	)
	WHEN MATCHED THEN 
		UPDATE SET
			t.[CandidateID] = s.[CandidateID],
			t.[CommentID] = s.[CommentID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([CandidateID], [CommentID])
		VALUES (s.[CandidateID], s.[CommentID])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;

	

END
GO
