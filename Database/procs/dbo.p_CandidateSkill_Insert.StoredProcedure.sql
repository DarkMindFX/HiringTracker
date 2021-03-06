USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_Insert]
			@CandidateID BIGINT,
			@SkillID BIGINT,
			@SkillProficiencyID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[CandidateSkill]
	SELECT 
		@CandidateID,
		@SkillID,
		@SkillProficiencyID
	
	

	SELECT
		e.*
	FROM
		[dbo].[CandidateSkill] e
	WHERE
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN e.[SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN e.[SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
