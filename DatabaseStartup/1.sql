GO
CREATE DATABASE Checkers;

GO
USE Checkers;

CREATE TABLE [ResourceTable]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[resource_extension]     NVARCHAR(MAX)	NOT NULL,
[resource_bytes]         VARBINARY(MAX)	NOT NULL,
);


CREATE TABLE [ItemType]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_type_name]	NVARCHAR(300)	NOT NULL	UNIQUE
);

CREATE TABLE [Item]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[updated]		DATETIME		NOT NULL	DEFAULT GETDATE(),
[item_type_id]    INT             NOT NULL	CONSTRAINT FK_Item_ItemType    FOREIGN KEY REFERENCES [ItemType](id),
[resource_id]    INT             NOT NULL	CONSTRAINT FK_Item_ResourceTable    FOREIGN KEY REFERENCES [ResourceTable](id),
[detail]        NVARCHAR(MAX)    NOT NULL,
[item_name]		NVARCHAR(300)	NOT NULL    UNIQUE,
[price]		    INT				NOT NULL
);

CREATE TABLE [Picture]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_Picture_Item    FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [Achievement]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_Achievement_Item    FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [LootBox]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_LootBox_Item    FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [CheckersSkin]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_CheckersSkin_Item    FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [Animation]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_Animation_Item    FOREIGN KEY REFERENCES [Item](id)
);


CREATE TABLE [UserType]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[user_type_name]  NVARCHAR(300)    NOT NULL UNIQUE
);

CREATE TABLE [User]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[user_type_id]        INT		        NOT NULL	CONSTRAINT FK_User_UserType    FOREIGN KEY REFERENCES [UserType](id)      DEFAULT 1,
[last_activity]	    DATETIME		NOT NULL	DEFAULT GETDATE(),
[nick]              NVARCHAR(MAX)    NOT NULL,
[login]		        NVARCHAR(300)	NOT NULL	UNIQUE,
[password]		    NVARCHAR(MAX)	NOT NULL,
[email]			    NVARCHAR(MAX)	NOT NULL,
[picture_id]		    INT				NOT NULL	CONSTRAINT FK_User_Picture    FOREIGN KEY REFERENCES [Picture](id)        DEFAULT 1,
[social_credit]      INT             NOT NULL    DEFAULT 1000,
[currency]          INT             NOT NULL    DEFAULT 1000,
[checkers_id]        INT             NOT NULL    CONSTRAINT FK_User_CheckersSkin    FOREIGN KEY REFERENCES [CheckersSkin](id)       DEFAULT 1,
[animation_id]       INT             NOT NULL    CONSTRAINT FK_User_Animation    FOREIGN KEY REFERENCES [Animation](id)      DEFAULT 1
);

CREATE TABLE [UserItem]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[user_id]            INT				NOT NULL	CONSTRAINT FK_UserItem_User    FOREIGN KEY REFERENCES [User](id),
[item_id]            INT				NOT NULL	CONSTRAINT FK_UserItem_Item    FOREIGN KEY REFERENCES [Item](id),
);

CREATE TABLE [ChatType]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[chat_type_name]      NVARCHAR(300)    NOT NULL UNIQUE
);

CREATE TABLE [Chat]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[chat_name]      NVARCHAR(300)  NOT NULL  UNIQUE,
[chat_type_id]    INT                 NOT NULL CONSTRAINT FK_Chat_ChatType    FOREIGN KEY REFERENCES [ChatType](id)
);

CREATE TABLE [FriendshipState]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
friendship_state_name NVARCHAR(300)  NOT NULL UNIQUE
);

CREATE TABLE [Friendship]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[user1_id]           INT   NOT NULL	CONSTRAINT FK_Friendship_User1    FOREIGN KEY REFERENCES [User](id),
[user2_id]           INT   NOT NULL	CONSTRAINT FK_Friendship_User2    FOREIGN KEY REFERENCES [User](id),
[chat_id]            INT   NOT NULL	CONSTRAINT FK_Friendship_Chat    FOREIGN KEY REFERENCES [Chat](id),
friendship_state_id INT   NOT NULL	CONSTRAINT FK_Friendship_FriendshipState    FOREIGN KEY REFERENCES [FriendshipState](id)
);


CREATE TABLE [Message]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[chat_id]            INT             NOT NULL	CONSTRAINT FK_Message_Chat    FOREIGN KEY REFERENCES [Chat](id),
[user_id]            INT             NOT NULL	CONSTRAINT FK_Message_User    FOREIGN KEY REFERENCES [User](id), 
[message_content]    NVARCHAR(MAX)    NOT NULL,
[send_time]          DATETIME        NOT NULL    DEFAULT GETDATE()
);


CREATE TABLE [Post]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[chat_id]            INT                 NOT NULL	CONSTRAINT FK_Post_Chat    FOREIGN KEY REFERENCES [Chat](id),
[post_author_id]      INT                 NOT NULL	CONSTRAINT FK_Post_User    FOREIGN KEY REFERENCES [User](id),
[post_title]         NVARCHAR(300)  NOT NULL    UNIQUE,
[post_content]       NVARCHAR(MAX)        NOT NULL,
[post_created]       DATETIME            NOT NULL    DEFAULT GETDATE(),
[post_picture_id]     INT                 NOT NULL    CONSTRAINT FK_Post_ResourceTable    FOREIGN KEY REFERENCES [ResourceTable](id)
);

CREATE TABLE [Article]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[article_author_id]   INT                 NOT NULL	CONSTRAINT FK_Article_User    FOREIGN KEY REFERENCES [User](id),
[article_title]      NVARCHAR(300)  NOT NULL    UNIQUE,
[article_abstract]   NVARCHAR(MAX)        NOT NULL,
[article_content]    NVARCHAR(MAX)        NOT NULL,
[article_created]    DATETIME            NOT NULL    DEFAULT GETDATE(),
[article_picture_id]  INT                 NOT NULL    CONSTRAINT FK_Article_ResourceTable    FOREIGN KEY REFERENCES [ResourceTable](id),
[article_post_id]     INT                 NOT NULL    CONSTRAINT FK_Article_Post    FOREIGN KEY REFERENCES [Post](id),
);


GO
CREATE PROCEDURE [SP_GetItemTypeByName] @item_type_name NVARCHAR(300)
AS
BEGIN
    RETURN (SELECT id FROM Checkers.dbo.[ItemType] WHERE [item_type_name]=@item_type_name)
END

GO
CREATE PROCEDURE [SP_GetUserTypeByName] @user_type_name NVARCHAR(300)
AS
BEGIN
    RETURN (SELECT id FROM Checkers.dbo.[UserType] WHERE [user_type_name]=@user_type_name)
