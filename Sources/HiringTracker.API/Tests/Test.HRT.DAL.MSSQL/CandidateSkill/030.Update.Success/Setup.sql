

DECLARE @CandidateID BIGINT = 100003
DECLARE @SkillID BIGINT = 12
DECLARE @SkillProficiencyID BIGINT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[CandidateSkill]
				WHERE 
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[CandidateSkill]
		(
	 [CandidateID],
	 [SkillID],
	 [SkillProficiencyID]
		)
	SELECT 		
			 @CandidateID,
	 @SkillID,
	 @SkillProficiencyID
END

SELECT TOP 1 
	@CandidateID = [CandidateID], 
	@SkillID = [SkillID]
FROM 
	[dbo].[CandidateSkill] e
WHERE
	(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN [CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@CandidateID, 
	@SkillID
