

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name c681049b72064e1dae9b337539d7c4b3'
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[ProposalStatus]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[ProposalStatus]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ProposalStatus was not deleted', 1
END