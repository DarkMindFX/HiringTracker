

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 28e5da9df76b40ab9c248af1136e35ff'
 


IF(EXISTS(SELECT 1 FROM 
					[dbo].[InterviewStatus]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM [dbo].[InterviewStatus]
WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

END