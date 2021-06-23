

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 05ba7cf0e66648b1ba8bc4065cdc230d'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Skill]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Skill]
		(
	 [Name]
		)
	SELECT 		
			 @Name
END

SELECT TOP 1 @ID = [ID] 
FROM 
	[dbo].[Skill] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT @ID