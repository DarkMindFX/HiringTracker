USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateComment_GetDetails]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateComment_GetDetails]
		@CandidateID BIGINT,	
		@CommentID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateComment] c 
				WHERE 
								[CandidateID] = @CandidateID	AND
								[CommentID] = @CommentID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateComment] e
		WHERE 
								[CandidateID] = @CandidateID	AND
								[CommentID] = @CommentID	
				END
	ELSE
		SET @Found = 0;
END
GO
