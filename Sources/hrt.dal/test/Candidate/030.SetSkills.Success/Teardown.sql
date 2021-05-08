
DECLARE @CandidateFirstName AS NVARCHAR(50) = '[Test] First 89757WEG67';
DECLARE @CandidateLastName AS NVARCHAR(50) = '[Test] Last 89757WEG67';
DECLARE @Email AS NVARCHAR(50) = 'Last_89757WEG67@gmail.com';
DECLARE @NewCandidateID AS BIGINT
DECLARE @editorID AS BIGINT = 100002
DECLARE @Fail AS BIT = 0

DELETE FROM dbo.Candidate WHERE Email = @Email


