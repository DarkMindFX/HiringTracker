USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_GetBySkillID]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_GetBySkillID]

	@SkillID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateSkill] c 
				WHERE
					[SkillID] = @SkillID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateSkill] e
		WHERE 
			[SkillID] = @SkillID	

	END
	ELSE
		SET @Found = 0;
END
GO
