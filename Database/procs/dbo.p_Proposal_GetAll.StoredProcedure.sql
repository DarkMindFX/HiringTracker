USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Proposal_GetAll]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Proposal_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Proposal] e
END
GO
