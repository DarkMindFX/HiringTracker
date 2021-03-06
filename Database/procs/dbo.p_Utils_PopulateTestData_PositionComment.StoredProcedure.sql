USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_PositionComment]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_PositionComment] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @PositionComment AS TABLE (
	[PositionID] [bigint],
	[CommentID] [bigint])

	DECLARE @positionId1 BIGINT = 100001
	DECLARE @positionId2 BIGINT = 100002
	DECLARE @positionId3 BIGINT = 100003
	DECLARE @positionId4 BIGINT = 100004
	DECLARE @positionId5 BIGINT = 100005
	DECLARE @positionId6 BIGINT = 100006
	DECLARE @positionId7 BIGINT = 100007
	DECLARE @positionId8 BIGINT = 100008
	DECLARE @positionId9 BIGINT = 100009

	DECLARE @id1 BIGINT = 100001
	DECLARE @id2 BIGINT = 100002
	DECLARE @id3 BIGINT = 100003
	DECLARE @id4 BIGINT = 100004
	DECLARE @id5 BIGINT = 100005
	DECLARE @id6 BIGINT = 100006
	DECLARE @id7 BIGINT = 100007
	DECLARE @id8 BIGINT = 100008
	DECLARE @id9 BIGINT = 100009

	INSERT INTO @PositionComment 
	SELECT @positionId1, @id1 UNION
	SELECT @positionId2, @id2 UNION
	SELECT @positionId3, @id3 UNION
	SELECT @positionId4, @id4 UNION
	SELECT @positionId5, @id5 UNION
	SELECT @positionId6, @id6 UNION
	SELECT @positionId7, @id7 UNION
	SELECT @positionId8, @id8 UNION
	SELECT @positionId9, @id9
	


	MERGE dbo.PositionComment as t USING @PositionComment as s ON
	(
		t.PositionID = s.PositionID AND
		t.CommentID = s.CommentID
	)
	WHEN MATCHED THEN 
		UPDATE SET
			t.[PositionID] = s.[PositionID],
			t.[CommentID] = s.[CommentID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([PositionID], [CommentID])
		VALUES (s.[PositionID], s.[CommentID])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;

	

END
GO
