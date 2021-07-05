

DECLARE @ID BIGINT = NULL
DECLARE @PositionID BIGINT = 100002
DECLARE @CandidateID BIGINT = 100002
DECLARE @Proposed DATETIME = '12/21/2020 5:03:34 PM'
DECLARE @CurrentStepID BIGINT = 11
DECLARE @StepSetDate DATETIME = '12/9/2022 6:52:34 AM'
DECLARE @NextStepID BIGINT = 4
DECLARE @DueDate DATETIME = '10/19/2021 10:01:34 AM'
DECLARE @StatusID BIGINT = 4
DECLARE @CreatedByID BIGINT = 100004
DECLARE @CreatedDate DATETIME = '1/16/2022 8:15:34 PM'
DECLARE @ModifiedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '6/6/2019 6:02:34 AM'
 


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
