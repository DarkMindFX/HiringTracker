DECLARE @Title AS NVARCHAR(50) = '[Test {16C8B6BE-19D3-4FD5-B97D-B0F31C0ECFF3}]'

DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM dbo.Position WHERE Title = @Title))
BEGIN
	SET @Fail = 1
	DELETE FROM dbo.Position WHERE Title = @Title
END

IF(@Fail = 1)
BEGIN
	THROW 51001, 'TEST FAILED: Position was not deleted', 1
END