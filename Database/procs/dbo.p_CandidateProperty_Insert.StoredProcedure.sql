USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateProperty_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateProperty_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Value NVARCHAR(1000),
			@CandidateID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[CandidateProperty]
	SELECT 
		@Name,
		@Value,
		@CandidateID
	
	

	SELECT
		e.*
	FROM
		[dbo].[CandidateProperty] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN e.[Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
