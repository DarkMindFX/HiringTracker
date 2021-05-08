
DECLARE @CandidateFirstName AS NVARCHAR(50) = '[Test] First 45645645GF';
DECLARE @CandidateLastName AS NVARCHAR(50) = '[Test] Last 45645645GF';
DECLARE @Email AS NVARCHAR(50) = 'Last_45645645GF@gmail.com';
DECLARE @NewCandidateID AS BIGINT
DECLARE @editorID AS BIGINT = 100002
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM dbo.Candidate c WHERE c.FirstName = @CandidateFirstName AND LastName = @CandidateLastName))
BEGIN
    SET @Fail = 1
END

DELETE FROM dbo.Candidate WHERE FirstName = @CandidateFirstName AND LastName = @CandidateLastName

IF(@Fail = 1) 
BEGIN
    THROW 51001, '[Test Fail] Canidate was not deleted', 1
END