﻿DECLARE @FirstName AS NVARCHAR(50) = '[Test {7DE3B9014362458C8709B8D1DD3D8EBA}] First'
DECLARE @MiddleName AS NVARCHAR(50) = '[Test {7DE3B9014362458C8709B8D1DD3D8EBA}] Middle'
DECLARE @LastName AS NVARCHAR(50) = '[Test {7DE3B9014362458C8709B8D1DD3D8EBA}] Last'
DECLARE @Email AS NVARCHAR(50) = 'test_7DE3B9014362458C8709B8D1DD3D8EBA@email.com'
DECLARE @Phone AS NVARCHAR(50) = '+7DE3B9014362458C8709B8D1DD3D8EBA'
DECLARE @CVLink AS NVARCHAR(1000) = 'http://www.dropbox.com/cvs/test_7DE3B9014362458C8709B8D1DD3D8EBA.pdf'
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