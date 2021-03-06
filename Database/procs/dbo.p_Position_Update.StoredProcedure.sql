USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Position_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_Update]
			@ID BIGINT,
			@DepartmentID BIGINT,
			@Title NVARCHAR(50),
			@ShortDesc NVARCHAR(250),
			@Description NVARCHAR(4000),
			@StatusID BIGINT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Position]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Position]
		SET
									[DepartmentID] = IIF( @DepartmentID IS NOT NULL, @DepartmentID, [DepartmentID] ) ,
									[Title] = IIF( @Title IS NOT NULL, @Title, [Title] ) ,
									[ShortDesc] = IIF( @ShortDesc IS NOT NULL, @ShortDesc, [ShortDesc] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[StatusID] = IIF( @StatusID IS NOT NULL, @StatusID, [StatusID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Position was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Position] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DepartmentID IS NOT NULL THEN (CASE WHEN e.[DepartmentID] = @DepartmentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ShortDesc IS NOT NULL THEN (CASE WHEN e.[ShortDesc] = @ShortDesc THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN e.[StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
