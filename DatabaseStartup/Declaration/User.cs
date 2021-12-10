using static Common.Entity.UserType;
using static DatabaseStartup.Declaration.Markup;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.StatisticsRepository;

namespace DatabaseStartup.Declaration;

internal static class User
{
    internal static readonly string Table =
        $@"
CREATE TABLE {UserTable}
(
{Identity},
{UserTypeId}        INT		        NOT NULL	{Fk(UserTable, UserTypeTable)}      DEFAULT 1,
{LastActivity}	    DATETIME		NOT NULL	DEFAULT GETDATE(),
{Nick}              {StringType}    NOT NULL,
{Login}		        {UniqueStringType}	NOT NULL	UNIQUE,
{Password}		    {StringType}	NOT NULL,
{Email}			    {StringType}	NOT NULL,
{PictureId}		    INT				NOT NULL	{Fk(UserTable, PictureTable)}        DEFAULT 1,
{SocialCredit}      INT             NOT NULL    DEFAULT 1000,
{Currency}          INT             NOT NULL    DEFAULT 1000,
{CheckersSkinId}        INT             NOT NULL    {Fk(UserTable, CheckersSkinTable)}       DEFAULT 1,
{AnimationId}       INT             NOT NULL    {Fk(UserTable, AnimationTable)}      DEFAULT 1
);";

    internal static readonly string Type = $@"
CREATE TABLE {UserTypeTable}
(
{Identity},
{UserTypeName}  {UniqueStringType}    NOT NULL UNIQUE
);";

    private static readonly string TypeByName = $@"
GO
CREATE PROCEDURE {GetUserTypeByTypeNameProc} {UserTypeNameVar} {UniqueStringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{UserTypeTable} WHERE {UserTypeName}={UserTypeNameVar})
END";

    private static readonly string CheckAccess = $@"
GO
CREATE PROCEDURE {CheckAccessProc} {IdVar} INT, {UserTypeNameVar} {UniqueStringType}
AS
BEGIN
    DECLARE {UserTypeIdVar} INT, {AdminTypeIdVar} INT, {RequestedTypeIdVar} INT
    SET {UserTypeIdVar} = (SELECT {UserTypeId} FROM {Schema}.{UserTable} WHERE {Id}={IdVar});
    EXEC {AdminTypeIdVar} = {GetUserTypeByTypeNameProc} {SqlString(Admin)};
    EXEC {RequestedTypeIdVar} = {GetUserTypeByTypeNameProc} {UserTypeNameVar}
    IF {UserTypeIdVar}={AdminTypeIdVar} OR {UserTypeIdVar}={RequestedTypeIdVar}
        RETURN {ValidAccess}
    ELSE
        RETURN {InvalidAccess}
END";

