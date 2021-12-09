using static System.Console;
using Common.Entity;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.MessageRepository;
using static WebService.Repository.MSSqlImplementation.NewsRepository;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;
using static WebService.Repository.MSSqlImplementation.StatisticsRepository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static DatabaseStartup.CsvTable;

Write(
    @$"GO
CREATE DATABASE Checkers;

GO
USE Checkers;

CREATE TABLE {ResourceTable}
(
{Identity},
{ResourceExtension}     {StringType}	NOT NULL,
{ResourceBytes}         {BinaryType}	NOT NULL,
);

CREATE TABLE {PictureTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{ResourceId}    INT                     NOT NULL    {Fk(PictureTable,ResourceTable)}
);

CREATE TABLE {AchievementTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{Detail}        {StringType}            NOT NULL,
{ResourceId}    INT                     NOT NULL    {Fk(AchievementTable, ResourceTable)}
);

CREATE TABLE {LootBoxTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{Detail}        {StringType}            NOT NULL,
{ResourceId}    INT                     NOT NULL    {Fk(LootBoxTable, ResourceTable)},
{Price}         INT                     NOT NULL    DEFAULT 100
);

CREATE TABLE {CheckersSkinTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{Detail}        {StringType}            NOT NULL,
{ResourceId}    INT                     NOT NULL    {Fk(CheckersSkinTable, ResourceTable)},
{Price}         INT                     NOT NULL    DEFAULT 100
);

CREATE TABLE {AnimationTable}
(
{Identity},
{Name}          {UniqueStringType}      NOT NULL    UNIQUE,
{Detail}        {StringType}            NOT NULL,
{ResourceId}    INT                     NOT NULL    {Fk(AnimationTable, ResourceTable)},
{Price}         INT                     NOT NULL    DEFAULT 100
);

CREATE TABLE {UserTypeTable}
(
{Identity},
{UserTypeName}  {UniqueStringType}    NOT NULL UNIQUE
);

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
);


CREATE TABLE {UserAnimationTable}
(
{Identity},
{AnimationId}   INT     NOT NULL    {Fk(UserAnimationTable,AnimationTable)},
{UserId}        INT     NOT NULL    {Fk(UserAnimationTable,UserTable)}
);

CREATE TABLE {UserCheckersSkinTable}
(
{Identity},
{CheckersSkinId}   INT     NOT NULL    {Fk(UserCheckersSkinTable, CheckersSkinTable)},
{UserId}        INT     NOT NULL    {Fk(UserCheckersSkinTable, UserTable)}
);

CREATE TABLE {UserAchievementTable}
(
{Identity},
{AchievementId}   INT     NOT NULL    {Fk(UserAchievementTable, AnimationTable)},
{UserId}        INT     NOT NULL    {Fk(UserAchievementTable, UserTable)}
);

CREATE TABLE {ChatTypeTable}
(
{Identity},
{ChatTypeName}      {UniqueStringType}    NOT NULL UNIQUE
);

CREATE TABLE {ChatTable}
(
{Identity},
{ChatName}      {UniqueStringType}  NOT NULL  UNIQUE,
{ChatTypeId}    INT                 NOT NULL {Fk(ChatTable, ChatTypeTable)}
);

CREATE TABLE {FriendshipStateTable}
(
{Identity},
{FriendshipStateName} {UniqueStringType}  NOT NULL UNIQUE
);

CREATE TABLE {FriendshipTable}
(
{Identity},
{User1Id}           INT   NOT NULL	{Fk(FriendshipTable, UserTable, "1")},
{User2Id}           INT   NOT NULL	{Fk(FriendshipTable, UserTable, "2")},
{ChatId}            INT   NOT NULL	{Fk(FriendshipTable, ChatTable)},
{FriendshipStateId} INT   NOT NULL	{Fk(FriendshipTable, FriendshipStateTable)}
);


CREATE TABLE {MessageTable}
(
{Identity},
{ChatId}            INT             NOT NULL	{Fk(MessageTable, ChatTable)},
{UserId}            INT             NOT NULL	{Fk(MessageTable, UserTable)}, 
{MessageContent}    {StringType}    NOT NULL,
{SendTime}          DATETIME        NOT NULL    DEFAULT GETDATE()
);


CREATE TABLE {PostTable}
(
{Identity},
{ChatId}            INT                 NOT NULL	{Fk(PostTable, ChatTable)},
{PostAuthorId}      INT                 NOT NULL	{Fk(PostTable, UserTable)},
{PostTitle}         {UniqueStringType}  NOT NULL    UNIQUE,
{PostContent}       {StringType}        NOT NULL,
{PostCreated}       DATETIME            NOT NULL    DEFAULT GETDATE(),
{PostPictureId}     INT                 NOT NULL    {Fk(PostTable, ResourceTable)}
);

