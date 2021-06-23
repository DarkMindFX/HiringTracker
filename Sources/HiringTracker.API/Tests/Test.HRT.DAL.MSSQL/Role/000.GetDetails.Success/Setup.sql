

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 8e42f2ba8df5469b9cbe7b824ba7c0f0'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Role]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Role]
		(
	 [Name]
		)
	SELECT 		
			 @Name
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Role] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
