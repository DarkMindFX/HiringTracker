USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_UserRoleCandidate]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_UserRoleCandidate]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testCandidateRoles AS TABLE (
	[CandidateID] [bigint] NOT NULL,
	[UserID]  [bigint] NOT NULL,
	[RoleID]  [bigint] NOT NULL)


	DECLARE @candidateId1 BIGINT = 100001
	DECLARE @candidateId2 BIGINT = 100002
	DECLARE @candidateId3 BIGINT = 100003
	DECLARE @candidateId4 BIGINT = 100004
	DECLARE @candidateId5 BIGINT = 100005
	DECLARE @candidateId6 BIGINT = 100006
	DECLARE @candidateId7 BIGINT = 100007
	

	DECLARE @idJoeBiden BIGINT = 100001
	DECLARE @idDonaldTrump BIGINT = 100002
	DECLARE @idBarakObama BIGINT = 100003
	DECLARE @idGeraldFord BIGINT = 100004
	DECLARE @idAbrahamLinkoln BIGINT = 100005

	DECLARE @role1 BIGINT = 1
	DECLARE @role2 BIGINT = 2
	DECLARE @role3 BIGINT = 3
	DECLARE @role4 BIGINT = 4
	DECLARE @role5 BIGINT = 5
	DECLARE @role6 BIGINT = 6

	INSERT INTO @testCandidateRoles
	SELECT @candidateId1, @idJoeBiden, @role1 UNION
	SELECT @candidateId1, @idDonaldTrump, @role2 UNION
	SELECT @candidateId1, @idAbrahamLinkoln, @role3 UNION
	SELECT @candidateId2, @idBarakObama, @role2 UNION
	SELECT @candidateId2, @idDonaldTrump, @role3 UNION
	SELECT @candidateId2, @idAbrahamLinkoln, @role4 UNION
	SELECT @candidateId3, @idGeraldFord, @role1 UNION
	SELECT @candidateId3, @idBarakObama, @role3 UNION
	SELECT @candidateId3, @idDonaldTrump, @role6 UNION
	SELECT @candidateId4, @idGeraldFord, @role1 UNION
	SELECT @candidateId5, @idAbrahamLinkoln, @role3 UNION
	SELECT @candidateId6, @idDonaldTrump, @role6 
	


	MERGE dbo.[UserRoleCandidate] t USING @testCandidateRoles s
	ON t.CandidateID = s.CandidateID AND t.UserID = s.UserID
	WHEN MATCHED THEN
		UPDATE SET
			t.[CandidateID] = s.[CandidateID],
			t.[UserID] = s.[UserID],
			t.[RoleID] = s.[RoleID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([CandidateID], [UserID], [RoleID])
		VALUES (s.[CandidateID], s.[UserID], s.[RoleID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

END
GO
