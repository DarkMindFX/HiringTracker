USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Utils_PopulateTestData]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Utils_PopulateTestData]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	EXEC dbo.p_Utils_PopulateTestData_Users
	EXEC dbo.p_Utils_PopulateTestData_Skills
	EXEC dbo.p_Utils_PopulateTestData_Comments
	EXEC dbo.p_Utils_PopulateTestData_Positions
	EXEC dbo.p_Utils_PopulateTestData_PositionComment
	EXEC dbo.p_Utils_PopulateTestData_Candidates
	EXEC dbo.p_Utils_PopulateTestData_CandidateComment
	EXEC dbo.p_Utils_PopulateTestData_CandidateProperty
    EXEC dbo.p_Utils_PopulateTestData_Proposals
	EXEC dbo.p_Utils_PopulateTestData_ProposalComment
	EXEC dbo.p_Utils_PopulateTestData_Interview
	EXEC dbo.p_Utils_PopulateTestData_InterviewFeedback

END
GO
