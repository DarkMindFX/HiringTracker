

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 933d5e37a44d4c12a7603f2056ae19c5'
DECLARE @UUID NVARCHAR(50) = 'UUID 933d5e37a44d4c12a7603f2056ae19c5'
DECLARE @ParentID BIGINT = NULL
DECLARE @ManagerID BIGINT = 100003
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updName NVARCHAR(50) = 'Name 3496de309b884a9ab2af83eca50259ee'
DECLARE @updUUID NVARCHAR(50) = 'UUID 3496de309b884a9ab2af83eca50259ee'
DECLARE @updParentID BIGINT = NULL
DECLARE @updManagerID BIGINT = 100004
 

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