USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_Upsert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_CandidateSkill_Upsert] 
	@CandidateID BIGINT,
	@Skills dbo.TYPE_Candidate_Skills READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE FROM dbo.CandidateSkill
	WHERE CandidateID = @CandidateID

	MERGE dbo.CandidateSkill t USING
	(SELECT @CandidateID as CandidateID, * FROM @Skills) s
	ON t.CandidateID = s.CandidateID AND t.SkillID = s.SkillID
	WHEN MATCHED THEN 
		UPDATE SET
			t.SkillProficiencyID = ProficiencyID 
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (CandidateID, SkillID, SkillProficiencyID)
		VALUES (@CandidateID, s.SkillID, s.ProficiencyID);
   
END
GO
