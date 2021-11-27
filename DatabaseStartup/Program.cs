using Checkers.Data.Entity;
using static System.Console;
using static Checkers.Data.Repository.MSSqlImplementation.ItemRepository;
using static Checkers.Data.Repository.MSSqlImplementation.Repository;
using static Checkers.Data.Repository.MSSqlImplementation.UserRepository;
using static Checkers.Data.Repository.MSSqlImplementation.ChatRepository;
using static Checkers.Data.Repository.MSSqlImplementation.MessageRepository;
using static Checkers.Data.Repository.MSSqlImplementation.ResourceRepository;
using static Checkers.Data.Repository.MSSqlImplementation.ForumRepository;
using static Checkers.Data.Repository.MSSqlImplementation.NewsRepository;
using static Checkers.Data.Repository.MSSqlImplementation.StatisticsRepository;

const string image = "img";
var path = Directory.GetCurrentDirectory();

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


CREATE TABLE {ItemTypeTable}
(
{Identity},
{ItemTypeName}	{UniqueStringType}	NOT NULL	UNIQUE
);

CREATE TABLE {ItemTable}
(
{Identity},
{Updated}		DATETIME		NOT NULL	DEFAULT GETDATE(),
{ItemTypeId}    INT             NOT NULL	{Fk(ItemTable,ItemTypeTable)},
{ResourceId}    INT             NOT NULL	{Fk(ItemTable, ResourceTable)},
{Detail}        {StringType}    NOT NULL,
{ItemName}		{UniqueStringType}	NOT NULL    UNIQUE,
{Price}		    INT				NOT NULL
);

CREATE TABLE {PictureTable}
(
{Identity},
{ItemId}		INT		NOT NULL	{Fk(PictureTable,ItemTable)}
);

CREATE TABLE {AchievementTable}
(
{Identity},
{ItemId}		INT		NOT NULL	{Fk(AchievementTable,ItemTable)}
);

CREATE TABLE {LootBoxTable}
(
{Identity},
{ItemId}		INT		NOT NULL	{Fk(LootBoxTable,ItemTable)}
);

CREATE TABLE {CheckersTable}
(
{Identity},
{ItemId}		INT		NOT NULL	{Fk(CheckersTable,ItemTable)}
);

CREATE TABLE {AnimationTable}
(
{Identity},
{ItemId}		INT		NOT NULL	{Fk(AnimationTable,ItemTable)}
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
{PictureId}		    INT				NOT NULL	{Fk(UserTable,PictureTable)}        DEFAULT 1,
{SocialCredit}      INT             NOT NULL    DEFAULT 1000,
{Currency}          INT             NOT NULL    DEFAULT 1000,
{CheckersId}        INT             NOT NULL    {Fk(UserTable,CheckersTable)}       DEFAULT 1,
{AnimationId}       INT             NOT NULL    {Fk(UserTable,AnimationTable)}      DEFAULT 1
);

CREATE TABLE {UserItemTable}
(
{Identity},
{UserId}            INT				NOT NULL	{Fk(UserItemTable,UserTable)},
{ItemId}            INT				NOT NULL	{Fk(UserItemTable,ItemTable)},
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
{ChatTypeId}    INT                 NOT NULL {Fk(ChatTable,ChatTypeTable)}
);

CREATE TABLE {FriendshipStateTable}
(
{Identity},
{FriendshipStateName} {UniqueStringType}  NOT NULL UNIQUE
);

CREATE TABLE {FriendshipTable}
(
{Identity},
{User1Id}           INT   NOT NULL	{Fk(FriendshipTable, UserTable,"1")},
{User2Id}           INT   NOT NULL	{Fk(FriendshipTable, UserTable,"2")},
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
{ChatId}            INT             NOT NULL	{Fk(PostTable, ChatTable)},
{PostAuthorId}      INT             NOT NULL	{Fk(PostTable, UserTable)},
{PostTitle}         {StringType}    NOT NULL,
{PostContent}       {StringType}    NOT NULL,
{PostCreated}       DATETIME        NOT NULL    DEFAULT GETDATE(),
{PostPictureId}     INT             NOT NULL    {Fk(PostTable,ResourceTable)}
);

