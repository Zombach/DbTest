﻿CREATE PROCEDURE InsertTVP @TVP InsertSequenceTVP READONLY
AS
BEGIN
	INSERT INTO [Sequences] (Seq)
    SELECT [SEQ]
    FROM @TVP;
END