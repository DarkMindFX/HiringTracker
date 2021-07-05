

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 452d7b3bcd0b4355aede71f1c78f0327'
DECLARE @Value NVARCHAR(1000) = 'Value 452d7b3bcd0b4355aede71f1c78f0327'
DECLARE @CandidateID BIGINT = 100004
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[CandidateProperty]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN [Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[CandidateProperty]
		(
	 [Name],
	 [Value],
	 [CandidateID]
		)
	SELECT 		
			 @Name,
	 @Value,
	 @CandidateID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[CandidateProperty] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN [Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