CREATE TABLE {ArticleTable}
(
{Identity},
{ArticleAuthorId}   INT             NOT NULL	{Fk(ArticleTable, UserTable)},
{ArticleTitle}      {StringType}    NOT NULL,
{ArticleAbstract}   {StringType}    NOT NULL,
{ArticleContent}    {StringType}    NOT NULL,
{ArticleCreated}    DATETIME        NOT NULL    DEFAULT GETDATE(),
{ArticlePictureId}  INT             NOT NULL    {Fk(ArticleTable, ResourceTable)},
{ArticlePostId}     INT             NOT NULL    {Fk(ArticleTable, PostTable)},
);


GO
CREATE PROCEDURE {GetItemTypeByTypeNameProc} {ItemTypeNameVar} {UniqueStringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{ItemTypeTable} WHERE {ItemTypeName}={ItemTypeNameVar})
END

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
    EXEC {AdminTypeIdVar} = {GetUserTypeByTypeNameProc} '{UserType.Admin}';
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
    EXEC {ChatIdVar} = {CreateChatProc} {ChatNameVar}, '{ChatType.Private}';
    EXEC {IdVar} = {GetFriendshipStateByNameProc} '{FriendshipState.Accepted}';
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
CREATE PROCEDURE {SelectItemPictureProc} {IdVar} INT
AS
BEGIN
    DECLARE {ResourceIdVar} INT
    SET {ResourceIdVar} = (SELECT {ResourceId} FROM {Schema}.{ItemTable} WHERE {Id}={IdVar});
    EXEC {SelectResourceProc} {ResourceIdVar};
END

GO
CREATE PROCEDURE {SelectItemProc} {IdVar} INT
AS
BEGIN
    SELECT I.{Id},I.{Updated},I.{ItemTypeId},I.{ItemName},I.{Detail},R.{ResourceExtension},I.{Price}
    FROM {Schema}.{ItemTable} AS I 
    JOIN {Schema}.{ResourceTable} AS R ON R.{Id}=I.{ResourceId}
    WHERE I.{Id}={IdVar}
END

GO
CREATE PROCEDURE {SelectItemsProc}
AS
BEGIN
    SELECT {Id}, {Updated} FROM {Schema}.{ItemTable}
END

GO
CREATE PROCEDURE {UserAddItemProc} {UserIdVar} INT, {ItemIdVar} INT
AS
BEGIN
    INSERT INTO {Schema}.{UserItemTable}({UserId},{ItemId}) VALUES ({UserIdVar},{ItemIdVar}); 
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
    DECLARE {UserIdVar} INT, @support_id INT, {IdVar} INT
    BEGIN TRANSACTION;  
    INSERT INTO {Schema}.{UserTable}({Nick},{Login},{Password},{Email},{UserTypeId})
    VALUES ({NickVar},{LoginVar},{PasswordVar},{EmailVar},(SELECT {Id} FROM {UserTypeTable} WHERE {UserTypeName}={UserTypeNameVar}));
    SET {UserIdVar} = @@IDENTITY;
    IF {UserTypeNameVar}!='{UserType.Support}'
        BEGIN
        EXEC {IdVar} = {GetUserTypeByTypeNameProc} '{UserType.Support}'
        SET @support_id = (SELECT TOP 1 {Id} FROM {Schema}.{UserTable} WHERE {UserTypeId} = {IdVar});
        EXEC {CreateFriendshipProc} {UserIdVar}, @support_id
        END
    SET {IdVar} = (SELECT TOP 1 {ItemId} FROM {AnimationTable});
    EXEC {UserAddItemProc} {UserIdVar}, {IdVar};
    SET {IdVar} = (SELECT TOP 1 {ItemId} FROM {CheckersTable});
    EXEC {UserAddItemProc} {UserIdVar}, {IdVar};
    SET {IdVar} = (SELECT TOP 1 {ItemId} FROM {PictureTable});
    EXEC {UserAddItemProc} {UserIdVar}, {IdVar};
    COMMIT;
    RETURN {UserIdVar}
