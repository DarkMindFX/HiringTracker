USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Candidate_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Candidate]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[Candidate] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Candidate_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Candidate] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_GetByCreatedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Candidate_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Candidate] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Candidate] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_GetByModifiedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Candidate_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Candidate] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Candidate] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Candidate_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Candidate] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Candidate] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Candidate_Insert]
			@ID BIGINT,
			@FirstName NVARCHAR(50),
			@MiddleName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@Email NVARCHAR(50),
			@Phone NVARCHAR(50),
			@CVLink NVARCHAR(1000),
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Candidate]
	SELECT 
		@FirstName,
		@MiddleName,
		@LastName,
		@Email,
		@Phone,
		@CVLink,
		@CreatedByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[Candidate] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN e.[MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Email IS NOT NULL THEN (CASE WHEN e.[Email] = @Email THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Phone IS NOT NULL THEN (CASE WHEN e.[Phone] = @Phone THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CVLink IS NOT NULL THEN (CASE WHEN e.[CVLink] = @CVLink THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Candidate_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Candidate_Update]
			@ID BIGINT,
			@FirstName NVARCHAR(50),
			@MiddleName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@Email NVARCHAR(50),
			@Phone NVARCHAR(50),
			@CVLink NVARCHAR(1000),
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Candidate]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Candidate]
		SET
									[FirstName] = IIF( @FirstName IS NOT NULL, @FirstName, [FirstName] ) ,
									[MiddleName] = IIF( @MiddleName IS NOT NULL, @MiddleName, [MiddleName] ) ,
									[LastName] = IIF( @LastName IS NOT NULL, @LastName, [LastName] ) ,
									[Email] = IIF( @Email IS NOT NULL, @Email, [Email] ) ,
									[Phone] = IIF( @Phone IS NOT NULL, @Phone, [Phone] ) ,
									[CVLink] = IIF( @CVLink IS NOT NULL, @CVLink, [CVLink] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Candidate was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Candidate] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN e.[MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Email IS NOT NULL THEN (CASE WHEN e.[Email] = @Email THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Phone IS NOT NULL THEN (CASE WHEN e.[Phone] = @Phone THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CVLink IS NOT NULL THEN (CASE WHEN e.[CVLink] = @CVLink THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateComment_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateComment_Delete]
		@CandidateID BIGINT,	
		@CommentID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[CandidateComment]  
				WHERE 
							[CandidateID] = @CandidateID	AND
							[CommentID] = @CommentID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[CandidateComment] 
		WHERE 
						[CandidateID] = @CandidateID	AND
						[CommentID] = @CommentID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateComment_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateComment_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[CandidateComment] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateComment_GetByCandidateID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateComment_GetByCandidateID]

	@CandidateID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateComment] c 
				WHERE
					[CandidateID] = @CandidateID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateComment] e
		WHERE 
			[CandidateID] = @CandidateID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateComment_GetByCommentID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateComment_GetByCommentID]

	@CommentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateComment] c 
				WHERE
					[CommentID] = @CommentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateComment] e
		WHERE 
			[CommentID] = @CommentID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateComment_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateComment_GetDetails]
		@CandidateID BIGINT,	
		@CommentID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateComment] c 
				WHERE 
								[CandidateID] = @CandidateID	AND
								[CommentID] = @CommentID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateComment] e
		WHERE 
								[CandidateID] = @CandidateID	AND
								[CommentID] = @CommentID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateComment_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateComment_Insert]
			@CandidateID BIGINT,
			@CommentID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[CandidateComment]
	SELECT 
		@CandidateID,
		@CommentID
	
	

	SELECT
		e.*
	FROM
		[dbo].[CandidateComment] e
	WHERE
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN e.[CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateComment_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateComment_Update]
			@CandidateID BIGINT,
			@CommentID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateComment]
					WHERE 
												[CandidateID] = @CandidateID	AND
												[CommentID] = @CommentID	
							))
	BEGIN
		UPDATE [dbo].[CandidateComment]
		SET
									[CandidateID] = IIF( @CandidateID IS NOT NULL, @CandidateID, [CandidateID] ) ,
									[CommentID] = IIF( @CommentID IS NOT NULL, @CommentID, [CommentID] ) 
						WHERE 
												[CandidateID] = @CandidateID	AND
												[CommentID] = @CommentID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'CandidateComment was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[CandidateComment] e
	WHERE
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN e.[CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateProperty_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateProperty_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[CandidateProperty]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[CandidateProperty] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateProperty_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateProperty_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[CandidateProperty] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateProperty_GetByCandidateID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateProperty_GetByCandidateID]

	@CandidateID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateProperty] c 
				WHERE
					[CandidateID] = @CandidateID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateProperty] e
		WHERE 
			[CandidateID] = @CandidateID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateProperty_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateProperty_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateProperty] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateProperty] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateProperty_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateProperty_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Value NVARCHAR(1000),
			@CandidateID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[CandidateProperty]
	SELECT 
		@Name,
		@Value,
		@CandidateID
	
	

	SELECT
		e.*
	FROM
		[dbo].[CandidateProperty] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN e.[Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateProperty_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateProperty_Update]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Value NVARCHAR(1000),
			@CandidateID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateProperty]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[CandidateProperty]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[Value] = IIF( @Value IS NOT NULL, @Value, [Value] ) ,
									[CandidateID] = IIF( @CandidateID IS NOT NULL, @CandidateID, [CandidateID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'CandidateProperty was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[CandidateProperty] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN e.[Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_Delete]
		@CandidateID BIGINT,	
		@SkillID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[CandidateSkill]  
				WHERE 
							[CandidateID] = @CandidateID	AND
							[SkillID] = @SkillID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[CandidateSkill] 
		WHERE 
						[CandidateID] = @CandidateID	AND
						[SkillID] = @SkillID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[CandidateSkill] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_GetByCandidate]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_GetByCandidate]
	@CandidateID BIGINT,
	@Found BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF(EXISTS(SELECT 1 FROM dbo.Candidate p WHERE p.ID = @CandidateID)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found	


		SELECT
			ps.*,
			s.[Name] as SkillName,
			sp.[Name] as SkillProficiency
		FROM
			dbo.CandidateSkill ps
		INNER JOIN dbo.Skill s ON ps.SkillID = s.ID
		INNER JOIN dbo.SkillProficiency sp ON sp.ID = ps.SkillProficiencyID
		WHERE
			ps.CandidateID = @CandidateID
	END
	ELSE
	BEGIN
		SET @Found = 0; -- notifying that record was not found
	END
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_GetByCandidateID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_GetByCandidateID]

	@CandidateID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateSkill] c 
				WHERE
					[CandidateID] = @CandidateID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateSkill] e
		WHERE 
			[CandidateID] = @CandidateID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_GetBySkillID]    Script Date: 8/21/2021 11:22:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_GetBySkillProficiencyID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_GetBySkillProficiencyID]

	@SkillProficiencyID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateSkill] c 
				WHERE
					[SkillProficiencyID] = @SkillProficiencyID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateSkill] e
		WHERE 
			[SkillProficiencyID] = @SkillProficiencyID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_GetDetails]
		@CandidateID BIGINT,	
		@SkillID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateSkill] c 
				WHERE 
								[CandidateID] = @CandidateID	AND
								[SkillID] = @SkillID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[CandidateSkill] e
		WHERE 
								[CandidateID] = @CandidateID	AND
								[SkillID] = @SkillID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_Insert]
			@CandidateID BIGINT,
			@SkillID BIGINT,
			@SkillProficiencyID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[CandidateSkill]
	SELECT 
		@CandidateID,
		@SkillID,
		@SkillProficiencyID
	
	

	SELECT
		e.*
	FROM
		[dbo].[CandidateSkill] e
	WHERE
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN e.[SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN e.[SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_Update]
			@CandidateID BIGINT,
			@SkillID BIGINT,
			@SkillProficiencyID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM dbo.CandidateSkill 
					WHERE 
												[CandidateID] = @CandidateID	AND
												[SkillID] = @SkillID	
							))
	BEGIN
		UPDATE [dbo].[CandidateSkill]
		SET
									[CandidateID] = IIF( @CandidateID IS NOT NULL, @CandidateID, [CandidateID] ) ,
									[SkillID] = IIF( @SkillID IS NOT NULL, @SkillID, [SkillID] ) ,
									[SkillProficiencyID] = IIF( @SkillProficiencyID IS NOT NULL, @SkillProficiencyID, [SkillProficiencyID] ) 
						WHERE 
												[CandidateID] = @CandidateID	AND
												[SkillID] = @SkillID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'CandidateSkill was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[CandidateSkill] e
	WHERE
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN e.[SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN e.[SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_Upsert]    Script Date: 8/21/2021 11:22:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Comment_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Comment_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Comment]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[Comment] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Comment_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Comment_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Comment] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_Comment_GetByCreatedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Comment_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Comment] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Comment] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Comment_GetByModifiedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Comment_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Comment] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Comment] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Comment_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Comment_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Comment] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Comment] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Comment_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Comment_Insert]
			@ID BIGINT,
			@Text NVARCHAR(4000),
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Comment]
	SELECT 
		@Text,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[Comment] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Text IS NOT NULL THEN (CASE WHEN e.[Text] = @Text THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Comment_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Comment_Update]
			@ID BIGINT,
			@Text NVARCHAR(4000),
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Comment]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Comment]
		SET
									[Text] = IIF( @Text IS NOT NULL, @Text, [Text] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Comment was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Comment] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Text IS NOT NULL THEN (CASE WHEN e.[Text] = @Text THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Department_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Department_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Department]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[Department] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Department_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Department_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Department] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_Department_GetByManagerID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Department_GetByManagerID]

	@ManagerID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Department] c 
				WHERE
					[ManagerID] = @ManagerID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Department] e
		WHERE 
			[ManagerID] = @ManagerID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Department_GetByParentID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Department_GetByParentID]

	@ParentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Department] c 
				WHERE
					[ParentID] = @ParentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Department] e
		WHERE 
			[ParentID] = @ParentID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Department_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Department_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Department] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Department] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Department_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Department_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@UUID NVARCHAR(50),
			@ParentID BIGINT,
			@ManagerID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Department]
	SELECT 
		@Name,
		@UUID,
		@ParentID,
		@ManagerID
	
	

	SELECT
		e.*
	FROM
		[dbo].[Department] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN e.[UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN e.[ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN e.[ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Department_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Department_Update]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@UUID NVARCHAR(50),
			@ParentID BIGINT,
			@ManagerID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Department]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Department]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[UUID] = IIF( @UUID IS NOT NULL, @UUID, [UUID] ) ,
									[ParentID] = IIF( @ParentID IS NOT NULL, @ParentID, [ParentID] ) ,
									[ManagerID] = IIF( @ManagerID IS NOT NULL, @ManagerID, [ManagerID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Department was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Department] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN e.[UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN e.[ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN e.[ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Interview]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[Interview] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Interview] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_GetByCreatedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Interview] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Interview] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_GetByInterviewStatusID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_GetByInterviewStatusID]

	@InterviewStatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Interview] c 
				WHERE
					[InterviewStatusID] = @InterviewStatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Interview] e
		WHERE 
			[InterviewStatusID] = @InterviewStatusID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_GetByInterviewTypeID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_GetByInterviewTypeID]

	@InterviewTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Interview] c 
				WHERE
					[InterviewTypeID] = @InterviewTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Interview] e
		WHERE 
			[InterviewTypeID] = @InterviewTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_GetByModifiedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Interview] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Interview] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_GetByProposalID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_GetByProposalID]

	@ProposalID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Interview] c 
				WHERE
					[ProposalID] = @ProposalID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Interview] e
		WHERE 
			[ProposalID] = @ProposalID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Interview] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Interview] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_Insert]
			@ID BIGINT,
			@ProposalID BIGINT,
			@InterviewTypeID BIGINT,
			@StartTime DATETIME,
			@EndTime DATETIME,
			@InterviewStatusID BIGINT,
			@CreatedByID BIGINT,
			@CretedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Interview]
	SELECT 
		@ProposalID,
		@InterviewTypeID,
		@StartTime,
		@EndTime,
		@InterviewStatusID,
		@CreatedByID,
		@CretedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[Interview] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN e.[ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewTypeID IS NOT NULL THEN (CASE WHEN e.[InterviewTypeID] = @InterviewTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StartTime IS NOT NULL THEN (CASE WHEN e.[StartTime] = @StartTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EndTime IS NOT NULL THEN (CASE WHEN e.[EndTime] = @EndTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewStatusID IS NOT NULL THEN (CASE WHEN e.[InterviewStatusID] = @InterviewStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CretedDate IS NOT NULL THEN (CASE WHEN e.[CretedDate] = @CretedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Interview_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Interview_Update]
			@ID BIGINT,
			@ProposalID BIGINT,
			@InterviewTypeID BIGINT,
			@StartTime DATETIME,
			@EndTime DATETIME,
			@InterviewStatusID BIGINT,
			@CreatedByID BIGINT,
			@CretedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Interview]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Interview]
		SET
									[ProposalID] = IIF( @ProposalID IS NOT NULL, @ProposalID, [ProposalID] ) ,
									[InterviewTypeID] = IIF( @InterviewTypeID IS NOT NULL, @InterviewTypeID, [InterviewTypeID] ) ,
									[StartTime] = IIF( @StartTime IS NOT NULL, @StartTime, [StartTime] ) ,
									[EndTime] = IIF( @EndTime IS NOT NULL, @EndTime, [EndTime] ) ,
									[InterviewStatusID] = IIF( @InterviewStatusID IS NOT NULL, @InterviewStatusID, [InterviewStatusID] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[CretedDate] = IIF( @CretedDate IS NOT NULL, @CretedDate, [CretedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Interview was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Interview] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN e.[ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewTypeID IS NOT NULL THEN (CASE WHEN e.[InterviewTypeID] = @InterviewTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StartTime IS NOT NULL THEN (CASE WHEN e.[StartTime] = @StartTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EndTime IS NOT NULL THEN (CASE WHEN e.[EndTime] = @EndTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewStatusID IS NOT NULL THEN (CASE WHEN e.[InterviewStatusID] = @InterviewStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CretedDate IS NOT NULL THEN (CASE WHEN e.[CretedDate] = @CretedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewFeedback_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[InterviewFeedback]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[InterviewFeedback] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewFeedback_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[InterviewFeedback] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_GetByCreatedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewFeedback_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewFeedback] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewFeedback] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_GetByInterviewerID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewFeedback_GetByInterviewerID]

	@InterviewerID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewFeedback] c 
				WHERE
					[InterviewerID] = @InterviewerID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewFeedback] e
		WHERE 
			[InterviewerID] = @InterviewerID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_GetByInterviewID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewFeedback_GetByInterviewID]

	@InterviewID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewFeedback] c 
				WHERE
					[InterviewID] = @InterviewID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewFeedback] e
		WHERE 
			[InterviewID] = @InterviewID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_GetByModifiedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewFeedback_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewFeedback] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewFeedback] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewFeedback_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewFeedback] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewFeedback] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewFeedback_Insert]
			@ID BIGINT,
			@Comment NVARCHAR(4000),
			@Rating INT,
			@InterviewID BIGINT,
			@InterviewerID BIGINT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[InterviewFeedback]
	SELECT 
		@Comment,
		@Rating,
		@InterviewID,
		@InterviewerID,
		@CreatedByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[InterviewFeedback] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Rating IS NOT NULL THEN (CASE WHEN e.[Rating] = @Rating THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN e.[InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewerID IS NOT NULL THEN (CASE WHEN e.[InterviewerID] = @InterviewerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewFeedback_Update]
			@ID BIGINT,
			@Comment NVARCHAR(4000),
			@Rating INT,
			@InterviewID BIGINT,
			@InterviewerID BIGINT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewFeedback]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[InterviewFeedback]
		SET
									[Comment] = IIF( @Comment IS NOT NULL, @Comment, [Comment] ) ,
									[Rating] = IIF( @Rating IS NOT NULL, @Rating, [Rating] ) ,
									[InterviewID] = IIF( @InterviewID IS NOT NULL, @InterviewID, [InterviewID] ) ,
									[InterviewerID] = IIF( @InterviewerID IS NOT NULL, @InterviewerID, [InterviewerID] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'InterviewFeedback was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[InterviewFeedback] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Rating IS NOT NULL THEN (CASE WHEN e.[Rating] = @Rating THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN e.[InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @InterviewerID IS NOT NULL THEN (CASE WHEN e.[InterviewerID] = @InterviewerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_Delete]
		@InterviewID BIGINT,	
		@UserID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[InterviewRole]  
				WHERE 
							[InterviewID] = @InterviewID	AND
							[UserID] = @UserID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[InterviewRole] 
		WHERE 
						[InterviewID] = @InterviewID	AND
						[UserID] = @UserID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[InterviewRole] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetByInterviewID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_GetByInterviewID]

	@InterviewID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewRole] c 
				WHERE
					[InterviewID] = @InterviewID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewRole] e
		WHERE 
			[InterviewID] = @InterviewID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetByRoleID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_GetByRoleID]

	@RoleID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewRole] c 
				WHERE
					[RoleID] = @RoleID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewRole] e
		WHERE 
			[RoleID] = @RoleID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetByUserID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewRole] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewRole] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_GetDetails]
		@InterviewID BIGINT,	
		@UserID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewRole] c 
				WHERE 
								[InterviewID] = @InterviewID	AND
								[UserID] = @UserID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewRole] e
		WHERE 
								[InterviewID] = @InterviewID	AND
								[UserID] = @UserID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_Insert]
			@InterviewID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[InterviewRole]
	SELECT 
		@InterviewID,
		@UserID,
		@RoleID
	
	

	SELECT
		e.*
	FROM
		[dbo].[InterviewRole] e
	WHERE
				(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN e.[InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewRole_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewRole_Update]
			@InterviewID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewRole]
					WHERE 
												[InterviewID] = @InterviewID	AND
												[UserID] = @UserID	
							))
	BEGIN
		UPDATE [dbo].[InterviewRole]
		SET
									[InterviewID] = IIF( @InterviewID IS NOT NULL, @InterviewID, [InterviewID] ) ,
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[RoleID] = IIF( @RoleID IS NOT NULL, @RoleID, [RoleID] ) 
						WHERE 
												[InterviewID] = @InterviewID	AND
												[UserID] = @UserID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'InterviewRole was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[InterviewRole] e
	WHERE
				(CASE WHEN @InterviewID IS NOT NULL THEN (CASE WHEN e.[InterviewID] = @InterviewID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewStatus_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[InterviewStatus]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[InterviewStatus] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewStatus_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[InterviewStatus] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewStatus_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewStatus] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewStatus] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewStatus_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[InterviewStatus]
	SELECT 
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[InterviewStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_Populate]    Script Date: 8/21/2021 11:22:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewStatus_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewStatus]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[InterviewStatus]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'InterviewStatus was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[InterviewStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewType_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[InterviewType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[InterviewType] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewType_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[InterviewType] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewType_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[InterviewType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewType_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewType_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[InterviewType]
	SELECT 
		@ID,
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[InterviewType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewType_Populate]    Script Date: 8/21/2021 11:22:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_InterviewType_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewType_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[InterviewType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[InterviewType]
		SET
									[ID] = IIF( @ID IS NOT NULL, @ID, [ID] ) ,
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'InterviewType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[InterviewType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Position]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[Position] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Position] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetByCreatedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Position] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Position] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetByDepartmentID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_GetByDepartmentID]

	@DepartmentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Position] c 
				WHERE
					[DepartmentID] = @DepartmentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Position] e
		WHERE 
			[DepartmentID] = @DepartmentID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetByModifiedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Position] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Position] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetByStatusID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_GetByStatusID]

	@StatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Position] c 
				WHERE
					[StatusID] = @StatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Position] e
		WHERE 
			[StatusID] = @StatusID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Position] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Position] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_Insert]
			@ID BIGINT,
			@DepartmentID BIGINT,
			@Title NVARCHAR(50),
			@ShortDesc NVARCHAR(250),
			@Description NVARCHAR(4000),
			@StatusID BIGINT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Position]
	SELECT 
		@DepartmentID,
		@Title,
		@ShortDesc,
		@Description,
		@StatusID,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[Position] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DepartmentID IS NOT NULL THEN (CASE WHEN e.[DepartmentID] = @DepartmentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ShortDesc IS NOT NULL THEN (CASE WHEN e.[ShortDesc] = @ShortDesc THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN e.[StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Position_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_Update]
			@ID BIGINT,
			@DepartmentID BIGINT,
			@Title NVARCHAR(50),
			@ShortDesc NVARCHAR(250),
			@Description NVARCHAR(4000),
			@StatusID BIGINT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Position]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Position]
		SET
									[DepartmentID] = IIF( @DepartmentID IS NOT NULL, @DepartmentID, [DepartmentID] ) ,
									[Title] = IIF( @Title IS NOT NULL, @Title, [Title] ) ,
									[ShortDesc] = IIF( @ShortDesc IS NOT NULL, @ShortDesc, [ShortDesc] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[StatusID] = IIF( @StatusID IS NOT NULL, @StatusID, [StatusID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Position was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Position] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DepartmentID IS NOT NULL THEN (CASE WHEN e.[DepartmentID] = @DepartmentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ShortDesc IS NOT NULL THEN (CASE WHEN e.[ShortDesc] = @ShortDesc THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN e.[StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionComment_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionComment_Delete]
		@PositionID BIGINT,	
		@CommentID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PositionComment]  
				WHERE 
							[PositionID] = @PositionID	AND
							[CommentID] = @CommentID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[PositionComment] 
		WHERE 
						[PositionID] = @PositionID	AND
						[CommentID] = @CommentID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionComment_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionComment_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PositionComment] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionComment_GetByCommentID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionComment_GetByCommentID]

	@CommentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionComment] c 
				WHERE
					[CommentID] = @CommentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PositionComment] e
		WHERE 
			[CommentID] = @CommentID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionComment_GetByPositionID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionComment_GetByPositionID]

	@PositionID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionComment] c 
				WHERE
					[PositionID] = @PositionID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PositionComment] e
		WHERE 
			[PositionID] = @PositionID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionComment_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionComment_GetDetails]
		@PositionID BIGINT,	
		@CommentID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionComment] c 
				WHERE 
								[PositionID] = @PositionID	AND
								[CommentID] = @CommentID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PositionComment] e
		WHERE 
								[PositionID] = @PositionID	AND
								[CommentID] = @CommentID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionComment_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionComment_Insert]
			@PositionID BIGINT,
			@CommentID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PositionComment]
	SELECT 
		@PositionID,
		@CommentID
	
	

	SELECT
		e.*
	FROM
		[dbo].[PositionComment] e
	WHERE
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN e.[CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionComment_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionComment_Update]
			@PositionID BIGINT,
			@CommentID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionComment]
					WHERE 
												[PositionID] = @PositionID	AND
												[CommentID] = @CommentID	
							))
	BEGIN
		UPDATE [dbo].[PositionComment]
		SET
									[PositionID] = IIF( @PositionID IS NOT NULL, @PositionID, [PositionID] ) ,
									[CommentID] = IIF( @CommentID IS NOT NULL, @CommentID, [CommentID] ) 
						WHERE 
												[PositionID] = @PositionID	AND
												[CommentID] = @CommentID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PositionComment was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PositionComment] e
	WHERE
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN e.[CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_Delete]
		@PositionID BIGINT,	
		@SkillID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PositionSkill]  
				WHERE 
							[PositionID] = @PositionID	AND
							[SkillID] = @SkillID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[PositionSkill] 
		WHERE 
						[PositionID] = @PositionID	AND
						[SkillID] = @SkillID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PositionSkill] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_GetByPositionID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_GetByPositionID]

	@PositionID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionSkill] c 
				WHERE
					[PositionID] = @PositionID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PositionSkill] e
		WHERE 
			[PositionID] = @PositionID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_GetBySkillID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_GetBySkillID]

	@SkillID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionSkill] c 
				WHERE
					[SkillID] = @SkillID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PositionSkill] e
		WHERE 
			[SkillID] = @SkillID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_GetBySkillProficiencyID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_GetBySkillProficiencyID]

	@SkillProficiencyID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionSkill] c 
				WHERE
					[SkillProficiencyID] = @SkillProficiencyID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PositionSkill] e
		WHERE 
			[SkillProficiencyID] = @SkillProficiencyID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_GetDetails]
		@PositionID BIGINT,	
		@SkillID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionSkill] c 
				WHERE 
								[PositionID] = @PositionID	AND
								[SkillID] = @SkillID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PositionSkill] e
		WHERE 
								[PositionID] = @PositionID	AND
								[SkillID] = @SkillID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_Insert]
			@PositionID BIGINT,
			@SkillID BIGINT,
			@IsMandatory BIT,
			@SkillProficiencyID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PositionSkill]
	SELECT 
		@PositionID,
		@SkillID,
		@IsMandatory,
		@SkillProficiencyID
	
	

	SELECT
		e.*
	FROM
		[dbo].[PositionSkill] e
	WHERE
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN e.[SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsMandatory IS NOT NULL THEN (CASE WHEN e.[IsMandatory] = @IsMandatory THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN e.[SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_Update]
			@PositionID BIGINT,
			@SkillID BIGINT,
			@IsMandatory BIT,
			@SkillProficiencyID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionSkill]
					WHERE 
												[PositionID] = @PositionID	AND
												[SkillID] = @SkillID	
							))
	BEGIN
		UPDATE [dbo].[PositionSkill]
		SET
									[PositionID] = IIF( @PositionID IS NOT NULL, @PositionID, [PositionID] ) ,
									[SkillID] = IIF( @SkillID IS NOT NULL, @SkillID, [SkillID] ) ,
									[IsMandatory] = IIF( @IsMandatory IS NOT NULL, @IsMandatory, [IsMandatory] ) ,
									[SkillProficiencyID] = IIF( @SkillProficiencyID IS NOT NULL, @SkillProficiencyID, [SkillProficiencyID] ) 
						WHERE 
												[PositionID] = @PositionID	AND
												[SkillID] = @SkillID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PositionSkill was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PositionSkill] e
	WHERE
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillID IS NOT NULL THEN (CASE WHEN e.[SkillID] = @SkillID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsMandatory IS NOT NULL THEN (CASE WHEN e.[IsMandatory] = @IsMandatory THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SkillProficiencyID IS NOT NULL THEN (CASE WHEN e.[SkillProficiencyID] = @SkillProficiencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_Upsert]    Script Date: 8/21/2021 11:22:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionSkills_GetByPosition]    Script Date: 8/21/2021 11:22:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionStatus_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PositionStatus]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[PositionStatus] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionStatus_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PositionStatus] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionStatus_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionStatus] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PositionStatus] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionStatus_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PositionStatus]
	SELECT 
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[PositionStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_Populate]    Script Date: 8/21/2021 11:22:09 AM ******/
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

	SET IDENTITY_INSERT dbo.[PositionStatus] ON;

	MERGE dbo.PositionStatus t USING @statuses s 
	ON
		t.[ID] = s.[StatusID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Name]) VALUES (s.[StatusID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;


	SET IDENTITY_INSERT dbo.[PositionStatus] OFF;


END
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionStatus_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PositionStatus]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[PositionStatus]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PositionStatus was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PositionStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Proposal]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[Proposal] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Proposal] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetByCandidateID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetByCandidateID]

	@CandidateID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Proposal] c 
				WHERE
					[CandidateID] = @CandidateID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Proposal] e
		WHERE 
			[CandidateID] = @CandidateID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetByCreatedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Proposal] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Proposal] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetByCurrentStepID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetByCurrentStepID]

	@CurrentStepID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Proposal] c 
				WHERE
					[CurrentStepID] = @CurrentStepID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Proposal] e
		WHERE 
			[CurrentStepID] = @CurrentStepID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetByModifiedByID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Proposal] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Proposal] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetByNextStepID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetByNextStepID]

	@NextStepID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Proposal] c 
				WHERE
					[NextStepID] = @NextStepID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Proposal] e
		WHERE 
			[NextStepID] = @NextStepID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetByPositionID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetByPositionID]

	@PositionID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Proposal] c 
				WHERE
					[PositionID] = @PositionID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Proposal] e
		WHERE 
			[PositionID] = @PositionID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetByStatusID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetByStatusID]

	@StatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Proposal] c 
				WHERE
					[StatusID] = @StatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Proposal] e
		WHERE 
			[StatusID] = @StatusID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Proposal] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Proposal] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_Insert]
			@ID BIGINT,
			@PositionID BIGINT,
			@CandidateID BIGINT,
			@Proposed DATETIME,
			@CurrentStepID BIGINT,
			@StepSetDate DATETIME,
			@NextStepID BIGINT,
			@DueDate DATETIME,
			@StatusID BIGINT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Proposal]
	SELECT 
		@PositionID,
		@CandidateID,
		@Proposed,
		@CurrentStepID,
		@StepSetDate,
		@NextStepID,
		@DueDate,
		@StatusID,
		@CreatedByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[Proposal] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Proposed IS NOT NULL THEN (CASE WHEN e.[Proposed] = @Proposed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CurrentStepID IS NOT NULL THEN (CASE WHEN e.[CurrentStepID] = @CurrentStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StepSetDate IS NOT NULL THEN (CASE WHEN e.[StepSetDate] = @StepSetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NextStepID IS NOT NULL THEN (CASE WHEN e.[NextStepID] = @NextStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DueDate IS NOT NULL THEN (CASE WHEN e.[DueDate] = @DueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN e.[StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_Update]
			@ID BIGINT,
			@PositionID BIGINT,
			@CandidateID BIGINT,
			@Proposed DATETIME,
			@CurrentStepID BIGINT,
			@StepSetDate DATETIME,
			@NextStepID BIGINT,
			@DueDate DATETIME,
			@StatusID BIGINT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Proposal]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Proposal]
		SET
									[PositionID] = IIF( @PositionID IS NOT NULL, @PositionID, [PositionID] ) ,
									[CandidateID] = IIF( @CandidateID IS NOT NULL, @CandidateID, [CandidateID] ) ,
									[Proposed] = IIF( @Proposed IS NOT NULL, @Proposed, [Proposed] ) ,
									[CurrentStepID] = IIF( @CurrentStepID IS NOT NULL, @CurrentStepID, [CurrentStepID] ) ,
									[StepSetDate] = IIF( @StepSetDate IS NOT NULL, @StepSetDate, [StepSetDate] ) ,
									[NextStepID] = IIF( @NextStepID IS NOT NULL, @NextStepID, [NextStepID] ) ,
									[DueDate] = IIF( @DueDate IS NOT NULL, @DueDate, [DueDate] ) ,
									[StatusID] = IIF( @StatusID IS NOT NULL, @StatusID, [StatusID] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Proposal was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Proposal] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Proposed IS NOT NULL THEN (CASE WHEN e.[Proposed] = @Proposed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CurrentStepID IS NOT NULL THEN (CASE WHEN e.[CurrentStepID] = @CurrentStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StepSetDate IS NOT NULL THEN (CASE WHEN e.[StepSetDate] = @StepSetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NextStepID IS NOT NULL THEN (CASE WHEN e.[NextStepID] = @NextStepID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DueDate IS NOT NULL THEN (CASE WHEN e.[DueDate] = @DueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN e.[StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalComment_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalComment_Delete]
		@ProposalID BIGINT,	
		@CommentID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ProposalComment]  
				WHERE 
							[ProposalID] = @ProposalID	AND
							[CommentID] = @CommentID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[ProposalComment] 
		WHERE 
						[ProposalID] = @ProposalID	AND
						[CommentID] = @CommentID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalComment_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalComment_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ProposalComment] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalComment_GetByCommentID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalComment_GetByCommentID]

	@CommentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ProposalComment] c 
				WHERE
					[CommentID] = @CommentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ProposalComment] e
		WHERE 
			[CommentID] = @CommentID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalComment_GetByProposalID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalComment_GetByProposalID]

	@ProposalID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ProposalComment] c 
				WHERE
					[ProposalID] = @ProposalID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ProposalComment] e
		WHERE 
			[ProposalID] = @ProposalID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalComment_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalComment_GetDetails]
		@ProposalID BIGINT,	
		@CommentID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ProposalComment] c 
				WHERE 
								[ProposalID] = @ProposalID	AND
								[CommentID] = @CommentID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ProposalComment] e
		WHERE 
								[ProposalID] = @ProposalID	AND
								[CommentID] = @CommentID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalComment_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalComment_Insert]
			@ProposalID BIGINT,
			@CommentID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ProposalComment]
	SELECT 
		@ProposalID,
		@CommentID
	
	

	SELECT
		e.*
	FROM
		[dbo].[ProposalComment] e
	WHERE
				(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN e.[ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN e.[CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalComment_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalComment_Update]
			@ProposalID BIGINT,
			@CommentID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ProposalComment]
					WHERE 
												[ProposalID] = @ProposalID	AND
												[CommentID] = @CommentID	
							))
	BEGIN
		UPDATE [dbo].[ProposalComment]
		SET
									[ProposalID] = IIF( @ProposalID IS NOT NULL, @ProposalID, [ProposalID] ) ,
									[CommentID] = IIF( @CommentID IS NOT NULL, @CommentID, [CommentID] ) 
						WHERE 
												[ProposalID] = @ProposalID	AND
												[CommentID] = @CommentID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ProposalComment was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ProposalComment] e
	WHERE
				(CASE WHEN @ProposalID IS NOT NULL THEN (CASE WHEN e.[ProposalID] = @ProposalID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CommentID IS NOT NULL THEN (CASE WHEN e.[CommentID] = @CommentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStatus_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStatus_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ProposalStatus]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[ProposalStatus] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStatus_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStatus_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ProposalStatus] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStatus_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStatus_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ProposalStatus] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ProposalStatus] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStatus_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStatus_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ProposalStatus]
	SELECT 
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[ProposalStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStatus_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStatus_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ProposalStatus]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ProposalStatus]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ProposalStatus was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ProposalStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStep_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStep_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ProposalStep]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[ProposalStep] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStep_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStep_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ProposalStep] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStep_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStep_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ProposalStep] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ProposalStep] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStep_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStep_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@ReqDueDate BIT,
			@RequiresRespInDays INT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ProposalStep]
	SELECT 
		@Name,
		@ReqDueDate,
		@RequiresRespInDays
	
	

	SELECT
		e.*
	FROM
		[dbo].[ProposalStep] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN e.[ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN e.[RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_ProposalStep_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_ProposalStep_Update]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@ReqDueDate BIT,
			@RequiresRespInDays INT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ProposalStep]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ProposalStep]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[ReqDueDate] = IIF( @ReqDueDate IS NOT NULL, @ReqDueDate, [ReqDueDate] ) ,
									[RequiresRespInDays] = IIF( @RequiresRespInDays IS NOT NULL, @RequiresRespInDays, [RequiresRespInDays] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ProposalStep was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ProposalStep] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN e.[ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN e.[RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Role_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Role_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Role]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[Role] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Role_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Role_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Role] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_Role_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Role_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Role] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Role] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Role_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Role_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Role]
	SELECT 
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[Role] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Role_Populate]    Script Date: 8/21/2021 11:22:09 AM ******/
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
	SELECT 5, 'Interviewer'		UNION
	SELECT 6, 'Recruiter'

	SET IDENTITY_INSERT dbo.[Role] ON;

	MERGE dbo.[Role] t USING @role s 
	ON
		t.[ID] = s.[RoleID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Name]) VALUES (s.[RoleID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[Role] OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[p_Role_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Role_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Role]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Role]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Role was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Role] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Skill_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Skill_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Skill]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[Skill] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_Skill_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Skill_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Skill] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_Skill_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Skill_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Skill] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Skill] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Skill_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Skill_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Skill]
	SELECT 
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[Skill] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Skill_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Skill_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Skill]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Skill]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Skill was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Skill] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_SkillProficiency_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[SkillProficiency]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[SkillProficiency] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_SkillProficiency_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[SkillProficiency] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_SkillProficiency_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[SkillProficiency] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[SkillProficiency] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_SkillProficiency_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[SkillProficiency]
	SELECT 
		@ID,
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[SkillProficiency] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_Populate]    Script Date: 8/21/2021 11:22:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_SkillProficiency_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_SkillProficiency_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[SkillProficiency]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[SkillProficiency]
		SET
									[ID] = IIF( @ID IS NOT NULL, @ID, [ID] ) ,
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'SkillProficiency was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[SkillProficiency] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_User_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_User_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[User]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[User] 
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_User_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_User_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[User] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_User_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_User_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[User] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[User] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_User_GetDetailsByLogin]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_User_GetDetailsByLogin]
	@Login NVARCHAR(50),
	@Found BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF(EXISTS(SELECT 1 FROM dbo.[User] p WHERE LOWER(p.[Login]) = LOWER(@Login))) 
	BEGIN
		SET @Found = 1; -- notifying that record was found

		SELECT 
			p.*
		FROM
			dbo.[User] p		
		WHERE
			LOWER(p.[Login]) = LOWER(@Login)
	
	END
	ELSE
	BEGIN
		SET @Found = 0; -- notifying that record was not found
	END
