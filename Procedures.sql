USE [SACHiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_Populate]    Script Date: 3/14/2021 11:45:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewStatus_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @statuses AS TABLE (
		[StatusID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @statuses
	SELECT 1, 'Scheduled' UNION
	SELECT 2, 'In Progress' UNION
	SELECT 3, 'Waiting Feedback' UNION
	SELECT 4, 'Completed' UNION
	SELECT 5, 'Cancelled' 


	MERGE dbo.InterviewStatus t USING @statuses s 
	ON
		t.[StatusID] = s.[StatusID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([StatusID], [Name]) VALUES (s.[StatusID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;





END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewType_Populate]    Script Date: 3/14/2021 11:45:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewType_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @type AS TABLE (
		[TypeID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @type
	SELECT 1, 'Screening'	UNION
	SELECT 2, 'Technical'	UNION
	SELECT 3, 'HR'			UNION
	SELECT 4, 'Offer'		UNION
	SELECT 5, 'Follow-up' 


	MERGE dbo.InterviewType t USING @type s 
	ON
		t.[TypeID] = s.[TypeID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([TypeID], [Name]) VALUES (s.[TypeID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;



END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetDetails]    Script Date: 3/14/2021 11:45:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_GetDetails]
	@PositionID BIGINT,
	@Found BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF(EXISTS(SELECT 1 FROM dbo.Position p WHERE p.PositionID = @PositionID)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found

		SELECT 
			p.*,
			u_created.[Login] as 'CreatedBy',
			u_modified.[Login] as 'ModifiedBy'
		FROM
			dbo.Position p
		INNER JOIN dbo.[User] u_created ON u_created.UserID = p.CreatedByID
		LEFT JOIN dbo.[User] u_modified ON u_modified.UserID = p.ModifiedByID
		WHERE
			p.PositionID = @PositionID


		SELECT
			ps.*,
			s.[Name] as SkillName,
			sp.[Name] as SkillProficiency
		FROM
			dbo.PositionSkill ps
		INNER JOIN dbo.Skill s ON ps.SkillID = s.SkillID
		INNER JOIN dbo.SkillProficiency sp ON sp.ProficiencyID = ps.SkillProficiencyID
		WHERE
			ps.PositionID = @PositionID
	END
	ELSE
	BEGIN
		SET @Found = 0; -- notifying that record was not found
	END
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_Upsert]    Script Date: 3/14/2021 11:45:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_Position_Upsert]
	@PositionID BIGINT OUTPUT,
	@DepartmentID BIGINT,
	@Title NVARCHAR(50),
	@ShortDesc NVARCHAR(250),
	@Description NVARCHAR(MAX),
	@StatusID BIGINT,	

	@Skills [dbo].[TYPE_Position_Skills] READONLY,

	@ChangedByUserID BIGINT
	
AS
BEGIN
	SET NOCOUNT ON;

	IF( @PositionID IS NOT NULL )
	BEGIN
		IF(EXISTS( SELECT 1 FROM dbo.Position p WHERE p.PositionID = @PositionID ))
		UPDATE dbo.Position SET
			Title = @Title,
			ShortDesc = @ShortDesc,
			[Description] = @Description,
			StatusID = @StatusID,
			ModifiedDate = GETUTCDATE(),
			ModifiedByID = @ChangedByUserID,
			DepartmentID = @DepartmentID
		WHERE
			PositionID = @PositionID
		ELSE
			THROW 51001, 'Position with given id not found', 1
	END
	ELSE
	BEGIN
		INSERT INTO dbo.Position
		SELECT @DepartmentID, @Title, @ShortDesc, @Description, @StatusID, GETUTCDATE(), @ChangedByUserID, NULL, NULL
		
		SELECT @PositionID = @@IDENTITY
	END

	MERGE dbo.PositionSkill t 
	USING (SELECT @PositionID as PositionID,
			skills.SkillID as SkillID,
			skills.IsMandatory as IsMandatory,
			skills.ProficiencyID as ProficiencyID
			FROM @Skills skills) s
	ON (t.PositionID = s.PositionID and t.SkillID = s.SkillID)
	WHEN MATCHED THEN
		UPDATE SET
			t.IsMandatory = s.IsMandatory,
			t.SkillProficiencyID = s.ProficiencyID
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (PositionID, SkillId, IsMandatory, SkillProficiencyID)
		VALUES (s.PositionID, s.SkillId, s.IsMandatory, s.ProficiencyID)
	WHEN NOT MATCHED BY SOURCE 
		THEN DELETE;
		

END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStatus_Populate]    Script Date: 3/14/2021 11:45:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionCandidateStatus_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @statuses AS TABLE (
		[StatusID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @statuses
	SELECT 1, 'Processing' UNION
	SELECT 2, 'On Hold' UNION
	SELECT 3, 'Rejected' UNION
	SELECT 4, 'Onboarding' 


	MERGE dbo.PositionCandidateStatus t USING @statuses s 
	ON
		t.[StatusID] = s.[StatusID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([StatusID], [Name]) VALUES (s.[StatusID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;





END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStep_Populate]    Script Date: 3/14/2021 11:45:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionCandidateStep_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @steps AS TABLE (
		[StepID]				BIGINT,
		[Name]					NVARCHAR(50),
		[ReqDueDate]			BIT,
		[RequiresRespInDays]	INT
	)

	INSERT INTO @steps
	SELECT 1, 'CV Review', 1, 1 UNION
	SELECT 2, 'Screen Interview', 1, 1 UNION
	SELECT 3, 'Screen Interview Feedback', 1, 1 UNION
	SELECT 4, 'Technical Task', 0, 0 UNION
	SELECT 5, 'Technical Feedback', 0, 0 UNION
	SELECT 6, 'Panel Interview', 0, 0 UNION
	SELECT 7, 'Panel Interview Feedback', 0, 0 UNION
	SELECT 8, 'Debrief', 0, 0 UNION
	SELECT 9, 'Debrief Feedback', 1, 1 UNION
	SELECT 10, 'Offer', 0, 0 UNION
	SELECT 11, 'Onboarding', 0, 0

	MERGE dbo.PositionCandidateStep t USING @steps s 
	ON
		t.StepID = s.StepID
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name],
			t.[ReqDueDate] = s.[ReqDueDate],
			t.[RequiresRespInDays] = s.[RequiresRespInDays]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([StepID], [Name], [ReqDueDate], [RequiresRespInDays]) VALUES (s.[StepID], s.[Name], s.[ReqDueDate], s.[RequiresRespInDays])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;





END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_Populate]    Script Date: 3/14/2021 11:45:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionStatus_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @statuses AS TABLE (
		[StatusID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @statuses
	SELECT 1, 'Draft' UNION
	SELECT 2, 'Open' UNION
	SELECT 3, 'On Hold' UNION
	SELECT 4, 'Closed' UNION
	SELECT 5, 'Cancelled' 


	MERGE dbo.PositionStatus t USING @statuses s 
	ON
		t.[StatusID] = s.[StatusID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([StatusID], [Name]) VALUES (s.[StatusID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;





END
GO
/****** Object:  StoredProcedure [dbo].[p_Proficiency_Populate]    Script Date: 3/14/2021 11:45:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proficiency_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @prof AS TABLE (
		[ProficiencyID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @prof
	SELECT 1, 'Beginner'			UNION
	SELECT 2, 'Intermediate'			UNION
	SELECT 3, 'Advanced'	UNION
	SELECT 4, 'Expert'	


	MERGE dbo.SkillProficiency t USING @prof s 
	ON
		t.[ProficiencyID] = s.[ProficiencyID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ProficiencyID], [Name]) VALUES (s.[ProficiencyID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Role_Populate]    Script Date: 3/14/2021 11:45:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Role_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @role AS TABLE (
		[RoleID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @role
	SELECT 1, 'Admin'			UNION
	SELECT 2, 'Viewer'			UNION
	SELECT 3, 'Position Owner'	UNION
	SELECT 4, 'Candidate Owner'	UNION
	SELECT 5, 'Interviewer' 


	MERGE dbo.[Role] t USING @role s 
	ON
		t.[RoleID] = s.[RoleID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([RoleID], [Name]) VALUES (s.[RoleID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;



END
GO
