USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_User_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_User_Insert]
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


	INSERT INTO [dbo].[User]
	SELECT 
		@Login,
		@FirstName,
		@LastName,
		@Email,
		@Description,
		@PwdHash,
		@Salt
	
	

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
