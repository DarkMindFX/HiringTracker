DECLARE @ID BIGINT
DECLARE @Name NVARCHAR(50) = 'Name 7dcf0303dc684e38a579181f89c31012'

DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
					[dbo].[PositionStatus]
				WHERE 
					(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1))
BEGIN
	SET @Fail = 1
END

DELETE FROM [dbo].[PositionStatus]
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'PositionStatus was not deleted', 1
END