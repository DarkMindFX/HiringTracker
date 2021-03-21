DECLARE @login AS NVARCHAR(50) = '[Test RTYHGFVBN] Inserted User'

IF( NOT EXISTS(SELECT 1 FROM dbo.[User] s WHERE s.[Login] = @login) ) 
BEGIN
    THROW 51001, 'User was not inserted', 1
END
ELSE
BEGIN

    DELETE FROM dbo.[User]
    WHERE
        [Login] = @login
END