END
GO
/****** Object:  StoredProcedure [dbo].[p_User_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_User_Insert]
			@ID BIGINT,
			@Login NVARCHAR(50),
			@FirstName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@Email NVARCHAR(50),
			@Description NVARCHAR(255),
			@PwdHash NVARCHAR(255),
			@Salt NVARCHAR(255)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[User]
	SELECT 
		@Login,
		@FirstName,
		@LastName,
		@Email,
		@Description,
		@PwdHash,
		@Salt
	
	

	SELECT
		e.*
	FROM
		[dbo].[User] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN e.[Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Email IS NOT NULL THEN (CASE WHEN e.[Email] = @Email THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN e.[PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN e.[Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_User_Populate]    Script Date: 8/21/2021 11:22:09 AM ******/
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
	ON t.ID = s.UserID
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
		INSERT ([ID], [Login], [FirstName], [LastName], [Email], [Description], [PwdHash], [Salt])
		VALUES (s.[UserID], s.[Login], s.[FirstName], s.[LastName], s.[Email], s.[Description], s.[PwdHash], s.[Salt])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE

	;
	SET IDENTITY_INSERT dbo.[User] OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[p_User_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_User_Update]
			@ID BIGINT,
			@Login NVARCHAR(50),
			@FirstName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@Email NVARCHAR(50),
			@Description NVARCHAR(255),
			@PwdHash NVARCHAR(255),
			@Salt NVARCHAR(255)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM dbo.[User] 
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[User]
		SET
									[Login] = IIF( @Login IS NOT NULL, @Login, [Login] ) ,
									[FirstName] = IIF( @FirstName IS NOT NULL, @FirstName, [FirstName] ) ,
									[LastName] = IIF( @LastName IS NOT NULL, @LastName, [LastName] ) ,
									[Email] = IIF( @Email IS NOT NULL, @Email, [Email] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[PwdHash] = IIF( @PwdHash IS NOT NULL, @PwdHash, [PwdHash] ) ,
									[Salt] = IIF( @Salt IS NOT NULL, @Salt, [Salt] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'User was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[User] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN e.[Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Email IS NOT NULL THEN (CASE WHEN e.[Email] = @Email THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN e.[PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN e.[Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_Delete]
		@CandidateID BIGINT,	
		@UserID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserRoleCandidate]  
				WHERE 
							[CandidateID] = @CandidateID	AND
							[UserID] = @UserID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[UserRoleCandidate] 
		WHERE 
						[CandidateID] = @CandidateID	AND
						[UserID] = @UserID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserRoleCandidate] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_GetByCandidateID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_GetByCandidateID]

	@CandidateID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleCandidate] c 
				WHERE
					[CandidateID] = @CandidateID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRoleCandidate] e
		WHERE 
			[CandidateID] = @CandidateID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_GetByRoleID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_GetByRoleID]

	@RoleID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleCandidate] c 
				WHERE
					[RoleID] = @RoleID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRoleCandidate] e
		WHERE 
			[RoleID] = @RoleID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_GetByUserID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleCandidate] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRoleCandidate] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_GetDetails]
		@CandidateID BIGINT,	
		@UserID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleCandidate] c 
				WHERE 
								[CandidateID] = @CandidateID	AND
								[UserID] = @UserID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRoleCandidate] e
		WHERE 
								[CandidateID] = @CandidateID	AND
								[UserID] = @UserID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_Insert]
			@CandidateID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserRoleCandidate]
	SELECT 
		@CandidateID,
		@UserID,
		@RoleID
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserRoleCandidate] e
	WHERE
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleCandidate_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleCandidate_Update]
			@CandidateID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleCandidate]
					WHERE 
												[CandidateID] = @CandidateID	AND
												[UserID] = @UserID	
							))
	BEGIN
		UPDATE [dbo].[UserRoleCandidate]
		SET
									[CandidateID] = IIF( @CandidateID IS NOT NULL, @CandidateID, [CandidateID] ) ,
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[RoleID] = IIF( @RoleID IS NOT NULL, @RoleID, [RoleID] ) 
						WHERE 
												[CandidateID] = @CandidateID	AND
												[UserID] = @UserID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserRoleCandidate was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserRoleCandidate] e
	WHERE
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRolePosition_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRolePosition_Delete]
		@PositionID BIGINT,	
		@UserID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserRolePosition]  
				WHERE 
							[PositionID] = @PositionID	AND
							[UserID] = @UserID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[UserRolePosition] 
		WHERE 
						[PositionID] = @PositionID	AND
						[UserID] = @UserID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRolePosition_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRolePosition_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserRolePosition] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRolePosition_GetByPositionID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRolePosition_GetByPositionID]

	@PositionID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRolePosition] c 
				WHERE
					[PositionID] = @PositionID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRolePosition] e
		WHERE 
			[PositionID] = @PositionID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRolePosition_GetByRoleID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRolePosition_GetByRoleID]

	@RoleID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRolePosition] c 
				WHERE
					[RoleID] = @RoleID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRolePosition] e
		WHERE 
			[RoleID] = @RoleID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRolePosition_GetByUserID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRolePosition_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRolePosition] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRolePosition] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRolePosition_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRolePosition_GetDetails]
		@PositionID BIGINT,	
		@UserID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRolePosition] c 
				WHERE 
								[PositionID] = @PositionID	AND
								[UserID] = @UserID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRolePosition] e
		WHERE 
								[PositionID] = @PositionID	AND
								[UserID] = @UserID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRolePosition_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRolePosition_Insert]
			@PositionID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserRolePosition]
	SELECT 
		@PositionID,
		@UserID,
		@RoleID
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserRolePosition] e
	WHERE
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRolePosition_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRolePosition_Update]
			@PositionID BIGINT,
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRolePosition]
					WHERE 
												[PositionID] = @PositionID	AND
												[UserID] = @UserID	
							))
	BEGIN
		UPDATE [dbo].[UserRolePosition]
		SET
									[PositionID] = IIF( @PositionID IS NOT NULL, @PositionID, [PositionID] ) ,
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[RoleID] = IIF( @RoleID IS NOT NULL, @RoleID, [RoleID] ) 
						WHERE 
												[PositionID] = @PositionID	AND
												[UserID] = @UserID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserRolePosition was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserRolePosition] e
	WHERE
				(CASE WHEN @PositionID IS NOT NULL THEN (CASE WHEN e.[PositionID] = @PositionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleSystem_Delete]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleSystem_Delete]
		@UserID BIGINT,	
		@RoleID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserRoleSystem]  
				WHERE 
							[UserID] = @UserID	AND
							[RoleID] = @RoleID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[UserRoleSystem] 
		WHERE 
						[UserID] = @UserID	AND
						[RoleID] = @RoleID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleSystem_GetAll]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleSystem_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserRoleSystem] e
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleSystem_GetByRoleID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleSystem_GetByRoleID]

	@RoleID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleSystem] c 
				WHERE
					[RoleID] = @RoleID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRoleSystem] e
		WHERE 
			[RoleID] = @RoleID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleSystem_GetByUserID]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleSystem_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleSystem] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRoleSystem] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleSystem_GetDetails]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleSystem_GetDetails]
		@UserID BIGINT,	
		@RoleID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleSystem] c 
				WHERE 
								[UserID] = @UserID	AND
								[RoleID] = @RoleID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserRoleSystem] e
		WHERE 
								[UserID] = @UserID	AND
								[RoleID] = @RoleID	
				END
	ELSE
		SET @Found = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleSystem_Insert]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleSystem_Insert]
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserRoleSystem]
	SELECT 
		@UserID,
		@RoleID
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserRoleSystem] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_UserRoleSystem_Update]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_UserRoleSystem_Update]
			@UserID BIGINT,
			@RoleID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserRoleSystem]
					WHERE 
												[UserID] = @UserID	AND
												[RoleID] = @RoleID	
							))
	BEGIN
		UPDATE [dbo].[UserRoleSystem]
		SET
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[RoleID] = IIF( @RoleID IS NOT NULL, @RoleID, [RoleID] ) 
						WHERE 
												[UserID] = @UserID	AND
												[RoleID] = @RoleID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserRoleSystem was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserRoleSystem] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RoleID IS NOT NULL THEN (CASE WHEN e.[RoleID] = @RoleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	EXEC dbo.p_Utils_PopulateTestData_Users
	EXEC dbo.p_Utils_PopulateTestData_Skills
	EXEC dbo.p_Utils_PopulateTestData_Comments
	EXEC dbo.p_Utils_PopulateTestData_Positions
	EXEC dbo.p_Utils_PopulateTestData_PositionComment
	EXEC dbo.p_Utils_PopulateTestData_Candidates
	EXEC dbo.p_Utils_PopulateTestData_CandidateComment
	EXEC dbo.p_Utils_PopulateTestData_CandidateProperty
    EXEC dbo.p_Utils_PopulateTestData_Proposals
	EXEC dbo.p_Utils_PopulateTestData_ProposalComment
	EXEC dbo.p_Utils_PopulateTestData_Interview
	EXEC dbo.p_Utils_PopulateTestData_InterviewFeedback

END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_CandidateComment]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_CandidateComment] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @CandidateComment AS TABLE (
	[CandidateID] [bigint],
	[CommentID] [bigint])

	DECLARE @candidateId1 BIGINT = 100001
	DECLARE @candidateId2 BIGINT = 100002
	DECLARE @candidateId3 BIGINT = 100003
	DECLARE @candidateId4 BIGINT = 100004
	DECLARE @candidateId5 BIGINT = 100005
	DECLARE @candidateId6 BIGINT = 100006
	DECLARE @candidateId7 BIGINT = 100007
	DECLARE @candidateId8 BIGINT = 100008
	DECLARE @candidateId9 BIGINT = 100009

	DECLARE @id1 BIGINT = 100001
	DECLARE @id2 BIGINT = 100002
	DECLARE @id3 BIGINT = 100003
	DECLARE @id4 BIGINT = 100004
	DECLARE @id5 BIGINT = 100005
	DECLARE @id6 BIGINT = 100006
	DECLARE @id7 BIGINT = 100007
	DECLARE @id8 BIGINT = 100008
	DECLARE @id9 BIGINT = 100009

	INSERT INTO @CandidateComment 
	SELECT @candidateId1, @id1 UNION
	SELECT @candidateId2, @id2 UNION
	SELECT @candidateId3, @id3 UNION
	SELECT @candidateId4, @id4 UNION
	SELECT @candidateId5, @id5 UNION
	SELECT @candidateId6, @id6 UNION
	SELECT @candidateId7, @id7 UNION
	SELECT @candidateId8, @id8 UNION
	SELECT @candidateId9, @id9
	


	MERGE dbo.CandidateComment as t USING @CandidateComment as s ON
	(
		t.CandidateID = s.CandidateID AND
		t.CommentID = s.CommentID
	)
	WHEN MATCHED THEN 
		UPDATE SET
			t.[CandidateID] = s.[CandidateID],
			t.[CommentID] = s.[CommentID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([CandidateID], [CommentID])
		VALUES (s.[CandidateID], s.[CommentID])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;

	

END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_CandidateProperty]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_CandidateProperty]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testProperties AS TABLE (
	[ID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](1000) NULL,
	[CandidateID] [bigint] NOT NULL)

	DECLARE @candidateId1 BIGINT = 100001
	DECLARE @candidateId2 BIGINT = 100002
	DECLARE @candidateId3 BIGINT = 100003
	DECLARE @candidateId4 BIGINT = 100004
	DECLARE @candidateId5 BIGINT = 100005
	DECLARE @candidateId6 BIGINT = 100006
	DECLARE @candidateId7 BIGINT = 100007
	DECLARE @candidateId8 BIGINT = 100008
	DECLARE @candidateId9 BIGINT = 100009

	INSERT INTO @testProperties
	SELECT 100001, 'Notice Period', '3 months', @candidateId1 UNION
	SELECT 100002, 'Salary Expectations', '120K/year', @candidateId1 UNION
	SELECT 100003, 'Notice Period', '1 month', @candidateId2 UNION
	SELECT 100004, 'Preferable work location', 'Remote', @candidateId2 UNION
	SELECT 100005, 'Preferable role', 'Tech Lead', @candidateId2 UNION
	SELECT 100006, 'Type of work', 'No work with legacy code', @candidateId3 UNION
	SELECT 100007, 'Salary Expectations', '180/h', @candidateId4 UNION
	SELECT 100008, 'Availability', '1 month', @candidateId4 UNION
	SELECT 100009, 'Specialization', 'ETL', @candidateId4 UNION
	SELECT 1000010, 'Salary Expectations', '220K/year', @candidateId5 UNION
	SELECT 1000011, 'Notice Period', '2 months', @candidateId6 UNION
	SELECT 1000012, 'Type of work', 'OK to work with legacy', @candidateId6


	SET IDENTITY_INSERT dbo.[CandidateProperty] ON; 

	MERGE dbo.[CandidateProperty] t USING @testProperties s
	ON t.ID = s.ID
	WHEN MATCHED THEN
		UPDATE SET
			t.[Name] = s.[Name],
			t.[Value] = s.[Value],
			t.[CandidateID] = s.[CandidateID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Name], [Value], [CandidateID])
		VALUES (s.[ID], s.[Name], s.[Value], s.[CandidateID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[CandidateProperty] OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Candidates]    Script Date: 8/21/2021 11:22:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Comments]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_Comments] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @Comments AS TABLE (
	[ID] [bigint],
	[Text] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL)

	DECLARE @idJoeBiden BIGINT = 100001
	DECLARE @idDonaldTrump BIGINT = 100002
	DECLARE @idBarakObama BIGINT = 100003

	DECLARE @id1 BIGINT = 100001
	DECLARE @id2 BIGINT = 100002
	DECLARE @id3 BIGINT = 100003
	DECLARE @id4 BIGINT = 100004
	DECLARE @id5 BIGINT = 100005
	DECLARE @id6 BIGINT = 100006
	DECLARE @id7 BIGINT = 100007
	DECLARE @id8 BIGINT = 100008
	DECLARE @id9 BIGINT = 100009

	INSERT INTO @Comments 
	SELECT @id1, 'Task completed', GETUTCDATE(), @idJoeBiden, NULL, NULL UNION
	SELECT @id2, 'Some comment bla bla', GETUTCDATE(), @idJoeBiden, NULL, NULL UNION
	SELECT @id3, 'Not really cool comment', GETUTCDATE(), @idJoeBiden, NULL, NULL UNION
	SELECT @id4, 'Make America great again', GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @id5, 'Trum mines coal', GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @id6, 'Where looting - there will be shooting', GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @id7, 'Ugly comment!', GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @id8, 'Obama''s comment 1', GETUTCDATE(), @idBarakObama, NULL, NULL UNION
	SELECT @id9, 'Obama''s comment 2', GETUTCDATE(), @idBarakObama, NULL, NULL 

	SET IDENTITY_INSERT dbo.[Comment] ON; 

	MERGE dbo.Comment as t USING @Comments as s ON
	(
		t.ID = s.ID
	)
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Text] = s.[Text],
			t.[CreatedDate] = s.[CreatedDate],
			t.[CreatedByID] = s.[CreatedByID],
			t.[ModifiedDate] = s.[ModifiedDate],
			t.[ModifiedByID] = s.[ModifiedByID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Text], [CreatedDate], [CreatedByID], [ModifiedDate], [ModifiedByID])
		VALUES (s.[ID], s.[Text], s.[CreatedDate], s.[CreatedByID], s.[ModifiedDate], s.[ModifiedByID])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;

	SET IDENTITY_INSERT dbo.[Comment] ON;

