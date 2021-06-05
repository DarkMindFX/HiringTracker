{FIELDS_VARIABLES_LIST_DELETE}

DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
					[dbo].[{Entity}]
				WHERE 
					{WHERE_FIELDS_LIST}))
BEGIN
	SET @Fail = 1
END

DELETE FROM [dbo].[{Entity}]
WHERE
	{WHERE_FIELDS_LIST}

IF(@Fail = 1) 
BEGIN
	THROW 51001, '{Entity} was not deleted', 1
END