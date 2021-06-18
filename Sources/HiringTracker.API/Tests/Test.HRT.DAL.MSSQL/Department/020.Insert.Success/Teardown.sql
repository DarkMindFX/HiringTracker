DECLARE @ID BIGINT
DECLARE @Name NVARCHAR(50) = 'Name 6de9d6731940477b9c034dfae62e602b'
DECLARE @UUID NVARCHAR(50) = 'UUID 6de9d6731940477b9c034dfae62e602b'
DECLARE @ParentID BIGINT = 3
DECLARE @ManagerID BIGINT = 33000067
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
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
	THROW 51001, 'Department was not inserted', 1
END