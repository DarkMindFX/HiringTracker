

-- original values --
DECLARE @CandidateID BIGINT = 100001
DECLARE @SkillID BIGINT = 5
DECLARE @SkillProficiencyID BIGINT = 3
 
-- updated values --

DECLARE @updCandidateID BIGINT = 100001
DECLARE @updSkillID BIGINT = 5
DECLARE @updSkillProficiencyID BIGINT = 2
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[CandidateSkill]
				WHERE 
	(CASE WHEN @updCandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @updCandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @updSkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @updSkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[CandidateSkill]
	WHERE 
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[CandidateSkill]
	WHERE 
	(CASE WHEN @updCandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @updCandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @updSkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @updSkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'CandidateSkill was not updated', 1
END