USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_Delete]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Candidate_Delete]
	@CandidateID BIGINT,
	@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;

   SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM dbo.Candidate WHERE CandidateID = @CandidateID ) )
	BEGIN
		DELETE FROM dbo.Candidate WHERE CandidateID = @CandidateID
		SET @Removed = 1

	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_GetDetails]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Candidate_GetDetails] 
	@CandidateID BIGINT,
	@Found BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;

	IF(EXISTS(SELECT 1 FROM dbo.Candidate c WHERE c.CandidateID = @CandidateID)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found

		-- Table 1 - candidate date
		SELECT 
			c.*,
			u_created.[Login] as 'CreatedBy',
			u_modified.[Login] as 'ModifiedBy'
		FROM
			dbo.Candidate c
		INNER JOIN dbo.[User] u_created ON u_created.UserID = c.CreatedByID
		LEFT JOIN dbo.[User] u_modified ON u_modified.UserID = c.ModifiedByID
		WHERE
			c.CandidateID = @CandidateID

		-- Table 2 - candidate properties
		SELECT
			cp.*
		FROM
			dbo.CandidateProperty cp
		WHERE CandidateID = @CandidateID
	END
	ELSE
		SET @Found = 0;
		
END
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_Upsert]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Candidate_Upsert]
	@CandidateID BIGINT OUTPUT,
	@FirstName NVARCHAR(50) ,
	@MiddleName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Email NVARCHAR(50),
	@Phone NVARCHAR(50),
	@CVLink NVARCHAR(1000),
	@RecruiterID BIGINT,
	@EditorUserID BIGINT

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
			WHERE	
					[CandidateID] = @CandidateID

 		END
		ELSE
		BEGIN
			THROW 51001, 'Candidate with given ID was not found', 1;
		END
		
	END


END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateProperties_Upsert]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_CandidateProperties_Upsert] 
	@CandidateID BIGINT,
	@Properties dbo.TYPE_Candidate_Property READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

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
GO
/****** Object:  StoredProcedure [dbo].[p_Comment_Upsert]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Comment_Upsert] 
	@CommentID BIGINT OUTPUT,
	@Text NVARCHAR(MAX),
	@EditorUserID BIGINT

AS
BEGIN
	
	SET NOCOUNT ON;

    IF(@CommentID IS NOT NULL)
	BEGIN
		IF(EXISTS(SELECT 1 FROM dbo.Comment c WHERE c.CommentID = @CommentID))
			UPDATe dbo.Comment 
			SET
				[Text] = @Text,
				[ModifiedDate] = GETUTCDATE(),
				[ModifiedByID] = @EditorUserID
			WHERE
				[CommentID] = @CommentID
		ELSE
		BEGIN
			DECLARE @Error AS NVARCHAR(250)
			SET @Error = N'Comment not found: CommentID = ' + @CommentID;
			THROW 51001, @Error, 1
		END

	END
	ELSE
	BEGIN
		INSERT INTO dbo.Comment
		SELECT @Text, GETUTCDATE(), @EditorUserID, NULL, NULL

		SELECT @CommentID = @@IDENTITY
	END

END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetByInterview]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetByRole]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetByUser]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_RemoveUserRole]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_UpsertUserRole]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_GetAll]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_Populate]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewType_GetAll]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewType_Populate]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Position_Delete]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Position_Delete] 
	@PositionID BIGINT,
	@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;

    IF( EXISTS( SELECT 1 FROM dbo.Position WHERE PositionID = @PositionID ) )
	BEGIN
		DELETE FROM dbo.Position WHERE PositionID = @PositionID
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetAll]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
			p.*,
			u_created.[Login] as 'CreatedBy',
			u_modified.[Login] as 'ModifiedBy'
	FROM
			dbo.Position p
	INNER JOIN dbo.[User] u_created ON u_created.UserID = p.CreatedByID
	LEFT JOIN dbo.[User] u_modified ON u_modified.UserID = p.ModifiedByID
		
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetDetails]    Script Date: 3/20/2021 11:10:56 AM ******/
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
	
	END
	ELSE
	BEGIN
		SET @Found = 0; -- notifying that record was not found
	END
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_Upsert]    Script Date: 3/20/2021 11:10:56 AM ******/
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

