

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name c05c5367645d4b2f8e95cad6fda28352'
 

DELETE FROM [Skill]
FROM 
	[dbo].[Skill] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
