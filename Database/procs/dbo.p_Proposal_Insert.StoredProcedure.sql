USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_Insert]
			@ID BIGINT,
			@PositionID BIGINT,
			@CandidateID BIGINT,
			@Proposed DATETIME,
			@CurrentStepID BIGINT,
			@StepSetDate DATETIME,
			@NextStepID BIGINT,
			@DueDate DATETIME,
			@StatusID BIGINT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Proposal]
	SELECT 
		@PositionID,
		@CandidateID,
		@Proposed,
		@CurrentStepID,
		@StepSetDate,
		@NextStepID,
		@DueDate,
		@StatusID,
		@CreatedByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[Proposal] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Proposed IS NOT NULL THEN (CASE WHEN e.[Proposed] = @Proposed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CurrentStepID IS NOT NULL THEN (CASE WHEN e.[CurrentStepID] = @CurrentStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StepSetDate IS NOT NULL THEN (CASE WHEN e.[StepSetDate] = @StepSetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NextStepID IS NOT NULL THEN (CASE WHEN e.[NextStepID] = @NextStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DueDate IS NOT NULL THEN (CASE WHEN e.[DueDate] = @DueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN e.[StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
