

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name c7ffa425000a41a78a2958f0bc9c61eb'
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[InterviewStatus]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[InterviewStatus]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'InterviewStatus was not deleted', 1
END