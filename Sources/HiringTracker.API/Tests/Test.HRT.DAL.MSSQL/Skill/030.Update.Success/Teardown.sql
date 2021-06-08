DECLARE @ID BIGINT
DECLARE @Name NVARCHAR(50) = 'Name 3eace487dea8457b90004271cbc4c3b2'
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Skill]
				WHERE 
					(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1))
BEGIN
	SET @Fail = 1
END

DELETE FROM [dbo].[Skill]
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Skill was not updated', 1
END