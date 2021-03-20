
DECLARE @PositionTitle AS NVARCHAR(50) = '[Test] Position 65TRF435G';
DECLARE @PositionShortDesc AS NVARCHAR(50) = '[Test] Position Short Desc 65TRF435G';
DECLARE @PositionDesc AS NVARCHAR(50) = '[Test] Position 65TRF435G Full Desc';

DELETE FROM dbo.Position 
WHERE
	Title = @PositionTitle