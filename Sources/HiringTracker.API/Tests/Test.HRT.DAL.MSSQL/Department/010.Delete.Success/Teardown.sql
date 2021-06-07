DECLARE @ID BIGINT
DECLARE @Name NVARCHAR(50) = 'Name 0a63dcb461114997b2382f9a2651c925'
DECLARE @UUID NVARCHAR(50) = 'UUID 0a63dcb461114997b2382f9a2651c925'
DECLARE @ParentID BIGINT = 3
DECLARE @ManagerID BIGINT = 100003

DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
					[dbo].[Department]
				WHERE 
					(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN [UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1))
BEGIN
	SET @Fail = 1
END

DELETE FROM [dbo].[Department]
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN [UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Department was not deleted', 1
END