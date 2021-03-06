USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStep_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStep_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@ReqDueDate BIT,
			@RequiresRespInDays INT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ProposalStep]
	SELECT 
		@Name,
		@ReqDueDate,
		@RequiresRespInDays
	
	

	SELECT
		e.*
	FROM
		[dbo].[ProposalStep] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN e.[ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN e.[RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
