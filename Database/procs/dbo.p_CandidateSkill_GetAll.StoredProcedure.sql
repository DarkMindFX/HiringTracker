USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateSkill_GetAll]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateSkill_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[CandidateSkill] e
END
GO
