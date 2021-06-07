DECLARE @ID BIGINT
DECLARE @Name NVARCHAR(50) = 'Name 2bf433350cbb4a788612520b44cfa1ff'
DECLARE @UUID NVARCHAR(50) = 'UUID 2bf433350cbb4a788612520b44cfa1ff'
DECLARE @ParentID BIGINT = 2
DECLARE @ManagerID BIGINT = 100003

IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Department]
				WHERE 
					(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN [UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1))
BEGIN
	INSERT INTO [dbo].[Department]
		([Name],[UUID],[ParentID],[ManagerID])
	SELECT 		
		@Name,@UUID,@ParentID,@ManagerID
END

SELECT TOP 1 @ID = [ID] 
FROM 
	[dbo].[Department] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN [UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1

SELECT @ID