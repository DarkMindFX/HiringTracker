

DECLARE @ID BIGINT = 748707
DECLARE @Name NVARCHAR(50) = 'Name 8ff86dc69dd64a99b30e36204fe60591'
 


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
