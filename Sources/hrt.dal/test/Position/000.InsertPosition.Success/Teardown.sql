
DECLARE @PositionTitle AS NVARCHAR(50) = '[Test] Position TYHGFVB543';
DECLARE @PositionShortDesc AS NVARCHAR(50) = '[Test] Position Short Desc TYHGFVB543';
DECLARE @PositionDesc AS NVARCHAR(50) = '[Test] Position TYHGFVB543 Full Desc';
DECLARE @userID AS BIGINT = 100001

IF(EXISTS(SELECT 1 FROM dbo.Position WHERE
	Title = @PositionTitle AND
	ShortDesc = @PositionShortDesc AND
	[Description] = @PositionDesc))
BEGIN
	DELETE FROM 
	dbo.Position 
	WHERE
	Title = @PositionTitle AND
	ShortDesc = @PositionShortDesc AND
	[Description] = @PositionDesc
END
ELSE
BEGIN
	THROW 51001, '[TEST FAIL] Position was not created', 1
END
