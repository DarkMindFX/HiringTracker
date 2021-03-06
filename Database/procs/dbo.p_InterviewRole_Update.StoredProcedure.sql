USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_Update]
			@InterviewID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewRole]
					WHERE 
												[InterviewID] = @InterviewID	AND
												[UserID] = @UserID	
							))
	BEGIN
		UPDATE [dbo].[InterviewRole]
		SET
									[InterviewID] = IIF( @InterviewID IS NOT NULL, @InterviewID, [InterviewID] ) ,
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[RoleID] = IIF( @RoleID IS NOT NULL, @RoleID, [RoleID] ) 
						WHERE 
												[InterviewID] = @InterviewID	AND
												[UserID] = @UserID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'InterviewRole was not found', 1;
	END	

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
