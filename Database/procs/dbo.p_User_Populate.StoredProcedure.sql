USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_User_Populate]    Script Date: 8/21/2021 12:25:46 PM ******/
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
