USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_Insert]
			@InterviewID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[InterviewRole]
	SELECT 
		@InterviewID,
		@UserID,
		@RoleID
	
	

	SELECT
		e.*
	FROM
		[dbo].[InterviewRole] e
	WHERE
				(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN e.[InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
