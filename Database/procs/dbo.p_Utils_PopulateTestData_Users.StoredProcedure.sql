USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Users]    Script Date: 8/21/2021 12:25:46 PM ******/
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
