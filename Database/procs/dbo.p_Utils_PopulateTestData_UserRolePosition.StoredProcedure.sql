USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_UserRolePosition]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_UserRolePosition]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @testPositionRoles AS TABLE (
	[PositionID] [bigint] NOT NULL,
	[UserID]  [bigint] NOT NULL,
	[RoleID]  [bigint] NOT NULL)


	DECLARE @positionId1 BIGINT = 100001
	DECLARE @positionId2 BIGINT = 100002
	DECLARE @positionId3 BIGINT = 100003
	DECLARE @positionId4 BIGINT = 100004
	DECLARE @positionId5 BIGINT = 100005
	DECLARE @positionId6 BIGINT = 100006
	DECLARE @positionId7 BIGINT = 100007
	

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

	INSERT INTO @testPositionRoles
	SELECT @positionId1, @idJoeBiden, @role1 UNION
	SELECT @positionId1, @idDonaldTrump, @role2 UNION
	SELECT @positionId1, @idAbrahamLinkoln, @role3 UNION
	SELECT @positionId2, @idBarakObama, @role2 UNION
	SELECT @positionId2, @idDonaldTrump, @role3 UNION
	SELECT @positionId2, @idAbrahamLinkoln, @role4 UNION
	SELECT @positionId3, @idGeraldFord, @role1 UNION
	SELECT @positionId3, @idBarakObama, @role3 UNION
	SELECT @positionId3, @idDonaldTrump, @role6 UNION
	SELECT @positionId4, @idGeraldFord, @role1 UNION
	SELECT @positionId5, @idAbrahamLinkoln, @role3 UNION
	SELECT @positionId6, @idDonaldTrump, @role6 
	


	MERGE dbo.[UserRolePosition] t USING @testPositionRoles s
	ON t.PositionID = s.PositionID AND t.UserID = s.UserID
	WHEN MATCHED THEN
		UPDATE SET
			t.[PositionID] = s.[PositionID],
			t.[UserID] = s.[UserID],
			t.[RoleID] = s.[RoleID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([PositionID], [UserID], [RoleID])
		VALUES (s.[PositionID], s.[UserID], s.[RoleID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

END
GO
