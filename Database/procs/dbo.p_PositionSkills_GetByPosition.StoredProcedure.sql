USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkills_GetByPosition]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkills_GetByPosition]
	@PositionID BIGINT,
	@Found BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF(EXISTS(SELECT 1 FROM dbo.Position p WHERE p.ID = @PositionID)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found	


		SELECT
			ps.*,
			s.[Name] as SkillName,
			sp.[Name] as SkillProficiency
		FROM
			dbo.PositionSkill ps
		INNER JOIN dbo.Skill s ON ps.SkillID = s.ID
		INNER JOIN dbo.SkillProficiency sp ON sp.ID = ps.SkillProficiencyID
		WHERE
			ps.PositionID = @PositionID
	END
	ELSE
	BEGIN
		SET @Found = 0; -- notifying that record was not found
	END
END
GO
