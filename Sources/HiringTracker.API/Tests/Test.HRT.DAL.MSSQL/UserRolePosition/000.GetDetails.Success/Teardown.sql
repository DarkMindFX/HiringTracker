

DECLARE @PositionID BIGINT = 100005
DECLARE @UserID BIGINT = 33020042
DECLARE @RoleID BIGINT = 5
 

DELETE FROM [UserRolePosition]
FROM 
	[dbo].[UserRolePosition] e
WHERE
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
