

DECLARE @ID BIGINT = NULL
DECLARE @ProposalID BIGINT = 100006
DECLARE @InterviewTypeID BIGINT = 1
DECLARE @StartTime DATETIME = '12/16/2019 8:43:34 AM'
DECLARE @EndTime DATETIME = '12/16/2019 8:43:34 AM'
DECLARE @InterviewStatusID BIGINT = 4
DECLARE @CreatedByID BIGINT = 100005
DECLARE @CretedDate DATETIME = '3/14/2020 6:56:34 PM'
DECLARE @ModifiedByID BIGINT = 100001
DECLARE @ModifiedDate DATETIME = '6/12/2020 2:30:34 PM'
 


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
