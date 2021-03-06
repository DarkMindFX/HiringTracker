USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_Update]
			@CandidateID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleCandidate]
					WHERE 
												[CandidateID] = @CandidateID	AND
												[UserID] = @UserID	
							))
	BEGIN
		UPDATE [dbo].[UserRoleCandidate]
		SET
									[CandidateID] = IIF( @CandidateID IS NOT NULL, @CandidateID, [CandidateID] ) ,
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[RoleID] = IIF( @RoleID IS NOT NULL, @RoleID, [RoleID] ) 
						WHERE 
												[CandidateID] = @CandidateID	AND
												[UserID] = @UserID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserRoleCandidate was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserRoleCandidate] e
	WHERE
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
