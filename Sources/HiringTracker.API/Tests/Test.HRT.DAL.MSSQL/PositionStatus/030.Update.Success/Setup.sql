

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name e52d8253c3734af2b61b26bd31404b2f'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[PositionStatus]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[PositionStatus]
		(
	 [Name]
		)
	SELECT 		
			 @Name
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[PositionStatus] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
