

-- original values --
DECLARE @PositionID BIGINT = 100007
DECLARE @UserID BIGINT = 100005
DECLARE @RoleID BIGINT = 3
 
-- updated values --

DECLARE @updPositionID BIGINT = 100007
DECLARE @updUserID BIGINT = 100005
DECLARE @updRoleID BIGINT = 9
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[UserRolePosition]
				WHERE 
	(CASE WHEN @updPositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @updPositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUserID IS NOT NULL THEN (CASE WHEN [UserID] = @updUserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @updRoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[UserRolePosition]
	WHERE 
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[UserRolePosition]
	WHERE 
	(CASE WHEN @updPositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @updPositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUserID IS NOT NULL THEN (CASE WHEN [UserID] = @updUserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @updRoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserRolePosition was not updated', 1
END