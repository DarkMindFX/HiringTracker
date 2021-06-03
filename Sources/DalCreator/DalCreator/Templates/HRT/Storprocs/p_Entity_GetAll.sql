

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_{Entity}_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_{Entity}_GetAll]
GO

CREATE PROCEDURE [dbo].[p_{Entity}_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[{Entity}] e
END
GO