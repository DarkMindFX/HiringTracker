USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_UserRolePosition_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRolePosition_Update]
			@PositionID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRolePosition]
					WHERE 
												[PositionID] = @PositionID	AND
												[UserID] = @UserID	
							))
	BEGIN
		UPDATE [dbo].[UserRolePosition]
		SET
									[PositionID] = IIF( @PositionID IS NOT NULL, @PositionID, [PositionID] ) ,
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[RoleID] = IIF( @RoleID IS NOT NULL, @RoleID, [RoleID] ) 
						WHERE 
												[PositionID] = @PositionID	AND
												[UserID] = @UserID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserRolePosition was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserRolePosition] e
	WHERE
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
