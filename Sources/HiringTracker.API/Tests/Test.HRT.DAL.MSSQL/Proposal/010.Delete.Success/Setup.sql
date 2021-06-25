

DECLARE @ID BIGINT = NULL
DECLARE @PositionID BIGINT = 100003
DECLARE @CandidateID BIGINT = 100009
DECLARE @Proposed DATETIME = '8/12/2020 6:16:12 AM'
DECLARE @CurrentStepID BIGINT = 4
DECLARE @StepSetDate DATETIME = '8/12/2020 6:16:12 AM'
DECLARE @NextStepID BIGINT = 4
DECLARE @DueDate DATETIME = '6/23/2023 4:03:12 PM'
DECLARE @StatusID BIGINT = 4
DECLARE @CreatedByID BIGINT = 33020042
DECLARE @CreatedDate DATETIME = '11/11/2020 1:50:12 AM'
DECLARE @ModifiedByID BIGINT = 33000067
DECLARE @ModifiedDate DATETIME = '11/11/2020 1:50:12 AM'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Proposal]
				WHERE 
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Proposed IS NOT NULL THEN (CASE WHEN [Proposed] = @Proposed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CurrentStepID IS NOT NULL THEN (CASE WHEN [CurrentStepID] = @CurrentStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StepSetDate IS NOT NULL THEN (CASE WHEN [StepSetDate] = @StepSetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @NextStepID IS NOT NULL THEN (CASE WHEN [NextStepID] = @NextStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DueDate IS NOT NULL THEN (CASE WHEN [DueDate] = @DueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN [StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Proposal]
		(
	 [PositionID],
	 [CandidateID],
	 [Proposed],
	 [CurrentStepID],
	 [StepSetDate],
	 [NextStepID],
	 [DueDate],
	 [StatusID],
	 [CreatedByID],
	 [CreatedDate],
	 [ModifiedByID],
	 [ModifiedDate]
		)
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
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Proposal] e
WHERE
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Proposed IS NOT NULL THEN (CASE WHEN [Proposed] = @Proposed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CurrentStepID IS NOT NULL THEN (CASE WHEN [CurrentStepID] = @CurrentStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StepSetDate IS NOT NULL THEN (CASE WHEN [StepSetDate] = @StepSetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @NextStepID IS NOT NULL THEN (CASE WHEN [NextStepID] = @NextStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DueDate IS NOT NULL THEN (CASE WHEN [DueDate] = @DueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN [StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
