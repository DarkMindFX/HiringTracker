DECLARE @FirstName AS NVARCHAR(50) = '[Test {A340848A514F429EA5768209D6BED3E0}] First'
DECLARE @MiddleName AS NVARCHAR(50) = '[Test {A340848A514F429EA5768209D6BED3E0}] Middle'
DECLARE @LastName AS NVARCHAR(50) = '[Test {A340848A514F429EA5768209D6BED3E0}] Last'
DECLARE @Email AS NVARCHAR(50) = 'test_A340848A514F429EA5768209D6BED3E0@email.com'
DECLARE @Phone AS NVARCHAR(50) = '+A340848A514F429EA5768209D6BED3E0'
DECLARE @CVLink AS NVARCHAR(1000) = 'http://www.dropbox.com/cvs/test_A340848A514F429EA5768209D6BED3E0.pdf'
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM dbo.Candidate 
	WHERE
	FirstName = @FirstName AND
	LastName = @LastName AND
	MiddleName = @MiddleName AND
	Email = @Email AND
	Phone = @Phone AND 
	CVLink = @CVLink))
BEGIN
	SET @Fail = 1
END

DELETE FROM dbo.Candidate 
	WHERE
	FirstName = @FirstName AND
	LastName = @LastName AND
	MiddleName = @MiddleName AND
	Email = @Email AND
	Phone = @Phone AND 
	CVLink = @CVLink

IF(@Fail = 1)
BEGIN
	THROW 51001, 'TEST FAILED: Candidate was not inserted', 1
END