USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_Insert]
			@ID BIGINT,
			@ProposalID BIGINT,
			@InterviewTypeID BIGINT,
			@StartTime DATETIME,
			@EndTime DATETIME,
			@InterviewStatusID BIGINT,
			@CreatedByID BIGINT,
			@CretedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Interview]
	SELECT 
		@ProposalID,
		@InterviewTypeID,
		@StartTime,
		@EndTime,
		@InterviewStatusID,
		@CreatedByID,
		@CretedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[Interview] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN e.[ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewTypeID IS NOT NULL THEN (CASE WHEN e.[InterviewTypeID] = @InterviewTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StartTime IS NOT NULL THEN (CASE WHEN e.[StartTime] = @StartTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EndTime IS NOT NULL THEN (CASE WHEN e.[EndTime] = @EndTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewStatusID IS NOT NULL THEN (CASE WHEN e.[InterviewStatusID] = @InterviewStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CretedDate IS NOT NULL THEN (CASE WHEN e.[CretedDate] = @CretedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
