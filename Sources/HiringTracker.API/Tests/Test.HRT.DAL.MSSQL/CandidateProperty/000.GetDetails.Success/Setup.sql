

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name ddd7213b9da947e4b3b56e06e17f314d'
DECLARE @Value NVARCHAR(1000) = 'Value ddd7213b9da947e4b3b56e06e17f314d'
DECLARE @CandidateID BIGINT = 110125
 


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
