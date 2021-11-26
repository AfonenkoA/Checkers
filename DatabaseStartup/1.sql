GO
CREATE DATABASE Checkers;

GO
USE Checkers;

CREATE TABLE [ResourceTable]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[resource_extension]     NVARCHAR(300)	NOT NULL,
[resource_bytes]         VARBINARY(MAX)	NOT NULL,
);


CREATE TABLE [ItemType]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_type_name]	NVARCHAR(300)	NOT NULL	UNIQUE
);

CREATE TABLE [Item]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[updated]		DATETIME		NOT NULL	DEFAULT GETDATE(),
[item_type_id]    INT             NOT NULL	CONSTRAINT FK_Item_ItemType    FOREIGN KEY REFERENCES [ItemType](id),
[resource_id]    INT             NOT NULL	CONSTRAINT FK_Item_ResourceTable    FOREIGN KEY REFERENCES [ResourceTable](id),
[detail]        NVARCHAR(300)    NOT NULL,
[item_name]		NVARCHAR(300)	NOT NULL,
[price]		    INT				NOT NULL
);

CREATE TABLE [Picture]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_Picture_Item    FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [Achievement]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_Achievement_Item    FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [LootBox]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_LootBox_Item    FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [CheckersSkin]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_CheckersSkin_Item    FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [Animation]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_Animation_Item    FOREIGN KEY REFERENCES [Item](id)
);


CREATE TABLE [UserType]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[user_type_name]  NVARCHAR(300)    NOT NULL
);

CREATE TABLE [User]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[user_type_id]        INT		        NOT NULL	CONSTRAINT FK_User_UserType    FOREIGN KEY REFERENCES [UserType](id)      DEFAULT 1,
[last_activity]	    DATETIME		NOT NULL	DEFAULT GETDATE(),
[nick]              NVARCHAR(300)    NOT NULL,
[login]		        NVARCHAR(300)	NOT NULL	UNIQUE,
[password]		    NVARCHAR(300)	NOT NULL,
[email]			    NVARCHAR(300)	NOT NULL,
[picture_id]		    INT				NOT NULL	CONSTRAINT FK_User_Picture    FOREIGN KEY REFERENCES [Picture](id)        DEFAULT 1,
[social_credit]      INT             NOT NULL    DEFAULT 1000,
[checkers_id]        INT             NOT NULL    CONSTRAINT FK_User_CheckersSkin    FOREIGN KEY REFERENCES [CheckersSkin](id)       DEFAULT 1,
[animation_id]       INT             NOT NULL    CONSTRAINT FK_User_Animation    FOREIGN KEY REFERENCES [Animation](id)      DEFAULT 1
);

CREATE TABLE [UserItem]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[user_id]            INT				NOT NULL	CONSTRAINT FK_UserItem_User    FOREIGN KEY REFERENCES [User](id),
[item_id]            INT				NOT NULL	CONSTRAINT FK_UserItem_Item    FOREIGN KEY REFERENCES [Item](id),
);

CREATE TABLE [ChatType]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[chat_type_name]      NVARCHAR(300)    NOT NULL
);

CREATE TABLE [Chat]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[chat_name]      NVARCHAR(300)    NOT NULL,
[chat_type_id]    INT             NOT NULL CONSTRAINT FK_Chat_ChatType    FOREIGN KEY REFERENCES [ChatType](id)
);

CREATE TABLE [FriendshipState]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
friendship_state_name NVARCHAR(300)  NOT NULL
);

CREATE TABLE [Friendship]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[user1_id]           INT   NOT NULL	CONSTRAINT FK_Friendship_User1    FOREIGN KEY REFERENCES [User](id),
[user2_id]           INT   NOT NULL	CONSTRAINT FK_Friendship_User2    FOREIGN KEY REFERENCES [User](id),
[chat_id]            INT   NOT NULL	CONSTRAINT FK_Friendship_Chat    FOREIGN KEY REFERENCES [Chat](id),
friendship_state_id INT   NOT NULL	CONSTRAINT FK_Friendship_FriendshipState    FOREIGN KEY REFERENCES [FriendshipState](id)
);


