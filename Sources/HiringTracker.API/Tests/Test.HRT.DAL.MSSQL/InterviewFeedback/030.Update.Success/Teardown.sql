

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Comment NVARCHAR(4000) = 'Comment ca010a4564c244579f3f7e0993b1ae49'
DECLARE @Rating INT = 884
DECLARE @InterviewID BIGINT = 100002
DECLARE @InterviewerID BIGINT = 100005
DECLARE @CreatedByID BIGINT = 100003
DECLARE @CreatedDate DATETIME = '12/30/2020 1:59:25 AM'
DECLARE @ModifiedByID BIGINT = 100004
DECLARE @ModifiedDate DATETIME = '3/29/2021 12:12:25 PM'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updComment NVARCHAR(4000) = 'Comment 06075afbffe1452d85f11fb3d55fff67'
DECLARE @updRating INT = 451
DECLARE @updInterviewID BIGINT = 100006
DECLARE @updInterviewerID BIGINT = 100002
DECLARE @updCreatedByID BIGINT = 100005
DECLARE @updCreatedDate DATETIME = '6/28/2021 7:46:25 AM'
DECLARE @updModifiedByID BIGINT = 100001
DECLARE @updModifiedDate DATETIME = '6/28/2021 7:46:25 AM'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[InterviewFeedback]
				WHERE 
	(CASE WHEN @updComment IS NOT NULL THEN (CASE WHEN [Comment] = @updComment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRating IS NOT NULL THEN (CASE WHEN [Rating] = @updRating THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updInterviewID IS NOT NULL THEN (CASE WHEN [InterviewID] = @updInterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updInterviewerID IS NOT NULL THEN (CASE WHEN [InterviewerID] = @updInterviewerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[InterviewFeedback]
	WHERE 
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Rating IS NOT NULL THEN (CASE WHEN [Rating] = @Rating THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN [InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewerID IS NOT NULL THEN (CASE WHEN [InterviewerID] = @InterviewerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[InterviewFeedback]
	WHERE 
	(CASE WHEN @updComment IS NOT NULL THEN (CASE WHEN [Comment] = @updComment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRating IS NOT NULL THEN (CASE WHEN [Rating] = @updRating THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updInterviewID IS NOT NULL THEN (CASE WHEN [InterviewID] = @updInterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updInterviewerID IS NOT NULL THEN (CASE WHEN [InterviewerID] = @updInterviewerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'InterviewFeedback was not updated', 1
END