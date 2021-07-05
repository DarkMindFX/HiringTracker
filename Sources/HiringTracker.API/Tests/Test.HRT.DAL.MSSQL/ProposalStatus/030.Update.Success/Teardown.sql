

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 1e743a16b4d541c3812ea9e99652ce0d'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updName NVARCHAR(50) = 'Name 4f21f0d918134d5eb10eebbb77ec6e01'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ProposalStatus]
				WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[ProposalStatus]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[ProposalStatus]
	WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ProposalStatus was not updated', 1
END