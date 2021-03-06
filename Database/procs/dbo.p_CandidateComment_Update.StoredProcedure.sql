USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateComment_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateComment_Update]
			@CandidateID BIGINT,
			@CommentID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateComment]
					WHERE 
												[CandidateID] = @CandidateID	AND
												[CommentID] = @CommentID	
							))
	BEGIN
		UPDATE [dbo].[CandidateComment]
		SET
									[CandidateID] = IIF( @CandidateID IS NOT NULL, @CandidateID, [CandidateID] ) ,
									[CommentID] = IIF( @CommentID IS NOT NULL, @CommentID, [CommentID] ) 
						WHERE 
												[CandidateID] = @CandidateID	AND
												[CommentID] = @CommentID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'CandidateComment was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[CandidateComment] e
	WHERE
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN e.[CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
