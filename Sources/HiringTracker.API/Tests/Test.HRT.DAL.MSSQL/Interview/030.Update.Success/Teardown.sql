

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @ProposalID BIGINT = 100003
DECLARE @InterviewTypeID BIGINT = 3
DECLARE @StartTime DATETIME = '8/26/2021 9:01:10 AM'
DECLARE @EndTime DATETIME = '8/26/2021 9:01:10 AM'
DECLARE @InterviewStatusID BIGINT = 1
DECLARE @CreatedByID BIGINT = 100002
DECLARE @CretedDate DATETIME = '1/7/2020 9:02:10 PM'
DECLARE @ModifiedByID BIGINT = 33020042
DECLARE @ModifiedDate DATETIME = '11/18/2022 6:49:10 AM'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updProposalID BIGINT = 100004
DECLARE @updInterviewTypeID BIGINT = 5
DECLARE @updStartTime DATETIME = '8/13/2023 10:50:10 PM'
DECLARE @updEndTime DATETIME = '8/13/2023 10:50:10 PM'
DECLARE @updInterviewStatusID BIGINT = 2
DECLARE @updCreatedByID BIGINT = 100002
DECLARE @updCretedDate DATETIME = '11/16/2018 2:51:10 PM'
DECLARE @updModifiedByID BIGINT = 100002
DECLARE @updModifiedDate DATETIME = '11/16/2018 2:51:10 PM'
 

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