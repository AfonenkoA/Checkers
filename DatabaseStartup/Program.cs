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
{ResourceBytes}         VARBINARY(MAX)	NOT NULL,
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

CREATE TABLE {ChatTable}
(
{Identity},
{ChatName}  {StringType}    NOT NULL
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
CREATE PROCEDURE {UpdateUserActivityProc} {LoginVar} {StringType}, {PasswordVar} {StringType}
AS
BEGIN
    UPDATE {UserTable} SET {LastActivity}=GETDATE() WHERE {UserAuthCondition}
END

GO
CREATE PROCEDURE {SelectItemPictureProc} {IdVar} INT
AS
BEGIN
    SELECT {ResourceBytes}, {ResourceExtension}
    FROM {Schema}.{ResourceTable} WHERE {Id}=(SELECT {ResourceId} FROM {Schema}.{ItemTable} WHERE {Id}={IdVar});
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
{EmailVar} {StringType}
AS
BEGIN
    DECLARE @user_id INT
    INSERT INTO {Schema}.{UserTable}({Nick},{Login},{Password},{Email})
    VALUES ({NickVar},{LoginVar},{PasswordVar},{EmailVar});
    SET @user_id = @@IDENTITY;
    INSERT INTO {UserItemTable}({UserId},{ItemId}) VALUES (@user_id,(SELECT {ItemId} FROM {AnimationTable} WHERE {Id}=1));
    INSERT INTO {UserItemTable}({UserId},{ItemId}) VALUES (@user_id,(SELECT {ItemId} FROM {CheckersTable} WHERE {Id}=1));
    INSERT INTO {UserItemTable}({UserId},{ItemId}) VALUES (@user_id,(SELECT {ItemId} FROM {PictureTable} WHERE {Id}=1));
END

GO
CREATE PROCEDURE {SelectUserProc} {IdVar} INT
AS
BEGIN
    SELECT {Id},{Nick},{PictureId},{CheckersId},{AnimationId},{SocialCredit},{LastActivity}
    FROM {Schema}.{UserTable}
    WHERE {Id}={IdVar}
END

GO 
CREATE PROCEDURE {SelectUserItemProc} {IdVar} INT, {ItemTypeVar} {StringType}
AS
BEGIN
    SELECT I.{Id} FROM {Schema}.{UserItemTable} AS UI 
    JOIN {ItemTable} AS I ON UI.{ItemId}=I.{Id}  WHERE I.{ItemTypeId}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}={ItemTypeVar}); 
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
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    SELECT {Id} FROM {Schema}.{UserTable} WHERE {UserAuthCondition};
END


