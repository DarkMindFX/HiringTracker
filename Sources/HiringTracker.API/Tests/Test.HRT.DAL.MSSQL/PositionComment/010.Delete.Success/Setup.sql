

DECLARE @PositionID BIGINT = 100003
DECLARE @CommentID BIGINT = 100001
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[PositionComment]
				WHERE 
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[PositionComment]
		(
	 [PositionID],
	 [CommentID]
		)
	SELECT 		
			 @PositionID,
	 @CommentID
END

SELECT TOP 1 @ID = [ID] 
FROM 
	[dbo].[PositionComment] e
WHERE
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT @ID