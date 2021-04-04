DECLARE @login AS NVARCHAR(50) = '[Test OPRFGHUB] Inserted User'

IF(NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE [Login] = @login))
BEGIN
    DECLARE @NewUserID AS BIGINT = NULL

    EXEC dbo.[p_User_Upsert]
        NULL,
        @login,
        '[Test OPRFGHUB] Test First',
        '[Test OPRFGHUB] Test Last',
        'user2testdupinsert@email.com',
        NULL,
        'PwdHash346',
        'Salt123',
        100001,
        @NewUserID OUT

    IF(@NewUserID IS NULL) 
        THROW 51001, 'Testuser for duplicate insert was not prepared', 1
END