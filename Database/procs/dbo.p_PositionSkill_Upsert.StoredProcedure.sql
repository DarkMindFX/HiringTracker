USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_Upsert]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_PositionSkill_Upsert] 
	@PositionID BIGINT,
	@Skills dbo.TYPE_Position_Skills READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE FROM dbo.PositionSkill
	WHERE PositionID = @PositionID

	MERGE dbo.PositionSkill t USING
	(SELECT @PositionID as PositionID, * FROM @Skills) s
	ON t.PositionID = s.PositionID AND t.SkillID = s.SkillID
	WHEN MATCHED THEN 
		UPDATE SET
			t.SkillProficiencyID = s.ProficiencyID,
			t.IsMandatory = s.IsMandatory
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (PositionID, SkillID, SkillProficiencyID, IsMandatory)
		VALUES(@PositionID, s.SkillID, s.ProficiencyID, s.IsMandatory);
   
END
GO
