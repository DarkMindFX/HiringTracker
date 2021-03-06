USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Interview]    Script Date: 8/21/2021 12:25:46 PM ******/
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