END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Interview]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_Interview]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testInterviews AS TABLE (
	[ID] [bigint] NOT NULL,
	[ProposalID] [bigint] NOT NULL,
	[InterviewTypeID] [bigint] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[InterviewStatusID] [bigint] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[CretedDate] [datetime] NOT NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL)

	DECLARE @proposalId1 BIGINT = 100001
	DECLARE @proposalId2 BIGINT = 100002
	DECLARE @proposalId3 BIGINT = 100003
	DECLARE @proposalId4 BIGINT = 100004
	DECLARE @proposalId5 BIGINT = 100005
	DECLARE @proposalId6 BIGINT = 100006
	DECLARE @proposalId7 BIGINT = 100007

	DECLARE @interviewId1 BIGINT = 100001
	DECLARE @interviewId2 BIGINT = 100002
	DECLARE @interviewId3 BIGINT = 100003
	DECLARE @interviewId4 BIGINT = 100004
	DECLARE @interviewId5 BIGINT = 100005
	DECLARE @interviewId6 BIGINT = 100006
	DECLARE @interviewId7 BIGINT = 100007
	

	DECLARE @idJoeBiden BIGINT = 100001
	DECLARE @idDonaldTrump BIGINT = 100002
	DECLARE @idBarakObama BIGINT = 100003
	DECLARE @idGeraldFord BIGINT = 100004
	DECLARE @idAbrahamLinkoln BIGINT = 100005

	INSERT INTO @testInterviews
	SELECT @interviewId1, @proposalId7, 1, '2021-06-12 12:00', '2021-06-12 13:00', 2, @idJoeBiden, '2021-05-23 14:55', NULL, NULL UNION
	SELECT @interviewId2, @proposalId6, 2, '2021-06-13 12:00', '2021-06-13 13:00', 2, @idDonaldTrump, '2021-05-24 14:55', NULL, NULL UNION
	SELECT @interviewId3, @proposalId5, 3, '2021-06-14 12:00', '2021-06-14 13:00', 2, @idAbrahamLinkoln, '2021-05-25 14:55', NULL, NULL UNION
	SELECT @interviewId4, @proposalId4, 4, '2021-06-15 12:00', '2021-06-15 13:00', 2, @idJoeBiden, '2021-05-26 14:55', NULL, NULL UNION
	SELECT @interviewId5, @proposalId3, 5, '2021-06-16 12:00', '2021-06-16 13:00', 2, @idDonaldTrump, '2021-05-27 14:55', NULL, NULL UNION
	SELECT @interviewId6, @proposalId2, 4, '2021-06-17 12:00', '2021-06-17 13:00', 2, @idBarakObama, '2021-05-28 14:55', NULL, NULL UNION
	SELECT @interviewId7, @proposalId1, 3, '2021-06-18 12:00', '2021-06-18 13:00', 2, @idBarakObama, '2021-05-29 14:55' , NULL, NULL


	SET IDENTITY_INSERT dbo.[Interview] ON; 

	MERGE dbo.[Interview] t USING @testInterviews s
	ON t.ID = s.ID
	WHEN MATCHED THEN
		UPDATE SET
			t.[ProposalID] = s.[ProposalID],
			t.[InterviewTypeID] = s.[InterviewTypeID],
			t.[StartTime] = s.[StartTime],
			t.[EndTime] = s.[EndTime],
			t.[InterviewStatusID] = s.[InterviewStatusID],
			t.[CreatedByID] = s.[CreatedByID],
			t.[CretedDate] = s.[CretedDate]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [ProposalID], [InterviewTypeID], [StartTime], [EndTime], [InterviewStatusID], [CreatedByID], [CretedDate])
		VALUES (s.[ID], s.[ProposalID], s.[InterviewTypeID], s.[StartTime], s.[EndTime], s.[InterviewStatusID], s.[CreatedByID], s.[CretedDate])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[Interview] OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_InterviewFeedback]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_InterviewFeedback]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testInterviewFeedbacks AS TABLE (
	[ID] [bigint] NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[Rating] [int] NOT NULL,
	[InterviewID] [bigint] NOT NULL,
	[InterviewerID] [bigint] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL)


	DECLARE @interviewId1 BIGINT = 100001
	DECLARE @interviewId2 BIGINT = 100002
	DECLARE @interviewId3 BIGINT = 100003
	DECLARE @interviewId4 BIGINT = 100004
	DECLARE @interviewId5 BIGINT = 100005
	DECLARE @interviewId6 BIGINT = 100006
	DECLARE @interviewId7 BIGINT = 100007
	

	DECLARE @idJoeBiden BIGINT = 100001
	DECLARE @idDonaldTrump BIGINT = 100002
	DECLARE @idBarakObama BIGINT = 100003
	DECLARE @idGeraldFord BIGINT = 100004
	DECLARE @idAbrahamLinkoln BIGINT = 100005

	INSERT INTO @testInterviewFeedbacks
	SELECT 100001, 'Comment - Intervew feedback 100001', 1, @interviewId1, @idJoeBiden, @idJoeBiden, '2021-03-21 12:00', NULL, NULL UNION
	SELECT 100002, 'Comment - Intervew feedback 100002', 2, @interviewId2, @idDonaldTrump, @idDonaldTrump, '2021-03-22 12:00', NULL, NULL UNION
	SELECT 100003, 'Comment - Intervew feedback 100003', 3, @interviewId3, @idBarakObama, @idGeraldFord, '2021-03-23 12:00', NULL, NULL UNION
	SELECT 100004, 'Comment - Intervew feedback 100004', 4, @interviewId4, @idGeraldFord, @idJoeBiden, '2021-03-24 12:00', NULL, NULL UNION
	SELECT 100005, 'Comment - Intervew feedback 100005', 5, @interviewId5, @idGeraldFord, @idJoeBiden, '2021-03-25 12:00', NULL, NULL UNION
	SELECT 100006, 'Comment - Intervew feedback 100006', 6, @interviewId6, @idAbrahamLinkoln, @idJoeBiden, '2021-03-26 12:00', NULL, NULL UNION
	SELECT 100007, 'Comment - Intervew feedback 100007', 7, @interviewId7, @idJoeBiden, @idAbrahamLinkoln, '2021-03-27 12:00', NULL, NULL 
	


	SET IDENTITY_INSERT dbo.[InterviewFeedback] ON; 

	MERGE dbo.[InterviewFeedback] t USING @testInterviewFeedbacks s
	ON t.ID = s.ID
	WHEN MATCHED THEN
		UPDATE SET
			t.[Comment] = s.[Comment],
			t.[Rating] = s.[Rating],
			t.[InterviewID] = s.[InterviewID],
			t.[InterviewerID] = s.[InterviewerID],
			t.[CreatedByID] = s.[CreatedByID],
			t.[CreatedDate] = s.[CreatedDate],
			t.[ModifiedByID] = s.[ModifiedByID],
			t.[ModifiedDate] = s.[ModifiedDate]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Comment], [Rating], [InterviewID], [InterviewerID], [CreatedByID], [CreatedDate], [ModifiedByID], [ModifiedDate])
		VALUES (s.[ID], s.[Comment], s.[Rating], s.[InterviewID], s.[InterviewerID], s.[CreatedByID], s.[CreatedDate], s.[ModifiedByID], s.[ModifiedDate])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[InterviewFeedback] OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_PositionComment]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_PositionComment] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @PositionComment AS TABLE (
	[PositionID] [bigint],
	[CommentID] [bigint])

	DECLARE @positionId1 BIGINT = 100001
	DECLARE @positionId2 BIGINT = 100002
	DECLARE @positionId3 BIGINT = 100003
	DECLARE @positionId4 BIGINT = 100004
	DECLARE @positionId5 BIGINT = 100005
	DECLARE @positionId6 BIGINT = 100006
	DECLARE @positionId7 BIGINT = 100007
	DECLARE @positionId8 BIGINT = 100008
	DECLARE @positionId9 BIGINT = 100009

	DECLARE @id1 BIGINT = 100001
	DECLARE @id2 BIGINT = 100002
	DECLARE @id3 BIGINT = 100003
	DECLARE @id4 BIGINT = 100004
	DECLARE @id5 BIGINT = 100005
	DECLARE @id6 BIGINT = 100006
	DECLARE @id7 BIGINT = 100007
	DECLARE @id8 BIGINT = 100008
	DECLARE @id9 BIGINT = 100009

	INSERT INTO @PositionComment 
	SELECT @positionId1, @id1 UNION
	SELECT @positionId2, @id2 UNION
	SELECT @positionId3, @id3 UNION
	SELECT @positionId4, @id4 UNION
	SELECT @positionId5, @id5 UNION
	SELECT @positionId6, @id6 UNION
	SELECT @positionId7, @id7 UNION
	SELECT @positionId8, @id8 UNION
	SELECT @positionId9, @id9
	


	MERGE dbo.PositionComment as t USING @PositionComment as s ON
	(
		t.PositionID = s.PositionID AND
		t.CommentID = s.CommentID
	)
	WHEN MATCHED THEN 
		UPDATE SET
			t.[PositionID] = s.[PositionID],
			t.[CommentID] = s.[CommentID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([PositionID], [CommentID])
		VALUES (s.[PositionID], s.[CommentID])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;

	

END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Positions]    Script Date: 8/21/2021 11:22:09 AM ******/
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
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_ProposalComment]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_ProposalComment] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @ProposalComment AS TABLE (
	[ProposalID] [bigint],
	[CommentID] [bigint])

	DECLARE @proposalId1 BIGINT = 100001
	DECLARE @proposalId2 BIGINT = 100002
	DECLARE @proposalId3 BIGINT = 100003
	DECLARE @proposalId4 BIGINT = 100004
	DECLARE @proposalId5 BIGINT = 100005
	DECLARE @proposalId6 BIGINT = 100006
	DECLARE @proposalId7 BIGINT = 100007


	DECLARE @id1 BIGINT = 100001
	DECLARE @id2 BIGINT = 100002
	DECLARE @id3 BIGINT = 100003
	DECLARE @id4 BIGINT = 100004
	DECLARE @id5 BIGINT = 100005
	DECLARE @id6 BIGINT = 100006
	DECLARE @id7 BIGINT = 100007
	DECLARE @id8 BIGINT = 100008
	DECLARE @id9 BIGINT = 100009

	INSERT INTO @ProposalComment 
	SELECT @proposalId1, @id1 UNION
	SELECT @proposalId2, @id2 UNION
	SELECT @proposalId3, @id3 UNION
	SELECT @proposalId4, @id4 UNION
	SELECT @proposalId5, @id5 UNION
	SELECT @proposalId6, @id6 UNION
	SELECT @proposalId7, @id7 
	


	MERGE dbo.ProposalComment as t USING @ProposalComment as s ON
	(
		t.ProposalID = s.ProposalID AND
		t.CommentID = s.CommentID
	)
	WHEN MATCHED THEN 
		UPDATE SET
			t.[ProposalID] = s.[ProposalID],
			t.[CommentID] = s.[CommentID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ProposalID], [CommentID])
		VALUES (s.[ProposalID], s.[CommentID])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;

	