CREATE TABLE {ArticleTable}
(
{Identity},
{ArticleAuthorId}   INT                 NOT NULL	{Fk(ArticleTable, UserTable)},
{ArticleTitle}      {UniqueStringType}  NOT NULL    UNIQUE,
{ArticleAbstract}   {StringType}        NOT NULL,
{ArticleContent}    {StringType}        NOT NULL,
{ArticleCreated}    DATETIME            NOT NULL    DEFAULT GETDATE(),
{ArticlePictureId}  INT                 NOT NULL    {Fk(ArticleTable, ResourceTable)},
{ArticlePostId}     INT                 NOT NULL    {Fk(ArticleTable, PostTable)},
);


GO
CREATE PROCEDURE {GetUserTypeByTypeNameProc} {UserTypeNameVar} {UniqueStringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{UserTypeTable} WHERE {UserTypeName}={UserTypeNameVar})
END

GO
CREATE PROCEDURE {GetChatTypeByNameProc} {ChatTypeNameVar} {UniqueStringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{ChatTypeTable} WHERE {ChatTypeName}={ChatTypeNameVar})
END

GO
CREATE PROCEDURE {GetFriendshipStateByNameProc} {FriendshipStateNameVar} {UniqueStringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{FriendshipStateTable} WHERE {FriendshipStateName}={FriendshipStateNameVar})
END

GO
CREATE PROCEDURE {CheckAccessProc} {IdVar} INT, {UserTypeNameVar} {UniqueStringType}
AS
BEGIN
    DECLARE {UserTypeIdVar} INT, {AdminTypeIdVar} INT, {RequestedTypeIdVar} INT
    SET {UserTypeIdVar} = (SELECT {UserTypeId} FROM {Schema}.{UserTable} WHERE {Id}={IdVar});
    EXEC {AdminTypeIdVar} = {GetUserTypeByTypeNameProc} {SqlString(UserType.Admin)};
    EXEC {RequestedTypeIdVar} = {GetUserTypeByTypeNameProc} {UserTypeNameVar}
    IF {UserTypeIdVar}={AdminTypeIdVar} OR {UserTypeIdVar}={RequestedTypeIdVar}
        RETURN {ValidAccess}
    ELSE
        RETURN {InvalidAccess}
END


GO
CREATE PROCEDURE {SelectResourceProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{ResourceTable} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {CreateChatProc} {ChatNameVar} {UniqueStringType}, {ChatTypeNameVar} {UniqueStringType}
AS
BEGIN
    DECLARE {IdVar} INT
    BEGIN TRANSACTION;  
    EXEC {IdVar} = {GetChatTypeByNameProc} {ChatTypeNameVar};
    INSERT INTO {Schema}.{ChatTable}({ChatName},{ChatTypeId}) 
    VALUES ({ChatNameVar},{IdVar});
    COMMIT;
    RETURN @@IDENTITY
END

GO
CREATE PROCEDURE {CreateFriendshipProc} {User1IdVar} INT,{User2IdVar} INT
AS
BEGIN
    DECLARE {ChatIdVar} INT, @u1_login {UniqueStringType}, @u2_login {UniqueStringType},
    {ChatNameVar} {UniqueStringType}, {IdVar} INT
    BEGIN TRANSACTION;  
    SET @u1_login = (SELECT {Login} FROM {Schema}.{UserTable} WHERE {Id}={User1IdVar});
    SET @u2_login = (SELECT {Login} FROM {Schema}.{UserTable} WHERE {Id}={User2IdVar});
    SET {ChatNameVar} = N'Chat '+@u1_login+N' to '+ @u2_login;
    EXEC {ChatIdVar} = {CreateChatProc} {ChatNameVar}, {SqlString(ChatType.Private)};
    EXEC {IdVar} = {GetFriendshipStateByNameProc} {SqlString(FriendshipState.Accepted)};
    INSERT INTO {Schema}.{FriendshipTable}({User1Id},{User2Id},{ChatId},{FriendshipStateId})
    VALUES ({User1IdVar},{User2IdVar},{ChatIdVar},{IdVar}),({User2IdVar},{User1IdVar},{ChatIdVar},{IdVar});
    COMMIT;
END


GO
CREATE PROCEDURE {UpdateUserActivityProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    UPDATE {Schema}.{UserTable} SET {LastActivity}=GETDATE() WHERE {UserAuthCondition}
END


GO
CREATE PROCEDURE {SelectAchievementProc} {IdVar} INT
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AchievementTable} AS A 
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
    WHERE A.{Id}={IdVar}
END

GO
CREATE PROCEDURE {SelectAnimationProc} {IdVar} INT
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AnimationTable} AS A 
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
    WHERE A.{Id}={IdVar}
END

GO
CREATE PROCEDURE {SelectCheckersSkinProc} {IdVar} INT
AS
BEGIN
    SELECT CH.*, R.{ResourceExtension}
    FROM {Schema}.{CheckersSkinTable} AS CH 
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=CH.{ResourceId}
    WHERE CH.{Id}={IdVar}
END

GO
CREATE PROCEDURE {SelectPictureProc} {IdVar} INT
AS
BEGIN
    SELECT P.*, R.{ResourceExtension}
    FROM {Schema}.{PictureTable} AS P
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=P.{ResourceId}
    WHERE P.{Id}={IdVar}
