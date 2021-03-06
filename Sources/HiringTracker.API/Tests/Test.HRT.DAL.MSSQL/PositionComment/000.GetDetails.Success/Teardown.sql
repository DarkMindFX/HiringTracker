

DECLARE @PositionID BIGINT = 100005
DECLARE @CommentID BIGINT = 100003
 

DELETE FROM [PositionComment]
FROM 
	[dbo].[PositionComment] e
WHERE
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
