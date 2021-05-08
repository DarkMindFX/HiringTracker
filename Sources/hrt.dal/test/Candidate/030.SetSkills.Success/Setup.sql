

DECLARE @CandidateFirstName AS NVARCHAR(50) = '[Test] First 89757WEG67';
DECLARE @CandidateLastName AS NVARCHAR(50) = '[Test] Last 89757WEG67';
DECLARE @Email AS NVARCHAR(50) = 'Last_89757WEG67@gmail.com';
DECLARE @NewCandidateID AS BIGINT
DECLARE @editorID AS BIGINT = 100002
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM dbo.Candidate c WHERE c.FirstName = @CandidateFirstName AND LastName = @CandidateLastName))
BEGIN
    EXEC [dbo].[p_Candidate_Upsert]
        NULL,
        @CandidateFirstName,
        NULL,
        @CandidateLastName,
        @Email,
        NULL,
        'http://dropbox.com/cv/Last_89757WEG67.pdf',
        @editorID,
        @NewCandidateID OUT
END