

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 3c34d9a895104a7c9a93267c1fc5de33'
DECLARE @UUID NVARCHAR(50) = 'UUID 3c34d9a895104a7c9a93267c1fc5de33'
DECLARE @ParentID BIGINT = 2
DECLARE @ManagerID BIGINT = 100001
 

DELETE FROM [Department]
FROM 
	[dbo].[Department] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN [UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
