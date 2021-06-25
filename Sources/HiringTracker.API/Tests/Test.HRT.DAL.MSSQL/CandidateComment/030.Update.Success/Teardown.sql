

-- original values --
DECLARE @CandidateID BIGINT = 110118
DECLARE @CommentID BIGINT = 100004
 
-- updated values --

DECLARE @updCandidateID BIGINT = 110118
DECLARE @updCommentID BIGINT = 100004
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[CandidateComment]
				WHERE 
	(CASE WHEN @updCandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @updCandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @updCommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[CandidateComment]
	WHERE 
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[CandidateComment]
	WHERE 
	(CASE WHEN @updCandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @updCandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @updCommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'CandidateComment was not updated', 1
END