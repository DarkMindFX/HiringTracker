USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_Update]
			@PositionID BIGINT,
			@SkillID BIGINT,
			@IsMandatory BIT,
			@SkillProficiencyID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionSkill]
					WHERE 
												[PositionID] = @PositionID	AND
												[SkillID] = @SkillID	
							))
	BEGIN
		UPDATE [dbo].[PositionSkill]
		SET
									[PositionID] = IIF( @PositionID IS NOT NULL, @PositionID, [PositionID] ) ,
									[SkillID] = IIF( @SkillID IS NOT NULL, @SkillID, [SkillID] ) ,
									[IsMandatory] = IIF( @IsMandatory IS NOT NULL, @IsMandatory, [IsMandatory] ) ,
									[SkillProficiencyID] = IIF( @SkillProficiencyID IS NOT NULL, @SkillProficiencyID, [SkillProficiencyID] ) 
						WHERE 
												[PositionID] = @PositionID	AND
												[SkillID] = @SkillID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PositionSkill was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PositionSkill] e
	WHERE
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN e.[SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsMandatory IS NOT NULL THEN (CASE WHEN e.[IsMandatory] = @IsMandatory THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN e.[SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
