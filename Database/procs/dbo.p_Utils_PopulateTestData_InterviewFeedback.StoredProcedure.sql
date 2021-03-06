USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_InterviewFeedback]    Script Date: 8/21/2021 12:25:46 PM ******/
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
