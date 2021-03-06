USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Positions]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_Positions]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testPositions AS TABLE (
		[PositionID] [bigint]  NOT NULL,
		[DepartmentID] [bigint] NULL,
		[Title] [nvarchar](50) NOT NULL,
		[ShortDesc] [nvarchar](250) NOT NULL,
		[Description] [nvarchar](max) NOT NULL,
		[StatusID] [bigint] NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[CreatedByID] [bigint] NOT NULL,
		[ModifiedDate] [datetime] NULL,
		[ModifiedByID] [bigint] NULL)

	DECLARE @idJoeBiden BIGINT = 100001
	DECLARE @idDonaldTrump BIGINT = 100002
	DECLARE @idBarakObama BIGINT = 100003

	DECLARE @positionId1 BIGINT = 100001
	DECLARE @positionId2 BIGINT = 100002
	DECLARE @positionId3 BIGINT = 100003
	DECLARE @positionId4 BIGINT = 100004
	DECLARE @positionId5 BIGINT = 100005
	DECLARE @positionId6 BIGINT = 100006
	DECLARE @positionId7 BIGINT = 100007
	DECLARE @positionId8 BIGINT = 100008
	DECLARE @positionId9 BIGINT = 100009

	INSERT INTO @testPositions
	SELECT @positionId1, NULL, 'Senior .NET Engineer', 'Looking for Senior .Net Engineer', 'Senior .NET Engineer - Lorm ipsum Log desc', 1, GETUTCDATE(), @idJoeBiden, NULL, NULL UNION
	SELECT @positionId2, NULL, 'Middle .NET Engineer', 'Looking for Middle .Net Engineer', 'Middle .NET Engineer - Lorm ipsum Log desc', 2, GETUTCDATE(), @idJoeBiden, NULL, NULL UNION
	SELECT @positionId3, NULL, 'Junior .NET Engineer', 'Looking for Junior .Net Engineer', 'Junior .NET Engineer - Lorm ipsum Log desc', 3, GETUTCDATE(), @idJoeBiden, NULL, NULL UNION
	SELECT @positionId4, NULL, 'Senior Java Dev', 'Senior Java Dev', 'Senior Java Dev - Lorm ipsum Log desc', 2, GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @positionId5, NULL, 'Middle Java Dev', 'Looking for Middle Java Dev', 'Middle Java Dev - Lorm ipsum Log desc', 2, GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @positionId6, NULL, 'Junior Java Dev', 'Looking for Junior Java Dev', 'Junior Java Dev - Lorm ipsum Log desc', 3, GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @positionId7, NULL, 'Principal .NET Engineer', 'Looking for Principal .NET Engineer', 'Principal .NET Engineer - Lorm ipsum Log desc', 4, GETUTCDATE(), @idBarakObama, NULL, NULL UNION
	SELECT @positionId8, NULL, 'Solutions Architect', 'Looking for Solutions Architect', 'Solutions Architect - Lorm ipsum Log desc', 3, GETUTCDATE(), @idBarakObama, NULL, NULL UNION
	SELECT @positionId9, NULL, 'Senior QA', 'Looking for Senior QA', 'Senior QA - Lorm ipsum Log desc', 4, GETUTCDATE(), @idBarakObama, NULL, NULL

	SET IDENTITY_INSERT dbo.[Position] ON

	MERGE dbo.[Position] t USING @testPositions s
	ON t.ID = s.PositionID
	WHEN MATCHED THEN
		UPDATE SET
			t.[DepartmentID] = s.[DepartmentID],
			t.[Title] = s.[Title],
			t.[ShortDesc] = s.[ShortDesc],
			t.[Description] = s.[Description],
			t.[StatusID] = s.[StatusID],
			t.[CreatedDate] = s.[CreatedDate],
			t.[CreatedByID] = s.[CreatedByID],
			t.[ModifiedDate] = s.[ModifiedDate],
			t.[ModifiedByID] = s.[ModifiedByID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [DepartmentID], [Title], [ShortDesc], [Description], [StatusID], [CreatedDate], [CreatedByID], [ModifiedDate], [ModifiedByID])
		VALUES (s.[PositionID], s.[DepartmentID], s.[Title], s.[ShortDesc], s.[Description], s.[StatusID], s.[CreatedDate], s.[CreatedByID], s.[ModifiedDate], s.[ModifiedByID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[Position] OFF

	DECLARE @Skills AS dbo.TYPE_Position_Skills

	-- ============ SETTING SKILLS ==========================
	DECLARE @posID AS BIGINT =  NULL
	-- POSITION 1
	DELETE FROM @Skills
	SET @posID = @positionId1

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('EXpert') UNION
	SELECT dbo.fn_Skill_GetIDByName('AZure Devops'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') 

	EXEC p_PositionSkill_Upsert @posID, @Skills

	-- POSITION 2
	DELETE FROM @Skills
	SET @posID = @positionId2

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('VB.NET'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('AZure Devops'), 0, dbo.fn_SkillProficiency_GetIDByName('Beginner') 

	EXEC p_PositionSkill_Upsert @posID, @Skills

	-- POSITION 3
	DELETE FROM @Skills
	SET @posID = @positionId3

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('Jenkins'), 0, dbo.fn_SkillProficiency_GetIDByName('Beginner') 

	EXEC p_PositionSkill_Upsert @posID, @Skills

	-- POSITION 4
	DELETE FROM @Skills
	SET @posID = @positionId4

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('KAFKA'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('SPRING'), 1, dbo.fn_SkillProficiency_GetIDByName('EXpert') UNION
	SELECT dbo.fn_Skill_GetIDByName('PL/SQL'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') 

	EXEC p_PositionSkill_Upsert @posID, @Skills

	-- POSITION 5
	DELETE FROM @Skills
	SET @posID = @positionId5

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('PYTHON'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('SPRING'), 1, dbo.fn_SkillProficiency_GetIDByName('EXpert') UNION
	SELECT dbo.fn_Skill_GetIDByName('ORacle'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') 

	EXEC p_PositionSkill_Upsert @posID, @Skills

	-- POSITION 6
	DELETE FROM @Skills
	SET @posID = @positionId6

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('ACTIVEMQ'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVASCRIPT'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('ORacle'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('Bamboo'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate')

	EXEC p_PositionSkill_Upsert @posID, @Skills	
	
	-- POSITION 7
	DELETE FROM @Skills
	SET @posID = @positionId7

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('AZure Devops'), 0, dbo.fn_SkillProficiency_GetIDByName('Expert') 

	EXEC p_PositionSkill_Upsert @posID, @Skills
	
END
GO
