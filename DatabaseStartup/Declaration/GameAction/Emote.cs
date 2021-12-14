using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.GameRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;

namespace DatabaseStartup.Declaration.GameAction;

internal static class Emote
{
    internal static readonly string Table = $@"
CREATE TABLE {GameEmoteTable}
(
{Identity},
{GameId}        INT     NOT NULL    {Fk(GameEmoteTable, GameTable)},
{Time}          TIME    NOT NULL,
{SideId}        INT     NOT NULL    {Fk(GameEmoteTable, GameSideTable)},
{EmotionId}     INT     NOT NULL    {Fk(GameEmoteTable, EmotionTable)},
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectEmoteActionProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{GameEmoteTable} WHERE {GameId}={IdVar}
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateEmoteActionProc}
{GameIdVar}     INT,
{TimeVar}       TIME,
{SideIdVar}     INT,
{IdVar}         INT
AS
BEGIN
    INSERT INTO {Schema}.{GameEmoteTable}({GameId},{Time},{SideId},{EmotionId})
    VALUES ({GameIdVar},{TimeVar},{SideIdVar},{IdVar})
END";

    internal const string Function = $@"
{Select}
{Create}";
}