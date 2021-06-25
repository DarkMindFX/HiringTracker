

DECLARE @ID BIGINT = NULL
DECLARE @PositionID BIGINT = 100002
DECLARE @SkillID BIGINT = 10098
DECLARE @IsMandatory BIT = 1
DECLARE @SkillProficiencyID BIGINT = 2
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[PositionSkill]
				WHERE 
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsMandatory IS NOT NULL THEN (CASE WHEN [IsMandatory] = @IsMandatory THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[PositionSkill]
		(
	 [PositionID],
	 [SkillID],
	 [IsMandatory],
	 [SkillProficiencyID]
		)
	SELECT 		
			 @PositionID,
	 @SkillID,
	 @IsMandatory,
	 @SkillProficiencyID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[PositionSkill] e
WHERE
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsMandatory IS NOT NULL THEN (CASE WHEN [IsMandatory] = @IsMandatory THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