END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionCandidates_Remove]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_PositionCandidates_Remove]
	@PositionID bigint,
	@CandidateID bigint,
	@EditorUserID BIGINT,
	@Unassigned BIT OUTPUT
AS
BEGIN
	IF(EXISTS(SELECT 1 FROM dbo.PositionCandidates ps WHERE ps.CandidateID = @CandidateID AND ps.PositionID = @PositionID ))
	BEGIN
		DELETE ps FROM dbo.PositionCandidates ps WHERE ps.CandidateID = @CandidateID AND ps.PositionID = @PositionID
		SET @Unassigned = 1
	END
	ELSE
		SET @Unassigned = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionCandidates_Upsert]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_PositionCandidates_Upsert]
	@ProposalID bigint OUTPUT,
	@PositionID bigint,
	@CandidateID bigint,
	@Proposed datetime,
	@CurrentStepID bigint,
	@NextStepID bigint,
	@DueDate datetime,
	@StatusID bigint,
	@EditorUserID BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @Error AS NVARCHAR(250)

	IF(@ProposalID IS NOT NULL)
	BEGIN
		IF(EXISTS(SELECT 1 FROM dbo.PositionCandidates ps WHERE ps.ProposalID = @ProposalID ))
		BEGIN
			UPDATE dbo.PositionCandidates
			SET
				Proposed = @Proposed,
				CurrentStepID = @CurrentStepID,
				NextStepID = @NextStepID,
				DueDate = @DueDate,
				StatusID = @StatusID,
				ModifiedByID = @EditorUserID,
				ModifiedDate = GETUTCDATE()
			WHERE
				ProposalID = @PositionID
		END
		ELSE
		BEGIN
			SET @Error = N'Proposal with given ID was not found. PropisalID = ' + @ProposalID;
			THROW 51001, @Error, 1
		END
	END
	ELSE
	BEGIN

		IF(NOT EXISTS(SELECT 1 FROM dbo.PositionCandidates ps WHERE ps.CandidateID = @CandidateID AND ps.PositionID = @PositionID ))
		BEGIN
			INSERT INTO dbo.PositionCandidates
			SELECT	@PositionID,
					@CandidateID,
					@Proposed,
					@CurrentStepID,
					GETUTCDATE(),
					@NextStepID,
					@DueDate,
					@StatusID,
					@EditorUserID,
					GETUTCDATE(),
					NULL,
					NULL

			SELECT @ProposalID = @@IDENTITY
		END
		ELSE
		BEGIN
			SET @Error = N'Candidate is already assigned to this position. PositionID = ' + @PositionID + ', CandidateID = ' + @CandidateID;
			THROW 51001, @Error, 1
		END		

	END

	
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStatus_GetAll]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStatus_Populate]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStep_GetAll]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionCandidateStep_Populate]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionSkills_GetByPosition]    Script Date: 3/20/2021 11:10:56 AM ******/
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

	IF(EXISTS(SELECT 1 FROM dbo.Position p WHERE p.PositionID = @PositionID)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found	


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
/****** Object:  StoredProcedure [dbo].[p_PositionSkills_Upsert]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkills_Upsert] 
	@PositionID BIGINT,
	@Skills TYPE_Position_Skills READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    MERGE dbo.PositionSkill t 
	USING 
	(
		SELECT @PositionID as PositionID, * FROM @Skills
	) s
	ON (t.PositionID = s.PositionID AND t.SkillID = s.SkillID)
	WHEN MATCHED AND t.PositionID = @PositionID THEN
		UPDATE SET
			t.IsMandatory = s.IsMandatory,
			t.SkillProficiencyID = s.ProficiencyID
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (PositionID, SkillId, IsMandatory, SkillProficiencyID)
		VALUES (@PositionID, s.SkillId, s.IsMandatory, s.ProficiencyID)
	WHEN NOT MATCHED BY SOURCE  AND t.PositionID = @PositionID THEN
		DELETE;
		
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_GetAll]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_Populate]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Proficiency_Populate]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Role_Populate]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Skill_Delete]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Skill_GetAll]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Skill_Upsert]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_GetAll]    Script Date: 3/20/2021 11:10:56 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_Populate]    Script Date: 3/20/2021 11:10:56 AM ******/
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

	MERGE dbo.[SkillProficiency] t USING @profs s
	ON t.ProficiencyID = s.ProficiencyID
	WHEN MATCHED THEN
		UPDATE SET t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ProficiencyID], [Name]) 
		VALUES (s.[ProficiencyID], s.[Name]) 
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;
END
GO
/****** Object:  StoredProcedure [dbo].[p_User_Populate]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_User_Populate] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @serviceUsers AS TABLE (
	[UserID] [bigint] NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[PwdHash] [nvarchar](255) NOT NULL,
	[Salt] [nvarchar](255) NOT NULL)

	INSERT INTO @serviceUsers
	SELECT 33000001, 'SvcServiceAccount01', NULL, NULL, NULL, NULL, 'PWdHash#1', 'QWVBGH' UNION
	SELECT 33000002, 'SvcServiceAccount02', NULL, NULL, NULL, NULL, 'PWdHash#2', 'KLERGD' UNION
	SELECT 33000003, 'SvcServiceAccount03', NULL, NULL, NULL, NULL, 'PWdHash#3', 'AFCEOP'

	SET IDENTITY_INSERT dbo.[User] ON; 

	MERGE dbo.[User] t USING @serviceUsers s
	ON t.UserID = s.UserID
	WHEN MATCHED THEN
		UPDATE SET
			t.[Login] = s.[Login],
			t.[FirstName] = s.[FirstName],
			t.[LastName] = s.[LastName],
			t.[Email] = s.[Email],
			t.[Description] = s.[Description],
			t.[PwdHash] = s.[PwdHash],
			t.[Salt] = s.[Salt]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([UserID], [Login], [FirstName], [LastName], [Email], [Description], [PwdHash], [Salt])
		VALUES (s.[UserID], s.[Login], s.[FirstName], s.[LastName], s.[Email], s.[Description], s.[PwdHash], s.[Salt])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE

	;
	SET IDENTITY_INSERT dbo.[User] OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Positions]    Script Date: 3/20/2021 11:10:56 AM ******/
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
	ON t.PositionID = s.PositionID
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
		INSERT ([PositionID], [DepartmentID], [Title], [ShortDesc], [Description], [StatusID], [CreatedDate], [CreatedByID], [ModifiedDate], [ModifiedByID])
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

	EXEC p_PositionSkills_Upsert @posID, @Skills

	-- POSITION 2
	DELETE FROM @Skills
	SET @posID = @positionId2

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('VB.NET'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('AZure Devops'), 0, dbo.fn_SkillProficiency_GetIDByName('Beginner') 

	EXEC p_PositionSkills_Upsert @posID, @Skills

	-- POSITION 3
	DELETE FROM @Skills
	SET @posID = @positionId3

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('Jenkins'), 0, dbo.fn_SkillProficiency_GetIDByName('Beginner') 

	EXEC p_PositionSkills_Upsert @posID, @Skills

	-- POSITION 4
	DELETE FROM @Skills
	SET @posID = @positionId4

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('KAFKA'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('SPRING'), 1, dbo.fn_SkillProficiency_GetIDByName('EXpert') UNION
	SELECT dbo.fn_Skill_GetIDByName('PL/SQL'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') 

	EXEC p_PositionSkills_Upsert @posID, @Skills

	-- POSITION 5
	DELETE FROM @Skills
	SET @posID = @positionId5

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('PYTHON'), 1, dbo.fn_SkillProficiency_GetIDByName('Advanced') UNION
	SELECT dbo.fn_Skill_GetIDByName('SPRING'), 1, dbo.fn_SkillProficiency_GetIDByName('EXpert') UNION
	SELECT dbo.fn_Skill_GetIDByName('ORacle'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') 

	EXEC p_PositionSkills_Upsert @posID, @Skills

	-- POSITION 6
	DELETE FROM @Skills
	SET @posID = @positionId5

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('ACTIVEMQ'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVASCRIPT'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Beginner') UNION
	SELECT dbo.fn_Skill_GetIDByName('ORacle'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate') UNION
	SELECT dbo.fn_Skill_GetIDByName('Bamboo'), 0, dbo.fn_SkillProficiency_GetIDByName('Intermediate')

	EXEC p_PositionSkills_Upsert @posID, @Skills	

	-- POSITION 7
	DELETE FROM @Skills
	SET @posID = @positionId7

	INSERT INTO @Skills
	SELECT dbo.fn_Skill_GetIDByName('C#'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('.NET CORE'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('JAVA'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('T-SQL'), 1, dbo.fn_SkillProficiency_GetIDByName('Expert') UNION
	SELECT dbo.fn_Skill_GetIDByName('AZure Devops'), 0, dbo.fn_SkillProficiency_GetIDByName('Expert') 

	EXEC p_PositionSkills_Upsert @posID, @Skills

END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Skills]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_Skills] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @Skills AS dbo.TYPE_Skills

	INSERT INTO @Skills
	SELECT 1, 'C++' UNION
	SELECT 2, 'C#' UNION
	SELECT 3, 'JAVA' UNION
	SELECT 4, 'PYTHON' UNION
	SELECT 5, 'JAVASCRIPT' UNION
	SELECT 6, '.NET' UNION
	SELECT 7, '.NET CORE' UNION
	SELECT 8, 'VB.NET' UNION
	SELECT 9, 'WEBAPI' UNION
	SELECT 10, 'SPRING' UNION
	SELECT 11, 'KAFKA' UNION
	SELECT 12, 'ACTIVEMQ' UNION
	SELECT 13, 'MS SQL SERVER' UNION
	SELECT 14, 'T-SQL' UNION
	SELECT 15, 'APACHE TOMCAT' UNION
	SELECT 16, 'ORACLE' UNION
	SELECT 17, 'PL/SQL' UNION
	SELECT 18, 'JENKINS' UNION
	SELECT 19, 'BAMBOO' UNION
	SELECT 20, 'AZURE DEVOPS' 

	SET IDENTITY_INSERT dbo.[Skill] ON; 

	MERGE dbo.Skill t USING @Skills s
	ON t.SkillID = s.SkillID
	WHEN MATCHED THEN
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED THEN
		INSERT ([SkillID], [Name]) VALUES (s.[SkillID], s.[Name]);

	SET IDENTITY_INSERT dbo.[Skill] ON;

END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Users]    Script Date: 3/20/2021 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_Users]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testUsers AS TABLE (
	[UserID] [bigint] NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[PwdHash] [nvarchar](255) NOT NULL,
	[Salt] [nvarchar](255) NOT NULL)

	DECLARE @idJoeBiden BIGINT = 100001
	DECLARE @idDonaldTrump BIGINT = 100002
	DECLARE @idBarakObama BIGINT = 100003

	INSERT INTO @testUsers
	SELECT @idJoeBiden, 'HRT\JoeB', 'Joe', 'Biden', NULL, NULL, 'PWdHash#1', 'QWVBGH' UNION
	SELECT @idDonaldTrump, 'HRT\DonaldT', 'Donald', 'Trump', NULL, NULL, 'PWdHash#2', 'KLERGD' UNION
	SELECT @idBarakObama, 'HRT\BarakO', 'Barak', 'Obama', NULL, NULL, 'PWdHash#3', 'AFCEOP'

	SET IDENTITY_INSERT dbo.[User] ON; 

	MERGE dbo.[User] t USING @testUsers s
	ON t.UserID = s.UserID
	WHEN MATCHED THEN
		UPDATE SET
			t.[Login] = s.[Login],
			t.[FirstName] = s.[FirstName],
			t.[LastName] = s.[LastName],
			t.[Email] = s.[Email],
			t.[Description] = s.[Description],
			t.[PwdHash] = s.[PwdHash],
			t.[Salt] = s.[Salt]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([UserID], [Login], [FirstName], [LastName], [Email], [Description], [PwdHash], [Salt])
		VALUES (s.[UserID], s.[Login], s.[FirstName], s.[LastName], s.[Email], s.[Description], s.[PwdHash], s.[Salt])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE

	;
	SET IDENTITY_INSERT dbo.[User] OFF;
END
GO
