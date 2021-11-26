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
{ItemTypeName}	{StringType}	NOT NULL	UNIQUE
);

CREATE TABLE {ItemTable}
(
{Identity},
{Updated}		DATETIME		NOT NULL	DEFAULT GETDATE(),
{ItemTypeId}    INT             NOT NULL	{Fk(ItemTable,ItemTypeTable)},
{ResourceId}    INT             NOT NULL	{Fk(ItemTable, ResourceTable)},
{Detail}        {StringType}    NOT NULL,
{ItemName}		{StringType}	NOT NULL,
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
{UserTypeName}  {StringType}    NOT NULL
);

CREATE TABLE {UserTable}
(
{Identity},
{UserTypeId}        INT		        NOT NULL	{Fk(UserTable, UserTypeTable)}      DEFAULT 1,
{LastActivity}	    DATETIME		NOT NULL	DEFAULT GETDATE(),
{Nick}              {StringType}    NOT NULL,
{Login}		        {StringType}	NOT NULL	UNIQUE,
{Password}		    {StringType}	NOT NULL,
{Email}			    {StringType}	NOT NULL,
{PictureId}		    INT				NOT NULL	{Fk(UserTable,PictureTable)}        DEFAULT 1,
{SocialCredit}      INT             NOT NULL    DEFAULT 1000,
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
{ChatTypeName}      {StringType}    NOT NULL
);

CREATE TABLE {ChatTable}
(
{Identity},
{ChatName}      {StringType}    NOT NULL,
{ChatTypeId}    INT             NOT NULL {Fk(ChatTable,ChatTypeTable)}
);