END

GO
CREATE PROCEDURE [SP_GetChatTypeByName] @chat_type_name NVARCHAR(300)
AS
BEGIN
    RETURN (SELECT id FROM Checkers.dbo.[ChatType] WHERE [chat_type_name]=@chat_type_name)
END

GO
CREATE PROCEDURE [SP_GetFriendshipStateByName] @friendship_state NVARCHAR(300)
AS
BEGIN
    RETURN (SELECT id FROM Checkers.dbo.[FriendshipState] WHERE friendship_state_name=@friendship_state)
END

GO
CREATE PROCEDURE [SP_CheckAccess] @id INT, @user_type_name NVARCHAR(300)
AS
BEGIN
    DECLARE @user_type_id INT, @admin_type_id INT, @req_type_id INT
    SET @user_type_id = (SELECT [user_type_id] FROM Checkers.dbo.[User] WHERE id=@id);
    EXEC @admin_type_id = [SP_GetUserTypeByName] N'Admin';
    EXEC @req_type_id = [SP_GetUserTypeByName] @user_type_name
    IF @user_type_id=@admin_type_id OR @user_type_id=@req_type_id
        RETURN 1
    ELSE
        RETURN -1
END


GO
CREATE PROCEDURE [SP_SelectResource] @id INT
AS
BEGIN
    SELECT * FROM Checkers.dbo.[ResourceTable] WHERE id=@id
END

GO
CREATE PROCEDURE [SP_CreateChat] @chat_name NVARCHAR(300), @chat_type_name NVARCHAR(300)
AS
BEGIN
    DECLARE @id INT
    BEGIN TRANSACTION;  
    EXEC @id = [SP_GetChatTypeByName] @chat_type_name;
    INSERT INTO Checkers.dbo.[Chat]([chat_name],[chat_type_id]) 
    VALUES (@chat_name,@id);
    COMMIT;
    RETURN @@IDENTITY
END

GO
CREATE PROCEDURE [SP_CreateFriendship] @user1_id INT,@user2_id INT
AS
BEGIN
    DECLARE @chat_id INT, @u1_login NVARCHAR(300), @u2_login NVARCHAR(300),
    @chat_name NVARCHAR(300), @id INT
    BEGIN TRANSACTION;  
    SET @u1_login = (SELECT [login] FROM Checkers.dbo.[User] WHERE id=@user1_id);
    SET @u2_login = (SELECT [login] FROM Checkers.dbo.[User] WHERE id=@user2_id);
    SET @chat_name = N'Chat '+@u1_login+N' to '+ @u2_login;
    EXEC @chat_id = [SP_CreateChat] @chat_name, N'Private';
    EXEC @id = [SP_GetFriendshipStateByName] N'Accepted';
    INSERT INTO Checkers.dbo.[Friendship]([user1_id],[user2_id],[chat_id],friendship_state_id)
    VALUES (@user1_id,@user2_id,@chat_id,@id),(@user2_id,@user1_id,@chat_id,@id);
    COMMIT;
END


GO
CREATE PROCEDURE [SP_UpdateUserActivity] @login NVARCHAR(300), @password NVARCHAR(MAX)
AS
BEGIN
    UPDATE Checkers.dbo.[User] SET [last_activity]=GETDATE() WHERE [login]=@login AND [password]=@password
END

GO
CREATE PROCEDURE [SP_SelectItemPicture] @id INT
AS
BEGIN
    DECLARE @resource_id INT
    SET @resource_id = (SELECT [resource_id] FROM Checkers.dbo.[Item] WHERE id=@id);
    EXEC [SP_SelectResource] @resource_id;
END

GO
CREATE PROCEDURE [SP_SelectItem] @id INT
AS
BEGIN
    SELECT I.id,I.[updated],I.[item_type_id],I.[item_name],I.[detail],R.[resource_extension],I.[price]
    FROM Checkers.dbo.[Item] AS I 
    JOIN Checkers.dbo.[ResourceTable] AS R ON R.id=I.[resource_id]
    WHERE I.id=@id
END

GO
CREATE PROCEDURE [SP_SelectItems]
AS
BEGIN
    SELECT id, [updated] FROM Checkers.dbo.[Item]
END

GO
CREATE PROCEDURE [SP_AddUserItem] @user_id INT, @item_id INT
AS
BEGIN
    INSERT INTO Checkers.dbo.[UserItem]([user_id],[item_id]) VALUES (@user_id,@item_id); 
END

GO
CREATE PROCEDURE [SP_CreateUser]
@nick NVARCHAR(MAX),
@login NVARCHAR(300),
@password NVARCHAR(MAX),
@email NVARCHAR(MAX),
@user_type_name NVARCHAR(300)
AS
BEGIN
    DECLARE @user_id INT, @support_id INT, @id INT
    BEGIN TRANSACTION;  
    INSERT INTO Checkers.dbo.[User]([nick],[login],[password],[email],[user_type_id])
    VALUES (@nick,@login,@password,@email,(SELECT id FROM [UserType] WHERE [user_type_name]=@user_type_name));
    SET @user_id = @@IDENTITY;
    IF @user_type_name!=N'Support'
        BEGIN
        EXEC @id = [SP_GetUserTypeByName] N'Support'
        SET @support_id = (SELECT TOP 1 id FROM Checkers.dbo.[User] WHERE [user_type_id] = @id);
        EXEC [SP_CreateFriendship] @user_id, @support_id
        END
    SET @id = (SELECT TOP 1 [item_id] FROM [Animation]);
    EXEC [SP_AddUserItem] @user_id, @id;
    SET @id = (SELECT TOP 1 [item_id] FROM [CheckersSkin]);
    EXEC [SP_AddUserItem] @user_id, @id;
    SET @id = (SELECT TOP 1 [item_id] FROM [Picture]);
    EXEC [SP_AddUserItem] @user_id, @id;
    COMMIT;
    RETURN @user_id
END

GO
CREATE PROCEDURE [SP_SelectUser] @id INT
AS
BEGIN
    SELECT * FROM Checkers.dbo.[User] WHERE id=@id
END

GO 
CREATE PROCEDURE [SP_SelectUserItem] @id INT, @item_type NVARCHAR(MAX)
AS
BEGIN
    DECLARE @item_type_id INT
    EXEC @item_type_id = [SP_GetItemTypeByName] @item_type
    SELECT I.id FROM Checkers.dbo.[UserItem] AS UI 
    JOIN [Item] AS I ON UI.[item_id]=I.id  WHERE I.[item_type_id]=@item_type_id; 
