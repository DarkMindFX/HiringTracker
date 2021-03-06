USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewStatus_Populate]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewStatus_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @statuses AS TABLE (
		[StatusID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @statuses
	SELECT 1, 'Scheduled' UNION
	SELECT 2, 'In Progress' UNION
	SELECT 3, 'Waiting Feedback' UNION
	SELECT 4, 'Completed' UNION
	SELECT 5, 'Cancelled' 


	MERGE dbo.InterviewStatus t USING @statuses s 
	ON
		t.[StatusID] = s.[StatusID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([StatusID], [Name]) VALUES (s.[StatusID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;





END
GO