GO
CREATE PROCEDURE {UpdateUserAnimationProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    IF {IdVar} IN (SELECT {ItemId} FROM {Schema}.{UserItemTable} WHERE {UserId}=(SELECT {Id} FROM {Schema}.{UserTable} WHERE {UserAuthCondition}))
        UPDATE {Schema}.{UserTable} SET {AnimationId}={IdVar};
END

GO
CREATE PROCEDURE {UpdateUserCheckersProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    EXEC {UpdateUserActivityProc} {LoginVar},{PasswordVar};
    IF {IdVar} IN (SELECT {ItemId} FROM {Schema}.{UserItemTable} WHERE {UserId}=(SELECT {Id} FROM {Schema}.{UserTable} WHERE {UserAuthCondition}))
        UPDATE {Schema}.{UserTable} SET {CheckersId}={IdVar};
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
    DECLARE @ext {StringType}, @id INT, @sql {StringType}, @bytes VARBINARY(MAX), @type_id INT
    SET @type_id = (SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}={ItemTypeVar});
    SET @ext = (SELECT RIGHT({PathVar}, CHARINDEX('.', REVERSE({PathVar}) + '.') - 1));
    SET @sql = FORMATMESSAGE ( 'SELECT @bytes = BulkColumn FROM OPENROWSET ( BULK ''%s'', SINGLE_BLOB ) AS x;', @path );
    EXEC sp_executesql @sql, N'@bytes VARBINARY(MAX) OUT', @bytes = @bytes OUT;
    INSERT INTO {Schema}.{ResourceTable}({ResourceExtension},{ResourceBytes}) VALUES (@ext,@bytes);
    SET @id=@@IDENTITY;
    INSERT INTO {Schema}.{ItemTable}({ItemName},{ItemTypeId},{Detail},{ResourceId},{Price})
    VALUES ({ItemNameVar},@type_id,{DetailVar},@id,{PriceVar});
END

GO
CREATE PROCEDURE {CreatePostProc} {LoginVar} {StringType}, {PasswordVar} {StringType},
{PostTitleVar} {StringType}, {PostContentVar} {StringType},{PostPictureIdVar} INT
AS
BEGIN
    DECLARE @id INT, @user_id INT
    CREATE TABLE #Temp(id INT);
    INSERT INTO #Temp EXEC {AuthenticateProc} {LoginVar}, {PasswordVar};
    SET @user_id = (SELECT * FROM #Temp);
    INSERT INTO {Schema}.{ChatTable}({ChatName}) VALUES (N'Chat ' + {PostTitleVar});
    SET @id = @@IDENTITY;
    INSERT INTO {Schema}.{PostTable}({PostContent},{PostTitle},{PostPictureId},{ChatId},{PostAuthorId})
    VALUES ({PostContentVar},{PostTitleVar},{PostPictureIdVar},@id,@user_id);
    DROP TABLE #Temp;
    SELECT @@IDENTITY AS {PostId};
END

GO
CREATE PROCEDURE {CreateArticleProc} {LoginVar} {StringType}, {PasswordVar} {StringType},
{ArticleTitleVar} {StringType}, {ArticleAbstractVar} {StringType}, 
{ArticleContentVar} {StringType}, {ArticlePictureIdVar} INT
AS
BEGIN
    DECLARE @id INT, @user_id INT, @post {StringType}
    SET @post = N'Discussion ' + {ArticleTitleVar};
    CREATE TABLE #Temp(id INT);
    INSERT INTO #Temp EXEC {AuthenticateProc} {LoginVar}, {PasswordVar};
    SET @user_id = (SELECT * FROM #Temp);
    TRUNCATE TABLE #Temp;
    EXEC {CreatePostProc} {LoginVar},{PasswordVar},{ArticleTitleVar},@post, {ArticlePictureIdVar};
    SET @id = @@IDENTITY;
    INSERT INTO {Schema}.{ArticleTable}({ArticleTitle},{ArticleContent},{ArticleAbstract},{ArticleAuthorId},{ArticlePictureId},{ArticlePostId})
    VALUES  ({ArticleTitleVar},{ArticleContentVar},{ArticleAbstractVar},@user_id,{ArticlePictureIdVar},@id);
    DROP TABLE #Temp;
    SELECT @@IDENTITY AS {ArticleId};
END


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

EXEC {CreateItemProc} '{ItemType.Picture}','Picture 1','First Picture detail','{path}\{image}\1.png',100;
EXEC {CreateItemProc} '{ItemType.Achievement}','Achievement 1','First Achievement detail','{path}\{image}\2.png',100;
EXEC {CreateItemProc} '{ItemType.CheckersSkin}','CheckersSkin 1','First CheckersSkin detail','{path}\{image}\3.png',100;
EXEC {CreateItemProc} '{ItemType.Animation}','Animation 1','First Animation detail','{path}\{image}\4.png',100;


INSERT INTO {AchievementTable}({ItemId}) 
(SELECT {Id} FROM {ItemTable} WHERE {ItemTypeId}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.Achievement}')); 
INSERT INTO {CheckersTable}({ItemId}) 
(SELECT {Id} FROM {ItemTable} WHERE {ItemTypeId}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.CheckersSkin}')); 
INSERT INTO {AnimationTable}({ItemId}) 
(SELECT {Id} FROM {ItemTable} WHERE {ItemTypeId}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.Animation}')); 
INSERT INTO {PictureTable}({ItemId}) 
(SELECT {Id} FROM {ItemTable} WHERE {ItemTypeId}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.Picture}')); 

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
EXEC {CreateUserProc} 'b','b','b','b';
INSERT INTO {UserItemTable}({UserId},{ItemId})
VALUES (
(SELECT TOP 1 {Id} FROM {UserTable}),
(SELECT TOP 1 {ItemId} FROM {AchievementTable}));

EXEC {CreateArticleProc} 'b','b','Title','Abstract','Content', 1;
");