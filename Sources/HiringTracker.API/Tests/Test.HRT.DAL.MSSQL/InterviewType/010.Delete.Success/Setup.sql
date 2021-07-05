

DECLARE @ID BIGINT = 326774
DECLARE @Name NVARCHAR(50) = 'Name d5cd2ac4876f478091cf56e944a8fa29'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[InterviewType]
				WHERE 
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[InterviewType]
		(
	 [ID],
	 [Name]
		)
	SELECT 		
			 @ID,
	 @Name
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[InterviewType] e
WHERE
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
