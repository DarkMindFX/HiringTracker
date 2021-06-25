

DECLARE @CandidateID BIGINT = 100002
DECLARE @SkillID BIGINT = 7
DECLARE @SkillProficiencyID BIGINT = 4
 

DELETE FROM [CandidateSkill]
FROM 
	[dbo].[CandidateSkill] e
WHERE
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
