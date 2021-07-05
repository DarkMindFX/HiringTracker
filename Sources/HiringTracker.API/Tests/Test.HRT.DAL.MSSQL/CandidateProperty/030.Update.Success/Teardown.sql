

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 452d7b3bcd0b4355aede71f1c78f0327'
DECLARE @Value NVARCHAR(1000) = 'Value 452d7b3bcd0b4355aede71f1c78f0327'
DECLARE @CandidateID BIGINT = 100004
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updName NVARCHAR(50) = 'Name afd740f712c34083bbd38a38a4d2ba16'
DECLARE @updValue NVARCHAR(1000) = 'Value afd740f712c34083bbd38a38a4d2ba16'
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