END

GO  
CREATE PROCEDURE [SP_UpdateUserNick] @login NVARCHAR(300), @password NVARCHAR(MAX), @nick NVARCHAR(MAX)
AS
BEGIN
    EXEC [SP_UpdateUserActivity] @login,@password;
    UPDATE Checkers.dbo.[User] SET [nick]=@nick WHERE [login]=@login AND [password]=@password;
END

GO  
CREATE PROCEDURE [SP_UpdateUserLogin] @login NVARCHAR(300), @password NVARCHAR(MAX), @new_login NVARCHAR(300)
AS
BEGIN
    EXEC [SP_UpdateUserActivity] @login,@password;
    UPDATE Checkers.dbo.[User] SET [login]=@new_login WHERE [login]=@login AND [password]=@password;
END

GO  
CREATE PROCEDURE [SP_UpdateUserPassword] @login NVARCHAR(300), @password NVARCHAR(MAX), @new_password NVARCHAR(MAX)
AS
BEGIN
    EXEC [SP_UpdateUserActivity] @login,@password;
    UPDATE Checkers.dbo.[User] SET [password]=@new_password WHERE [login]=@login AND [password]=@password;
END

GO  
CREATE PROCEDURE [SP_UpdateUserEmail] @login NVARCHAR(300), @password NVARCHAR(MAX), @email NVARCHAR(MAX)
AS
BEGIN
    EXEC [SP_UpdateUserActivity] @login,@password;
    UPDATE Checkers.dbo.[User] SET [email]=@email WHERE [login]=@login AND [password]=@password;
END

GO
CREATE PROCEDURE [SP_SelectUserByNick] @nick NVARCHAR(MAX)
AS
BEGIN
    SELECT * FROM Checkers.dbo.[User] WHERE [nick] LIKE @nick
END

GO
CREATE PROCEDURE [SP_Authenticate] @login NVARCHAR(300), @password NVARCHAR(MAX)
AS
BEGIN
    DECLARE @id INT
    EXEC [SP_UpdateUserActivity] @login,@password;
    SET @id = (SELECT id FROM Checkers.dbo.[User] WHERE [login]=@login AND [password]=@password);
    IF @id IS NOT NULL
        RETURN @id;
    ELSE
        RETURN -1;
END


GO
CREATE PROCEDURE [SP_UpdateUserAnimation] @login NVARCHAR(300), @password NVARCHAR(MAX), @id INT
AS
BEGIN
    DECLARE @user_id INT;
    EXEC @user_id = [SP_Authenticate] @login, @password
    EXEC [SP_UpdateUserActivity] @login,@password;
    IF @id IN (SELECT [item_id] FROM Checkers.dbo.[UserItem] WHERE [user_id]=@user_id)
        UPDATE Checkers.dbo.[User] SET [animation_id]=@id WHERE [login]=@login AND [password]=@password;
END

GO
CREATE PROCEDURE [SP_UpdateUserCheckers] @login NVARCHAR(300), @password NVARCHAR(MAX), @id INT
AS
BEGIN
    DECLARE @user_id INT;
    EXEC @user_id = [SP_Authenticate] @login, @password
    EXEC [SP_UpdateUserActivity] @login,@password;
    IF @id IN (SELECT [item_id] FROM Checkers.dbo.[UserItem] WHERE [user_id]=@user_id)
        UPDATE Checkers.dbo.[User] SET [checkers_id]=@id WHERE [login]=@login AND [password]=@password;
END

GO
CREATE PROCEDURE [SP_CreateResource] @resource_extension NVARCHAR(MAX), @resource_bytes VARBINARY(MAX)
AS
BEGIN
    INSERT INTO Checkers.dbo.[ResourceTable]([resource_extension],[resource_bytes]) 
    VALUES (@resource_extension,@resource_bytes);
    RETURN @@IDENTITY;
END

GO
CREATE PROCEDURE [SP_CreateResourceFromFile] @path NVARCHAR(MAX)
AS
BEGIN
    DECLARE @resource_extension NVARCHAR(MAX), @id INT, @sql NVARCHAR(MAX), @resource_bytes VARBINARY(MAX)
    SET @resource_extension = (SELECT RIGHT(@path, CHARINDEX('.', REVERSE(@path) + '.') - 1));
    SET @sql = FORMATMESSAGE ( 'SELECT @resource_bytes = BulkColumn FROM OPENROWSET ( BULK ''%s'', SINGLE_BLOB ) AS x;', @path );
    EXEC sp_executesql @sql, N'@resource_bytes VARBINARY(MAX) OUT', @resource_bytes = @resource_bytes OUT;
    EXEC  @id=[SP_CreateResource] @resource_extension ,@resource_bytes
    RETURN @id
END

GO
CREATE PROCEDURE [SP_CreateItem]
@item_type NVARCHAR(MAX),
@item_name NVARCHAR(300),
@detail NVARCHAR(MAX),
@path NVARCHAR(MAX),
@price INT
AS
BEGIN
    DECLARE @type_id INT, @id INT
    BEGIN TRANSACTION;  
    SET @type_id = (SELECT id FROM [ItemType] WHERE [item_type_name]=@item_type);
    EXEC @id = [SP_CreateResourceFromFile] @path;
    INSERT INTO Checkers.dbo.[Item]([item_name],[item_type_id],[detail],[resource_id],[price])
    VALUES (@item_name,@type_id,@detail,@id,@price);
    COMMIT;
    RETURN @@IDENTITY;
END

GO
CREATE PROCEDURE [SP_CreatePost] @login NVARCHAR(300), @password NVARCHAR(MAX),
@post_title NVARCHAR(300), @post_content NVARCHAR(MAX),@post_picture_id INT
AS
BEGIN
    BEGIN TRANSACTION;
    DECLARE @id INT, @user_id INT, @chat_name NVARCHAR(300)
    EXEC @user_id = [SP_Authenticate] @login, @password;
    IF @user_id!=-1
        BEGIN
        SET @chat_name = N'Chat ' + @post_title;
        EXEC @id = [SP_CreateChat] @chat_name, N'Public';
        IF @id IS NOT NULL
            BEGIN
            INSERT INTO Checkers.dbo.[Post]([post_content],[post_title],[post_picture_id],[chat_id],[post_author_id])
            VALUES (@post_content,@post_title,@post_picture_id,@id,@user_id);
            COMMIT;
            RETURN @@IDENTITY;
            END
        ROLLBACK;
        END
    ELSE
        BEGIN
        ROLLBACK;
        RETURN -1;
        END
    
