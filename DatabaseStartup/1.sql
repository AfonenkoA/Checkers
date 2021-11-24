GO
CREATE DATABASE Checkers;

GO
USE Checkers;
CREATE TABLE [ItemType]
(
id		INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_type_name]		NVARCHAR(300)	NOT NULL	UNIQUE
);

CREATE TABLE [Item]
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[updated]		DATETIME		NOT NULL	DEFAULT GETDATE(),
[type]      INT NOT NULL	CONSTRAINT FK_Item_ItemType FOREIGN KEY REFERENCES [ItemType](id),
[detail] NVARCHAR(300)   NOT NULL,
[extension]	NVARCHAR(300)	NOT NULL,
[item_name]		NVARCHAR(300)	NOT NULL,
[picture]	VARBINARY(MAX)	NOT NULL,
[price]		INT				NOT NULL
);

CREATE TABLE [Picture]
(
id			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_Picture_Item FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [Achievement]
(
id			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_Achievement_Item FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [LootBox]
(
id			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_LootBox_Item FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [CheckersSkin]
(
id			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_CheckersSkin_Item FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [Animation]
(
id			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[item_id]		INT		NOT NULL	CONSTRAINT FK_Animation_Item FOREIGN KEY REFERENCES [Item](id)
);

CREATE TABLE [User]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[last_activity]	DATETIME		NOT NULL	DEFAULT GETDATE(),
[nick] NVARCHAR(300)    NOT NULL,
[login]		NVARCHAR(300)	NOT NULL	UNIQUE,
[password]		NVARCHAR(300)	NOT NULL,
[email]			NVARCHAR(300)	NOT NULL,
[picture_id]		INT				NOT NULL	CONSTRAINT FK_User_Picture FOREIGN KEY REFERENCES [Picture](id) DEFAULT 1,
[social_credit]  INT             NOT NULL    DEFAULT 1000,
[checkers_id]    INT             NOT NULL    CONSTRAINT FK_User_CheckersSkin FOREIGN KEY REFERENCES [CheckersSkin](id) DEFAULT 1,
[animation_id]    INT             NOT NULL    CONSTRAINT FK_User_Animation FOREIGN KEY REFERENCES [Animation](id) DEFAULT 1
);

CREATE TABLE [UserItem]
(
id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[user_id]            INT				NOT NULL	CONSTRAINT FK_UserItem_User FOREIGN KEY REFERENCES [User](id),
[item_id]            INT				NOT NULL	CONSTRAINT FK_UserItem_Item FOREIGN KEY REFERENCES [Item](id),
);

GO
CREATE PROCEDURE [SelectItemPicture] @id INT
AS
BEGIN
    SELECT [picture], [extension]
    FROM Checkers.dbo.[Item] WHERE id=@id
END

GO
CREATE PROCEDURE [SelectItem] @id INT
AS
BEGIN
    SELECT id,
    [updated],
    [type],
    [item_name],
    [detail],
    [extension],
    [price]
    FROM Checkers.dbo.[Item] WHERE id=@id
END

GO
CREATE PROCEDURE [SelectItems]
AS
BEGIN
    SELECT id, [updated] FROM Checkers.dbo.[Item]
END

GO
CREATE PROCEDURE [CreateUser]
@nick NVARCHAR(300),
@login NVARCHAR(300),
@password NVARCHAR(300),
@email NVARCHAR(300)
AS
BEGIN
    DECLARE @user_id INT
    INSERT INTO Checkers.dbo.[User]([nick],[login],[password],[email])
    VALUES (@nick,@login,@password,@email);
    SET @user_id = @@IDENTITY;
    INSERT INTO [UserItem]([user_id],[item_id]) VALUES (@user_id,(SELECT [item_id] FROM [Animation] WHERE id=1));
    INSERT INTO [UserItem]([user_id],[item_id]) VALUES (@user_id,(SELECT [item_id] FROM [CheckersSkin] WHERE id=1));
    INSERT INTO [UserItem]([user_id],[item_id]) VALUES (@user_id,(SELECT [item_id] FROM [Picture] WHERE id=1));
END

GO
CREATE PROCEDURE [SelectUser] @id INT
AS
BEGIN
    SELECT id,[nick],[picture_id],[checkers_id],[animation_id],[social_credit],[last_activity]
    FROM Checkers.dbo.[User]
    WHERE id=@id
END

GO 
CREATE PROCEDURE [SelectUserItem] @id INT, @item_type NVARCHAR(300)
AS
BEGIN
    SELECT I.id FROM Checkers.dbo.[UserItem] AS UI 
    JOIN [Item] AS I ON UI.[item_id]=I.id  WHERE I.[type]=(SELECT id FROM [ItemType] WHERE [item_type_name]=@item_type); 
END

GO  
CREATE PROCEDURE [UpdateUserNick] @login NVARCHAR(300), @password NVARCHAR(300), @new_nick NVARCHAR(300)
AS
BEGIN
    UPDATE Checkers.dbo.[User] SET [nick]=@new_nick WHERE [login]=@login AND [password]=@password;
END

GO  
CREATE PROCEDURE [UpdateUserLogin] @login NVARCHAR(300), @password NVARCHAR(300), @new_login NVARCHAR(300)
AS
BEGIN
    UPDATE Checkers.dbo.[User] SET [login]=@new_login WHERE [login]=@login AND [password]=@password;
END

GO  
CREATE PROCEDURE [UpdateUserPassword] @login NVARCHAR(300), @password NVARCHAR(300), @new_password NVARCHAR(300)
AS
BEGIN
    UPDATE Checkers.dbo.[User] SET [password]=@new_password WHERE [login]=@login AND [password]=@password;
END

GO  
CREATE PROCEDURE [UpdateUserEmailProc] @login NVARCHAR(300), @password NVARCHAR(300), @new_email NVARCHAR(300)
AS
BEGIN
    UPDATE Checkers.dbo.[User] SET [email]=@new_email WHERE [login]=@login AND [password]=@password;
END

GO
CREATE PROCEDURE [SelectUserByNick] @nick NVARCHAR(300)
AS
BEGIN
    SELECT id,[nick],[picture_id],[checkers_id],[animation_id],[social_credit]
    FROM Checkers.dbo.[User]
    WHERE [nick] LIKE @nick
END

GO
CREATE PROCEDURE [Authenticate] @login NVARCHAR(300), @password NVARCHAR(300)
AS
BEGIN
    SELECT id FROM Checkers.dbo.[User] WHERE [login]=@login AND [password]=@password;
END


GO
CREATE PROCEDURE [UpdateUserAnimationProc] @login NVARCHAR(300), @password NVARCHAR(300), @id INT
AS
BEGIN
    IF @id IN (SELECT [item_id] FROM Checkers.dbo.[UserItem] WHERE [user_id]=(SELECT id FROM Checkers.dbo.[User] WHERE [login]=@login AND [password]=@password))
        UPDATE Checkers.dbo.[User] SET [animation_id]=@id;
END

GO
CREATE PROCEDURE [UpdateUserCheckersProc] @login NVARCHAR(300), @password NVARCHAR(300), @id INT
AS
BEGIN
    IF @id IN (SELECT [item_id] FROM Checkers.dbo.[UserItem] WHERE [user_id]=(SELECT id FROM Checkers.dbo.[User] WHERE [login]=@login AND [password]=@password))
        UPDATE Checkers.dbo.[User] SET [checkers_id]=@id;
END

GO
INSERT INTO ItemType([item_type_name]) 
VALUES ('Picture'),
('Achievement'),
('CheckersSkin'),
('Animation'),
('LootBox');

INSERT INTO Item([item_name],
[type],
[detail],
[extension],
[picture],
[price]) 
VALUES 
('Picture 1',
(SELECT id FROM [ItemType] WHERE [item_type_name]='Picture'),
'First picture detail',
'png',
(SELECT * FROM OPENROWSET(BULK 'W:\IT\C#\Checkers\DatabaseStartup\img\1.png', SINGLE_BLOB) AS D),
100),
('Achievement 1',
(SELECT id FROM [ItemType] WHERE [item_type_name]='Achievement'),
'First achievement detail',
'png',
(SELECT * FROM OPENROWSET(BULK 'W:\IT\C#\Checkers\DatabaseStartup\img\2.png', SINGLE_BLOB) AS D),
100),
('Checkers 1',
(SELECT id FROM [ItemType] WHERE [item_type_name]='CheckersSkin'),
'First checkers detail',
'png',
(SELECT * FROM OPENROWSET(BULK 'W:\IT\C#\Checkers\DatabaseStartup\img\3.png', SINGLE_BLOB) AS D),
100),
('Animations 1',
(SELECT id FROM [ItemType] WHERE [item_type_name]='Animation'),
'First animation detail',
'png',
(SELECT * FROM OPENROWSET(BULK 'W:\IT\C#\Checkers\DatabaseStartup\img\4.png', SINGLE_BLOB) AS D),
100);

INSERT INTO [Achievement]([item_id]) 
(SELECT id FROM [Item] WHERE [type]=(SELECT id FROM [ItemType] WHERE [item_type_name]='Achievement')); 
INSERT INTO [CheckersSkin]([item_id]) 
(SELECT id FROM [Item] WHERE [type]=(SELECT id FROM [ItemType] WHERE [item_type_name]='CheckersSkin')); 
INSERT INTO [Animation]([item_id]) 
(SELECT id FROM [Item] WHERE [type]=(SELECT id FROM [ItemType] WHERE [item_type_name]='Animation')); 
INSERT INTO [Picture]([item_id]) 
(SELECT id FROM [Item] WHERE [type]=(SELECT id FROM [ItemType] WHERE [item_type_name]='Picture')); 

GO
CREATE VIEW [UserItemExtended] AS
SELECT UI.[user_id],U.[nick], UI.[item_id], I.[updated], I.[detail], I.[extension], I.[item_name], 
        I.[price], IT.[item_type_name]
FROM [UserItem] AS UI
JOIN [Item] AS I ON UI.[item_id] = I.id
JOIN [ItemType] AS IT ON IT.id = I.[type]
JOIN [User] AS U ON UI.[user_id] = U.id

GO
Use Checkers
EXEC CreateUser 'b','b','b','b';
INSERT INTO [UserItem]([user_id],[item_id])
VALUES (
(SELECT TOP 1 id FROM [User]),
(SELECT TOP 1 [item_id] FROM [Achievement]));
