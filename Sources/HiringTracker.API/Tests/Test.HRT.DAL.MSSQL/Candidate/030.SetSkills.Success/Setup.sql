﻿DECLARE @FirstName AS NVARCHAR(50) = '[Test {C6E0BFF2A9584726ACA898DBF51A3AD9}] First'
DECLARE @MiddleName AS NVARCHAR(50) = '[Test {C6E0BFF2A9584726ACA898DBF51A3AD9}] Middle'
DECLARE @LastName AS NVARCHAR(50) = '[Test {C6E0BFF2A9584726ACA898DBF51A3AD9}] Last'
DECLARE @Email AS NVARCHAR(50) = 'test_C6E0BFF2A9584726ACA898DBF51A3AD9@email.com'
DECLARE @Phone AS NVARCHAR(50) = '+C6E0BFF2A9584726ACA898DBF51A3AD9'
DECLARE @CVLink AS NVARCHAR(1000) = 'http://www.dropbox.com/cvs/test_C6E0BFF2A9584726ACA898DBF51A3AD9.pdf'
DECLARE @UserID AS BIGINT = 100002
DECLARE @StatusID AS BIGINT = 1
DECLARE @CandidateID AS BIGINT

IF(NOT EXISTS(SELECT 1 FROM dbo.Candidate WHERE Email = @Email))
BEGIN
	EXEC [dbo].[p_Candidate_Upsert]
		NULL,
		@FirstName,
		@MiddleName,
		@LastName,
		@Email,
		@Phone,
		@CVLink,
		@UserID,
		@CandidateID OUT
END