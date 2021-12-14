using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.GameRepository;

namespace DatabaseStartup.Declaration.GameAction;

internal static class Move
{
    internal static readonly string Table = $@"
CREATE TABLE {GameMoveTable}
(
{Identity},
{GameId}        INT     NOT NULL    {Fk(GameMoveTable, GameTable)},
{Time}          TIME    NOT NULL,
{SideId}        INT     NOT NULL    {Fk(GameMoveTable, GameSideTable)},
{From}          INT     NOT NULL,
{To}            INT     NOT NULL
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectMoveActionProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{GameMoveTable} WHERE {GameId}={IdVar}
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateMoveActionProc}
{GameIdVar}     INT,
{TimeVar}       TIME,
{SideIdVar}     INT,
{FromVar}       INT,
{ToVar}         INT

AS
BEGIN
    INSERT INTO {Schema}.{GameMoveTable}({GameId},{Time},{SideId},{From},{To})
    VALUES ({GameIdVar},{TimeVar},{SideIdVar},{FromVar},{ToVar})
END";

    internal const string Function = $@"
{Select}
{Create}";
}