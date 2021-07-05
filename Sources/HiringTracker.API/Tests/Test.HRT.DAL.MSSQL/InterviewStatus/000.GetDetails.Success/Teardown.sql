

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 5eb1c149eefd47debeb7c479f138d881'
 

DELETE FROM [InterviewStatus]
FROM 
	[dbo].[InterviewStatus] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
