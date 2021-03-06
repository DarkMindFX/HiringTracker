USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_Update]
			@CandidateID BIGINT,
			@SkillID BIGINT,
			@SkillProficiencyID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM dbo.CandidateSkill 
					WHERE 
												[CandidateID] = @CandidateID	AND
												[SkillID] = @SkillID	
							))
	BEGIN
		UPDATE [dbo].[CandidateSkill]
		SET
									[CandidateID] = IIF( @CandidateID IS NOT NULL, @CandidateID, [CandidateID] ) ,
									[SkillID] = IIF( @SkillID IS NOT NULL, @SkillID, [SkillID] ) ,
									[SkillProficiencyID] = IIF( @SkillProficiencyID IS NOT NULL, @SkillProficiencyID, [SkillProficiencyID] ) 
						WHERE 
												[CandidateID] = @CandidateID	AND
												[SkillID] = @SkillID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'CandidateSkill was not found', 1;
	END	

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
