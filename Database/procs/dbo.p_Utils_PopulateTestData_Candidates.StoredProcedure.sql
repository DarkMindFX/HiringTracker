USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Candidates]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_Candidates]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testCandidates AS TABLE (
		[CandidateID] [bigint] NOT NULL,
		[FirstName] [nvarchar](50) NOT NULL,
		[MiddleName] [nvarchar](50) NULL,
		[LastName] [nvarchar](50) NOT NULL,
		[Email] [nvarchar](50) NOT NULL,
		[Phone] [nvarchar](50) NULL,
		[CVLink] [nvarchar](1000) NULL,
		[CreatedByID] [bigint] NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[ModifiedByID] [bigint] NULL,
		[ModifiedDate] [datetime] NULL)

	DECLARE @idJoeBiden BIGINT = 100001
	DECLARE @idDonaldTrump BIGINT = 100002
	DECLARE @idBarakObama BIGINT = 100003

	DECLARE @candidateId1 BIGINT = 100001
	DECLARE @candidateId2 BIGINT = 100002
	DECLARE @candidateId3 BIGINT = 100003
	DECLARE @candidateId4 BIGINT = 100004
	DECLARE @candidateId5 BIGINT = 100005
	DECLARE @candidateId6 BIGINT = 100006
	DECLARE @candidateId7 BIGINT = 100007
	DECLARE @candidateId8 BIGINT = 100008
	DECLARE @candidateId9 BIGINT = 100009

	INSERT INTO @testCandidates
	SELECT @candidateId1, 'George', NULL, 'Washington', 'gw@georgy.com',  '+12355567566', 'http://dropbox.com/cv/georgewashington.pdf',	@idJoeBiden, GETUTCDATE(), NULL, NULL UNION
	SELECT @candidateId2, 'John', NULL, 'Adams', 'johnadams@gmail.com',  NULL, 'http://dropbox.com/cv/johnadams.pdf',			@idJoeBiden, GETUTCDATE(), NULL, NULL UNION
	SELECT @candidateId3, 'Thomas', NULL, 'Jefferson', 'tjeff@whitehouse.com',  NULL, 'http://dropbox.com/cv/jefferson.pdf',	@idJoeBiden, GETUTCDATE(), NULL, NULL UNION
	SELECT @candidateId4, 'James', NULL, 'Madison', 'jmpresident@cool.guy',  NULL, 'http://dropbox.com/cv/jamesmedison.pdf',	@idBarakObama, GETUTCDATE(), NULL, NULL UNION
	SELECT @candidateId5, 'James', NULL, 'Monroe', 'monroe4president@cool.guy',  '+15556566', 'http://dropbox.com/cv/monroecv.pdf',	@idBarakObama, GETUTCDATE(), NULL, NULL UNION
	SELECT @candidateId6, 'John', 'Quincy', 'Adams', 'jqadams@whitehouse.biz',  '+12355556566', 'http://dropbox.com/cv/jqadamscv.pdf',	@idBarakObama, GETUTCDATE(), NULL, NULL UNION
	SELECT @candidateId7, 'Martin', 'Van', 'Buren', 'm_v_buren@gmail.com',  NULL, 'http://dropbox.com/cv/m_v_buren@gmail.com',	@idDonaldTrump, GETUTCDATE(), NULL, NULL UNION
	SELECT @candidateId8, 'William', 'Henry', 'Harrison', 'wharrison4president@usa.guy',  NULL, 'http://dropbox.com/cv/cv_wharrison4president.pdf',	@idDonaldTrump, GETUTCDATE(), NULL, NULL UNION
	SELECT @candidateId9, 'John', NULL, 'Tyler', 'tyler_da_cool@whitehouse.biz',  NULL, 'http://dropbox.com/cv/cv_tyler_da_cool.pdf',	@idDonaldTrump, GETUTCDATE(), NULL, NULL 

	SET IDENTITY_INSERT dbo.[Candidate] ON

	MERGE dbo.[Candidate] t USING @testCandidates s
	ON t.ID = s.CandidateID
	WHEN MATCHED THEN
		UPDATE SET
			t.[FirstName] = s.[FirstName],
			t.[MiddleName] = s.[MiddleName],
			t.[LastName] = s.[LastName],
			t.[Email] = s.[Email],
			t.[Phone] = s.[Phone],
			t.[CVLink] = s.[CVLink],
			t.[CreatedDate] = s.[CreatedDate],
			t.[CreatedByID] = s.[CreatedByID],
			t.[ModifiedDate] = s.[ModifiedDate],
			t.[ModifiedByID] = s.[ModifiedByID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [FirstName], [MiddleName], [LastName], [Email], [Phone], [CVLink], [CreatedDate], [CreatedByID], [ModifiedDate], [ModifiedByID])
		VALUES (s.[CandidateID], s.[FirstName], s.[MiddleName], s.[LastName], s.[Email], s.[Phone], s.[CVLink], s.[CreatedDate], s.[CreatedByID], s.[ModifiedDate], s.[ModifiedByID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[Candidate] OFF

	DECLARE @Skills AS dbo.TYPE_Candidate_Skills

	-- ============ SETTING SKILLS ==========================
	DECLARE @candID AS BIGINT =  NULL
	-- CANDIDATE 1
	DELETE FROM @Skills
	SET @candID = @candidateId1

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('EXpert') UNION
	SELECT dbo.fn_Skill_GetIDByName('AZure Devops'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') 

	EXEC p_CandidateSkill_Upsert @candID, @Skills

	-- CANDIDATE 2
	DELETE FROM @Skills
	SET @candID = @candidateId2

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('VB.NET'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('AZure Devops'), 0, dbo.fn_SkillProficiency_GetIDByName('Beginner') 

	EXEC p_CandidateSkill_Upsert @candID, @Skills

	-- CANDIDATE 3
	DELETE FROM @Skills
	SET @candID = @candidateId3

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('Jenkins'), 0, dbo.fn_SkillProficiency_GetIDByName('Beginner') 

	EXEC p_CandidateSkill_Upsert @candID, @Skills

	-- CANDIDATE 4
	DELETE FROM @Skills
	SET @candID = @candidateId4

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('KAFKA'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('SPRING'), 1, dbo.fn_SkillProficiency_GetIDByName('EXpert') UNION
	SELECT dbo.fn_Skill_GetIDByName('PL/SQL'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') 

	EXEC p_CandidateSkill_Upsert @candID, @Skills

	-- CANDIDATE 5
	DELETE FROM @Skills
	SET @candID = @candidateId5

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('PYTHON'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('SPRING'), 1, dbo.fn_SkillProficiency_GetIDByName('EXpert') UNION
	SELECT dbo.fn_Skill_GetIDByName('ORacle'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') 

	EXEC p_CandidateSkill_Upsert @candID, @Skills

	-- CANDIDATE 6
	DELETE FROM @Skills
	SET @candID = @candidateId5

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('ACTIVEMQ'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVASCRIPT'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('ORacle'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('Bamboo'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate')

	EXEC p_CandidateSkill_Upsert @candID, @Skills	

	-- CANDIDATE 7
	DELETE FROM @Skills
	SET @candID = @candidateId7

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('AZure Devops'), 0, dbo.fn_SkillProficiency_GetIDByName('Expert') 

	EXEC p_CandidateSkill_Upsert @candID, @Skills

	-- CANDIDATE 8
	DELETE FROM @Skills
	SET @candID = @candidateId8

	INSERT INTO @Skills

	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 0, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('Jenkins'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('Bamboo'), 0, dbo.fn_SkillProficiency_GetIDByName('Expert') 

	EXEC p_CandidateSkill_Upsert @candID, @Skills

	-- CANDIDATE 9
	DELETE FROM @Skills
	SET @candID = @candidateId9

	INSERT INTO @Skills

	SELECT dbo.fn_Skill_GetIDByName('C++'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('C#'), 0, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('Java'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 0, dbo.fn_SkillProficiency_GetIDByName('Advanced') 

	EXEC p_CandidateSkill_Upsert @candID, @Skills

END
GO
