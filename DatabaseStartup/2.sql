GO
CREATE DATABASE Checkers;

GO
USE Checkers;
CREATE TABLE [ItemType]
(
id		INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
[name]		NVARCHAR(20)	NOT NULL	UNIQUE
);

CREATE TABLE Item
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
updated		DATETIME		NOT NULL	DEFAULT GETDATE(),
[type]      INT NOT NULL	CONSTRAINT FK_Item_ItemType FOREIGN KEY REFERENCES ItemType(id),
detail NVARCHAR(200)   NOT NULL,
extension	NVARCHAR(10)	NOT NULL,
[name]		NVARCHAR(20)	NOT NULL,
picture		VARBINARY(MAX)	NOT NULL,
price		INT				NOT NULL
);

CREATE TABLE UserPicture
(
    id			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
    item_id		INT		NOT NULL	CONSTRAINT FK_UserPicture_Item FOREIGN KEY REFERENCES Item(id)
);

CREATE TABLE Achievement
(
    id			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
    item_id		INT		NOT NULL	CONSTRAINT FK_Achievement_Item FOREIGN KEY REFERENCES Item(id)
);

CREATE TABLE[Resource]
(
    id	INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
    picture	VARBINARY(MAX)	NOT NULL
);

CREATE TABLE[User]
    (
        id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
        last_activity	DATETIME		NOT NULL	DEFAULT GETDATE(),
        nick NVARCHAR(50)    NOT NULL,
    [login]			NVARCHAR(50)	NOT NULL	UNIQUE,
[password]		NVARCHAR(50)	NOT NULL,
    email			NVARCHAR(50)	NOT NULL,
    picture_id		INT				NOT NULL	CONSTRAINT FK_User_UserPicture FOREIGN KEY REFERENCES UserPicture(id)
    );

CREATE TABLE[Message]
    (
        id				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
    [user_id]		INT				NOT NULL	CONSTRAINT FK_Message_User FOREIGN KEY REFERENCES [User](id),
content NVARCHAR(200)   NOT NULL,
    send_time		DATETIME		NOT NULL	DEFAULT GETDATE(),
    );


GO
INSERT INTO ItemType([name]) 
VALUES ('Picture'),
('Achievement'),
('Checker'),
('Animation'),
('LootBox');

INSERT INTO Item([name],
[type],
detail,
extension,
picture,
price) 
VALUES 
('Picture1',
(SELECT id FROM ItemType WHERE [name]='Picture'),
'First picture detail',
'png',
(SELECT * FROM OPENROWSET(BULK 'System.Func`1[System.String]/img/1.png', SINGLE_BLOB)),
100);



GO
CREATE PROCEDURE SelectItemPicture @idcode INT
AS
BEGIN
    SELECT picture, extension
    FROM Checkers.dbo.Item WHERE id=@idcode
END

GO
CREATE PROCEDURE SelectItem @idcode INT
AS
BEGIN
    SELECT id,
    updated,
    [type],
    [name],
    detail,
    extension,
    price
    FROM Checkers.dbo.Item WHERE id=@idcode
END

GO
CREATE PROCEDURE SelectItems
AS
BEGIN
    SELECT id, updated FROM Checkers.dbo.Item
END
