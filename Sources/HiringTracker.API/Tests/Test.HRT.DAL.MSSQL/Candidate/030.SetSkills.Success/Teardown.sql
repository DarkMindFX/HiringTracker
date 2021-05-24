DECLARE @Email AS NVARCHAR(50) = 'test_C6E0BFF2A9584726ACA898DBF51A3AD9@email.com'
DECLARE @SkillsCount AS BIGINT = 0
DECLARE @Fail AS BIT = 0

SELECT @SkillsCount = COUNT(1) FROM dbo.CandidateSkill ps
INNER JOIN dbo.Candidate p ON ps.CandidateID = p.CandidateID
WHERe p.Email = @Email

IF(@SkillsCount <> 3)
SET @Fail = 1

DELETE FROM dbo.Candidate WHERE Email = @Email

IF(@Fail = 1)
BEGIN
	THROW 51001, 'TEST FAILED: Failed to set candidate skills', 1;
END