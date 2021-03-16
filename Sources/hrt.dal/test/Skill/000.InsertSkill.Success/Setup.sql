DECLARE @skillName AS NVARCHAR(50) = '[Test RTYHGFVBN] Inserted Skill'

DELETE FROM dbo.Skill
WHERE
    [Name] = @skillName
