USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Proposals]    Script Date: 8/21/2021 12:25:46 PM ******/
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
