
DECLARE @CandidateFirstName AS NVARCHAR(50) = '[Test] First 45645645GF';
DECLARE @CandidateLastName AS NVARCHAR(50) = '[Test] Last 45645645GF';
DECLARE @Email AS NVARCHAR(50) = 'Last_45645645GF@gmail.com';
DECLARE @NewCandidateID AS BIGINT
DECLARE @editorID AS BIGINT = 100002

IF(NOT EXISTS(SELECT 1 FROM dbo.Candidate c WHERE c.FirstName = @CandidateFirstName AND LastName = @CandidateLastName))
BEGIN
    EXEC [dbo].[p_Candidate_Upsert]
        NULL,
        @CandidateFirstName,
        NULL,
        @CandidateLastName,
        @Email,
        NULL,
        'http://dropbox.com/cv/Last_45645645GF.pdf',
        @editorID,
        @NewCandidateID OUT
END