

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 341ecb0d2c0b4ecaaa628c91a8a12074'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updName NVARCHAR(50) = 'Name 7e82b630cc75460d80cd25ce2cffbf8e'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[InterviewStatus]
				WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[InterviewStatus]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[InterviewStatus]
	WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'InterviewStatus was not updated', 1
END