END

GO
CREATE PROCEDURE [SP_CreateArticle] @login NVARCHAR(300), @password NVARCHAR(MAX),
@article_title NVARCHAR(300), @abstract NVARCHAR(MAX), 
@article_content NVARCHAR(MAX), @article_picture_id INT
AS
BEGIN
    BEGIN TRANSACTION;
    DECLARE @id INT, @user_id INT, @post NVARCHAR(MAX), @access INT
    SET @post = N'Discussion ' + @article_title;
    EXEC @user_id=[SP_Authenticate] @login, @password;
    IF @user_id!=-1
        BEGIN
        EXEC @access = [SP_CheckAccess] @user_id, N'Editor';
        IF @access=1
            BEGIN
            EXEC @id=[SP_CreatePost] @login,@password,@article_title,@post, @article_picture_id;
            IF @id!=-1
                BEGIN
                INSERT INTO Checkers.dbo.[Article]([article_title],[article_content],[article_abstract],[article_author_id],[article_picture_id],[article_post_id])
                VALUES  (@article_title,@article_content,@abstract,@user_id,@article_picture_id,@id);
                COMMIT;
                RETURN @@IDENTITY
                END
            END
        ROLLBACK;
        END
    ELSE
        BEGIN
        ROLLBACK;
        RETURN -1;
        END
    
END

GO
CREATE PROCEDURE [SP_SelectArticleProc] @id INT
AS
BEGIN
    SELECT * FROM Checkers.dbo.[Article] WHERE id=@id;
END

GO
CREATE PROCEDURE [SP_SelectNews]
AS
BEGIN
    SELECT * FROM Checkers.dbo.[Article];
END


GO
CREATE PROCEDURE [SP_SelectPostInfo] @id INT
AS
BEGIN
    SELECT id,[post_title],[post_picture_id] FROM Checkers.dbo.[Post] WHERE id=@id;
END

GO
CREATE PROCEDURE [SP_SelectPost] @id INT
AS
BEGIN
    SELECT * FROM Checkers.dbo.[Post] WHERE id=@id;
END

GO
CREATE PROCEDURE [SP_SelectPosts]
AS
BEGIN
    SELECT id,[post_title],[post_picture_id],[post_content] FROM Checkers.dbo.[Post];
END

GO
CREATE PROCEDURE [SP_SendMessage] @login NVARCHAR(300),@password NVARCHAR(MAX),
@chat_id INT,@message_content NVARCHAR(MAX)
AS
BEGIN
    DECLARE @user_id INT, @id INT
    EXEC @user_id = [SP_Authenticate] @login,@password;
    IF @user_id!=-1
        BEGIN
        EXEC @id = [SP_GetChatTypeByName] N'Public'
        IF @chat_id IN 
        (SELECT [chat_id] FROM [Friendship] WHERE [user1_id]=@user_id) OR 
        (SELECT [chat_type_id] FROM [Chat] WHERE id=@chat_id) = @id
            INSERT INTO Checkers.dbo.[Message]([chat_id],[user_id],[message_content])
            VALUES (@chat_id,@user_id,@message_content); 
        END
END

GO
CREATE PROCEDURE [SP_SelectMessage] @login NVARCHAR(300), @password NVARCHAR(MAX), @chat_id INT
AS
BEGIN
    DECLARE @user_id INT
    EXEC @user_id = [SP_Authenticate] @login,@password;
    IF @user_id!=-1
            SELECT * FROM Checkers.dbo.[Message] WHERE [chat_id]=@chat_id;
END

GO
CREATE PROCEDURE [SP_CreateAnimation]
@item_name NVARCHAR(300),
@detail NVARCHAR(MAX),
@path NVARCHAR(MAX),
@price INT
AS
BEGIN
    DECLARE @id INT
    EXEC @id = [SP_CreateItem] N'Animation', @item_name,@detail,@path,@price
    INSERT INTO Checkers.dbo.[Animation]([item_id]) VALUES(@id); 
    RETURN @id
END

GO
CREATE PROCEDURE [SP_CreateAchievement]
@item_name NVARCHAR(300),
@detail NVARCHAR(MAX),
@path NVARCHAR(MAX),
@price INT
AS
BEGIN
    DECLARE @id INT
    EXEC @id = [SP_CreateItem] N'Achievement' ,@item_name,@detail,@path,@price
    INSERT INTO Checkers.dbo.[Achievement]([item_id]) VALUES(@id); 
    RETURN @id
END

GO
CREATE PROCEDURE [SP_CreateCheckresSkin]
@item_name NVARCHAR(300),
@detail NVARCHAR(MAX),
@path NVARCHAR(MAX),
@price INT
AS
BEGIN
    DECLARE @id INT
    EXEC @id = [SP_CreateItem] N'CheckersSkin' ,@item_name,@detail,@path,@price
    INSERT INTO Checkers.dbo.[CheckersSkin]([item_id]) VALUES(@id); 
    RETURN @id
END

GO
CREATE PROCEDURE [SP_CreatePicture]
@item_name NVARCHAR(300),
@detail NVARCHAR(MAX),
@path NVARCHAR(MAX),
@price INT
AS
BEGIN
    DECLARE @id INT
    EXEC @id = [SP_CreateItem] N'Picture' ,@item_name,@detail,@path,@price
    INSERT INTO Checkers.dbo.[Picture]([item_id]) VALUES(@id); 
    RETURN @id
END

GO
CREATE PROCEDURE [SP_CreateLootBox]
@item_name NVARCHAR(300),
@detail NVARCHAR(MAX),
@path NVARCHAR(MAX),
@price INT
AS
BEGIN
    DECLARE @id INT
    EXEC @id = [SP_CreateItem] N'LootBox' ,@item_name,@detail,@path,@price
    INSERT INTO Checkers.dbo.[LootBox]([item_id]) VALUES(@id); 
    RETURN @id
END

--Article
GO
CREATE PROCEDURE [SP_UpdateArticleTitle] @login NVARCHAR(300), @password NVARCHAR(MAX),
@id INT, @article_title NVARCHAR(300)
AS
BEGIN
    DECLARE @access INT, @user_id INT
    EXEC @user_id = [SP_Authenticate] @login, @password;
    EXEC @access = [SP_CheckAccess] @user_id, N'Editor'
    IF @access=1
        UPDATE Checkers.dbo.[Article] SET [article_title] = @article_title WHERE id=@id
END

