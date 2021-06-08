DECLARE @ID BIGINT = 361640
DECLARE @Name NVARCHAR(50) = 'Name 4f199c3f8a934a97847da99b1c0a210c'

DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
					[dbo].[SkillProficiency]
				WHERE 
					(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1))
BEGIN
	SET @Fail = 1
END

DELETE FROM [dbo].[SkillProficiency]
WHERE
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'SkillProficiency was not deleted', 1
END