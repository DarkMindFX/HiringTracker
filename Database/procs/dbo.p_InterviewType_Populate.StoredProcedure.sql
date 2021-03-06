USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_InterviewType_Populate]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_InterviewType_Populate]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @type AS TABLE (
		[TypeID]				BIGINT,
		[Name]					NVARCHAR(50)
	)

	INSERT INTO @type
	SELECT 1, 'Screening'	UNION
	SELECT 2, 'Technical'	UNION
	SELECT 3, 'HR'			UNION
	SELECT 4, 'Offer'		UNION
	SELECT 5, 'Follow-up' 


	MERGE dbo.InterviewType t USING @type s 
	ON
		t.[TypeID] = s.[TypeID]
	WHEN MATCHED THEN 
		UPDATE SET
			t.[Name] = s.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([TypeID], [Name]) VALUES (s.[TypeID], s.[Name])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;



END
GO
