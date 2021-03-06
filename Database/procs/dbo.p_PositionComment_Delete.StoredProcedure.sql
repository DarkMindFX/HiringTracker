USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_PositionComment_Delete]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionComment_Delete]
		@PositionID BIGINT,	
		@CommentID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PositionComment]  
				WHERE 
							[PositionID] = @PositionID	AND
							[CommentID] = @CommentID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[PositionComment] 
		WHERE 
						[PositionID] = @PositionID	AND
						[CommentID] = @CommentID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
