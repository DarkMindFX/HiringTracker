

DECLARE @ID BIGINT = 488704
DECLARE @Name NVARCHAR(50) = 'Name f61f4753a4ae4e3baeed78edd096b022'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[SkillProficiency]
				WHERE 
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[SkillProficiency]
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
	[dbo].[SkillProficiency] e
WHERE
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
