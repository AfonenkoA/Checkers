using static Common.Entity.UserType;
using static DatabaseStartup.CsvTable;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;

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

    internal static readonly string TypeByName = $@"
GO
CREATE PROCEDURE {GetUserTypeByTypeNameProc} {UserTypeNameVar} {UniqueStringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{UserTypeTable} WHERE {UserTypeName}={UserTypeNameVar})
END";

    internal static readonly string CheckAccess = $@"
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

    internal static readonly string UpdateActivity = $@"
GO
CREATE PROCEDURE {UpdateUserActivityProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    UPDATE {Schema}.{UserTable} SET {LastActivity}=GETDATE() WHERE {UserAuthCondition}
END";

    internal static readonly string Create = $@"
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

    internal static readonly string Authenticate = $@"
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
}