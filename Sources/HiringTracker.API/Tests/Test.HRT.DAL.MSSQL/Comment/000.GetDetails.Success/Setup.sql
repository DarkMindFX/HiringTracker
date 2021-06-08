DECLARE @ID BIGINT
DECLARE @Text NVARCHAR(4000) = 'Text effd539ecd9e4367ac63bd677073b21d'
DECLARE @CreatedDate DATETIME = '10/23/2019 4:33:03 PM'
DECLARE @CreatedByID BIGINT = 100003
DECLARE @ModifiedDate DATETIME = '12/25/2023 3:45:03 PM'
DECLARE @ModifiedByID BIGINT = 33000067

IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Comment]
				WHERE 
					(CASE WHEN @Text IS NOT NULL THEN (CASE WHEN [Text] = @Text THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1))
BEGIN
	INSERT INTO [dbo].[Comment]
		([Text],[CreatedDate],[CreatedByID],[ModifiedDate],[ModifiedByID])
	SELECT 		
		@Text,@CreatedDate,@CreatedByID,@ModifiedDate,@ModifiedByID
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