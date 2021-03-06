USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData_Comments]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData_Comments] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @Comments AS TABLE (
	[ID] [bigint],
	[Text] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL)

	DECLARE @idJoeBiden BIGINT = 100001
	DECLARE @idDonaldTrump BIGINT = 100002
	DECLARE @idBarakObama BIGINT = 100003

	DECLARE @id1 BIGINT = 100001
	DECLARE @id2 BIGINT = 100002
	DECLARE @id3 BIGINT = 100003
	DECLARE @id4 BIGINT = 100004
	DECLARE @id5 BIGINT = 100005
	DECLARE @id6 BIGINT = 100006
	DECLARE @id7 BIGINT = 100007
	DECLARE @id8 BIGINT = 100008
	DECLARE @id9 BIGINT = 100009

	INSERT INTO @Comments 
	SELECT @id1, 'Task completed', GETUTCDATE(), @idJoeBiden, NULL, NULL UNION
	SELECT @id2, 'Some comment bla bla', GETUTCDATE(), @idJoeBiden, NULL, NULL UNION
	SELECT @id3, 'Not really cool comment', GETUTCDATE(), @idJoeBiden, NULL, NULL UNION
	SELECT @id4, 'Make America great again', GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @id5, 'Trum mines coal', GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @id6, 'Where looting - there will be shooting', GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @id7, 'Ugly comment!', GETUTCDATE(), @idDonaldTrump, NULL, NULL UNION
	SELECT @id8, 'Obama''s comment 1', GETUTCDATE(), @idBarakObama, NULL, NULL UNION
	SELECT @id9, 'Obama''s comment 2', GETUTCDATE(), @idBarakObama, NULL, NULL 

	SET IDENTITY_INSERT dbo.[Comment] ON; 

	MERGE dbo.Comment as t USING @Comments as s ON
	(
		t.ID = s.ID
	)
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Text] = s.[Text],
			t.[CreatedDate] = s.[CreatedDate],
			t.[CreatedByID] = s.[CreatedByID],
			t.[ModifiedDate] = s.[ModifiedDate],
			t.[ModifiedByID] = s.[ModifiedByID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Text], [CreatedDate], [CreatedByID], [ModifiedDate], [ModifiedByID])
		VALUES (s.[ID], s.[Text], s.[CreatedDate], s.[CreatedByID], s.[ModifiedDate], s.[ModifiedByID])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;

	SET IDENTITY_INSERT dbo.[Comment] ON;

END
GO
