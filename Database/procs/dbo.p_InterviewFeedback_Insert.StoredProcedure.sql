USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewFeedback_Insert]    Script Date: 8/21/2021 12:25:46 PM ******/
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
