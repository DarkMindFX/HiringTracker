﻿DECLARE @FirstName AS NVARCHAR(50) = '[Test {BEF0FD82C7C54784995C028C298706A8}] First'
DECLARE @MiddleName AS NVARCHAR(50) = '[Test {BEF0FD82C7C54784995C028C298706A8}] Middle'
DECLARE @LastName AS NVARCHAR(50) = '[Test {BEF0FD82C7C54784995C028C298706A8}] Last'
DECLARE @Email AS NVARCHAR(50) = 'test_BEF0FD82C7C54784995C028C298706A8@email.com'
DECLARE @Phone AS NVARCHAR(50) = '+BEF0FD82C7C54784995C028C298706A8'
DECLARE @CVLink AS NVARCHAR(1000) = 'http://www.dropbox.com/cvs/test_BEF0FD82C7C54784995C028C298706A8.pdf'
DECLARE @CreateByID AS BIGINT = 100002
DECLARE @CreatedDate AS DATETIME = '2021-06-23'
DECLARE @ModifiedByID AS BIGINT = 100003
DECLARE @ModifiedDate AS DATETIME = '2021-06-24'
DECLARE @ID AS BIGINT


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Candidate]
				WHERE 
					FirstName = @FirstName AND
					MiddleName = @MiddleName AND
					LastName = @LastName))
BEGIN
	INSERT INTO [dbo].[Candidate]
	SELECT 		
		@FirstName,
		@MiddleName,
		@LastName,
		@Email,
		@Phone,
		@CVLink,
		@CreateByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate
END

SELECT @ID = [ID] FROM [dbo].[Candidate]
WHERE
	[FirstName] = @FirstName AND
	[MiddleName] = @MiddleName AND 
	[LastName] = @LastName AND 
	[Email] = @Email AND 
	[Phone] = @Phone AND 
	[CVLink] = @CVLink AND 
	[CreatedByID] = @CreateByID AND 
	[CreatedDate] = @CreatedDate AND 
	[ModifiedByID] = @ModifiedByID AND 
	[ModifiedDate] = @ModifiedDate

SELECT @ID