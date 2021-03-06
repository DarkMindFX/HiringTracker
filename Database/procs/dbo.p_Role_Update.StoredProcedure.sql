USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Role_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Role_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Role]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Role]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Role was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Role] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
