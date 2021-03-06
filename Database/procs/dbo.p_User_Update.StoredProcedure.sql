USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_User_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_User_Update]
			@ID BIGINT,
			@Login NVARCHAR(50),
			@FirstName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@Email NVARCHAR(50),
			@Description NVARCHAR(255),
			@PwdHash NVARCHAR(255),
			@Salt NVARCHAR(255)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM dbo.[User] 
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[User]
		SET
									[Login] = IIF( @Login IS NOT NULL, @Login, [Login] ) ,
									[FirstName] = IIF( @FirstName IS NOT NULL, @FirstName, [FirstName] ) ,
									[LastName] = IIF( @LastName IS NOT NULL, @LastName, [LastName] ) ,
									[Email] = IIF( @Email IS NOT NULL, @Email, [Email] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[PwdHash] = IIF( @PwdHash IS NOT NULL, @PwdHash, [PwdHash] ) ,
									[Salt] = IIF( @Salt IS NOT NULL, @Salt, [Salt] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'User was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[User] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN e.[Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Email IS NOT NULL THEN (CASE WHEN e.[Email] = @Email THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN e.[PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN e.[Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
