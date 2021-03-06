USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_InterviewRole]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_InterviewRole]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testInterviewRoles AS TABLE (
	[InterviewID] [bigint] NOT NULL,
	[UserID]  [bigint] NOT NULL,
	[RoleID]  [bigint] NOT NULL)


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

	DECLARE @role1 BIGINT = 1
	DECLARE @role2 BIGINT = 2
	DECLARE @role3 BIGINT = 3
	DECLARE @role4 BIGINT = 4
	DECLARE @role5 BIGINT = 5
	DECLARE @role6 BIGINT = 6

	INSERT INTO @testInterviewRoles
	SELECT @interviewId1, @idJoeBiden, @role1 UNION
	SELECT @interviewId1, @idDonaldTrump, @role2 UNION
	SELECT @interviewId1, @idAbrahamLinkoln, @role3 UNION
	SELECT @interviewId2, @idBarakObama, @role2 UNION
	SELECT @interviewId2, @idDonaldTrump, @role3 UNION
	SELECT @interviewId2, @idAbrahamLinkoln, @role4 UNION
	SELECT @interviewId3, @idGeraldFord, @role1 UNION
	SELECT @interviewId3, @idBarakObama, @role3 UNION
	SELECT @interviewId3, @idDonaldTrump, @role6 UNION
	SELECT @interviewId4, @idGeraldFord, @role1 UNION
	SELECT @interviewId5, @idAbrahamLinkoln, @role3 UNION
	SELECT @interviewId6, @idDonaldTrump, @role6 
	


	MERGE dbo.[InterviewRole] t USING @testInterviewRoles s
	ON t.InterviewID = s.InterviewID AND t.UserID = s.UserID
	WHEN MATCHED THEN
		UPDATE SET
			t.[InterviewID] = s.[InterviewID],
			t.[UserID] = s.[UserID],
			t.[RoleID] = s.[RoleID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([InterviewID], [UserID], [RoleID])
		VALUES (s.[InterviewID], s.[UserID], s.[RoleID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

END
GO
