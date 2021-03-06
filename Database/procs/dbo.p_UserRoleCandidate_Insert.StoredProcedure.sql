USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_Insert]
			@CandidateID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserRoleCandidate]
	SELECT 
		@CandidateID,
		@UserID,
		@RoleID
	
	

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
