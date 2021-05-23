﻿DECLARE @Title AS NVARCHAR(50) = '[Test {E82969A94FA5417986C4F70040CBF6F5}] Skills'
DECLARE @ShortDesc AS NVARCHAR(250) = '[Test {E82969A94FA5417986C4F70040CBF6F5}] ShortDesc'
DECLARE @Desc AS NVARCHAR(250) = '[Test {E82969A94FA5417986C4F70040CBF6F5}] Desc'
DECLARE @UserID AS BIGINT = 100001
DECLARE @StatusID AS BIGINT = 1
DECLARE @PositionID AS BIGINT

IF(NOT EXISTS(SELECT 1 FROM dbo.Position WHERE Title = @Title))
BEGIN
	EXEC [dbo].[p_Position_Upsert]
		NULL,
		NULL,
		@Title,
		@ShortDesc,
		@Desc,
		@StatusID,
		@UserID,
		@PositionID OUT
END