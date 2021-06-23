

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 9465a526305c40fa83f361dd8e1c29aa'
DECLARE @Value NVARCHAR(1000) = 'Value 9465a526305c40fa83f361dd8e1c29aa'
DECLARE @CandidateID BIGINT = 100003
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updName NVARCHAR(50) = 'Name 046a2ed31cb04722a362518c5fac27c8'
DECLARE @updValue NVARCHAR(1000) = 'Value 046a2ed31cb04722a362518c5fac27c8'
DECLARE @updCandidateID BIGINT = 100007
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[CandidateProperty]
				WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updValue IS NOT NULL THEN (CASE WHEN [Value] = @updValue THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @updCandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[CandidateProperty]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN [Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[CandidateProperty]
	WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updValue IS NOT NULL THEN (CASE WHEN [Value] = @updValue THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @updCandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'CandidateProperty was not updated', 1
END