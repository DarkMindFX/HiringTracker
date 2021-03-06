USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_ProposalComment]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_ProposalComment] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @ProposalComment AS TABLE (
	[ProposalID] [bigint],
	[CommentID] [bigint])

	DECLARE @proposalId1 BIGINT = 100001
	DECLARE @proposalId2 BIGINT = 100002
	DECLARE @proposalId3 BIGINT = 100003
	DECLARE @proposalId4 BIGINT = 100004
	DECLARE @proposalId5 BIGINT = 100005
	DECLARE @proposalId6 BIGINT = 100006
	DECLARE @proposalId7 BIGINT = 100007


	DECLARE @id1 BIGINT = 100001
	DECLARE @id2 BIGINT = 100002
	DECLARE @id3 BIGINT = 100003
	DECLARE @id4 BIGINT = 100004
	DECLARE @id5 BIGINT = 100005
	DECLARE @id6 BIGINT = 100006
	DECLARE @id7 BIGINT = 100007
	DECLARE @id8 BIGINT = 100008
	DECLARE @id9 BIGINT = 100009

	INSERT INTO @ProposalComment 
	SELECT @proposalId1, @id1 UNION
	SELECT @proposalId2, @id2 UNION
	SELECT @proposalId3, @id3 UNION
	SELECT @proposalId4, @id4 UNION
	SELECT @proposalId5, @id5 UNION
	SELECT @proposalId6, @id6 UNION
	SELECT @proposalId7, @id7 
	


	MERGE dbo.ProposalComment as t USING @ProposalComment as s ON
	(
		t.ProposalID = s.ProposalID AND
		t.CommentID = s.CommentID
	)
	WHEN MATCHED THEN 
		UPDATE SET
			t.[ProposalID] = s.[ProposalID],
			t.[CommentID] = s.[CommentID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ProposalID], [CommentID])
		VALUES (s.[ProposalID], s.[CommentID])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;

	

END
GO
