DECLARE @login AS NVARCHAR(50) = '[Test QAZXCVFR34] Delete User'

IF(EXISTS(SELECT 1 FROM dbo.[User] s WHERE s.[Login] = @login) ) 
BEGIN
	DELETE FROM dbo.[User]
    WHERE
        [Login] = @login;

    THROW 51001, '[TEST FAIL] User was not deleted', 1
END
