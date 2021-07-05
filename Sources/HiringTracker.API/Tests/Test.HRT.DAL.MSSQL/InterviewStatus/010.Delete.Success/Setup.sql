

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name c7ffa425000a41a78a2958f0bc9c61eb'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[InterviewStatus]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[InterviewStatus]
		(
	 [Name]
		)
	SELECT 		
			 @Name
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[InterviewStatus] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
