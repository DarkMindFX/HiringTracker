

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 424e56b32e6a4a9cb42b2f86b4c75e5a'
 
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