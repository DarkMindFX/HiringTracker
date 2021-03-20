
DECLARE @PositionTitle AS NVARCHAR(50) = '[Test] Position 65TRF435G';
DECLARE @PositionShortDesc AS NVARCHAR(50) = '[Test] Position Short Desc 65TRF435G';
DECLARE @PositionDesc AS NVARCHAR(50) = '[Test] Position 65TRF435G Full Desc';
DECLARE @userID AS BIGINT

SET @userID = 100001;
DECLARE @NewPositionID AS BIGINT= NULL

IF(NOT EXISTS(SELECt 1 FROm dbo.Position WHERE Title = @PositionTitle))
BEGIN


	EXEC dbo.p_Position_Upsert 
		NULL,
		NULL,
		@PositionTitle,
		@PositionShortDesc,
		@PositionDesc,
		1,
		@userID,
		@NewPositionID OUT

	IF(@NewPositionID IS NULL) BEGIN
		THROW 51001, 'Test position for DELETE was not prepared', 1
	END
END
