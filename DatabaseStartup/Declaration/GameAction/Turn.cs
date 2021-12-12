using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.GameRepository;

namespace DatabaseStartup.Declaration.GameAction;

internal static class Turn
{
    internal static readonly string Table = $@"
GO
CREATE TABLE {GameTurnTable}
(
{Identity},
{GameId}        INT     NOT NULL    {Fk(GameTurnTable, GameTable)},
{Time}      TIME    NOT NULL,
{SideId}    INT     NOT NULL    {Fk(GameTurnTable, GameSideTable)},
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectTurnActionProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{GameTurnTable} WHERE {GameId}={IdVar}
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateTurnActionProc}
{GameIdVar}     INT,
{TimeVar}       TIME,
{SideIdVar}     INT
AS
BEGIN
    INSERT INTO {Schema}.{GameEmoteTable}({GameId},{Time},{SideId})
    VALUES ({GameIdVar},{TimeVar},{SideIdVar})
END";

    internal const string Function = $@"
{Select}
{Create}";
}