

DECLARE @ProposalID BIGINT = 100004
DECLARE @CommentID BIGINT = 100009
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[ProposalComment]
				WHERE 
	(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[ProposalComment]
	WHERE 
	(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ProposalComment was not deleted', 1
END