USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_GetBySkillProficiencyID]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_GetBySkillProficiencyID]

	@SkillProficiencyID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionSkill] c 
				WHERE
					[SkillProficiencyID] = @SkillProficiencyID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PositionSkill] e
		WHERE 
			[SkillProficiencyID] = @SkillProficiencyID	

	END
	ELSE
		SET @Found = 0;
END
GO
