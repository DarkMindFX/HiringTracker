

-- original values --
DECLARE @PositionID BIGINT = 100005
DECLARE @SkillID BIGINT = 13
DECLARE @IsMandatory BIT = 1
DECLARE @SkillProficiencyID BIGINT = 4
 
-- updated values --

DECLARE @updPositionID BIGINT = 100005
DECLARE @updSkillID BIGINT = 13
DECLARE @updIsMandatory BIT = 1
DECLARE @updSkillProficiencyID BIGINT = 2
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[PositionSkill]
				WHERE 
	(CASE WHEN @updPositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @updPositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @updSkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsMandatory IS NOT NULL THEN (CASE WHEN [IsMandatory] = @updIsMandatory THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @updSkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[PositionSkill]
	WHERE 
	(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsMandatory IS NOT NULL THEN (CASE WHEN [IsMandatory] = @IsMandatory THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[PositionSkill]
	WHERE 
	(CASE WHEN @updPositionID IS NOT NULL THEN (CASE WHEN [PositionID] = @updPositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSkillID IS NOT NULL THEN (CASE WHEN [SkillID] = @updSkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsMandatory IS NOT NULL THEN (CASE WHEN [IsMandatory] = @updIsMandatory THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSkillProficiencyID IS NOT NULL THEN (CASE WHEN [SkillProficiencyID] = @updSkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'PositionSkill was not updated', 1
END