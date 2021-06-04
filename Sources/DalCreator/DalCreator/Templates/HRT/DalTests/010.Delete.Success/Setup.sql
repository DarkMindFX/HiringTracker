﻿DECLARE @Name AS NVARCHAR(50) = '[Name 8841C55FF8124424AED00EA4494CA53A]'
DECLARE @UUID AS NVARCHAR(50) = '[UUID 8841C55FF8124424AED00EA4494CA53A]'
DECLARE @ParentID AS BIGINT = NULL

DECLARE @ID AS BIGINT


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[{Entity}]
				WHERE 
					{WHERE_FIELDS_LIST}))
BEGIN
	INSERT INTO [dbo].[{Entity}]
	SELECT 		
		@Name, @UUID, @ParentID
END

SELECT TOP 1 @ID = [ID] 
FROM 
	[dbo].[{Entity}] e
WHERE
	{WHERE_FIELDS_LIST}

SELECT @ID