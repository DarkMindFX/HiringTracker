

DECLARE @ID BIGINT = NULL
DECLARE @Text NVARCHAR(4000) = 'Text 6819a6b15b32427d9d7b3fc2db583f37'
DECLARE @CreatedDate DATETIME = '3/3/2023 9:26:10 AM'
DECLARE @CreatedByID BIGINT = 33000067
DECLARE @ModifiedDate DATETIME = '3/3/2023 9:26:10 AM'
DECLARE @ModifiedByID BIGINT = 100002
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Comment]
				WHERE 
	(CASE WHEN @Text IS NOT NULL THEN (CASE WHEN [Text] = @Text THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Comment]
		(
	 [Text],
	 [CreatedDate],
	 [CreatedByID],
	 [ModifiedDate],
	 [ModifiedByID]
		)
	SELECT 		
			 @Text,
	 @CreatedDate,
	 @CreatedByID,
	 @ModifiedDate,
	 @ModifiedByID
END

SELECT TOP 1 @ID = [ID] 
FROM 
	[dbo].[Comment] e
WHERE
	(CASE WHEN @Text IS NOT NULL THEN (CASE WHEN [Text] = @Text THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT @ID