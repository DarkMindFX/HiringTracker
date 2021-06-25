

DECLARE @PositionID BIGINT = 100005
DECLARE @UserID BIGINT = 33020042
DECLARE @RoleID BIGINT = 5
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[UserRolePosition]
				WHERE 
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[UserRolePosition]
		(
	 [PositionID],
	 [UserID],
	 [RoleID]
		)
	SELECT 		
			 @PositionID,
	 @UserID,
	 @RoleID
END

SELECT TOP 1 
	@PositionID = [PositionID], 
	@UserID = [UserID]
FROM 
	[dbo].[UserRolePosition] e
WHERE
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@PositionID, 
	@UserID