END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Proposals]    Script Date: 8/21/2021 11:22:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_Proposals]

AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @idJoeBiden BIGINT = 100001
	DECLARE @idDonaldTrump BIGINT = 100002
	DECLARE @idBarakObama BIGINT = 100003

	DECLARE @candidateId1 BIGINT = 100001
	DECLARE @candidateId2 BIGINT = 100002
	DECLARE @candidateId3 BIGINT = 100003

	DECLARE @positionId1 BIGINT = 100001
	DECLARE @positionId2 BIGINT = 100002
	DECLARE @positionId3 BIGINT = 100003
	DECLARE @positionId4 BIGINT = 100004
	DECLARE @positionId5 BIGINT = 100005
	DECLARE @positionId6 BIGINT = 100006
	DECLARE @positionId7 BIGINT = 100007
	DECLARE @positionId8 BIGINT = 100008
	DECLARE @positionId9 BIGINT = 100009

	DECLARE @proposals AS TABLE (
		[ProposalID] [bigint] NOT NULL,
		[PositionID] [bigint] NOT NULL,
		[CandidateID] [bigint] NOT NULL,
		[Proposed] [datetime] NOT NULL,
		[CurrentStepID] [bigint] NOT NULL,
		[StepSetDate] [datetime] NOT NULL,
		[NextStepID] [bigint] NULL,
		[DueDate] [datetime] NULL,
		[StatusID] [bigint] NOT NULL,
		[CreatedByID] [bigint] NULL,
		[CreatedDate] [datetime] NULL
	)

	INSERT INTO @proposals
	SELECT 100001, @positionId1, @candidateId1, '2021-03-25 16:45', 1, '2021-03-25 16:45', 2, '2021-04-01 11:34', 1, @idJoeBiden, '2021-03-25 16:45' UNION
	SELECT 100002, @positionId2, @candidateId2, '2021-03-26 06:45', 2, '2021-03-26 06:45', 3, '2021-04-15 15:00', 1, @idDonaldTrump, '2021-03-26 16:45' UNION
	SELECT 100003, @positionId3, @candidateId2, '2021-04-18 14:45', 3, '2021-04-18 14:45', 4, '2021-04-23 04:45', 2, @idBarakObama, '2021-04-18 14:45' UNION
	SELECT 100004, @positionId2, @candidateId3, '2021-03-26 06:45', 2, '2021-03-26 06:45', 3, '2021-04-15 15:00', 1, @idDonaldTrump, '2021-03-26 16:45' UNION
	SELECT 100005, @positionId3, @candidateId3, '2021-04-18 14:45', 3, '2021-04-18 14:45', 4, '2021-04-23 16:45', 3, @idBarakObama, '2021-04-18 14:45' UNION
	SELECT 100006, @positionId4, @candidateId3, '2021-03-26 06:45', 2, '2021-03-26 06:45', 3, '2021-04-15 15:00', 1, @idBarakObama, '2021-03-26 16:45' UNION
	SELECT 100007, @positionId5, @candidateId3, '2021-04-18 14:45', 3, '2021-04-18 14:45', 4, '2021-04-23 14:45', 2, @idBarakObama, '2021-04-18 14:45'

	SET IDENTITY_INSERT dbo.[Proposal] ON

	MERGE dbo.[Proposal] t USING @proposals s
	ON t.ID = s.ProposalID
	WHEN MATCHED THEN
		UPDATE SET
			t.PositionID = s.PositionID,
			t.CandidateID = s.CandidateID,
			t.Proposed = s.Proposed,
			t.CurrentStepID = s.CurrentStepID,
			t.StepSetDate = s.StepSetDate,
			t.NextStepID = s.NextStepID,
			t.DueDate = s.DueDate,
			t.StatusID = s.StatusID,
			t.[CreatedByID] = s.[CreatedByID],
			t.[CreatedDate] = s.[CreatedDate]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[PositionID],[CandidateID],[Proposed],[CurrentStepID],[StepSetDate],[NextStepID],[DueDate],[StatusID],[CreatedByID],[CreatedDate])
		VALUES (s.[ProposalID],s.[PositionID],s.[CandidateID],s.[Proposed],s.[CurrentStepID],s.[StepSetDate],s.[NextStepID],s.[DueDate],s.[StatusID],s.[CreatedByID],s.[CreatedDate])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[Proposal] OFF
