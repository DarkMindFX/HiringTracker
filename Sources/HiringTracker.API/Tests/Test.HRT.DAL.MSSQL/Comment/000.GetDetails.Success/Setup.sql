

DECLARE @ID BIGINT = NULL
DECLARE @Text NVARCHAR(4000) = 'Text 25defb2b9d344940992d4ff559d951ec'
DECLARE @CreatedDate DATETIME = '7/6/2019 6:19:12 AM'
DECLARE @CreatedByID BIGINT = 33020042
DECLARE @ModifiedDate DATETIME = '3/30/2020 10:20:12 PM'
DECLARE @ModifiedByID BIGINT = 33020042
 


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

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Comment] e
WHERE
	(CASE WHEN @Text IS NOT NULL THEN (CASE WHEN [Text] = @Text THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