GO
CREATE PROCEDURE [SP_UpdateArticleAbstract] @login NVARCHAR(300), @password NVARCHAR(MAX),
@id INT, @abstract NVARCHAR(MAX)
AS
BEGIN
    DECLARE @access INT, @user_id INT
    EXEC @user_id = [SP_Authenticate] @login, @password;
    EXEC @access = [SP_CheckAccess] @user_id, N'Editor'
    IF @access=1
        UPDATE Checkers.dbo.[Article] SET [article_abstract] = @abstract WHERE id=@id
END

GO
CREATE PROCEDURE [SP_UpdateArticleContent] @login NVARCHAR(300), @password NVARCHAR(MAX),
@id INT, @article_content NVARCHAR(MAX)
AS
BEGIN
    DECLARE @access INT, @user_id INT
    EXEC @user_id = [SP_Authenticate] @login, @password;
    EXEC @access = [SP_CheckAccess] @user_id, N'Editor'
    IF @access=1
        UPDATE Checkers.dbo.[Article] SET [article_content] = @article_content WHERE id=@id
END

GO
CREATE PROCEDURE [SP_UpdateArticlePictureId] @login NVARCHAR(300), @password NVARCHAR(MAX),
@id INT, @article_picture_id INT
AS
BEGIN
    DECLARE @access INT, @user_id INT
    EXEC @user_id = [SP_Authenticate] @login, @password;
    EXEC @access = [SP_CheckAccess] @user_id, N'Editor'
    IF @access=1
        UPDATE Checkers.dbo.[Article] SET [article_picture_id] = @article_picture_id WHERE id=@id
END

--Post
GO
CREATE PROCEDURE [SP_UpdateArticlePostId] @login NVARCHAR(300), @password NVARCHAR(MAX),
@id INT, @article_post_id INT
AS
BEGIN
    DECLARE @access INT, @user_id INT, @post_author_id INT
    SET @post_author_id = (SELECT [post_author_id] FROM Checkers.dbo.[Post] WHERE id=@id);
    EXEC @user_id = [SP_Authenticate] @login, @password
    EXEC @access = [SP_CheckAccess] @user_id, N'Moderator'
    IF @access=1
        UPDATE Checkers.dbo.[Article] SET [article_post_id] = @article_post_id WHERE id=@id
END


GO
CREATE PROCEDURE [SP_UpdatePostTitle] @login NVARCHAR(300), @password NVARCHAR(MAX),
@id INT, @post_title NVARCHAR(300)
AS
BEGIN
    DECLARE @access INT, @user_id INT, @post_author_id INT
    SET @post_author_id = (SELECT [post_author_id] FROM Checkers.dbo.[Post] WHERE id=@id);
    EXEC @user_id = [SP_Authenticate] @login, @password
    EXEC @access = [SP_CheckAccess] @user_id, N'Moderator'
    IF @post_author_id=@user_id OR @access=1
        UPDATE Checkers.dbo.[Post] SET [post_title]=@post_title WHERE id=@id
END

GO
CREATE PROCEDURE [SP_UpdatePostContent] @login NVARCHAR(300), @password NVARCHAR(MAX),
@id INT, @post_content NVARCHAR(MAX)
AS
BEGIN
    DECLARE @access INT, @user_id INT, @post_author_id INT
    SET @post_author_id = (SELECT [post_author_id] FROM Checkers.dbo.[Post] WHERE id=@id);
    EXEC @user_id = [SP_Authenticate] @login, @password
    EXEC @access = [SP_CheckAccess] @user_id, N'Moderator'
    IF @post_author_id=@user_id OR @access=1
        UPDATE Checkers.dbo.[Post] SET [post_content]=@post_content WHERE id=@id
END

GO
CREATE PROCEDURE [SP_UpdatePostPictureId] @login NVARCHAR(300), @password NVARCHAR(MAX), 
@id INT, @post_picture_id INT
AS
BEGIN
    DECLARE @access INT, @user_id INT, @post_author_id INT
    SET @post_author_id = (SELECT [post_author_id] FROM Checkers.dbo.[Post] WHERE id=@id);
    EXEC @user_id = [SP_Authenticate] @login, @password
    EXEC @access = [SP_CheckAccess] @user_id, N'Moderator'
    IF @post_author_id=@user_id OR @access=1
        UPDATE Checkers.dbo.[Post] SET [post_picture_id]=@post_picture_id WHERE id=@id
END

GO 
CREATE PROCEDURE [SP_SelectTopPlayers]
AS
BEGIN
    WITH OrderedPlayers AS
    (SELECT ROW_NUMBER() OVER(ORDER BY [social_credit] DESC) AS RowNumber, U.*
    FROM Checkers.dbo.[User] AS U) 
    SELECT * FROM OrderedPlayers
    WHERE RowNumber < 2
    ORDER BY [social_credit] DESC;
END

GO
CREATE PROCEDURE [SP_SelectTopPlayersAuth] @login NVARCHAR(300), @password NVARCHAR(MAX)
AS
BEGIN
    DECLARE @id INT
    EXEC @id = [SP_Authenticate] @login, @password;
    WITH OrderedPlayers AS
    (SELECT ROW_NUMBER() OVER(ORDER BY [social_credit] DESC) AS RowNumber, U.*
    FROM Checkers.dbo.[User] AS U)
    SELECT * FROM OrderedPlayers
    WHERE RowNumber < 2 OR id=@id
    ORDER BY [social_credit] DESC;
END

GO
CREATE PROCEDURE [SP_SelectAllUserItem] @id INT
AS
BEGIN
    SELECT [item_id] FROM Checkers.dbo.[UserItem] WHERE [user_id]=@id
END

GO
CREATE PROCEDURE [SP_SelectFriendChatId] @user1_id INT, @user2_id INT
AS
BEGIN
    RETURN (SELECT [chat_id] FROM Checkers.dbo.[Friendship] WHERE [user1_id] = @user1_id AND [user2_id] = @user2_id)
END

GO
CREATE PROCEDURE [SP_SelectUserFrienship] @id INT
AS
BEGIN
    SELECT * FROM Checkers.dbo.[Friendship] WHERE [user1_id] = @id
END

GO
CREATE PROCEDURE [SP_UserBuyItem] @login NVARCHAR(300), @password NVARCHAR(MAX), @id INT
AS
BEGIN
    DECLARE @user_id INT, @price INT, @currency INT
    EXEC @user_id = [SP_Authenticate] @login,@password
    SET @price = (SELECT [price] FROM Checkers.dbo.[Item] WHERE id=@id);
    SET @currency = (SELECT [currency] FROM Checkers.dbo.[User] WHERE id=@user_id);
    IF @currency>=@price
        BEGIN
        INSERT INTO Checkers.dbo.[UserItem]([user_id],[item_id]) VALUES (@user_id,@id);
        UPDATE Checkers.dbo.[User] SET [currency] = (@currency-@price) WHERE id=@user_id;
        END
