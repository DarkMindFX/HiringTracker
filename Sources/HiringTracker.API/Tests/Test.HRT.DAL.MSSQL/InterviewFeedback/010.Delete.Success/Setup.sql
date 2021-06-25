

DECLARE @ID BIGINT = 820130
DECLARE @Comment NVARCHAR(4000) = 'Comment 9118583175c74201a22349d615de0fe8'
DECLARE @Rating INT = 820
DECLARE @InterviewID BIGINT = NULL
DECLARE @InterviewerID BIGINT = 100003
DECLARE @CreatedByID BIGINT = 100003
DECLARE @CreatedDate DATETIME = '3/26/2019 7:00:12 AM'
DECLARE @ModifiedByID BIGINT = 100003
DECLARE @ModifiedDate DATETIME = '2/3/2022 4:47:12 PM'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[InterviewFeedback]
				WHERE 
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Rating IS NOT NULL THEN (CASE WHEN [Rating] = @Rating THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN [InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewerID IS NOT NULL THEN (CASE WHEN [InterviewerID] = @InterviewerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[InterviewFeedback]
		(
	 [ID],
	 [Comment],
	 [Rating],
	 [InterviewID],
	 [InterviewerID],
	 [CreatedByID],
	 [CreatedDate],
	 [ModifiedByID],
	 [ModifiedDate]
		)
	SELECT 		
			 @ID,
	 @Comment,
	 @Rating,
	 @InterviewID,
	 @InterviewerID,
	 @CreatedByID,
	 @CreatedDate,
	 @ModifiedByID,
	 @ModifiedDate
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[InterviewFeedback] e
WHERE
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Rating IS NOT NULL THEN (CASE WHEN [Rating] = @Rating THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN [InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @InterviewerID IS NOT NULL THEN (CASE WHEN [InterviewerID] = @InterviewerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
