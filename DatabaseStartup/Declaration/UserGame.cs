using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.GameRepository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;

namespace DatabaseStartup.Declaration;

internal static class UserGame
{
    internal static readonly string Table = $@"
CREATE TABLE {UserGameTable}
(
{Identity},
{SideId}                INT     NOT NULL    {Fk(UserGameTable, GameSideTable)},
{UserId}                INT     NOT NULL    {Fk(UserGameTable, UserTable)},
{GameId}                INT     NOT NULL    {Fk(UserGameTable, GameTable)},
{StartSocialCredit}     INT     NOT NULL,
{SocialCreditChange}    INT     NOT NULL,
{AnimationId}           INT     NOT NULL    {Fk(UserGameTable, AnimationTable)},
{CheckersSkinId}        INT     NOT NULL    {Fk(UserGameTable, CheckersSkinTable)}
);";

    private const string Select = $@"
GO
CREATE PROCEDURE {SelectUserGameProc} {UserIdVar} INT, {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{UserGameTable} WHERE {UserId}={UserIdVar} AND {Id}={IdVar}
END";

    private const string SelectAll = $@"
GO
CREATE PROCEDURE {SelectAllUserGameProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{UserGameTable} WHERE {UserId}={IdVar}
END";

    private const string Create = $@"
GO
CREATE PROCEDURE {CreateUserGameProc}
{SideIdVar} INT,
{UserIdVar} INT,
{GameIdVar} INT,
{SocialCreditChangeVar}    INT
AS
BEGIN
    DECLARE {AnimationIdVar} INT, {CheckersSkinIdVar} INT, {SocialCreditVar} INT
    SELECT {AnimationIdVar}={AnimationId}, {CheckersSkinIdVar}={CheckersSkinId},
    {SocialCreditVar}={SocialCredit}
    FROM {Schema}.{UserTable} WHERE {Id}={UserIdVar}
    
    INSERT INTO {Schema}.{UserGameTable}({SideId},{UserId},{GameId},{StartSocialCredit},{SocialCreditChange},{AnimationId},{CheckersSkinId})
    VALUES ({SideIdVar},{UserIdVar},{GameIdVar},{SocialCreditVar},{SocialCreditChangeVar},{AnimationIdVar},{CheckersSkinIdVar})
END
";

    public const string Function = $@"
{Create}
{Select}
{SelectAll}";
}