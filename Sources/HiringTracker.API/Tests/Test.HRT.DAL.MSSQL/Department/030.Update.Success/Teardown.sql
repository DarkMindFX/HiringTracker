

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 5aa06650910b457f959d70821ef387b8'
DECLARE @UUID NVARCHAR(50) = 'UUID 5aa06650910b457f959d70821ef387b8'
DECLARE @ParentID BIGINT = 10
DECLARE @ManagerID BIGINT = 100001
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updName NVARCHAR(50) = 'Name 72b4b3e87cab47ccb389fb9d1b995d31'
DECLARE @updUUID NVARCHAR(50) = 'UUID 72b4b3e87cab47ccb389fb9d1b995d31'
DECLARE @updParentID BIGINT = 3
DECLARE @updManagerID BIGINT = 100002
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Department]
				WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUUID IS NOT NULL THEN (CASE WHEN [UUID] = @updUUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @updParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @updManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Department]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN [UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Department]
	WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUUID IS NOT NULL THEN (CASE WHEN [UUID] = @updUUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @updParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @updManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Department was not updated', 1
END