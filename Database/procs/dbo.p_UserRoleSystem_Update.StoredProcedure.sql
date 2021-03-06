USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleSystem_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleSystem_Update]
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleSystem]
					WHERE 
												[UserID] = @UserID
							))
	BEGIN
		UPDATE [dbo].[UserRoleSystem]
		SET
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[RoleID] = IIF( @RoleID IS NOT NULL, @RoleID, [RoleID] ) 
						WHERE 
												[UserID] = @UserID
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserRoleSystem was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserRoleSystem] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
