USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_Delete]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_Delete]
		@CandidateID BIGINT,	
		@UserID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserRoleCandidate]  
				WHERE 
							[CandidateID] = @CandidateID	AND
							[UserID] = @UserID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[UserRoleCandidate] 
		WHERE 
						[CandidateID] = @CandidateID	AND
						[UserID] = @UserID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
