CREATE PROCEDURE SelectTVP @TVP SelectSequenceTVP READONLY
AS
BEGIN
    SELECT T.[Id], S.[Seq]
    FROM @TVP T
    INNER JOIN [dbo].[Sequences] S ON T.[Id] = S.[Id];
END