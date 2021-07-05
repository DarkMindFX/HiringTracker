

-- original values --
DECLARE @PositionID BIGINT = 100001
DECLARE @CommentID BIGINT = 100002
 
-- updated values --

DECLARE @updPositionID BIGINT = 100001
DECLARE @updCommentID BIGINT = 100002
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[PositionComment]
				WHERE 
	(CASE WHEN @updPositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @updPositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @updCommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[PositionComment]
	WHERE 
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[PositionComment]
	WHERE 
	(CASE WHEN @updPositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @updPositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @updCommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'PositionComment was not updated', 1
END