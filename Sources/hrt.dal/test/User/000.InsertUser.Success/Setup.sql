DECLARE @login AS NVARCHAR(50) = '[Test RTYHGFVBN] Inserted User'

DELETE FROM dbo.[User]
WHERE
    [Login] = @login
