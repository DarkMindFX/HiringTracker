

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 30248f7564d94e138fbfd63a2c11b6d5'
 


IF(EXISTS(SELECT 1 FROM 
					[dbo].[Role]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM [dbo].[Role]
WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

END