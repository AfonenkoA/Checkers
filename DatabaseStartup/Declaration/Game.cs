using static WebService.Repository.MSSqlImplementation.GameRepository;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;

namespace DatabaseStartup.Declaration;

internal static class Game
{
    internal const string Side = $@"
CREATE TABLE {GameSideTable}
(
{Identity},
{SideName}      {UniqueStringType}      NOT NULL
);";

    internal const string WinReason = $@"
CREATE TABLE {WinReasonTable}
(
{Identity},
{WinReasonName}   {UniqueStringType}      NOT NULL
);";


    internal static readonly string Table = $@"
CREATE TABLE {GameTable}
(
{Identity},
{GameStartTime}     DATETIME    NOT NULL,
{GameDuration}      TIME        NOT NULL,
{WinnerSideId}      INT         NOT NULL    {Fk(GameTable, GameSideTable)},
{WinReasonId}       INT         NOT NULL    {Fk(GameTable, WinReasonTable)},
);";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateGameProc} 
{GameStartTimeVar} DATETIME,
{GameDurationVar}   TIME,
{WinnerSideIdVar}   INT,
{WinReasonIdVar}    INT
AS
BEGIN
    INSERT INTO {Schema}.{GameTable}({GameStartTime},{GameDuration},{WinnerSideId},{WinReasonId})
    VALUES ({GameStartTimeVar},{GameDurationVar},{WinnerSideIdVar},{WinReasonIdVar})
    RETURN @@IDENTITY
END
";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectGameProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{GameTable} WHERE {Id}={IdVar}
END";

    public const string Function = $@"
{Create}
{Select}";
}