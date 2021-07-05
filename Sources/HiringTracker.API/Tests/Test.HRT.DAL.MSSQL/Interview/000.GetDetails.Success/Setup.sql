

DECLARE @ID BIGINT = NULL
DECLARE @ProposalID BIGINT = 100001
DECLARE @InterviewTypeID BIGINT = 4
DECLARE @StartTime DATETIME = '12/16/2023 3:21:34 PM'
DECLARE @EndTime DATETIME = '12/16/2023 3:21:34 PM'
DECLARE @InterviewStatusID BIGINT = 4
DECLARE @CreatedByID BIGINT = 100003
DECLARE @CretedDate DATETIME = '12/21/2018 9:08:34 PM'
DECLARE @ModifiedByID BIGINT = 100004
DECLARE @ModifiedDate DATETIME = '11/1/2021 6:55:34 AM'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Interview]
				WHERE 
	(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewTypeID IS NOT NULL THEN (CASE WHEN [InterviewTypeID] = @InterviewTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StartTime IS NOT NULL THEN (CASE WHEN [StartTime] = @StartTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @EndTime IS NOT NULL THEN (CASE WHEN [EndTime] = @EndTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewStatusID IS NOT NULL THEN (CASE WHEN [InterviewStatusID] = @InterviewStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CretedDate IS NOT NULL THEN (CASE WHEN [CretedDate] = @CretedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Interview]
		(
	 [ProposalID],
	 [InterviewTypeID],
	 [StartTime],
	 [EndTime],
	 [InterviewStatusID],
	 [CreatedByID],
	 [CretedDate],
	 [ModifiedByID],
	 [ModifiedDate]
		)
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
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Interview] e
WHERE
	(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewTypeID IS NOT NULL THEN (CASE WHEN [InterviewTypeID] = @InterviewTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StartTime IS NOT NULL THEN (CASE WHEN [StartTime] = @StartTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @EndTime IS NOT NULL THEN (CASE WHEN [EndTime] = @EndTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewStatusID IS NOT NULL THEN (CASE WHEN [InterviewStatusID] = @InterviewStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CretedDate IS NOT NULL THEN (CASE WHEN [CretedDate] = @CretedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
