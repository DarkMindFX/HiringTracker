

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name ae27500ece594b489de57fb16bd77853'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updName NVARCHAR(50) = 'Name 7491e01e15744ecd86af07da8e911600'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Skill]
				WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Skill]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Skill]
	WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Skill was not updated', 1
END