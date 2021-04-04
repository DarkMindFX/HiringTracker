DECLARE @login AS NVARCHAR(50) = '[Test OPRFGHUB] Inserted User'
DECLARE @usersCount AS BIGINT

SELECT @usersCount = COUNT(1) FROM dbo.[User] s WHERE s.[Login] = @login

DELETE FROM dbo.[User]
    WHERE
        [Login] = @login

IF(@usersCount > 1) 
BEGIN
    THROW 51001, 'User was inserted - duplicate logins', 1
END
