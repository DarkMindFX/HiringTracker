

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 2944b4bdcff74fb18aed50b0c4785ea8'
 

DELETE FROM [PositionStatus]
FROM 
	[dbo].[PositionStatus] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
