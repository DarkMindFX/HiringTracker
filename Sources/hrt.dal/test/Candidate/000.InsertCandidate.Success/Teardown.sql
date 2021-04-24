
DECLARE @CandidateFirstName AS NVARCHAR(50) = '[Test] First 65TRF435G';
DECLARE @CandidateLastName AS NVARCHAR(50) = '[Test] Last 65TRF435G';


IF(EXISTS(SELECT 1 FROM dbo.Candidate WHERE
	FirstName = @CandidateFirstName AND
	LastName = @CandidateLastName ))
BEGIN
	DELETE FROM 
		dbo.Candidate 
	WHERE
		FirstName = @CandidateFirstName AND
		LastName = @CandidateLastName 
END
ELSE
BEGIN
	THROW 51001, '[TEST FAIL] Candidate was not created', 1
END
