USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStatus_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStatus_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ProposalStatus]
	SELECT 
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[ProposalStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
