

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 9fb8ea83912048e389a8c59376de7a2d'
 
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
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
	THROW 51001, 'ProposalStatus was not inserted', 1
END