USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_Insert]
			@PositionID BIGINT,
			@SkillID BIGINT,
			@IsMandatory BIT,
			@SkillProficiencyID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PositionSkill]
	SELECT 
		@PositionID,
		@SkillID,
		@IsMandatory,
		@SkillProficiencyID
	
	

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
