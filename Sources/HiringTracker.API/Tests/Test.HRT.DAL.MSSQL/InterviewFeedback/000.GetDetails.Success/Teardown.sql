

DECLARE @ID BIGINT = NULL
DECLARE @Comment NVARCHAR(4000) = 'Comment 60243b2d2fd8489393b81146b4cd0b6f'
DECLARE @Rating INT = 509
DECLARE @InterviewID BIGINT = 100007
DECLARE @InterviewerID BIGINT = 100002
DECLARE @CreatedByID BIGINT = 100002
DECLARE @CreatedDate DATETIME = '4/5/2020 9:58:25 AM'
DECLARE @ModifiedByID BIGINT = 100001
DECLARE @ModifiedDate DATETIME = '4/5/2020 9:58:25 AM'
 

DELETE FROM [InterviewFeedback]
FROM 
	[dbo].[InterviewFeedback] e
WHERE
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Rating IS NOT NULL THEN (CASE WHEN [Rating] = @Rating THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN [InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewerID IS NOT NULL THEN (CASE WHEN [InterviewerID] = @InterviewerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
