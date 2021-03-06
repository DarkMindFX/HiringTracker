USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_PositionStatus_Populate]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionStatus_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @statuses AS TABLE (
		[StatusID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @statuses
	SELECT 1, 'Draft' UNION
	SELECT 2, 'Open' UNION
	SELECT 3, 'On Hold' UNION
	SELECT 4, 'Closed' UNION
	SELECT 5, 'Cancelled' 

	SET IDENTITY_INSERT dbo.[PositionStatus] ON;

	MERGE dbo.PositionStatus t USING @statuses s 
	ON
		t.[ID] = s.[StatusID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Name]) VALUES (s.[StatusID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;


	SET IDENTITY_INSERT dbo.[PositionStatus] OFF;


END
GO