CREATE TABLE [Message]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[chat_id]            INT             NOT NULL	CONSTRAINT FK_Message_Chat    FOREIGN KEY REFERENCES [Chat](id),
[user_id]            INT             NOT NULL	CONSTRAINT FK_Message_User    FOREIGN KEY REFERENCES [User](id), 
[message_content]    NVARCHAR(300)    NOT NULL,
[send_time]          DATETIME        NOT NULL    DEFAULT GETDATE()
);


CREATE TABLE [Post]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[chat_id]            INT             NOT NULL	CONSTRAINT FK_Post_Chat    FOREIGN KEY REFERENCES [Chat](id),
[post_author_id]      INT             NOT NULL	CONSTRAINT FK_Post_User    FOREIGN KEY REFERENCES [User](id),
[post_title]         NVARCHAR(300)    NOT NULL,
[post_content]       NVARCHAR(300)    NOT NULL,
[post_created]       DATETIME        NOT NULL    DEFAULT GETDATE(),
[post_picture_id]     INT             NOT NULL    CONSTRAINT FK_Post_ResourceTable    FOREIGN KEY REFERENCES [ResourceTable](id)
);

CREATE TABLE [Article]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[article_author_id]   INT             NOT NULL	CONSTRAINT FK_Article_User    FOREIGN KEY REFERENCES [User](id),
[article_title]      NVARCHAR(300)    NOT NULL,
[abstract]   NVARCHAR(300)    NOT NULL,
[article_content]    NVARCHAR(300)    NOT NULL,
[article_created]    DATETIME        NOT NULL    DEFAULT GETDATE(),
[article_picture_id]  INT             NOT NULL    CONSTRAINT FK_Article_ResourceTable    FOREIGN KEY REFERENCES [ResourceTable](id),
[article_post_id]     INT             NOT NULL    CONSTRAINT FK_Article_Post    FOREIGN KEY REFERENCES [Post](id),
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
    EXEC @admin_type_id = [SP_GetUserTypeByName]'Admin';
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
    EXEC @id = [SP_GetChatTypeByName] @chat_type_name;
    INSERT INTO Checkers.dbo.[Chat]([chat_name],[chat_type_id]) 
    VALUES (@chat_name,@id);
    RETURN @@IDENTITY
END

GO
CREATE PROCEDURE [SP_CreateFriendship] @user1_id INT,@user2_id INT
AS
BEGIN
    DECLARE @chat_id INT, @u1_login NVARCHAR(300), @u2_login NVARCHAR(300), @chat_name NVARCHAR(300), @id INT
    SET @u1_login = (SELECT [login] FROM Checkers.dbo.[User] WHERE id=@user1_id);
    SET @u2_login = (SELECT [login] FROM Checkers.dbo.[User] WHERE id=@user2_id);
    SET @chat_name = N'Chat '+@u1_login+N' to '+ @u2_login;
    EXEC @chat_id = [SP_CreateChat] @chat_name, 'Private';
    EXEC @id = [SP_GetFriendshipStateByName] 'Accepted';
    INSERT INTO Checkers.dbo.[Friendship]([user1_id],[user2_id],[chat_id],friendship_state_id)
    VALUES (@user1_id,@user2_id,@chat_id,@id);
END


GO
CREATE PROCEDURE [SP_UpdateUserActivity] @login NVARCHAR(300), @password NVARCHAR(300)
AS
BEGIN
    UPDATE [User] SET [last_activity]=GETDATE() WHERE [login]=@login AND [password]=@password
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
CREATE PROCEDURE [SP_CreateUser]
@nick NVARCHAR(300),
@login NVARCHAR(300),
@password NVARCHAR(300),
@email NVARCHAR(300),
@user_type_name NVARCHAR(300)
AS
BEGIN
    DECLARE @user_id INT, @support_id INT, @id INT
    INSERT INTO Checkers.dbo.[User]([nick],[login],[password],[email],[user_type_id])
    VALUES (@nick,@login,@password,@email,(SELECT id FROM [UserType] WHERE [user_type_name]=@user_type_name));
    SET @user_id = @@IDENTITY;
    IF @user_type_name!='Support'
        BEGIN
        EXEC @id = [SP_GetUserTypeByName] 'Support'
        SET @support_id = (SELECT TOP 1 id FROM Checkers.dbo.[User] WHERE [user_type_id] = @id);
        EXEC [SP_CreateFriendship] @user_id, @support_id
        END
    INSERT INTO [UserItem]([user_id],[item_id]) VALUES (@user_id,(SELECT [item_id] FROM [Animation] WHERE id=1));
    INSERT INTO [UserItem]([user_id],[item_id]) VALUES (@user_id,(SELECT [item_id] FROM [CheckersSkin] WHERE id=1));
    INSERT INTO [UserItem]([user_id],[item_id]) VALUES (@user_id,(SELECT [item_id] FROM [Picture] WHERE id=1));
    RETURN @user_id
