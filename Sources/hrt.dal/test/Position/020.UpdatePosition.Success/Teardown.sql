
DECLARE @PositionTitle AS NVARCHAR(50) = '[Test] Position 987GFGHHT';
DECLARE @PositionShortDesc AS NVARCHAR(50) = '[Test] Position Short Desc 987GFGHHT';
DECLARE @PositionDesc AS NVARCHAR(50) = '[Test] Position 987GFGHHT Full Desc';

DECLARE @NewPositionTitle AS NVARCHAR(50) = '[UPDATED][Test] Position 987GFGHHT';
DECLARE @NewPositionShortDesc AS NVARCHAR(50) = '[UPDATED][Test] Position Short Desc 987GFGHHT';
DECLARE @NewPositionDesc AS NVARCHAR(50) = '[UPDATED][Test] Position 987GFGHHT Full Desc';

DECLARE @userID AS BIGINT = 100001
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM dbo.Position WHERE
	Title = @NewPositionTitle AND
	ShortDesc = @NewPositionShortDesc AND
	[Description] = @NewPositionDesc))
BEGIN
	SET @Fail = 1
	
	DELETE FROM 
		dbo.Position 
	WHERE
		Title = @PositionTitle AND
		ShortDesc = @PositionShortDesc AND
		[Description] = @PositionDesc
END
ELSE
BEGIN
	DELETE FROM 
	dbo.Position 
	WHERE
	Title = @NewPositionTitle AND
	ShortDesc = @NewPositionShortDesc AND
	[Description] = @NewPositionDesc
END

IF(@Fail = 1)
BEGIN
	THROW 51001, '[TEST FAIL] Position was not updated by DAL', 1
END
