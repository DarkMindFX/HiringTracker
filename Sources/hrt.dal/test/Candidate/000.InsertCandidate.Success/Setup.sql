
DECLARE @CandidateFirstName AS NVARCHAR(50) = '[Test] First 65TRF435G';
DECLARE @CandidateLastName AS NVARCHAR(50) = '[Test] Last 65TRF435G';


DELETE FROM dbo.Candidate 
WHERE
	FirstName = @CandidateFirstName AND LastName = @CandidateLastName