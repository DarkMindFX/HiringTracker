USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalComment_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalComment_Update]
			@ProposalID BIGINT,
			@CommentID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ProposalComment]
					WHERE 
												[ProposalID] = @ProposalID	AND
												[CommentID] = @CommentID	
							))
	BEGIN
		UPDATE [dbo].[ProposalComment]
		SET
									[ProposalID] = IIF( @ProposalID IS NOT NULL, @ProposalID, [ProposalID] ) ,
									[CommentID] = IIF( @CommentID IS NOT NULL, @CommentID, [CommentID] ) 
						WHERE 
												[ProposalID] = @ProposalID	AND
												[CommentID] = @CommentID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ProposalComment was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ProposalComment] e
	WHERE
				(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN e.[ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN e.[CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
