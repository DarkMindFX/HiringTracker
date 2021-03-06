USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_Populate]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_SkillProficiency_Populate]	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @profs AS TABLE (
		ProficiencyID BIGINT,
		[Name] NVARCHAR(255)
	)

	INSERT INTO @profs
	SELECT 1, 'Beginner' UNION
	SELECT 2, 'Intermediate' UNION
	SELECT 3, 'Advanced' UNION
	SELECT 4, 'Expert' 

	SET IDENTITY_INSERT dbo.[SkillProficiency] ON;

	MERGE dbo.[SkillProficiency] t USING @profs s
	ON t.ID = s.ProficiencyID
	WHEN MATCHED THEN
		UPDATE SET t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Name]) 
		VALUES (s.[ProficiencyID], s.[Name]) 
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[SkillProficiency] OFF;
END
GO
