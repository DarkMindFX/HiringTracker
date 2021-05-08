
DECLARE @CandidateFirstName AS NVARCHAR(50) = '[Test] First MNY67DF4 UPDATED';
DECLARE @CandidateLastName AS NVARCHAR(50) = '[Test] Last MNY67DF4 UPDATED';
DECLARE @Email AS NVARCHAR(50) = 'Last_MNY67DF4@gmail.com';
DECLARE @NewCandidateID AS BIGINT
DECLARE @editorID AS BIGINT = 100002
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM dbo.Candidate c WHERE c.FirstName = @CandidateFirstName AND c.LastName = @CandidateLastName))
BEGIN
    SET @Fail = 1
END

DELETE FROM dbo.Candidate WHERE Email = @Email

IF(@Fail = 1) THROW 51001, '[Test Fail] Canidate was not updated', 1
