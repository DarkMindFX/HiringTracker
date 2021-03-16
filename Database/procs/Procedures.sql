USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_Upsert]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Candidate_Upsert]
	@CandidateID BIGINT ,
	@FirstName NVARCHAR(50) ,
	@MiddleName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Email NVARCHAR(50),
	@Phone NVARCHAR(50),
	@CVLink NVARCHAR(1000),
	@RecruiterID BIGINT,
	@Properties dbo.TYPE_Candidate_Property READONLY,
	@EditorUserID BIGINT = NULL,
	@UpdateDetails BIT = 1,
	@UpdateProperties BIT = 1

AS
BEGIN
	
	SET NOCOUNT ON;

    IF(@CandidateID IS NULL)
	BEGIN
		INSERT INTO dbo.Candidate
		SELECT @FirstName, @MiddleName, @LastName, @Email, @Phone, @CVLink, @RecruiterID, @EditorUserID, GETUTCDATE(), NULL, NULL

		SELECT @CandidateID = @@IDENTITY		

	END
	ELSE
	BEGIN
		IF(@UpdateDetails = 1) 
		BEGIN
			IF(EXISTS(SELECT 1 FROM dbo.Candidate WHERE CandidateID = @CandidateID))
			BEGIN
				UPDATE dbo.Candidate
				SET
					[FirstName] = @FirstName,
					[MiddleName] = @MiddleName,
					[LastName] = @LastName,
					[Email] = @Email,
					[Phone] = @Phone,
					[CVLink] = @CVLink,
					[RecruiterID] = @RecruiterID,
					[ModifiedByID] = @EditorUserID,
					[ModifiedDate] = GETUTCDATE()

 			END
			ELSE
			BEGIN
				THROW 51001, 'Candidate with given ID was not found', 1;
			END
		END
	END

	IF(@UpdateProperties = 1)
	BEGIN
		MERGE dbo.CandidateProperty as t 
		USING (SELECT @CandidateID as CandidateID, p.* FROM @Properties p) as s
		ON t.CandidateID = s.CandidateID AND t.[Name] = s.[Name]
		WHEN MATCHED THEN
			UPDATE SET
				t.[Value] = s.[Value]
		WHEN NOT MATCHED BY TARGET THEN
			INSERT ([CandidateID], [Name], [Value]) VALUES (s.[CandidateID], s.[Name], s.[Value]) 
		WHEN NOT MATCHED BY SOURCE THEN
			DELETE;		
	END
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetByInterview]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_InterviewRole_GetByInterview] 
	@InterviewID BIGINT
AS
BEGIN

	SET NOCOUNT ON;

    SELECT 
		ir.*,
		r.[Name] AS RoleName,
		u.[Login] AS InterviewerLogin,
		u.[FirstName] AS InterviewerFirstName,
		u.[LastName] AS InterviewerLastName
	FROM 
		dbo.InterviewRole ir
	INNER JOIN dbo.[Role] r ON ir.RoleID = r.RoleID 
	INNER JOIN dbo.[User] u ON ir.UserID = u.UserID 
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetByRole]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_InterviewRole_GetByRole] 
	@RoleID BIGINT
AS
BEGIN

	SET NOCOUNT ON;

    SELECT 
		ir.*,
		r.[Name] AS RoleName,
		u.[Login] AS InterviewerLogin,
		u.[FirstName] AS InterviewerFirstName,
		u.[LastName] AS InterviewerLastName
	FROM 
		dbo.InterviewRole ir
	INNER JOIN dbo.[Role] r ON ir.RoleID = r.RoleID 
	INNER JOIN dbo.[User] u ON ir.UserID = u.UserID 
	WHERE ir.RoleID = @RoleID
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetByUser]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_InterviewRole_GetByUser] 
	@UserID BIGINT
AS
BEGIN

	SET NOCOUNT ON;

    SELECT 
		ir.*,
		r.[Name] AS RoleName,
		u.[Login] AS InterviewerLogin,
		u.[FirstName] AS InterviewerFirstName,
		u.[LastName] AS InterviewerLastName
	FROM 
		dbo.InterviewRole ir
	INNER JOIN dbo.[Role] r ON ir.RoleID = r.RoleID 
	INNER JOIN dbo.[User] u ON ir.UserID = u.UserID 
	WHERE ir.UserID = @UserID
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_RemoveUserRole]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_InterviewRole_RemoveUserRole] 
	@InterviewID BIGINT,
	@UserID BIGINT
