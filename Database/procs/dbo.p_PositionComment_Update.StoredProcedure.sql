USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_PositionComment_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionComment_Update]
			@PositionID BIGINT,
			@CommentID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionComment]
					WHERE 
												[PositionID] = @PositionID	AND
												[CommentID] = @CommentID	
							))
	BEGIN
		UPDATE [dbo].[PositionComment]
		SET
									[PositionID] = IIF( @PositionID IS NOT NULL, @PositionID, [PositionID] ) ,
									[CommentID] = IIF( @CommentID IS NOT NULL, @CommentID, [CommentID] ) 
						WHERE 
												[PositionID] = @PositionID	AND
												[CommentID] = @CommentID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PositionComment was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PositionComment] e
	WHERE
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN e.[CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