END

GO
CREATE PROCEDURE {SelectUserProc} {IdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{UserTable} WHERE {Id}={IdVar}
END

GO 
CREATE PROCEDURE {SelectUserItemProc} {IdVar} INT, {ItemTypeVar} {StringType}
AS
BEGIN
    DECLARE {ItemTypeIdVar} INT
    EXEC {ItemTypeIdVar} = {GetItemTypeByTypeNameProc} {ItemTypeVar}
    SELECT I.{Id} FROM {Schema}.{UserItemTable} AS UI 
    JOIN {ItemTable} AS I ON UI.{ItemId}=I.{Id}  WHERE I.{ItemTypeId}={ItemTypeIdVar}; 
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
    IF {IdVar} IN (SELECT {ItemId} FROM {Schema}.{UserItemTable} WHERE {UserId}={UserIdVar})
        UPDATE {Schema}.{UserTable} SET {AnimationId}={IdVar} WHERE {UserAuthCondition};
END

GO
CREATE PROCEDURE {UpdateUserCheckersProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT;
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    IF {IdVar} IN (SELECT {ItemId} FROM {Schema}.{UserItemTable} WHERE {UserId}={UserIdVar})
        UPDATE {Schema}.{UserTable} SET {CheckersId}={IdVar} WHERE {UserAuthCondition};
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
CREATE PROCEDURE {CreateItemProc}
{ItemTypeVar} {StringType},
{ItemNameVar} {UniqueStringType},
{DetailVar} {StringType},
{PathVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {ResourceExtensionVar} {StringType}, {IdVar} INT, @sql {StringType}, {ResourceBytesVar} {BinaryType}, @type_id INT
    BEGIN TRANSACTION;  
    SET @type_id = (SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}={ItemTypeVar});
    SET {ResourceExtensionVar} = (SELECT RIGHT({PathVar}, CHARINDEX('.', REVERSE({PathVar}) + '.') - 1));
    SET @sql = FORMATMESSAGE ( 'SELECT {ResourceBytesVar} = BulkColumn FROM OPENROWSET ( BULK ''%s'', SINGLE_BLOB ) AS x;', @path );
    EXEC sp_executesql @sql, N'{ResourceBytesVar} {BinaryType} OUT', {ResourceBytesVar} = {ResourceBytesVar} OUT;
    EXEC  {IdVar}={CreateResourceProc} {ResourceExtensionVar} ,{ResourceBytesVar}
    INSERT INTO {Schema}.{ItemTable}({ItemName},{ItemTypeId},{Detail},{ResourceId},{Price})
    VALUES ({ItemNameVar},@type_id,{DetailVar},{IdVar},{PriceVar});
    COMMIT;
    RETURN @@IDENTITY;
END

GO
CREATE PROCEDURE {CreatePostProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{PostTitleVar} {StringType}, {PostContentVar} {StringType},{PostPictureIdVar} INT
AS
BEGIN
    BEGIN TRANSACTION;
    DECLARE {IdVar} INT, {UserIdVar} INT, {ChatNameVar} {UniqueStringType}
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        SET {ChatNameVar} = N'Chat ' + {PostTitleVar};
        EXEC {IdVar} = {CreateChatProc} {ChatNameVar}, '{ChatType.Public}';
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
{ArticleTitleVar} {StringType}, {ArticleAbstractVar} {StringType}, 
{ArticleContentVar} {StringType}, {ArticlePictureIdVar} INT
AS
BEGIN
    BEGIN TRANSACTION;
    DECLARE {IdVar} INT, {UserIdVar} INT, @post {StringType}, {AccessVar} INT
    SET @post = N'Discussion ' + {ArticleTitleVar};
    EXEC {UserIdVar}={AuthenticateProc} {LoginVar}, {PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, '{UserType.Editor}';
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
        EXEC {IdVar} = {GetChatTypeByNameProc} '{ChatType.Public}'
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
{ItemNameVar} {UniqueStringType},
{DetailVar} {StringType},
{PathVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateItemProc} '{ItemType.Animation}' ,{ItemNameVar},{DetailVar},{PathVar},{PriceVar}
    INSERT INTO {Schema}.{AnimationTable}({ItemId}) VALUES({IdVar}); 
    RETURN {IdVar}
END

GO
CREATE PROCEDURE {CreateAchievementProc}
{ItemNameVar} {UniqueStringType},
{DetailVar} {StringType},
{PathVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateItemProc} '{ItemType.Achievement}' ,{ItemNameVar},{DetailVar},{PathVar},{PriceVar}
    INSERT INTO {Schema}.{AchievementTable}({ItemId}) VALUES({IdVar}); 
    RETURN {IdVar}
END

GO
CREATE PROCEDURE {CreateCheckersSkinProc}
{ItemNameVar} {UniqueStringType},
{DetailVar} {StringType},
{PathVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateItemProc} '{ItemType.CheckersSkin}' ,{ItemNameVar},{DetailVar},{PathVar},{PriceVar}
    INSERT INTO {Schema}.{CheckersTable}({ItemId}) VALUES({IdVar}); 
    RETURN {IdVar}
END

GO
CREATE PROCEDURE {CreatePictureProc}
{ItemNameVar} {UniqueStringType},
{DetailVar} {StringType},
{PathVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateItemProc} '{ItemType.Picture}' ,{ItemNameVar},{DetailVar},{PathVar},{PriceVar}
    INSERT INTO {Schema}.{PictureTable}({ItemId}) VALUES({IdVar}); 
    RETURN {IdVar}
END

GO
CREATE PROCEDURE {CreateLootBoxProc}
{ItemNameVar} {UniqueStringType},
{DetailVar} {StringType},
{PathVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {CreateItemProc} '{ItemType.LootBox}' ,{ItemNameVar},{DetailVar},{PathVar},{PriceVar}
    INSERT INTO {Schema}.{LootBoxTable}({ItemId}) VALUES({IdVar}); 
    RETURN {IdVar}
END

--Article
GO
CREATE PROCEDURE {UpdateArticleTitleProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {ArticleTitleVar} {StringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, '{UserType.Editor}'
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, '{UserType.Editor}'
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, '{UserType.Editor}'
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, '{UserType.Editor}'
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, '{UserType.Moderator}'
    IF {AccessVar}={ValidAccess}
        UPDATE {Schema}.{ArticleTable} SET {ArticlePostId} = {ArticlePostIdVar} WHERE {Id}={IdVar}
END


GO
CREATE PROCEDURE {UpdatePostTitleProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType},
{IdVar} INT, {PostTitleVar} {StringType}
AS
BEGIN
    DECLARE {AccessVar} INT, {UserIdVar} INT, {PostAuthorIdVar} INT
    SET {PostAuthorIdVar} = (SELECT {PostAuthorId} FROM {Schema}.{PostTable} WHERE {Id}={IdVar});
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, '{UserType.Moderator}'
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, '{UserType.Moderator}'
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, '{UserType.Moderator}'
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
CREATE PROCEDURE {SelectAllUserItemProc} {IdVar} INT
AS
BEGIN
    SELECT {ItemId} FROM {Schema}.{UserItemTable} WHERE {UserId}={IdVar}
END

GO
CREATE PROCEDURE {SelectFriendChatIdProc} {User1IdVar} INT, {User2IdVar} INT
AS
BEGIN
    RETURN (SELECT {ChatId} FROM {Schema}.{FriendshipTable} WHERE {User1Id} = {User1IdVar} AND {User2Id} = {User2IdVar})
END

GO
CREATE PROCEDURE {SelectUserFriendshipProc} {UserIdVar} INT
AS
BEGIN
    SELECT * FROM {Schema}.{FriendshipTable} WHERE {User1Id} = {UserIdVar}
END

GO
CREATE PROCEDURE {UserBuyItemProc} {LoginVar} {UniqueStringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT, {PriceVar} INT, {CurrencyVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar}
    SET {PriceVar} = (SELECT {Price} FROM {Schema}.{ItemTable} WHERE {Id}={IdVar});
    SET {CurrencyVar} = (SELECT {Currency} FROM {Schema}.{UserTable} WHERE {Id}={UserIdVar});
    IF {CurrencyVar}>={PriceVar}
        BEGIN
        INSERT INTO {Schema}.{UserItemTable}({UserId},{ItemId}) VALUES ({UserIdVar},{IdVar});
        UPDATE {Schema}.{UserTable} SET {Currency} = ({CurrencyVar}-{PriceVar}) WHERE {Id}={UserIdVar};
        END
END



GO
INSERT INTO {ItemTypeTable}({ItemTypeName}) 
VALUES ('{ItemType.Picture}'),
('{ItemType.Achievement}'),
('{ItemType.CheckersSkin}'),
('{ItemType.Animation}'),
('{ItemType.LootBox}');

INSERT INTO {UserTypeTable}({UserTypeName}) VALUES 
('{UserType.Admin}'),
('{UserType.Editor}'),
('{UserType.Moderator}'),
('{UserType.Support}'),
('{UserType.Player}')

INSERT INTO {ChatTypeTable}({ChatTypeName}) VALUES ('{ChatType.Public}'),('{ChatType.Private}');

INSERT INTO {FriendshipStateTable}({FriendshipStateName}) VALUES
('{FriendshipState.Waiting}'),
('{FriendshipState.Accepted}'),
('{FriendshipState.Canceled}');

EXEC {CreatePictureProc} 'Picture 1','First Picture detail','{path}\{image}\1.png',100;
EXEC {CreateAchievementProc} 'Achievement 1','First Achievement detail','{path}\{image}\2.png',100;
EXEC {CreateCheckersSkinProc} 'CheckersSkin 1','First CheckersSkin detail','{path}\{image}\3.png',100;
EXEC {CreateAnimationProc} 'Animation 1','First Animation detail','{path}\{image}\4.png',100;

GO
CREATE VIEW {UserItemExtendedView} AS
SELECT UI.{UserId},U.{Nick}, UI.{ItemId}, I.{Updated}, I.{Detail}, R.{ResourceExtension}, I.{ItemName}, 
        I.{Price}, IT.{ItemTypeName}
FROM {UserItemTable} AS UI
JOIN {ItemTable} AS I ON UI.{ItemId} = I.{Id}
JOIN {ItemTypeTable} AS IT ON IT.{Id} = I.{ItemTypeId}
JOIN {UserTable} AS U ON UI.{UserId} = U.{Id}
JOIN {ResourceTable} AS R ON R.{Id}=I.{ResourceId}

GO
Use Checkers
EXEC {CreateChatProc} '{CommonChatName}','{ChatType.Public}'

EXEC {CreateUserProc} 'Support','Support','Support','Support','{UserType.Support}';
EXEC {CreateUserProc} 'Admin','Admin','Admin','Admin','{UserType.Admin}';

INSERT INTO {UserItemTable}({UserId},{ItemId})
VALUES (
(SELECT TOP 1 {Id} FROM {UserTable}),
(SELECT TOP 1 {ItemId} FROM {AchievementTable}));

EXEC {CreateArticleProc} 'Admin','Admin','Title','Abstract','Content', 1;
EXEC {SendMessageProc} 'Admin', 'Admin', {CommonChatId}, N'Привет всем';
EXEC {SendMessageProc} 'Admin', 'Admin', {CommonChatId}, N'Удачи и приятной игры';
EXEC {SendMessageProc} 'Support', 'Support', 2, N'Если что-то не получается, пиши сюда'
");