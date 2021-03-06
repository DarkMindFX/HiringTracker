USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Role_Populate]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Role_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @role AS TABLE (
		[RoleID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @role
	SELECT 1, 'Admin'			UNION
	SELECT 2, 'Viewer'			UNION
	SELECT 3, 'Position Owner'	UNION
	SELECT 4, 'Candidate Owner'	UNION
	SELECT 5, 'Interviewer'		UNION
	SELECT 6, 'Recruiter'

	SET IDENTITY_INSERT dbo.[Role] ON;

	MERGE dbo.[Role] t USING @role s 
	ON
		t.[ID] = s.[RoleID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Name]) VALUES (s.[RoleID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.[Role] OFF;

END
GO