END

GO
CREATE PROCEDURE {SelectLootBoxProc} {IdVar} INT
AS
BEGIN
    SELECT L.*, R.{ResourceExtension}
    FROM {Schema}.{LootBoxTable} AS L
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=L.{ResourceId}
    WHERE L.{Id}={IdVar}
END


GO
CREATE PROCEDURE {SelectAllAchievementProc}
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AchievementTable} AS A
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
END

GO
CREATE PROCEDURE {SelectAllAnimationProc}
AS
BEGIN
    SELECT A.*, R.{ResourceExtension}
    FROM {Schema}.{AnimationTable} AS A
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=A.{ResourceId}
END

GO
CREATE PROCEDURE {SelectAllCheckersSkinProc}
AS
BEGIN
    SELECT CH.*, R.{ResourceExtension}
    FROM {Schema}.{AnimationTable} AS CH
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=CH.{ResourceId}
END

GO
CREATE PROCEDURE {SelectAllPictureProc}
AS
BEGIN
    SELECT P.*, R.{ResourceExtension}
    FROM {Schema}.{PictureTable} AS P
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=P.{ResourceId}
END

GO
CREATE PROCEDURE {SelectAllLootBoxProc}
AS
BEGIN
    SELECT L.*, R.{ResourceExtension}
    FROM {Schema}.{LootBoxTable} AS L
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=L.{ResourceId}
END


GO
CREATE PROCEDURE {SelectUserAchievementProc} {IdVar} INT
AS
BEGIN
    SELECT {Id} FROM {Schema}.{UserAchievementTable} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {SelectUserAnimationProc} {IdVar} INT
AS
BEGIN
    SELECT {Id} FROM {Schema}.{UserAnimationTable} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {SelectUserCheckersSkinProc} {IdVar} INT
AS
BEGIN
    SELECT {Id} FROM {Schema}.{UserCheckersSkinTable} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {UserAddAnimationProc} {UserIdVar} INT, {IdVar} INT
AS
BEGIN
    INSERT INTO {UserAnimationTable}({UserId},{AnimationId}) VALUES({UserIdVar},{IdVar}) 
END

GO
CREATE PROCEDURE {UserAddCheckersSkinProc} {UserIdVar} INT, {IdVar} INT
AS
BEGIN
    INSERT INTO {UserCheckersSkinTable}({UserId},{CheckersSkinId}) VALUES({UserIdVar},{IdVar}) 
END

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
    IF {UserTypeNameVar}!={SqlString(UserType.Support)}
        BEGIN
        EXEC {IdVar} = {GetUserTypeByTypeNameProc} {SqlString(UserType.Support)}
        SET @support_id = (SELECT TOP 1 {Id} FROM {Schema}.{UserTable} WHERE {UserTypeId} = {IdVar});
        EXEC {CreateFriendshipProc} {UserIdVar}, @support_id
        END
    EXEC {UserAddAnimationProc} {UserIdVar}, @ch_id;
    EXEC {UserAddCheckersSkinProc} {UserIdVar}, @an_id;
    RETURN {UserIdVar}
END

