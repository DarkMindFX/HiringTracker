

DECLARE @ProposalID BIGINT = 100001
DECLARE @CommentID BIGINT = 100007
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ProposalComment]
				WHERE 
	(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[ProposalComment]
		(
	 [ProposalID],
	 [CommentID]
		)
	SELECT 		
			 @ProposalID,
	 @CommentID
END

SELECT TOP 1 @ID = [ID] 
FROM 
	[dbo].[ProposalComment] e
WHERE
	(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT @ID