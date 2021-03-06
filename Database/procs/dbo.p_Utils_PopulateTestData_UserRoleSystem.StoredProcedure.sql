USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_UserRoleSystem]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_UserRoleSystem]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testSystemRoles AS TABLE (
	[UserID]  [bigint] NOT NULL,
	[RoleID]  [bigint] NOT NULL)



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

	INSERT INTO @testSystemRoles
	SELECT  @idJoeBiden, @role1 UNION
	SELECT  @idDonaldTrump, @role2 UNION
	SELECT  @idBarakObama, @role4 UNION
	SELECT  @idGeraldFord, @role5 UNION
	SELECT  @idAbrahamLinkoln, @role6 	


	MERGE dbo.[UserRoleSystem] t USING @testSystemRoles s
	ON t.UserID = s.UserID
	WHEN MATCHED THEN
		UPDATE SET
			t.[UserID] = s.[UserID],
			t.[RoleID] = s.[RoleID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([UserID], [RoleID])
		VALUES (s.[UserID], s.[RoleID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

END
GO