GO
CREATE PROCEDURE {SelectUserProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{UserTable} WHERE {Id}={IdVar}
END

GO  
CREATE PROCEDURE {UpdateUserNickProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {NickVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Nick}={NickVar} WHERE {UserAuthCondition};
END

GO  
CREATE PROCEDURE {UpdateUserLoginProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {NewLoginVar} {UniqueStringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Login}={NewLoginVar} WHERE {UserAuthCondition};
END

GO  
CREATE PROCEDURE {UpdateUserPasswordProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {NewPasswordVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Password}={NewPasswordVar} WHERE {UserAuthCondition};
END

GO  
CREATE PROCEDURE {UpdateUserEmailProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {EmailVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Email}={EmailVar} WHERE {UserAuthCondition};
END

GO
CREATE PROCEDURE {SelectUserByNickProc} {NickVar} {StringType}
AS
BEGIN
    SELECT * FROM {Schema}.{UserTable} WHERE {Nick} LIKE {NickVar}
END

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
END


GO
CREATE PROCEDURE {UpdateUserAnimationProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT;
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    IF {IdVar} IN (SELECT {AnimationId} FROM {Schema}.{UserAnimationTable} WHERE {UserId}={UserIdVar})
        UPDATE {Schema}.{UserTable} SET {AnimationId}={IdVar} WHERE {UserAuthCondition};
END

GO
CREATE PROCEDURE {UpdateUserCheckersProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT;
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    IF {IdVar} IN (SELECT {CheckersSkinId} FROM {Schema}.{UserCheckersSkinTable} WHERE {UserId}={UserIdVar})
        UPDATE {Schema}.{UserTable} SET {CheckersSkinId}={IdVar} WHERE {UserAuthCondition};
END

GO
CREATE PROCEDURE {CreateResourceProc} {ResourceExtensionVar} {StringType}, {ResourceBytesVar} {BinaryType}
AS
BEGIN
    INSERT INTO {Schema}.{ResourceTable}({ResourceExtension},{ResourceBytes}) 
    VALUES ({ResourceExtensionVar},{ResourceBytesVar});
    RETURN @@IDENTITY;
END

GO
CREATE PROCEDURE {CreateResourceFromFileProc} {PathVar} {StringType}
AS
BEGIN
    DECLARE {ResourceExtensionVar} {StringType}, {IdVar} INT, @sql {StringType}, {ResourceBytesVar} {BinaryType}
    SET {ResourceExtensionVar} = (SELECT RIGHT({PathVar}, CHARINDEX('.', REVERSE({PathVar}) + '.') - 1));
    SET @sql = FORMATMESSAGE ( 'SELECT {ResourceBytesVar} = BulkColumn FROM OPENROWSET ( BULK ''%s'', SINGLE_BLOB ) AS x;', @path );
    EXEC sp_executesql @sql, N'{ResourceBytesVar} {BinaryType} OUT', {ResourceBytesVar} = {ResourceBytesVar} OUT;
    EXEC  {IdVar}={CreateResourceProc} {ResourceExtensionVar} ,{ResourceBytesVar}
    RETURN {IdVar}
END

GO
CREATE PROCEDURE {CreatePostProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{PostTitleVar} {UniqueStringType}, {PostContentVar} {StringType},{PostPictureIdVar} INT
AS
BEGIN
    BEGIN TRANSACTION;
    DECLARE {IdVar} INT, {UserIdVar} INT, {ChatNameVar} {UniqueStringType}
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        SET {ChatNameVar} = N'Chat ' + {PostTitleVar};
        EXEC {IdVar} = {CreateChatProc} {ChatNameVar}, {SqlString(ChatType.Public)};
        IF {IdVar} IS NOT NULL
            BEGIN
            INSERT INTO {Schema}.{PostTable}({PostContent},{PostTitle},{PostPictureId},{ChatId},{PostAuthorId})
            VALUES ({PostContentVar},{PostTitleVar},{PostPictureIdVar},{IdVar},{UserIdVar});
            COMMIT;
            RETURN @@IDENTITY;
            END
        ROLLBACK;
        END
    ELSE
        BEGIN
        ROLLBACK;
        RETURN {InvalidId};
        END
    
END

GO
CREATE PROCEDURE {CreateArticleProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{ArticleTitleVar} {UniqueStringType}, {ArticleAbstractVar} {StringType}, 
{ArticleContentVar} {StringType}, {ArticlePictureIdVar} INT
AS
BEGIN
    BEGIN TRANSACTION;
    DECLARE {IdVar} INT, {UserIdVar} INT, @post {StringType}, {AccessVar} INT
    SET @post = N'Discussion ' + {ArticleTitleVar};
    EXEC {UserIdVar}={AuthenticateProc} {LoginVar}, {PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(UserType.Editor)};
        IF {AccessVar}={ValidAccess}
            BEGIN
            EXEC {IdVar}={CreatePostProc} {LoginVar},{PasswordVar},{ArticleTitleVar},@post, {ArticlePictureIdVar};
            IF {IdVar}!={InvalidId}
                BEGIN
                INSERT INTO {Schema}.{ArticleTable}({ArticleTitle},{ArticleContent},{ArticleAbstract},{ArticleAuthorId},{ArticlePictureId},{ArticlePostId})
                VALUES  ({ArticleTitleVar},{ArticleContentVar},{ArticleAbstractVar},{UserIdVar},{ArticlePictureIdVar},{IdVar});
                COMMIT;
                RETURN @@IDENTITY
                END
            END
        ROLLBACK;
        END
    ELSE
        BEGIN
        ROLLBACK;
        RETURN {InvalidId};
        END
    
END

GO
CREATE PROCEDURE {SelectArticleProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{ArticleTable} WHERE {Id}={IdVar};
END

GO
CREATE PROCEDURE {SelectNewsProc}
AS
BEGIN
    SELECT * FROM {Schema}.{ArticleTable};
END


GO
CREATE PROCEDURE {SelectPostInfoProc} {IdVar} INT
AS
BEGIN
    SELECT {Id},{PostTitle},{PostPictureId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar};
END

GO
CREATE PROCEDURE {SelectPostProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{PostTable} WHERE {Id}={IdVar};
END

GO
CREATE PROCEDURE {SelectPostsProc}
AS
BEGIN
    SELECT {Id},{PostTitle},{PostPictureId},{PostContent} FROM {Schema}.{PostTable};
END

GO
CREATE PROCEDURE {SendMessageProc} {LoginVar} {UniqueStringType},{PasswordVar} {StringType},
{ChatIdVar} INT,{MessageContentVar} {StringType}
AS
BEGIN
    DECLARE {UserIdVar} INT, {IdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        EXEC {IdVar} = {GetChatTypeByNameProc} {SqlString(ChatType.Public)}
        IF {ChatIdVar} IN 
        (SELECT {ChatId} FROM {FriendshipTable} WHERE {User1Id}={UserIdVar}) OR 
        (SELECT {ChatTypeId} FROM {ChatTable} WHERE {Id}={ChatIdVar}) = {IdVar}
            INSERT INTO {Schema}.{MessageTable}({ChatId},{UserId},{MessageContent})
            VALUES ({ChatIdVar},{UserIdVar},{MessageContentVar}); 
        END
END

GO
CREATE PROCEDURE {SelectMessageProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {ChatIdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar};
    IF {UserIdVar}!={InvalidId}
            SELECT * FROM {Schema}.{MessageTable} WHERE {ChatId}={ChatIdVar};
END

GO
CREATE PROCEDURE {CreateAnimationProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType},
{DetailVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{AnimationTable}({ResourceId},{Name},{Detail},{Price}) 
    VALUES({IdVar},{NameVar},{DetailVar},{PriceVar});
END

GO
CREATE PROCEDURE {CreateAchievementProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType},
{DetailVar} {StringType}
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{AchievementTable}({ResourceId},{Name},{Detail}) 
    VALUES({IdVar},{NameVar},{DetailVar});
END

GO
CREATE PROCEDURE {CreateCheckersSkinProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType},
{DetailVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{CheckersSkinTable}({ResourceId},{Name},{Detail},{Price}) 
    VALUES({IdVar},{NameVar},{DetailVar},{PriceVar});
END

GO
CREATE PROCEDURE {CreatePictureProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType}
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{PictureTable}({ResourceId},{Name}) 
    VALUES ({IdVar},{NameVar});
END

GO
CREATE PROCEDURE {CreateLootBoxProc}
{NameVar} {UniqueStringType},
{PathVar} {StringType},
{DetailVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateResourceFromFileProc} {PathVar}
    INSERT INTO {Schema}.{LootBoxTable}({ResourceId},{Name},{Detail},{Price}) 
    VALUES({IdVar},{NameVar},{DetailVar},{PriceVar});
END

--Article
GO
CREATE PROCEDURE {UpdateArticleTitleProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticleTitleVar} {UniqueStringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(UserType.Editor)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticleTitle} = {ArticleTitleVar} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {UpdateArticleAbstractProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticleAbstractVar} {StringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(UserType.Editor)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticleAbstract} = {ArticleAbstractVar} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {UpdateArticleContentProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticleContentVar} {StringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(UserType.Editor)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticleContent} = {ArticleContentVar} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {UpdateArticlePictureIdProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticlePictureIdVar} INT
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(UserType.Editor)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticlePictureId} = {ArticlePictureIdVar} WHERE {Id}={IdVar}
END

--Post
GO
CREATE PROCEDURE {UpdateArticlePostIdProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticlePostIdVar} INT
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT, {PostAuthorIdVar} INT
    SET {PostAuthorIdVar} = (SELECT {PostAuthorId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar});
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(UserType.Moderator)}
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticlePostId} = {ArticlePostIdVar} WHERE {Id}={IdVar}
END


GO
CREATE PROCEDURE {UpdatePostTitleProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {PostTitleVar} {UniqueStringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT, {PostAuthorIdVar} INT
    SET {PostAuthorIdVar} = (SELECT {PostAuthorId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar});
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(UserType.Moderator)}
    IF {PostAuthorIdVar}={UserIdVar} OR {AccessVar}={ValidAccess}
        UPDATE {Schema}.{PostTable} SET {PostTitle}={PostTitleVar} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {UpdatePostContentProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {PostContentVar} {StringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT, {PostAuthorIdVar} INT
    SET {PostAuthorIdVar} = (SELECT {PostAuthorId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar});
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(UserType.Moderator)}
    IF {PostAuthorIdVar}={UserIdVar} OR {AccessVar}={ValidAccess}
        UPDATE {Schema}.{PostTable} SET {PostContent}={PostContentVar} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {UpdatePostPictureIdProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, 
{IdVar} INT, {PostPictureIdVar} INT
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT, {PostAuthorIdVar} INT
    SET {PostAuthorIdVar} = (SELECT {PostAuthorId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar});
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {SqlString(UserType.Moderator)}
    IF {PostAuthorIdVar}={UserIdVar} OR {AccessVar}={ValidAccess}
        UPDATE {Schema}.{PostTable} SET {PostPictureId}={PostPictureIdVar} WHERE {Id}={IdVar}
END

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
END

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
END


GO
CREATE PROCEDURE {SelectFriendChatIdProc} {User1IdVar} INT, {User2IdVar} INT
AS
BEGIN
    RETURN (SELECT {ChatId} FROM {Schema}.{FriendshipTable} WHERE {User1Id} = {User1IdVar} AND {User2Id} = {User2IdVar})
END

GO
CREATE PROCEDURE {SelectUserFriendshipProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{FriendshipTable} WHERE {User1Id} = {IdVar}
END

GO
CREATE PROCEDURE {UserBuyAnimationProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT, {PriceVar} INT, {CurrencyVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SET {PriceVar} = (SELECT {Price} FROM {Schema}.{AnimationTable} WHERE {Id}={IdVar});
    SET {CurrencyVar} = (SELECT {Currency} FROM {Schema}.{UserTable} WHERE {Id}={UserIdVar});
    IF {CurrencyVar}>={PriceVar}
        BEGIN
        EXEC {UserAddAnimationProc} {UserIdVar}, {IdVar}
        UPDATE {Schema}.{UserTable} SET {Currency} = ({CurrencyVar}-{PriceVar}) WHERE {Id}={UserIdVar};
        END
END

GO
CREATE PROCEDURE {UserBuyCheckersSkinProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT, {PriceVar} INT, {CurrencyVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SET {PriceVar} = (SELECT {Price} FROM {Schema}.{CheckersSkinTable} WHERE {Id}={IdVar});
    SET {CurrencyVar} = (SELECT {Currency} FROM {Schema}.{UserTable} WHERE {Id}={UserIdVar});
    IF {CurrencyVar}>={PriceVar}
        BEGIN
        EXEC {UserAddCheckersSkinProc} {UserIdVar}, {IdVar}
        UPDATE {Schema}.{UserTable} SET {Currency} = ({CurrencyVar}-{PriceVar}) WHERE {Id}={UserIdVar};
        END
END

GO
CREATE PROCEDURE {UserBuyLootBoxProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT, {PriceVar} INT, {CurrencyVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SET {PriceVar} = (SELECT {Price} FROM {Schema}.{LootBoxTable} WHERE {Id}={IdVar});
    SET {CurrencyVar} = (SELECT {Currency} FROM {Schema}.{UserTable} WHERE {Id}={UserIdVar});
    IF {CurrencyVar}>={PriceVar}
        UPDATE {Schema}.{UserTable} SET {Currency} = ({CurrencyVar}-{PriceVar}) WHERE {Id}={UserIdVar};
END

GO
CREATE PROCEDURE {UserGetAvailableAnimationProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SELECT {Id} FROM {Schema}.{AnimationTable} 
    EXCEPT
    SELECT {AnimationId} FROM {Schema}.{UserAnimationTable}
END

GO
CREATE PROCEDURE {UserGetAvailableCheckersSkinProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SELECT {Id} FROM {Schema}.{CheckersSkinTable} 
    EXCEPT
    SELECT {CheckersSkinId} FROM {Schema}.{UserCheckersSkinTable}
END

GO
CREATE PROCEDURE {UserGetAvailableLootBoxProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SELECT {Id} FROM {Schema}.{LootBoxTable} 
END

GO
CREATE PROCEDURE {UpdateUserPictureProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    UPDATE {Schema}.{UserTable} SET {PictureId} = {IdVar} WHERE {Id}={UserIdVar}
END

GO
CREATE PROCEDURE {GetCommonChatIdProc}  {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {ChatTable} WHERE {ChatName}={SqlString(CommonChatName)});
END

GO
INSERT INTO {UserTypeTable}({UserTypeName}) VALUES 
({SqlString((UserType)1)}),
({SqlString((UserType)2)}),
({SqlString((UserType)3)}),
({SqlString((UserType)4)}),
({SqlString((UserType)5)})

INSERT INTO {ChatTypeTable}({ChatTypeName}) VALUES
({SqlString((ChatType)1)}),
({SqlString((ChatType)2)});

INSERT INTO {FriendshipStateTable}({FriendshipStateName}) VALUES
({SqlString((FriendshipState)1)}),
({SqlString((FriendshipState)2)}),
({SqlString((FriendshipState)3)});

GO
Use Checkers
EXEC {CreateChatProc} {SqlString(CommonChatName)},{SqlString(ChatType.Public)}

GO
{LoadAchievements()}
GO
{LoadAnimations()}
GO
{LoadLootBoxes()}
GO
{LoadPictures()}
GO
{LoadCheckersSkins()}
GO
{LoadUsers()}
GO
{LoadFriends()}
GO
{LoadFriendMessages()}
GO
{LoadUserPictures()}
GO
{LoadNews()}
GO
{LoadNewsMessages()}
GO
{LoadPosts()}
GO
{LoadPostMessages()}
GO
{LoadCommonChat()}
");

namespace DatabaseStartup
{
    internal static class CsvTable
    {
        private static string[] ReadLines(string filename) => File.ReadAllLines(filename);
        private const string Declaration = $"DECLARE {IdVar} INT\n";
        public const string PictureSource = "AvatarPicture.csv";
        public const string CheckersSource = "CheckersSkins.csv";
        public const string AnimationSource = "Animations.csv";
        public const string AchievementsSource = "Achivements.csv";
        public const string LootBoxSource = "LootBoxes.csv";
        private const string UserSource = "Users.csv";
        private const string UserPictureSource = "UserAvatars.csv";
        private const string NewsSource = "News.csv";
        private const string PostSource = "ForumPosts.csv";
        private const string NewsChatSource = "NewsChat.csv";
        private const string ForumChatSource = "ForumChat.csv";
        private const string FriendChatSource = "FriendsChat.csv";
        private const string FriendshipSource = "Friends.csv";
        private const string CommonChatSource = "AllChat.csv";

        private static readonly string Path = Directory.GetCurrentDirectory();
        private static readonly Exception LineSplitException = new ArgumentNullException("line.Split(\";\")");

        public static string SqlString(object o) => $"N'{o.ToString()?.Replace("\'",@"''")}'";
        private static string DataFile(string filename) => $@"{Path}\Data\{filename}";
        private static string ResourceFile(string filename) => SqlString($@"{Path}\Img\{filename}");

        private static string Exec(string command, object args) =>
            $"EXEC {command} {args}";

        public static string LoadPictures()=>
            string.Join('\n',ReadLines(DataFile(PictureSource))
                .Select(s=>Exec(CreatePictureProc,new NamedItemArgs(s))));

        public static string LoadAchievements() =>
            string.Join('\n', ReadLines(DataFile(AchievementsSource))
                .Select(s => Exec(CreateAchievementProc, new DetailedItemArgs(s))));

        public static string LoadAnimations() =>
            string.Join('\n', ReadLines(DataFile(AnimationSource))
                .Select(s => Exec(CreateAnimationProc, new SoldItemArgs(s))));

        public static string LoadLootBoxes() =>
            string.Join('\n', ReadLines(DataFile(LootBoxSource))
                .Select(s => Exec(CreateLootBoxProc, new SoldItemArgs(s))));

        public static string LoadCheckersSkins() =>
            string.Join('\n', ReadLines(DataFile(CheckersSource))
                .Select(s => Exec(CreateCheckersSkinProc, new SoldItemArgs(s))));

        public static string LoadUsers() =>
            string.Join('\n', ReadLines(DataFile(UserSource))
                .Select(s => new UserArgs(s))
                .Select(i => Exec(CreateUserProc, i)));

        public static string LoadUserPictures()
        {
            static string PictureId(string name) =>
                $"(SELECT {Id} FROM {PictureTable} WHERE {Name} = {name})";

            static string Set(string name) => $"SET @id = {PictureId(name)}";
            static string ExecUpdate(string log, string pass) => $"EXEC {UpdateUserPictureProc} {log}, {pass}, @id";
            static string Update(UserPictureArgs u) => $"{Set(u.PicName)}\n{ExecUpdate(u.Login, u.Password)}";

            return Declaration +
                   string.Join('\n', ReadLines(DataFile(UserPictureSource))
                       .Select(s => Update(new UserPictureArgs(s))));
        }

        public static string LoadNews()
        {
            static string SetPicture(string filename) =>
                $"EXEC {IdVar} = {CreateResourceFromFileProc} {ResourceFile(filename)}\n";
            static string CreateArticle(ArticleArgs a) =>
                SetPicture(a.File) +
                $"EXEC {CreateArticleProc} {a.Login}, {a.Password}, {a.Title}, {a.Abstract}, {a.Content}, {IdVar}";

            return Declaration +
                   string.Join('\n', ReadLines(DataFile(NewsSource))
                       .Select(s => CreateArticle(new ArticleArgs(s))));
        }

        public static string LoadPosts()
        {
            static string SetPicture(string filename) =>
                $"EXEC {IdVar} = {CreateResourceFromFileProc} {ResourceFile(filename)}\n";

            static string CreatePost(PostArgs p) =>
                SetPicture(p.File) +
                $"EXEC {CreatePostProc} {p.Login}, {p.Password}, {p.Title}, {p.Content}, {IdVar}";

            return Declaration +
                   string.Join('\n', ReadLines(DataFile(PostSource))
                       .Select(s=>CreatePost(new PostArgs(s))));
        }

        public static string LoadNewsMessages()
        {
            static string GetPostId(string title) =>
                $"(SELECT {ArticlePostId} FROM {ArticleTable} WHERE {ArticleTitle}={title})";
        
            static string SetId(string title)
                => $"SET {IdVar} = (SELECT {ChatId} FROM {PostTable} WHERE {Id} = {GetPostId(title)});\n";

            static string CreateComment(DirectedMessageArgs m) =>
                SetId(m.Direction) +
                $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

            return Declaration +
                   string.Join('\n', ReadLines(DataFile(NewsChatSource))
                       .Select(s => CreateComment(new DirectedMessageArgs(s))));
        }

        public static string LoadPostMessages()
        {
            static string GetChatId(string title) =>
                $"(SELECT {ChatId} FROM {PostTable} WHERE {PostTitle}={title});\n";

            static string CreateComment(DirectedMessageArgs m) =>
                $"SET {IdVar} = {GetChatId(m.Direction)}" +
                $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

            return Declaration +
                   string.Join('\n', ReadLines(DataFile(ForumChatSource))
                       .Select(s => CreateComment(new DirectedMessageArgs(s))));
        }

        public static string LoadFriends()
        {
            const string declaration = "DECLARE @id1 INT, @id2 INT\n";

            static string GetUserId(string var,string login) =>
                $"SET {var} = (SELECT {Id} FROM {UserTable} WHERE {Login} = {login});\n";

            static string CreateFriendship(FriendshipArgs f) =>
                GetUserId("@id1",f.Friend)+
                GetUserId("@id2",f.Login)+
                $"EXEC {CreateFriendshipProc} @id1, @id2";

            return declaration +
                   string.Join('\n', ReadLines(DataFile(FriendshipSource))
                       .Select(s => CreateFriendship(new FriendshipArgs(s))));
        }

        public static string LoadFriendMessages()
        {
            const string declaration = $"DECLARE {IdVar} INT, @id1 INT, @id2 INT\n";

            static string SetUserId(string var, string login) =>
                $"SET {var} = (SELECT {Id} FROM {UserTable} WHERE {Login}={login});\n";
        
            static string SendMessage(DirectedMessageArgs m) =>
                SetUserId("@id1",m.Login) +
                SetUserId("@id2",m.Direction) +
                $"SET {IdVar} = (SELECT {ChatId} FROM {FriendshipTable} WHERE {User1Id}=@id1 AND {User2Id}=@id2)\n"+
                $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

            return declaration +
                   string.Join('\n', ReadLines(DataFile(FriendChatSource))
                       .Select(s => SendMessage(new DirectedMessageArgs(s))));
        }

        public static string LoadCommonChat()
        {
            var declaration =
                $"DECLARE {IdVar} INT\n" +
                $"EXEC {IdVar} = {GetCommonChatIdProc}\n";

            static string SendMessage(MessageArgs m) =>
                $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

            return declaration +
                   string.Join('\n', ReadLines(DataFile(CommonChatSource))
                       .Select(s => SendMessage(new MessageArgs(s))));
        }


        private class NamedItemArgs
        {
            private readonly string _name;
            private readonly string _path;

            internal NamedItemArgs(string line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;

                _name = SqlString(strings[0]);
                _path = ResourceFile(strings[1]);
            }

            public override string ToString() => $"{_name}, {_path}";
        }

        private class DetailedItemArgs : NamedItemArgs
        {
            private readonly string _detail;
            internal DetailedItemArgs(string line) : base(line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;

                _detail = SqlString(strings[2]);
            }

            public override string ToString() => base.ToString() + $", {_detail}";
        }

        private sealed class SoldItemArgs : DetailedItemArgs
        {
            private readonly string _price;
            internal SoldItemArgs(string line) : base(line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;

                _price = strings[3];
            }

            public override string ToString() => base.ToString() + $", {_price}";
        }

        private sealed class UserArgs
        {
            private readonly string _nick;
            private readonly string _login;
            private readonly string _password;
            private readonly string _email;
            private readonly string _type;

            internal UserArgs(string line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;
                _nick = SqlString(strings[0]);
                _login = SqlString(strings[1]);
                _password = SqlString(strings[2]);
                _email = SqlString(strings[3]);
                _type = SqlString(strings[4]);
            }

            public override string ToString() =>
                $"{_nick}, {_login}, {_password}, {_email}, {_type}";
        }

        private sealed class UserPictureArgs
        {
            internal readonly string Login;
            internal readonly string Password;
            internal readonly string PicName;

            internal UserPictureArgs(string line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;
                Login = SqlString(strings[0]);
                Password = SqlString(strings[1]);
                PicName = SqlString(strings[2]);
            }
        }

        private sealed class ArticleArgs
        {
            internal readonly string Login;
            internal readonly string Password;
            internal readonly string Title;
            internal readonly string Abstract;
            internal readonly string Content;
            internal readonly string File;

            internal ArticleArgs(string line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;
                Login = SqlString(strings[0]);
                Password = SqlString(strings[1]);
                Title = SqlString(strings[2]);
                Abstract = SqlString(strings[3]);
                Content = SqlString(strings[4]);
                File = strings[5];
            }
        }

        private sealed class PostArgs
        {
            internal readonly string Login;
            internal readonly string Password;
            internal readonly string Title;
            internal readonly string Content;
            internal readonly string File;

            internal PostArgs(string line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;
                Login = SqlString(strings[0]);
                Password = SqlString(strings[1]);
                Title = SqlString(strings[2]);
                Content = SqlString(strings[3]);
                File = strings[4];
            }

        }

        private class MessageArgs
        {
            internal readonly string Login;
            internal readonly string Password;
            internal readonly string Content;

            internal MessageArgs(string line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;
                Login = SqlString(strings[0]);
                Password = SqlString(strings[1]);
                Content = SqlString(strings[2]);
            }
        }

        private sealed class DirectedMessageArgs : MessageArgs
        {
            internal readonly string Direction;
            internal DirectedMessageArgs(string line) : base(line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;
                Direction = SqlString(strings[3]);
            }
        }

        private sealed class FriendshipArgs
        {
            internal readonly string Login;
            internal readonly string Friend;

            internal FriendshipArgs(string line)
            {
                var strings = line.Split(";") ??
                              throw LineSplitException;
                Login = SqlString(strings[0]);
                Friend = SqlString(strings[1]);
            }

        }
    }
}