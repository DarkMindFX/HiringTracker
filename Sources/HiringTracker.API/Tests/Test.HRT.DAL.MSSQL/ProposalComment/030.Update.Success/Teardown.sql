

-- original values --
DECLARE @ProposalID BIGINT = 100006
DECLARE @CommentID BIGINT = 100002
 
-- updated values --

DECLARE @updProposalID BIGINT = 100006
DECLARE @updCommentID BIGINT = 100002
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ProposalComment]
				WHERE 
	(CASE WHEN @updProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @updProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @updCommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[ProposalComment]
	WHERE 
	(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[ProposalComment]
	WHERE 
	(CASE WHEN @updProposalID IS NOT NULL THEN (CASE WHEN [ProposalID] = @updProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCommentID IS NOT NULL THEN (CASE WHEN [CommentID] = @updCommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ProposalComment was not updated', 1
END