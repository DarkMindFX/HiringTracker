

DECLARE @ID BIGINT = NULL
DECLARE @PositionID BIGINT = 100009
DECLARE @CandidateID BIGINT = 100001
DECLARE @Proposed DATETIME = '9/22/2023 11:37:12 AM'
DECLARE @CurrentStepID BIGINT = 10
DECLARE @StepSetDate DATETIME = '2/7/2021 12:03:12 PM'
DECLARE @NextStepID BIGINT = 10
DECLARE @DueDate DATETIME = '2/7/2021 12:03:12 PM'
DECLARE @StatusID BIGINT = 3
DECLARE @CreatedByID BIGINT = 100001
DECLARE @CreatedDate DATETIME = '12/19/2023 9:50:12 PM'
DECLARE @ModifiedByID BIGINT = 100001
DECLARE @ModifiedDate DATETIME = '5/9/2021 7:37:12 AM'
 
DECLARE @Fail AS BIT = 0

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
	SET @Fail = 1
END

DELETE FROM 
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

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Proposal was not inserted', 1
END