

DECLARE @InterviewID BIGINT = NULL
DECLARE @UserID BIGINT = 100001
DECLARE @RoleID BIGINT = 5
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[InterviewRole]
				WHERE 
	(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN [InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[InterviewRole]
		(
	 [InterviewID],
	 [UserID],
	 [RoleID]
		)
	SELECT 		
			 @InterviewID,
	 @UserID,
	 @RoleID
END

SELECT TOP 1 
	@InterviewID = [InterviewID], 
	@UserID = [UserID]
FROM 
	[dbo].[InterviewRole] e
WHERE
	(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN [InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@InterviewID, 
	@UserID
