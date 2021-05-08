
DECLARE @CandidateFirstName AS NVARCHAR(50) = '[Test] First MNY67DF4';
DECLARE @CandidateLastName AS NVARCHAR(50) = '[Test] Last MNY67DF4';
DECLARE @Email AS NVARCHAR(50) = 'Last_MNY67DF4@gmail.com';
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
        'http://dropbox.com/cv/Last_MNY67DF4.pdf',
        @editorID,
        @NewCandidateID OUT
END