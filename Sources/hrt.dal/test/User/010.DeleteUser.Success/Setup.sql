DECLARE @login AS NVARCHAR(50) = '[Test QAZXCVFR34] Delete User'

IF(NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE [Login] = @login))
BEGIN
    DECLARE @NewUserID AS BIGINT = NULL

    EXEC dbo.[p_User_Upsert]
        NULL,
        @login,
        '[Test QAZXCVFR34] 2Delete First',
        '[Test QAZXCVFR34] 2Delete Last',
        'user2delete@email.com',
        NULL,
        'PwdHash346',
        'Salt123',
        100001,
        @NewUserID OUT

    IF(@NewUserID IS NULL) 
        THROW 51001, 'Testuser for DELETE was not prepared', 1
END