END

GO
CREATE PROCEDURE [SP_SelectUser] @id INT
AS
BEGIN
    SELECT id,[nick],[picture_id],[checkers_id],[animation_id],[social_credit],[last_activity],[user_type_id]
    FROM Checkers.dbo.[User]
    WHERE id=@id
END

GO 
CREATE PROCEDURE [SP_SelectUserItem] @id INT, @item_type NVARCHAR(300)
AS
BEGIN
    DECLARE @item_type_id INT
    EXEC @item_type_id = [SP_GetItemTypeByName] @item_type
    SELECT I.id FROM Checkers.dbo.[UserItem] AS UI 
    JOIN [Item] AS I ON UI.[item_id]=I.id  WHERE I.[item_type_id]=@item_type_id; 
END

GO  
CREATE PROCEDURE [SP_UpdateUserNick] @login NVARCHAR(300), @password NVARCHAR(300), @new_nick NVARCHAR(300)
AS
BEGIN
    EXEC [SP_UpdateUserActivity] @login,@password;
    UPDATE Checkers.dbo.[User] SET [nick]=@new_nick WHERE [login]=@login AND [password]=@password;
END

GO  
CREATE PROCEDURE [SP_UpdateUserLogin] @login NVARCHAR(300), @password NVARCHAR(300), @new_login NVARCHAR(300)
AS
BEGIN
    EXEC [SP_UpdateUserActivity] @login,@password;
    UPDATE Checkers.dbo.[User] SET [login]=@new_login WHERE [login]=@login AND [password]=@password;
END

GO  
CREATE PROCEDURE [SP_UpdateUserPassword] @login NVARCHAR(300), @password NVARCHAR(300), @new_password NVARCHAR(300)
AS
BEGIN
    EXEC [SP_UpdateUserActivity] @login,@password;
    UPDATE Checkers.dbo.[User] SET [password]=@new_password WHERE [login]=@login AND [password]=@password;
END

GO  
CREATE PROCEDURE [SP_UpdateUserEmail] @login NVARCHAR(300), @password NVARCHAR(300), @new_email NVARCHAR(300)
AS
BEGIN
    EXEC [SP_UpdateUserActivity] @login,@password;
    UPDATE Checkers.dbo.[User] SET [email]=@new_email WHERE [login]=@login AND [password]=@password;
END

GO
CREATE PROCEDURE [SP_SelectUserByNick] @nick NVARCHAR(300)
AS
BEGIN
    SELECT id,[nick],[picture_id],[checkers_id],[animation_id],[social_credit]
    FROM Checkers.dbo.[User]
    WHERE [nick] LIKE @nick
END

GO
CREATE PROCEDURE [SP_Authenticate] @login NVARCHAR(300), @password NVARCHAR(300)
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
CREATE PROCEDURE [SP_UpdateUserAnimation] @login NVARCHAR(300), @password NVARCHAR(300), @id INT
AS
BEGIN
    DECLARE @user_id INT;
    EXEC @user_id = [SP_Authenticate] @login, @password
    EXEC [SP_UpdateUserActivity] @login,@password;
    IF @id IN (SELECT [item_id] FROM Checkers.dbo.[UserItem] WHERE [user_id]=@user_id)
        UPDATE Checkers.dbo.[User] SET [animation_id]=@id;
END

GO
CREATE PROCEDURE [SP_UpdateUserCheckers] @login NVARCHAR(300), @password NVARCHAR(300), @id INT
AS
BEGIN
    DECLARE @user_id INT;
    EXEC @user_id = [SP_Authenticate] @login, @password
    EXEC [SP_UpdateUserActivity] @login,@password;
    IF @id IN (SELECT [item_id] FROM Checkers.dbo.[UserItem] WHERE [user_id]=@user_id)
        UPDATE Checkers.dbo.[User] SET [checkers_id]=@id;
END