END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Skills]    Script Date: 8/21/2021 11:22:09 AM ******/
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
	ON t.ID = s.SkillID
	WHEN MATCHED THEN
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED THEN
		INSERT ([ID], [Name]) VALUES (s.[SkillID], s.[Name]);

	SET IDENTITY_INSERT dbo.[Skill] ON;

END
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Users]    Script Date: 8/21/2021 11:22:09 AM ******/
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
	DECLARE @idGeraldFord BIGINT = 100004
	DECLARE @idAbrahamLinkoln BIGINT = 100005

	-- Password for test users - 'TestPassword-202107061104'
	INSERT INTO @testUsers
	SELECT @idJoeBiden, 'HRT\JoeB', 'Joe', 'Biden', NULL, NULL, 'ZNX2SjDtumpPibGBWMUyRBJdaqF6q2WiZvaOA7ng0yg=', 'SALT12345' UNION
	SELECT @idDonaldTrump, 'HRT\DonaldT', 'Donald', 'Trump', NULL, NULL, 'ZNX2SjDtumpPibGBWMUyRBJdaqF6q2WiZvaOA7ng0yg=', 'SALT12345' UNION
	SELECT @idBarakObama, 'HRT\BarakO', 'Barak', 'Obama', NULL, NULL, 'ZNX2SjDtumpPibGBWMUyRBJdaqF6q2WiZvaOA7ng0yg=', 'SALT12345' UNION
	SELECT @idGeraldFord, 'HRT\GeraldF', 'Gerald', 'Ford', NULL, NULL, 'ZNX2SjDtumpPibGBWMUyRBJdaqF6q2WiZvaOA7ng0yg=', 'SALT12345' UNION
	SELECT @idAbrahamLinkoln, 'HRT\AbrahamL', 'Abraham', 'Linkoln', NULL, NULL, 'ZNX2SjDtumpPibGBWMUyRBJdaqF6q2WiZvaOA7ng0yg=', 'SALT12345' 


	SET IDENTITY_INSERT dbo.[User] ON; 

	MERGE dbo.[User] t USING @testUsers s
	ON t.ID = s.UserID
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
		INSERT ([ID], [Login], [FirstName], [LastName], [Email], [Description], [PwdHash], [Salt])
		VALUES (s.[UserID], s.[Login], s.[FirstName], s.[LastName], s.[Email], s.[Description], s.[PwdHash], s.[Salt])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE

	;
	SET IDENTITY_INSERT dbo.[User] OFF;
END
GO
