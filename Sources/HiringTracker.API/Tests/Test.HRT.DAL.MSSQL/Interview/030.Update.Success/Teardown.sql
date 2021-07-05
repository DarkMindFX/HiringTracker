

-- original values --
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
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updProposalID BIGINT = 100006
DECLARE @updInterviewTypeID BIGINT = 3
DECLARE @updStartTime DATETIME = '4/22/2023 2:57:34 PM'
DECLARE @updEndTime DATETIME = '4/22/2023 2:57:34 PM'
DECLARE @updInterviewStatusID BIGINT = 3
DECLARE @updCreatedByID BIGINT = 100001
DECLARE @updCretedDate DATETIME = '9/10/2020 12:44:34 AM'
DECLARE @updModifiedByID BIGINT = 100005
DECLARE @updModifiedDate DATETIME = '7/22/2023 10:31:34 AM'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Interview]
				WHERE 
	(CASE WHEN @updProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @updProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updInterviewTypeID IS NOT NULL THEN (CASE WHEN [InterviewTypeID] = @updInterviewTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updStartTime IS NOT NULL THEN (CASE WHEN [StartTime] = @updStartTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updEndTime IS NOT NULL THEN (CASE WHEN [EndTime] = @updEndTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updInterviewStatusID IS NOT NULL THEN (CASE WHEN [InterviewStatusID] = @updInterviewStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCretedDate IS NOT NULL THEN (CASE WHEN [CretedDate] = @updCretedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
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

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Interview]
	WHERE 
	(CASE WHEN @updProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @updProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updInterviewTypeID IS NOT NULL THEN (CASE WHEN [InterviewTypeID] = @updInterviewTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updStartTime IS NOT NULL THEN (CASE WHEN [StartTime] = @updStartTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updEndTime IS NOT NULL THEN (CASE WHEN [EndTime] = @updEndTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updInterviewStatusID IS NOT NULL THEN (CASE WHEN [InterviewStatusID] = @updInterviewStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCretedDate IS NOT NULL THEN (CASE WHEN [CretedDate] = @updCretedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Interview was not updated', 1
END