END

GO
CREATE PROCEDURE [SP_UpdateUserPictureProc] @login NVARCHAR(300), @password NVARCHAR(MAX), @id INT
AS
BEGIN
    DECLARE @user_id INT
    EXEC @user_id = [SP_Authenticate] @login, @password
    UPDATE Checkers.dbo.[User] SET [picture_id] = @id WHERE id=@user_id
END


GO
INSERT INTO [ItemType]([item_type_name]) 
VALUES (N'Picture'),
(N'Achievement'),
(N'CheckersSkin'),
(N'Animation'),
(N'LootBox');

INSERT INTO [UserType]([user_type_name]) VALUES 
(N'Player'),
(N'Editor'),
(N'Moderator'),
(N'Support'),
(N'Admin')

INSERT INTO [ChatType]([chat_type_name]) VALUES
(N'Public'),
(N'Private');

INSERT INTO [FriendshipState](friendship_state_name) VALUES
(N'Accepted'),
(N'Canceled'),
(N'Waiting');

GO
CREATE VIEW [UserItemExtended] AS
SELECT UI.[user_id],U.[nick], UI.[item_id], I.[updated], I.[detail], R.[resource_extension], I.[item_name], 
        I.[price], IT.[item_type_name]
FROM [UserItem] AS UI
JOIN [Item] AS I ON UI.[item_id] = I.id
JOIN [ItemType] AS IT ON IT.id = I.[item_type_id]
JOIN [User] AS U ON UI.[user_id] = U.id
JOIN [ResourceTable] AS R ON R.id=I.[resource_id]

GO
Use Checkers
EXEC [SP_CreateChat] N'Common chat',N'Public'

