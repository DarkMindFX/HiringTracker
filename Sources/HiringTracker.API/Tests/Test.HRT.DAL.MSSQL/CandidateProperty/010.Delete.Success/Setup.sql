

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 6617a9423d1e4e90a141e3ecb94fa79b'
DECLARE @Value NVARCHAR(1000) = 'Value 6617a9423d1e4e90a141e3ecb94fa79b'
DECLARE @CandidateID BIGINT = 100009
 


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