AS
BEGIN

	SET NOCOUNT ON;

    DELETE FROM dbo.InterviewRole	
	WHERE InterviewID = @InterviewID AND UserID = @UserID
	
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_UpsertUserRole]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_InterviewRole_UpsertUserRole] 
	@InterviewID BIGINT,
	@UserID BIGINT,
	@RoleID BIGINT
	
AS
BEGIN

	SET NOCOUNT ON;

    IF(NOT EXISTS(SELECT 1 FROM dbo.InterviewRole WHERE UserID = @UserID AND InterviewID = @InterviewID))
	BEGIN
		INSERT INTO dbo.InterviewRole (InterviewID, UserID, RoleID)
		VALUES (@InterviewID, @UserID, @RoleID)
	END
	ELSE
	BEGIN
		UPDATE dbo.InterviewRole 
		SET
			RoleID = @RoleID
		WHERE InterviewID = @InterviewID AND UserID = @UserID
	END
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_GetAll]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewStatus_GetAll] 	
AS
BEGIN
	
	SET NOCOUNT ON;
    
	SELECT 
		i.*
	FROM
		dbo.InterviewStatus i
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_Populate]    Script Date: 3/16/2021 10:05:58 PM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewType_GetAll]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewType_GetAll] 	
AS
BEGIN
	
	SET NOCOUNT ON;
    
	SELECT 
		it.*
	FROM
		dbo.InterviewType it
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewType_Populate]    Script Date: 3/16/2021 10:05:58 PM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Position_GetDetails]    Script Date: 3/16/2021 10:05:58 PM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Position_Upsert]    Script Date: 3/16/2021 10:05:58 PM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStatus_GetAll]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionCandidateStatus_GetAll] 	
AS
BEGIN
	
	SET NOCOUNT ON;
    
	SELECT 
		ps.*
	FROM
		dbo.PositionCandidateStatus ps
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStatus_Populate]    Script Date: 3/16/2021 10:05:58 PM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStep_GetAll]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionCandidateStep_GetAll] 	
AS
BEGIN
	
	SET NOCOUNT ON;
    
	SELECT 
		ps.*
	FROM
		dbo.PositionCandidateStep ps
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStep_Populate]    Script Date: 3/16/2021 10:05:58 PM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_Upsert]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_Upsert] 
	@PositionID BIGINT,
	@Skills TYPE_Position_Skills READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    MERGE dbo.PositionSkill t USING @Skills s
	ON (t.PositionID = @PositionID AND t.SkillID = s.SkillID)
	WHEN MATCHED THEN
		UPDATE SET
			t.IsMandatory = s.IsMandatory,
			t.SkillProficiencyID = s.ProficiencyID
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (PositionID, SkillId, IsMandatory, SkillProficiencyID)
		VALUES (@PositionID, s.SkillId, s.IsMandatory, s.ProficiencyID)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;
		
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_GetAll]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionStatus_GetAll] 	
AS
BEGIN
	
	SET NOCOUNT ON;
    
	SELECT 
		ps.*
	FROM
		dbo.PositionStatus ps
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_Populate]    Script Date: 3/16/2021 10:05:58 PM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Proficiency_Populate]    Script Date: 3/16/2021 10:05:58 PM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Role_Populate]    Script Date: 3/16/2021 10:05:58 PM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Skill_Delete]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Skill_Delete]
	@SkillID BIGINT,
	@Removed BIT OUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM dbo.Skill WHERE SkillID = @SkillID ) )
	BEGIN
		DELETE FROM dbo.Skill WHERE SkillID = @SkillID
		SET @Removed = 1

	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Skill_GetAll]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Skill_GetAll] 	
AS
BEGIN
	
	SET NOCOUNT ON;
    
	SELECT 
		sp.SkillID,
		sp.[Name]
	FROM
		dbo.Skill sp
END
GO
/****** Object:  StoredProcedure [dbo].[p_Skill_Upsert]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Skill_Upsert]
	@Skills [dbo].[TYPE_Skills] READONLY
AS
BEGIN
	
	SET NOCOUNT ON;
    
	MERGE dbo.Skill t USING @Skills s
	ON t.SkillID = s.SkillID
	WHEN MATCHED THEN
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED THEN
		INSERT ([Name]) VALUES (s.[Name]);
END
GO
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_GetAll]    Script Date: 3/16/2021 10:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_SkillProficiency_GetAll] 	
AS
BEGIN
	
	SET NOCOUNT ON;
    
	SELECT 
		sp.*
	FROM
		dbo.SkillProficiency sp
END
GO