GO
CREATE PROCEDURE [SP_CreateResource] @resource_extension NVARCHAR(300), @resource_bytes VARBINARY(MAX)
AS
BEGIN
    INSERT INTO Checkers.dbo.[ResourceTable]([resource_extension],[resource_bytes]) 
    VALUES (@resource_extension,@resource_bytes);
    RETURN @@IDENTITY;
END

GO
CREATE PROCEDURE [SP_CreateItem]
@item_type NVARCHAR(300),
@item_name NVARCHAR(300),
@detail NVARCHAR(300),
@path NVARCHAR(300),
@price INT
AS
BEGIN
    DECLARE @resource_extension NVARCHAR(300), @id INT, @sql NVARCHAR(300), @resource_bytes VARBINARY(MAX), @type_id INT
    SET @type_id = (SELECT id FROM [ItemType] WHERE [item_type_name]=@item_type);
    SET @resource_extension = (SELECT RIGHT(@path, CHARINDEX('.', REVERSE(@path) + '.') - 1));
    SET @sql = FORMATMESSAGE ( 'SELECT @resource_bytes = BulkColumn FROM OPENROWSET ( BULK ''%s'', SINGLE_BLOB ) AS x;', @path );
    EXEC sp_executesql @sql, N'@resource_bytes VARBINARY(MAX) OUT', @resource_bytes = @resource_bytes OUT;
    EXEC  @id=[SP_CreateResource] @resource_extension ,@resource_bytes
    INSERT INTO Checkers.dbo.[Item]([item_name],[item_type_id],[detail],[resource_id],[price])
    VALUES (@item_name,@type_id,@detail,@id,@price);
    RETURN @@IDENTITY;
END

GO
CREATE PROCEDURE [SP_CreatePost] @login NVARCHAR(300), @password NVARCHAR(300),
@post_title NVARCHAR(300), @post_content NVARCHAR(300),@post_picture_id INT
AS
BEGIN
    DECLARE @id INT, @user_id INT, @chat_name NVARCHAR(300)
    EXEC @user_id = [SP_Authenticate] @login, @password;
    IF @user_id!=-1
        BEGIN
        SET @chat_name = N'Chat ' + @post_title;
        EXEC @id = [SP_CreateChat] @chat_name, 'Public';
        IF @id IS NOT NULL
            BEGIN
            INSERT INTO Checkers.dbo.[Post]([post_content],[post_title],[post_picture_id],[chat_id],[post_author_id])
            VALUES (@post_content,@post_title,@post_picture_id,@id,@user_id);
            RETURN @@IDENTITY;
            END
        END
    ELSE
        RETURN -1;
END

GO
CREATE PROCEDURE [SP_CreateArticle] @login NVARCHAR(300), @password NVARCHAR(300),
@article_title NVARCHAR(300), @abstract NVARCHAR(300), 
@article_content NVARCHAR(300), @article_picture_id INT
AS
BEGIN
    DECLARE @id INT, @user_id INT, @post NVARCHAR(300), @access INT
    SET @post = N'Discussion ' + @article_title;
    EXEC @user_id=[SP_Authenticate] @login, @password;
    IF @user_id!=-1
        BEGIN
        EXEC @access = [SP_CheckAccess] @user_id, 'Editor';
        IF @access=1
            BEGIN
            EXEC @id=[SP_CreatePost] @login,@password,@article_title,@post, @article_picture_id;
            IF @id!=-1
                BEGIN
                INSERT INTO Checkers.dbo.[Article]([article_title],[article_content],[abstract],[article_author_id],[article_picture_id],[article_post_id])
                VALUES  (@article_title,@article_content,@abstract,@user_id,@article_picture_id,@id);
                RETURN @@IDENTITY
                END
            END
        END
    ELSE
        RETURN -1;
END

GO


