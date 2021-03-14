DECLARE @skillName AS NVARCHAR(50) = '[Test] Inserted Skill'

DELETE FROM dbo.Skill
WHERE
    [Name] = @skillName
