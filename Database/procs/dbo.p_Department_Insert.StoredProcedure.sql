USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Department_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Department_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@UUID NVARCHAR(50),
			@ParentID BIGINT,
			@ManagerID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Department]
	SELECT 
		@Name,
		@UUID,
		@ParentID,
		@ManagerID
	
	

	SELECT
		e.*
	FROM
		[dbo].[Department] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN e.[UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN e.[ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN e.[ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