GO
CREATE PROCEDURE [SP_SelectArticleInfo] @id INT
AS
BEGIN
    SELECT id,[article_picture_id],[article_title],[abstract],[article_picture_id] FROM Checkers.dbo.[Article] WHERE id=@id;
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
    SELECT id,[article_picture_id],[article_title],[abstract],[article_picture_id] FROM Checkers.dbo.[Article];
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
CREATE PROCEDURE [SP_SendMessage] @login NVARCHAR(300),@password NVARCHAR(300),
@chat_id INT,@message_content NVARCHAR(300)
AS
BEGIN
    DECLARE @user_id INT, @id INT
    EXEC @user_id = [SP_Authenticate] @login,@password;
    IF @user_id!=-1
        BEGIN
        EXEC @id = [SP_GetChatTypeByName] 'Public'
        IF @chat_id IN 
        (SELECT [chat_id] FROM [Friendship] WHERE [user1_id]=@user_id OR [user2_id]=@user_id) OR 
        (SELECT [chat_type_id] FROM [Chat] WHERE id=@chat_id) = @id
            INSERT INTO Checkers.dbo.[Message]([chat_id],[user_id],[message_content])
            VALUES (@chat_id,@user_id,@message_content); 
        END
END

GO
CREATE PROCEDURE [SP_SelectMessage] @login NVARCHAR(300), @password NVARCHAR(300), @chat_id INT
AS
BEGIN
    DECLARE @user_id INT
    EXEC @user_id = [SP_Authenticate] @login,@password;
    IF @user_id!=-1
            SELECT * FROM Checkers.dbo.[Message] WHERE [chat_id]=@chat_id;
END

GO
CREATE PROCEDURE [SP_SetItems]
AS
BEGIN
    DECLARE @id INT
    EXEC @id = [SP_GetItemTypeByName] 'Achievement';
    INSERT INTO [Achievement]([item_id]) (SELECT id FROM [Item] WHERE [item_type_id]=@id); 
    EXEC @id = [SP_GetItemTypeByName] 'CheckersSkin';
    INSERT INTO [CheckersSkin]([item_id]) (SELECT id FROM [Item] WHERE [item_type_id]=@id);
    EXEC @id = [SP_GetItemTypeByName] 'Animation';
    INSERT INTO [Animation]([item_id]) (SELECT id FROM [Item] WHERE [item_type_id]=@id); 
    EXEC @id = [SP_GetItemTypeByName] 'Picture';
    INSERT INTO [Picture]([item_id]) (SELECT id FROM [Item] WHERE [item_type_id]=@id); 
    EXEC @id = [SP_GetItemTypeByName] 'LootBox';
    INSERT INTO [LootBox]([item_id]) (SELECT id FROM [Item] WHERE [item_type_id]=@id); 
END


GO
INSERT INTO [ItemType]([item_type_name]) 
VALUES ('Picture'),
('Achievement'),
('CheckersSkin'),
('Animation'),
('LootBox');

INSERT INTO [UserType]([user_type_name]) VALUES 
('Admin'),
('Editor'),
('Moderator'),
('Support'),
('Player')

INSERT INTO [ChatType]([chat_type_name]) VALUES ('Public'),('Private');

INSERT INTO [FriendshipState](friendship_state_name) VALUES
('Waiting'),
('Accepted'),
('Canceled');

EXEC [SP_CreateItem] 'Picture','Picture 1','First Picture detail','W:\IT\C#\Checkers\DatabaseStartup\img\1.png',100;
EXEC [SP_CreateItem] 'Achievement','Achievement 1','First Achievement detail','W:\IT\C#\Checkers\DatabaseStartup\img\2.png',100;
EXEC [SP_CreateItem] 'CheckersSkin','CheckersSkin 1','First CheckersSkin detail','W:\IT\C#\Checkers\DatabaseStartup\img\3.png',100;
EXEC [SP_CreateItem] 'Animation','Animation 1','First Animation detail','W:\IT\C#\Checkers\DatabaseStartup\img\4.png',100;
EXEC [SP_SetItems]

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
EXEC [SP_CreateChat] 'Common chat','Public'

EXEC [SP_CreateUser] 'Support','Support','Support','Support','Support';
EXEC [SP_CreateUser] 'Admin','Admin','Admin','Admin','Admin';

INSERT INTO [UserItem]([user_id],[item_id])
VALUES (
(SELECT TOP 1 id FROM [User]),
(SELECT TOP 1 [item_id] FROM [Achievement]));

EXEC [SP_CreateArticle] 'Admin','Admin','Title','Abstract','Content', 1;
EXEC [SP_SendMessage] 'Admin', 'Admin', 1, N'Привет всем';
EXEC [SP_SendMessage] 'Admin', 'Admin', 1, N'Удачи и приятной игры';
EXEC [SP_SendMessage] 'Support', 'Support', 2, N'Если что-то не получается, пиши сюда'
