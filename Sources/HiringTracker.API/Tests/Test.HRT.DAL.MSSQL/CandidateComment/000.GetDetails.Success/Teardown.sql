

DECLARE @CandidateID BIGINT = 100001
DECLARE @CommentID BIGINT = 100007
 

DELETE FROM [CandidateComment]
FROM 
	[dbo].[CandidateComment] e
WHERE
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
