using static System.Console;
using Common.Entity;
using DatabaseStartup;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.MessageRepository;
using static WebService.Repository.MSSqlImplementation.NewsRepository;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;
using static WebService.Repository.MSSqlImplementation.StatisticsRepository;
using static WebService.Repository.MSSqlImplementation.UserRepository;

Write(
    @$"GO
CREATE DATABASE Checkers;

GO
USE Checkers;
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {CsvTable.SqlString(UserType.Editor)}
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {CsvTable.SqlString(UserType.Editor)}
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {CsvTable.SqlString(UserType.Editor)}
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {CsvTable.SqlString(UserType.Editor)}
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {CsvTable.SqlString(UserType.Moderator)}
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {CsvTable.SqlString(UserType.Moderator)}
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {CsvTable.SqlString(UserType.Moderator)}
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
    EXEC {AccessVar} = {CheckAccessProc} {UserIdVar}, {CsvTable.SqlString(UserType.Moderator)}
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
    RETURN (SELECT {Id} FROM {ChatTable} WHERE {ChatName}={CsvTable.SqlString(CommonChatName)});
END

GO
INSERT INTO {UserTypeTable}({UserTypeName}) VALUES 
({CsvTable.SqlString((UserType)1)}),
({CsvTable.SqlString((UserType)2)}),
({CsvTable.SqlString((UserType)3)}),
({CsvTable.SqlString((UserType)4)}),
({CsvTable.SqlString((UserType)5)})

INSERT INTO {ChatTypeTable}({ChatTypeName}) VALUES
({CsvTable.SqlString((ChatType)1)}),
({CsvTable.SqlString((ChatType)2)});

INSERT INTO {FriendshipStateTable}({FriendshipStateName}) VALUES
({CsvTable.SqlString((FriendshipState)1)}),
({CsvTable.SqlString((FriendshipState)2)}),
({CsvTable.SqlString((FriendshipState)3)});

GO
Use Checkers
EXEC {CreateChatProc} {CsvTable.SqlString(CommonChatName)},{CsvTable.SqlString(ChatType.Public)}

GO
{CsvTable.LoadAchievements()}
GO
{CsvTable.LoadAnimations()}
GO
{CsvTable.LoadLootBoxes()}
GO
{CsvTable.LoadPictures()}
GO
{CsvTable.LoadCheckersSkins()}
GO
{CsvTable.LoadUsers()}
GO
{CsvTable.LoadFriends()}
GO
{CsvTable.LoadFriendMessages()}
GO
{CsvTable.LoadUserPictures()}
GO
{CsvTable.LoadNews()}
GO
{CsvTable.LoadNewsMessages()}
GO
{CsvTable.LoadPosts()}
GO
{CsvTable.LoadPostMessages()}
GO
{CsvTable.LoadCommonChat()}
");