CREATE TABLE {FriendshipStateTable}
(
{Identity},
{FriendshipStateName} {StringType}  NOT NULL
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
CREATE PROCEDURE {GetItemTypeByNameProc} {ItemTypeNameVar} {StringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{ItemTypeTable} WHERE {ItemTypeName}={ItemTypeNameVar})
END

GO
CREATE PROCEDURE {GetUserTypeByNameProc} {UserTypeNameVar} {StringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{UserTypeTable} WHERE {UserTypeName}={UserTypeNameVar})
END

GO
CREATE PROCEDURE {GetChatTypeByNameProc} {ChatTypeNameVar} {StringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{ChatTypeTable} WHERE {ChatTypeName}={ChatTypeNameVar})
END

GO
CREATE PROCEDURE {GetFriendshipStateByNameProc} {FriendshipStateNameVar} {StringType}
AS
BEGIN
    RETURN (SELECT {Id} FROM {Schema}.{FriendshipStateTable} WHERE {FriendshipStateName}={FriendshipStateNameVar})
END

GO
CREATE PROCEDURE {CheckAccessProc} {IdVar} INT, {UserTypeNameVar} {StringType}
AS
BEGIN
    DECLARE {UserTypeIdVar} INT, {AdminTypeIdVar} INT, {RequestedTypeIdVar} INT
    SET {UserTypeIdVar} = (SELECT {UserTypeId} FROM {Schema}.{UserTable} WHERE {Id}={IdVar});
    EXEC {AdminTypeIdVar} = {GetUserTypeByNameProc}'{UserType.Admin}';
    EXEC {RequestedTypeIdVar} = {GetUserTypeByNameProc} {UserTypeNameVar}
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
CREATE PROCEDURE {CreateChatProc} {ChatNameVar} {StringType}, {ChatTypeNameVar} {StringType}
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {GetChatTypeByNameProc} {ChatTypeNameVar};
    INSERT INTO {Schema}.{ChatTable}({ChatName},{ChatTypeId}) 
    VALUES ({ChatNameVar},{IdVar});
    RETURN @@IDENTITY
END

GO
CREATE PROCEDURE {CreateFriendship} {User1IdVar} INT,{User2IdVar} INT
AS
BEGIN
    DECLARE {ChatIdVar} INT, @u1_login {StringType}, @u2_login {StringType}, {ChatNameVar} {StringType}, {IdVar} INT
    SET @u1_login = (SELECT {Login} FROM {Schema}.{UserTable} WHERE {Id}={User1IdVar});
    SET @u2_login = (SELECT {Login} FROM {Schema}.{UserTable} WHERE {Id}={User2IdVar});
    SET {ChatNameVar} = N'Chat '+@u1_login+N' to '+ @u2_login;
    EXEC {ChatIdVar} = {CreateChatProc} {ChatNameVar}, '{ChatType.Private}';
    EXEC {IdVar} = {GetFriendshipStateByNameProc} '{FriendshipState.Accepted}';
    INSERT INTO {Schema}.{FriendshipTable}({User1Id},{User2Id},{ChatId},{FriendshipStateId})
    VALUES ({User1IdVar},{User2IdVar},{ChatIdVar},{IdVar});
END


GO
CREATE PROCEDURE {UpdateUserActivityProc} {LoginVar} {StringType}, {PasswordVar} {StringType}
AS
BEGIN
    UPDATE {UserTable} SET {LastActivity}=GETDATE() WHERE {UserAuthCondition}
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
CREATE PROCEDURE {CreateUserProc}
{NickVar} {StringType},
{LoginVar} {StringType},
{PasswordVar} {StringType},
{EmailVar} {StringType},
{UserTypeNameVar} {StringType}
AS
BEGIN
    DECLARE {UserIdVar} INT, @support_id INT, {IdVar} INT
    INSERT INTO {Schema}.{UserTable}({Nick},{Login},{Password},{Email},{UserTypeId})
    VALUES ({NickVar},{LoginVar},{PasswordVar},{EmailVar},(SELECT {Id} FROM {UserTypeTable} WHERE {UserTypeName}={UserTypeNameVar}));
    SET {UserIdVar} = @@IDENTITY;
    IF {UserTypeNameVar}!='{UserType.Support}'
        BEGIN
        EXEC {IdVar} = {GetUserTypeByNameProc} '{UserType.Support}'
        SET @support_id = (SELECT TOP 1 {Id} FROM {Schema}.{UserTable} WHERE {UserTypeId} = {IdVar});
        EXEC {CreateFriendship} {UserIdVar}, @support_id
        END
    INSERT INTO {UserItemTable}({UserId},{ItemId}) VALUES ({UserIdVar},(SELECT {ItemId} FROM {AnimationTable} WHERE {Id}=1));
    INSERT INTO {UserItemTable}({UserId},{ItemId}) VALUES ({UserIdVar},(SELECT {ItemId} FROM {CheckersTable} WHERE {Id}=1));
    INSERT INTO {UserItemTable}({UserId},{ItemId}) VALUES ({UserIdVar},(SELECT {ItemId} FROM {PictureTable} WHERE {Id}=1));
    RETURN {UserIdVar}
END

GO
CREATE PROCEDURE {SelectUserProc} {IdVar} INT
AS
BEGIN
    SELECT {Id},{Nick},{PictureId},{CheckersId},{AnimationId},{SocialCredit},{LastActivity},{UserTypeId}
    FROM {Schema}.{UserTable}
    WHERE {Id}={IdVar}
END

GO 
CREATE PROCEDURE {SelectUserItemProc} {IdVar} INT, {ItemTypeVar} {StringType}
AS
BEGIN
    DECLARE {ItemTypeIdVar} INT
    EXEC {ItemTypeIdVar} = {GetItemTypeByNameProc} {ItemTypeVar}
    SELECT I.{Id} FROM {Schema}.{UserItemTable} AS UI 
    JOIN {ItemTable} AS I ON UI.{ItemId}=I.{Id}  WHERE I.{ItemTypeId}={ItemTypeIdVar}; 
END

GO  
CREATE PROCEDURE {UpdateUserNickProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {NewNickVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Nick}={NewNickVar} WHERE {UserAuthCondition};
END

GO  
CREATE PROCEDURE {UpdateUserLoginProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {NewLoginVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Login}={NewLoginVar} WHERE {UserAuthCondition};
END

GO  
CREATE PROCEDURE {UpdateUserPasswordProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {NewPasswordVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Password}={NewPasswordVar} WHERE {UserAuthCondition};
END

GO  
CREATE PROCEDURE {UpdateUserEmailProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {NewEmailVar} {StringType}
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    UPDATE {Schema}.{UserTable} SET {Email}={NewEmailVar} WHERE {UserAuthCondition};
END

GO
CREATE PROCEDURE {SelectUserByNickProc} {NickVar} {StringType}
AS
BEGIN
    SELECT {Id},{Nick},{PictureId},{CheckersId},{AnimationId},{SocialCredit}
    FROM {Schema}.{UserTable}
    WHERE {Nick} LIKE {NickVar}
END

GO
CREATE PROCEDURE {AuthenticateProc} {LoginVar} {StringType}, {PasswordVar} {StringType}
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
CREATE PROCEDURE {UpdateUserAnimationProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT;
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    IF {IdVar} IN (SELECT {ItemId} FROM {Schema}.{UserItemTable} WHERE {UserId}={UserIdVar})
        UPDATE {Schema}.{UserTable} SET {AnimationId}={IdVar};
END

GO
CREATE PROCEDURE {UpdateUserCheckersProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT;
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar}
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    IF {IdVar} IN (SELECT {ItemId} FROM {Schema}.{UserItemTable} WHERE {UserId}={UserIdVar})
        UPDATE {Schema}.{UserTable} SET {CheckersId}={IdVar};
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
{ItemNameVar} {StringType},
{DetailVar} {StringType},
{PathVar} {StringType},
{PriceVar} INT
AS
BEGIN
    DECLARE {ResourceExtensionVar} {StringType}, {IdVar} INT, @sql {StringType}, {ResourceBytesVar} {BinaryType}, @type_id INT
    SET @type_id = (SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}={ItemTypeVar});
    SET {ResourceExtensionVar} = (SELECT RIGHT({PathVar}, CHARINDEX('.', REVERSE({PathVar}) + '.') - 1));
    SET @sql = FORMATMESSAGE ( 'SELECT {ResourceBytesVar} = BulkColumn FROM OPENROWSET ( BULK ''%s'', SINGLE_BLOB ) AS x;', @path );
    EXEC sp_executesql @sql, N'{ResourceBytesVar} {BinaryType} OUT', {ResourceBytesVar} = {ResourceBytesVar} OUT;
    EXEC  {IdVar}={CreateResourceProc} {ResourceExtensionVar} ,{ResourceBytesVar}
    INSERT INTO {Schema}.{ItemTable}({ItemName},{ItemTypeId},{Detail},{ResourceId},{Price})
    VALUES ({ItemNameVar},@type_id,{DetailVar},{IdVar},{PriceVar});
    RETURN @@IDENTITY;
END

GO
CREATE PROCEDURE {CreatePostProc} {LoginVar} {StringType}, {PasswordVar} {StringType},
{PostTitleVar} {StringType}, {PostContentVar} {StringType},{PostPictureIdVar} INT
AS
BEGIN
    DECLARE {IdVar} INT, {UserIdVar} INT, {ChatNameVar} {StringType}
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar}, {PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        SET {ChatNameVar} = N'Chat ' + {PostTitleVar};
        EXEC {IdVar} = {CreateChatProc} {ChatNameVar}, '{ChatType.Public}';
        IF {IdVar} IS NOT NULL
            BEGIN
            INSERT INTO {Schema}.{PostTable}({PostContent},{PostTitle},{PostPictureId},{ChatId},{PostAuthorId})
            VALUES ({PostContentVar},{PostTitleVar},{PostPictureIdVar},{IdVar},{UserIdVar});
            RETURN @@IDENTITY;
            END
        END
    ELSE
        RETURN {InvalidId};
END

GO
CREATE PROCEDURE {CreateArticleProc} {LoginVar} {StringType}, {PasswordVar} {StringType},
{ArticleTitleVar} {StringType}, {ArticleAbstractVar} {StringType}, 
{ArticleContentVar} {StringType}, {ArticlePictureIdVar} INT
AS
BEGIN
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
                RETURN @@IDENTITY
                END
            END
        END
    ELSE
        RETURN {InvalidId};
END

GO


GO
CREATE PROCEDURE {SelectArticleInfoProc} {IdVar} INT
AS
BEGIN
    SELECT {Id},{ArticlePictureId},{ArticleTitle},{ArticleAbstract},{ArticlePictureId} FROM {Schema}.{ArticleTable} WHERE {Id}={IdVar};
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
    SELECT {Id},{ArticlePictureId},{ArticleTitle},{ArticleAbstract},{ArticlePictureId} FROM {Schema}.{ArticleTable};
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
CREATE PROCEDURE {SendMessageProc} {LoginVar} {StringType},{PasswordVar} {StringType},
{ChatIdVar} INT,{MessageContentVar} {StringType}
AS
BEGIN
    DECLARE {UserIdVar} INT, {IdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar};
    IF {UserIdVar}!={InvalidId}
        BEGIN
        EXEC {IdVar} = {GetChatTypeByNameProc} '{ChatType.Public}'
        IF {ChatIdVar} IN 
        (SELECT {ChatId} FROM {FriendshipTable} WHERE {User1Id}={UserIdVar} OR {User2Id}={UserIdVar}) OR 
        (SELECT {ChatTypeId} FROM {ChatTable} WHERE {Id}={ChatIdVar}) = {IdVar}
            INSERT INTO {Schema}.{MessageTable}({ChatId},{UserId},{MessageContent})
            VALUES ({ChatIdVar},{UserIdVar},{MessageContentVar}); 
        END
END

GO
CREATE PROCEDURE {SelectMessageProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {ChatIdVar} INT
AS
BEGIN
    DECLARE {UserIdVar} INT
    EXEC {UserIdVar} = {AuthenticateProc} {LoginVar},{PasswordVar};
    IF {UserIdVar}!={InvalidId}
            SELECT * FROM {Schema}.{MessageTable} WHERE {ChatId}={ChatIdVar};
END

GO
CREATE PROCEDURE {SetItemsProc}
AS
BEGIN
    DECLARE {IdVar} INT
    EXEC {IdVar} = {GetItemTypeByNameProc} '{ItemType.Achievement}';
    INSERT INTO {AchievementTable}({ItemId}) (SELECT {Id} FROM {ItemTable} WHERE {ItemTypeId}={IdVar}); 
    EXEC {IdVar} = {GetItemTypeByNameProc} '{ItemType.CheckersSkin}';
    INSERT INTO {CheckersTable}({ItemId}) (SELECT {Id} FROM {ItemTable} WHERE {ItemTypeId}={IdVar});
    EXEC {IdVar} = {GetItemTypeByNameProc} '{ItemType.Animation}';
    INSERT INTO {AnimationTable}({ItemId}) (SELECT {Id} FROM {ItemTable} WHERE {ItemTypeId}={IdVar}); 
    EXEC {IdVar} = {GetItemTypeByNameProc} '{ItemType.Picture}';
    INSERT INTO {PictureTable}({ItemId}) (SELECT {Id} FROM {ItemTable} WHERE {ItemTypeId}={IdVar}); 
    EXEC {IdVar} = {GetItemTypeByNameProc} '{ItemType.LootBox}';
    INSERT INTO {LootBoxTable}({ItemId}) (SELECT {Id} FROM {ItemTable} WHERE {ItemTypeId}={IdVar}); 
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

EXEC {CreateItemProc} '{ItemType.Picture}','Picture 1','First Picture detail','{path}\{image}\1.png',100;
EXEC {CreateItemProc} '{ItemType.Achievement}','Achievement 1','First Achievement detail','{path}\{image}\2.png',100;
EXEC {CreateItemProc} '{ItemType.CheckersSkin}','CheckersSkin 1','First CheckersSkin detail','{path}\{image}\3.png',100;
EXEC {CreateItemProc} '{ItemType.Animation}','Animation 1','First Animation detail','{path}\{image}\4.png',100;
EXEC {SetItemsProc}

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