

DECLARE @CandidateID BIGINT = 100007
DECLARE @CommentID BIGINT = 100003
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[CandidateComment]
				WHERE 
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[CandidateComment]
		(
	 [CandidateID],
	 [CommentID]
		)
	SELECT 		
			 @CandidateID,
	 @CommentID
END

SELECT TOP 1 
	@CandidateID = [CandidateID], 
	@CommentID = [CommentID]
FROM 
	[dbo].[CandidateComment] e
WHERE
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@CandidateID, 
	@CommentID
