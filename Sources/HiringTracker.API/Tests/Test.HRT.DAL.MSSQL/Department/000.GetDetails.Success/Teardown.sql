

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 920424c250c54fb7a296edac15de7fcf'
DECLARE @UUID NVARCHAR(50) = 'UUID 920424c250c54fb7a296edac15de7fcf'
DECLARE @ParentID BIGINT = 10
DECLARE @ManagerID BIGINT = 33000067
 

DELETE FROM [Department]
FROM 
	[dbo].[Department] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN [UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
