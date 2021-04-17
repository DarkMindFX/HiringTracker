
DECLARE @PositionTitle AS NVARCHAR(50) = '[Test] Position TGSVJ654';
DECLARE @PositionShortDesc AS NVARCHAR(50) = '[Test] Position Short Desc TGSVJ654';
DECLARE @PositionDesc AS NVARCHAR(50) = '[Test] Position TGSVJ654 Full Desc';

DECLARE @userID AS BIGINT = 100001
DECLARE @Fail AS BIT = 0

DELETE FROM 
		dbo.Position 
WHERE
		Title = @PositionTitle AND
		ShortDesc = @PositionShortDesc AND
		[Description] = @PositionDesc

