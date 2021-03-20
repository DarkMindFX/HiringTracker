
DECLARE @PositionTitle AS NVARCHAR(50) = '[Test] Position 65TRF435G';
DECLARE @PositionShortDesc AS NVARCHAR(50) = '[Test] Position Short Desc 65TRF435G';
DECLARE @PositionDesc AS NVARCHAR(50) = '[Test] Position 65TRF435G Full Desc';

DECLARE @userID AS BIGINT = 100001
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM dbo.Position WHERE
	Title = @PositionTitle AND
	ShortDesc = @PositionShortDesc AND
	[Description] = @PositionDesc))
BEGIN
	SET @Fail = 1
	
	DELETE FROM 
	dbo.Position 
	WHERE
	Title = @PositionTitle AND
	ShortDesc = @PositionShortDesc AND
	[Description] = @PositionDesc
END
IF(@Fail = 1)
BEGIN
	THROW 51001, '[TEST FAIL] Position was not deleted by DAL', 1
END