    private static readonly string UpdateActivity = $@"
GO
CREATE PROCEDURE {UpdateUserActivityProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    UPDATE {Schema}.{UserTable} SET {LastActivity}=GETDATE() WHERE {UserAuthCondition}
END";

    private static readonly string Create = $@"
GO
CREATE PROCEDURE {CreateUserProc}
{NickVar} {StringType},
{LoginVar} {UniqueStringType},
{PasswordVar} {StringType},
{EmailVar} {StringType},
{UserTypeNameVar} {UniqueStringType}
AS
BEGIN
    DECLARE {UserIdVar} INT, @support_id INT, {IdVar} INT, @ch_id INT, @an_id INT, {UserTypeIdVar} INT, @p_id INT
    SET {UserTypeIdVar} = (SELECT {Id} FROM {UserTypeTable} WHERE {UserTypeName}={UserTypeNameVar});
    SET @ch_id = (SELECT TOP 1 {Id} FROM {CheckersSkinTable} ORDER BY {Id});
    SET @an_id = (SELECT TOP 1 {Id} FROM {AnimationTable} ORDER BY {Id});
    SET @p_id = (SELECT TOP 1 {Id} FROM {PictureTable} ORDER BY {Id});
    INSERT INTO {Schema}.{UserTable}({Nick},{Login},{Password},{Email},{UserTypeId},{CheckersSkinId},{AnimationId},{PictureId})
    VALUES ({NickVar},{LoginVar},{PasswordVar},{EmailVar},{UserTypeIdVar},@ch_id,@an_id,@p_id);
    SET {UserIdVar} = @@IDENTITY;
    IF {UserTypeNameVar}!={SqlString(Support)}
        BEGIN
        EXEC {IdVar} = {GetUserTypeByTypeNameProc} {SqlString(Support)}
        SET @support_id = (SELECT TOP 1 {Id} FROM {Schema}.{UserTable} WHERE {UserTypeId} = {IdVar});
        EXEC {CreateFriendshipProc} {UserIdVar}, @support_id
        END
    EXEC {UserAddAnimationProc} {UserIdVar}, @ch_id;
    EXEC {UserAddCheckersSkinProc} {UserIdVar}, @an_id;
    RETURN {UserIdVar}
END";

    private static readonly string Authenticate = $@"
GO
CREATE PROCEDURE {AuthenticateProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    SET {IdVar} = (SELECT {Id} FROM {Schema}.{UserTable} WHERE {UserAuthCondition});
    IF {IdVar} IS NOT NULL
        RETURN {IdVar};
    ELSE
        RETURN {InvalidId};
END";

    private static readonly string Select = $@"
GO
CREATE PROCEDURE {SelectUserProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{UserTable} WHERE {Id}={IdVar}
END";

    private static readonly string UpdateNick = $@"
GO  
CREATE PROCEDURE {UpdateUserNickProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {NickVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Nick}={NickVar} WHERE {UserAuthCondition};
END";

    private static readonly string UpdateLogin = $@"
GO  
CREATE PROCEDURE {UpdateUserLoginProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {NewLoginVar} {UniqueStringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Login}={NewLoginVar} WHERE {UserAuthCondition};
END";

    private static readonly string UpdatePassword = $@"
GO  
CREATE PROCEDURE {UpdateUserPasswordProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {NewPasswordVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Password}={NewPasswordVar} WHERE {UserAuthCondition};
END";

    private static readonly string UpdateEmail = $@"
GO  
CREATE PROCEDURE {UpdateUserEmailProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {EmailVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Email}={EmailVar} WHERE {UserAuthCondition};
END";

    private static readonly string UpdatePicture = $@"
GO
CREATE PROCEDURE {UpdateUserPictureProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    UPDATE {Schema}.{UserTable} SET {PictureId} = {IdVar} WHERE {Id}={UserIdVar}
END";

    private static readonly string SelectByNick = $@"
GO
CREATE PROCEDURE {SelectUserByNickProc} {NickVar} {StringType}
AS
BEGIN
    SELECT * FROM {Schema}.{UserTable} WHERE {Nick} LIKE {NickVar}
END";

    private static readonly string SelectTop = $@"
GO 
CREATE PROCEDURE {SelectTopPlayersProc}
AS
BEGIN
    WITH {OrderedPlayers} AS
    (SELECT ROW_NUMBER() OVER(ORDER BY {SocialCredit} DESC) AS {StatisticPosition}, U.*
    FROM {Schema}.{UserTable} AS U) 
    SELECT * FROM {OrderedPlayers}
    WHERE {StatisticPosition} < 2
    ORDER BY {SocialCredit} DESC;
END";

    private static readonly string SelectTopAuth = $@"
GO
CREATE PROCEDURE {SelectTopPlayersAuthProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    WITH {OrderedPlayers} AS
    (SELECT ROW_NUMBER() OVER(ORDER BY {SocialCredit} DESC) AS {StatisticPosition}, U.*
    FROM {Schema}.{UserTable} AS U)
    SELECT * FROM {OrderedPlayers}
    WHERE {StatisticPosition} < 2 OR {Id}={IdVar}
    ORDER BY {SocialCredit} DESC;
END";


    internal static readonly string Function = $@"
--User
{UpdateActivity}
{TypeByName}
{Create}
{Authenticate}
{CheckAccess}
{UpdateEmail}
{UpdateLogin}
{UpdateNick}
{UpdatePassword}
{UpdatePicture}
{SelectTop}
{SelectTopAuth}
{SelectByNick}";
}