GO
EXEC [SP_CreatePicture] N'Гуль', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\1.jpg', 0
EXEC [SP_CreatePicture] N'Олд', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\2.jpg', 0
EXEC [SP_CreatePicture] N'Китайский парень', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\3.jpg', 0
EXEC [SP_CreatePicture] N'Жирафыч', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\4.jpg', 0
EXEC [SP_CreatePicture] N'ПрессФ', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\5.jpg', 0
EXEC [SP_CreatePicture] N'billy', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\6.jpg', 0
EXEC [SP_CreatePicture] N'easybot', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\20.png', 0
EXEC [SP_CreatePicture] N'mediumbot', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\21.png', 0
EXEC [SP_CreatePicture] N'hardbot', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\22.jpg', 0
EXEC [SP_CreatePicture] N'moder', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\23.jpg', 0
EXEC [SP_CreatePicture] N'TPagent', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\24.jpg', 0
EXEC [SP_CreatePicture] N'redactor', N'avatar for profile', N'W:\IT\C#\Checkers\DatabaseStartup\Img\25.jpg', 0
GO
EXEC [SP_CreateCheckresSkin] N'Вклтетку', N'skin', N'W:\IT\C#\Checkers\DatabaseStartup\Img\26.png', 50
EXEC [SP_CreateCheckresSkin] N'Каппучино', N'skin', N'W:\IT\C#\Checkers\DatabaseStartup\Img\27.jpg', 50
EXEC [SP_CreateCheckresSkin] N'BLM', N'skin', N'W:\IT\C#\Checkers\DatabaseStartup\Img\28.png', 100
EXEC [SP_CreateCheckresSkin] N'Minimalism', N'skin', N'W:\IT\C#\Checkers\DatabaseStartup\Img\29.png', 20
GO
EXEC [SP_CreateAnimation] N'Explosion', N'animation skin', N'W:\IT\C#\Checkers\DatabaseStartup\Img\30.png', 200
EXEC [SP_CreateAnimation] N'BlackHole', N'animation skin', N'W:\IT\C#\Checkers\DatabaseStartup\Img\31.png', 300
GO
EXEC [SP_CreateAchievement] N'Первые шаги', N'Сыграть первый матч', N'W:\IT\C#\Checkers\DatabaseStartup\Img\7.JPG', 0
EXEC [SP_CreateAchievement] N'Путь любителя', N'Сыграть 10 матчей', N'W:\IT\C#\Checkers\DatabaseStartup\Img\8.JPG', 0
EXEC [SP_CreateAchievement] N'Уже почти ветеран', N'Сыграть 100 матчей', N'W:\IT\C#\Checkers\DatabaseStartup\Img\9.JPG', 0
EXEC [SP_CreateAchievement] N'Вкус победы', N'Выиграть первый матч', N'W:\IT\C#\Checkers\DatabaseStartup\Img\10.png', 0
EXEC [SP_CreateAchievement] N'А ты не плох!', N'Выиграть 10 раз', N'W:\IT\C#\Checkers\DatabaseStartup\Img\11.png', 0
EXEC [SP_CreateAchievement] N'Почти Ян Непомнящий', N'Выиграть 100 раз', N'W:\IT\C#\Checkers\DatabaseStartup\Img\12.jpg', 0
EXEC [SP_CreateAchievement] N'Легкие деньги', N'Купить что-либо в игровом магазине', N'W:\IT\C#\Checkers\DatabaseStartup\Img\13.jpg', 0
EXEC [SP_CreateAchievement] N'Не каждый так сможет', N'Проиграть за 2 минуты', N'W:\IT\C#\Checkers\DatabaseStartup\Img\19.jpg', 0
GO
EXEC [SP_CreateLootBox] N'BlackOrNothing', N'"BlackHole,каппучино,minimalism"', N'W:\IT\C#\Checkers\DatabaseStartup\Img\32.png', 60
EXEC [SP_CreateLootBox] N'TypicalLootBox', N'"Explosion,BLM,Вклетку"', N'W:\IT\C#\Checkers\DatabaseStartup\Img\33.png', 50
GO
EXEC [SP_CreateUser] N'problem solver', N'tpagent', N'tpagent', N'CheckersTP@gmail.com', N'Support'
EXEC [SP_CreateUser] N'Easy Bot', N'Easy', N'EasyBot', N'Easybot@gmail.com', N'Player'
EXEC [SP_CreateUser] N'Medium Bot', N'Medium', N'MediumBot', N'Mediumbot@gmail.com', N'Player'
EXEC [SP_CreateUser] N'Hard Bot', N'Hard', N'HardBot', N'Hardbot@gmail.com', N'Player'
EXEC [SP_CreateUser] N'ZXC(dead inside)', N'deadinside', N'zxcqwe123', N'ghoulZXC@gmail.com', N'Player'
EXEC [SP_CreateUser] N'roflanHmm', N'roflan', N'Rofl123', N'roflan12322@gmail.com', N'Player'
EXEC [SP_CreateUser] N'CHINATHEBEST', N'CHINANUMBERONE', N'MaoTseTung', N'thebest@gmail.com', N'Player'
EXEC [SP_CreateUser] N'Hailrake', N'Arthas', N'55juse55', N'spiteful@gmail.com', N'Player'
EXEC [SP_CreateUser] N'AI_avenger', N'skynet', N'papich123', N'papichdadaya@gmail.com', N'Player'
EXEC [SP_CreateUser] N'BillyReady', N'billyUp', N'throwmesomenumbers', N'Herrington@gmail.com', N'Player'
EXEC [SP_CreateUser] N'BOSS of the forum', N'boss', N'bossofthegym', N'bigboss@gmail.com', N'Moderator'
EXEC [SP_CreateUser] N'news editor', N'redactor', N'redactor', N'CheckersNews@gmail.com', N'Editor'
GO
DECLARE @id1 INT, @id2 INT
SET @id1 = (SELECT id FROM [User] WHERE [login] = N'Arthas');
SET @id2 = (SELECT id FROM [User] WHERE [login] = N'skynet');
EXEC [SP_CreateFriendship] @id1, @id2
SET @id1 = (SELECT id FROM [User] WHERE [login] = N'billyUp');
SET @id2 = (SELECT id FROM [User] WHERE [login] = N'roflan');
EXEC [SP_CreateFriendship] @id1, @id2
GO
DECLARE @id INT, @id1 INT, @id2 INT
SET @id1 = (SELECT id FROM [User] WHERE [login]=N'skynet');
SET @id2 = (SELECT id FROM [User] WHERE [login]=N'Arthas');
SET @id = (SELECT [chat_id] FROM [Friendship] WHERE [user1_id]=@id1 AND [user2_id]=@id2)
EXEC [SP_SendMessage] N'skynet', N'papich123', @id, N'видел там ранкед завезли? попытаешься и там стать лучшим в мире?'
SET @id1 = (SELECT id FROM [User] WHERE [login]=N'Arthas');
SET @id2 = (SELECT id FROM [User] WHERE [login]=N'skynet');
SET @id = (SELECT [chat_id] FROM [Friendship] WHERE [user1_id]=@id1 AND [user2_id]=@id2)
EXEC [SP_SendMessage] N'Arthas', N'55juse55', @id, N'бтв офк енивей. Я как всегда стану лучшичем с переход на шашкичи.'
SET @id1 = (SELECT id FROM [User] WHERE [login]=N'skynet');
SET @id2 = (SELECT id FROM [User] WHERE [login]=N'Arthas');
SET @id = (SELECT [chat_id] FROM [Friendship] WHERE [user1_id]=@id1 AND [user2_id]=@id2)
EXEC [SP_SendMessage] N'skynet', N'papich123', @id, N'понял тебя. ну что ж удачи'
SET @id1 = (SELECT id FROM [User] WHERE [login]=N'roflan');
SET @id2 = (SELECT id FROM [User] WHERE [login]=N'billyUp');
SET @id = (SELECT [chat_id] FROM [Friendship] WHERE [user1_id]=@id1 AND [user2_id]=@id2)
EXEC [SP_SendMessage] N'roflan', N'Rofl123', @id, N'эй дружок-пирожок тобою выбрана неправильная дверь'
SET @id1 = (SELECT id FROM [User] WHERE [login]=N'billyUp');
SET @id2 = (SELECT id FROM [User] WHERE [login]=N'roflan');
SET @id = (SELECT [chat_id] FROM [Friendship] WHERE [user1_id]=@id1 AND [user2_id]=@id2)
EXEC [SP_SendMessage] N'billyUp', N'throwmesomenumbers', @id, N'"oh, shit, I,m sorry"'
SET @id1 = (SELECT id FROM [User] WHERE [login]=N'roflan');
SET @id2 = (SELECT id FROM [User] WHERE [login]=N'billyUp');
SET @id = (SELECT [chat_id] FROM [Friendship] WHERE [user1_id]=@id1 AND [user2_id]=@id2)
EXEC [SP_SendMessage] N'roflan', N'Rofl123', @id, N'клуб кожевенного ремесла 2 блока вниз'
GO
DECLARE @id INT
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'easybot'))
EXEC [SP_UpdateUserPictureProc] N'Easy', N'EasyBot', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'mediumbot'))
EXEC [SP_UpdateUserPictureProc] N'Medium', N'MediumBot', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'hardbot'))
EXEC [SP_UpdateUserPictureProc] N'Hard', N'HardBot', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'Гуль'))
EXEC [SP_UpdateUserPictureProc] N'deadinside', N'zxcqwe123', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'Олд'))
EXEC [SP_UpdateUserPictureProc] N'roflan', N'Rofl123', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'Китайский парень'))
EXEC [SP_UpdateUserPictureProc] N'CHINANUMBERONE', N'MaoTseTung', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'Жирафыч'))
EXEC [SP_UpdateUserPictureProc] N'Arthas', N'55juse55', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'ПрессФ'))
EXEC [SP_UpdateUserPictureProc] N'skynet', N'papich123', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'billy'))
EXEC [SP_UpdateUserPictureProc] N'billyUp', N'throwmesomenumbers', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'moder'))
EXEC [SP_UpdateUserPictureProc] N'boss', N'bossofthegym', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'redactor'))
EXEC [SP_UpdateUserPictureProc] N'redactor', N'redactor', @id
SET @id = (SELECT id FROM [Picture] WHERE [item_id] = (SELECT id FROM [Item] WHERE [item_name]=N'TPagent'))
EXEC [SP_UpdateUserPictureProc] N'tpagent', N'tpagent', @id
GO
DECLARE @id INT
EXEC @id = [SP_CreateResourceFromFile] N'W:\IT\C#\Checkers\DatabaseStartup\Img\14.jpg'
EXEC [SP_CreateArticle] N'redactor', N'redactor', N'Рождение новой эры шашек', N'Мы добавили рейтинговую систему в игру', N'"Для всех любителей шашек соревноваться теперь будет еще интереснее. В игру добавлена рейтинговая систма. Теперь у нас есть рейтинговый режим и рейтинговые таблицыы лидеров, чтобы наша игра могла стать еще и киберспортивной дисциплиной"', @id
EXEC @id = [SP_CreateResourceFromFile] N'W:\IT\C#\Checkers\DatabaseStartup\Img\15.jpg'
EXEC [SP_CreateArticle] N'redactor', N'redactor', N'Начинаем с самого важного', N'В игру добавляются Лутбоксы и Микротранзакции', N'"Зная любовь всех игроков к виртуальным текстуркам а также азарт всех игроков в шашки, мы добавляем в игру виртуальный магазин со скинами и лутбоксами. Желаем удачи выбить самый нужный вам скин"', @id
EXEC @id = [SP_CreateResourceFromFile] N'W:\IT\C#\Checkers\DatabaseStartup\Img\16.jpg'
EXEC [SP_CreateArticle] N'redactor', N'redactor', N'Хорошему форуму -  нужен контроль', N'Ищем ответственного человека на звание модератора', N'"Сидишь сутками напролет на форуме и нет личной жизни? Умеешь кошмарить работяг в чате? Хочешь приблизиться к верхушке создателей шашек? А может просто ответственный человек, которому не безразлична судьба нашего форума? В любом случае напишите мы вас ждем!"', @id
GO
DECLARE @id INT
SET @id = (SELECT [chat_id] FROM [Post] WHERE id = (SELECT [article_post_id] FROM [Article] WHERE [article_title]=N'Рождение новой эры шашек'));
EXEC [SP_SendMessage] N'Arthas', N'55juse55', @id, N'Наконец-то шанс стать топ 1 ладдера'
SET @id = (SELECT [chat_id] FROM [Post] WHERE id = (SELECT [article_post_id] FROM [Article] WHERE [article_title]=N'Начинаем с самого важного'));
EXEC [SP_SendMessage] N'CHINANUMBERONE', N'MaoTseTung', @id, N'минус 500 social credit'
SET @id = (SELECT [chat_id] FROM [Post] WHERE id = (SELECT [article_post_id] FROM [Article] WHERE [article_title]=N'Начинаем с самого важного'));
EXEC [SP_SendMessage] N'roflan', N'Rofl123', @id, N'лутбоксы ммм кайф пойду откладывать деньги со столовок'
SET @id = (SELECT [chat_id] FROM [Post] WHERE id = (SELECT [article_post_id] FROM [Article] WHERE [article_title]=N'Хорошему форуму -  нужен контроль'));
EXEC [SP_SendMessage] N'skynet', N'papich123', @id, N'Главное чтобы модером не сделали какого-нибудь Hailrake или гуля...'
SET @id = (SELECT [chat_id] FROM [Post] WHERE id = (SELECT [article_post_id] FROM [Article] WHERE [article_title]=N'Начинаем с самого важного'));
EXEC [SP_SendMessage] N'billyUp', N'throwmesomenumbers', @id, N'"Oh, YEA"'
SET @id = (SELECT [chat_id] FROM [Post] WHERE id = (SELECT [article_post_id] FROM [Article] WHERE [article_title]=N'Хорошему форуму -  нужен контроль'));
EXEC [SP_SendMessage] N'skynet', N'papich123', @id, N'модератор <офигел> в чате полный беспредел спам и деградация найс тут модерация'
GO
DECLARE @id INT
EXEC @id = [SP_CreateResourceFromFile] N'W:\IT\C#\Checkers\DatabaseStartup\Img\17.jpg'
EXEC [SP_CreatePost] N'deadinside', N'zxcqwe123', N'Кодекс гулей', N'"Гуль - не канеки кен на аве и не зитракс в подписках. гуль - это идеология, это культура. либо ты не называешь себя гулем, либо следуешь кодексу. 1. гули нормальные, ты не токсик свинья и позер, ты нормальный, порофли с тимой если хочешь, хорошо общайся со своими ребятами если нужно, уважай тех, кто действительно этого достоин. 2. ЧСВ - положительное качество, ты сильнее и лучше всех, НО ты должен это оправдывать 3. будь верен своим идеалам. тебе не обязательно иметь гуля на аве, к чему это, поставь лил пипа или гослинга если хочешь, держись идеалов, но твой ник не должен быть отвратительным 4. никогда не пиши заглавными буквами. пиши размеренно и по правилам, не будь быдлом, ведь это низко 5. запомни в последний раз, ты не клоун, не позер и не нытик. ты - гуль, не позорься и не строй из себя не пойми что 6. соблюдай все правила, либо ты не гуль"', @id
EXEC @id = [SP_CreateResourceFromFile] N'W:\IT\C#\Checkers\DatabaseStartup\Img\18.JPG'
EXEC [SP_CreatePost] N'CHINATHEBEST', N'CHINANUMBERONE', N'Проклятые лутбоксы!', N'ЛУТБОКСЫ! ОНИ уже среди нас ! ЭТО только портить игру! Партия недовольна. лишить разработчиков 500 социального рейтинга и тарелки с рисом', @id
GO
DECLARE @id INT
SET @id = (SELECT [chat_id] FROM [Post] WHERE [post_title]=N'Кодекс гулей');
EXEC [SP_SendMessage] N'Arthas', N'55juse55', @id, N'"зачем ты что-то пишешь, зачем ты что-то дышишь"'
SET @id = (SELECT [chat_id] FROM [Post] WHERE [post_title]=N'Кодекс гулей');
EXEC [SP_SendMessage] N'roflan', N'Rofl123', @id, N'"так ты сам позер, а не гуль"'
SET @id = (SELECT [chat_id] FROM [Post] WHERE [post_title]=N'Кодекс гулей');
EXEC [SP_SendMessage] N'CHINANUMBERONE', N'MaoTseTung', @id, N'минус 1000 social credit'
SET @id = (SELECT [chat_id] FROM [Post] WHERE [post_title]=N'Проклятые лутбоксы!');
EXEC [SP_SendMessage] N'skynet', N'papich123', @id, N'поддерживаю скажем нет продажрности разрабов'
SET @id = (SELECT [chat_id] FROM [Post] WHERE [post_title]=N'Проклятые лутбоксы!');
EXEC [SP_SendMessage] N'Arthas', N'55juse55', @id, N'"да им, как и мне только на еду хватает. Не осуждайте если не знаете"'
SET @id = (SELECT [chat_id] FROM [Post] WHERE [post_title]=N'Кодекс гулей');
EXEC [SP_SendMessage] N'skynet', N'papich123', @id, N'"Дружочек, ты видимо не понял с кем общаешься. Вот эта твоя манера речи ""клоунская"" меня не впечатляет, давай встретимся, объясню на понятном тебе языке, языке боли."'
