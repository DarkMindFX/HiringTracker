﻿DECLARE @Title AS NVARCHAR(50) = '[Test {A3841D7B15D74E9EAA9E0F81FA77A64B}] Updated'
DECLARE @ShortDesc AS NVARCHAR(250) = '[Test {A3841D7B15D74E9EAA9E0F81FA77A64B}] Updated'
DECLARE @Desc AS NVARCHAR(250) = '[Test {A3841D7B15D74E9EAA9E0F81FA77A64B}] Desc Updated'
DECLARE @UserID AS BIGINT = 100001
DECLARE @StatusID AS BIGINT = 1
DECLARE @PositionID AS BIGINT

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM dbo.Position WHERE [Title] = @Title AND [ShortDesc] = @ShortDesc AND [Description] = @Desc))
BEGIN
	SET @Fail = 1	
END

DELETE FROM dbo.Position WHERE Title LIKE '%{A3841D7B15D74E9EAA9E0F81FA77A64B}%'

IF(@Fail = 1)
BEGIN
	THROW 51001, 'TEST FAILED: Position was not updated', 1;
END