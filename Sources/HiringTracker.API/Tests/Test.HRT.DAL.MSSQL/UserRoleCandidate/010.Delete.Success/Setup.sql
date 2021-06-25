

DECLARE @CandidateID BIGINT = 100008
DECLARE @UserID BIGINT = 33000067
DECLARE @RoleID BIGINT = 4
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[UserRoleCandidate]
				WHERE 
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[UserRoleCandidate]
		(
	 [CandidateID],
	 [UserID],
	 [RoleID]
		)
	SELECT 		
			 @CandidateID,
	 @UserID,
	 @RoleID
END

SELECT TOP 1 
	@CandidateID = [CandidateID], 
	@UserID = [UserID]
FROM 
	[dbo].[UserRoleCandidate] e
WHERE
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN [RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@CandidateID, 
	@UserID
