using Checkers.Data.Entity;
using Checkers.Data.Repository.MSSqlImplementation;
using static System.Console;
using static Checkers.Data.Repository.MSSqlImplementation.ItemRepository;
using static Checkers.Data.Repository.MSSqlImplementation.Repository;
using static Checkers.Data.Repository.MSSqlImplementation.UserRepository;

const string image = "img";
var path = Directory.GetCurrentDirectory();

Write(
@$"GO
CREATE DATABASE Checkers;

GO
USE Checkers;
CREATE TABLE {ItemTypeTable}
(
{Id}		INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
{ItemTypeName}		{StringType}	NOT NULL	UNIQUE
);

CREATE TABLE {ItemTable}
(
{Id}			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
{Updated}		DATETIME		NOT NULL	DEFAULT GETDATE(),
{ItemRepository.Type}      INT NOT NULL	CONSTRAINT {Fk(ItemTable,ItemTypeTable)} FOREIGN KEY REFERENCES {ItemTypeTable}({Id}),
{Detail} {StringType}   NOT NULL,
{Extension}	{StringType}	NOT NULL,
{ItemName}		{StringType}	NOT NULL,
{Picture}	VARBINARY(MAX)	NOT NULL,
{Price}		INT				NOT NULL
);

CREATE TABLE {PictureTable}
(
{Id}			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
{ItemId}		INT		NOT NULL	CONSTRAINT {Fk(PictureTable,ItemTable)} FOREIGN KEY REFERENCES {ItemTable}({Id})
);

CREATE TABLE {AchievementTable}
(
{Id}			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
{ItemId}		INT		NOT NULL	CONSTRAINT {Fk(AchievementTable,ItemTable)} FOREIGN KEY REFERENCES {ItemTable}({Id})
);

CREATE TABLE {LootBoxTable}
(
{Id}			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
{ItemId}		INT		NOT NULL	CONSTRAINT {Fk(LootBoxTable,ItemTable)} FOREIGN KEY REFERENCES {ItemTable}({Id})
);

CREATE TABLE {CheckersTable}
(
{Id}			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
{ItemId}		INT		NOT NULL	CONSTRAINT {Fk(CheckersTable,ItemTable)} FOREIGN KEY REFERENCES {ItemTable}({Id})
);

CREATE TABLE {AnimationTable}
(
{Id}			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
{ItemId}		INT		NOT NULL	CONSTRAINT {Fk(AnimationTable,ItemTable)} FOREIGN KEY REFERENCES {ItemTable}({Id})
);

CREATE TABLE {UserTable}
(
{Id}				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
{LastActivity}	DATETIME		NOT NULL	DEFAULT GETDATE(),
{Nick} {StringType}    NOT NULL,
{Login}		{StringType}	NOT NULL	UNIQUE,
{Password}		{StringType}	NOT NULL,
{Email}			{StringType}	NOT NULL,
{PictureId}		INT				NOT NULL	CONSTRAINT {Fk(UserTable,PictureTable)} FOREIGN KEY REFERENCES {PictureTable}({Id}) DEFAULT 1,
{SocialCredit}  INT             NOT NULL    DEFAULT 1000,
{CheckersId}    INT             NOT NULL    CONSTRAINT {Fk(UserTable,CheckersTable)} FOREIGN KEY REFERENCES {CheckersTable}({Id}) DEFAULT 1,
{AnimationId}    INT             NOT NULL    CONSTRAINT {Fk(UserTable,AnimationTable)} FOREIGN KEY REFERENCES {AnimationTable}({Id}) DEFAULT 1
);

CREATE TABLE {UserItemTable}
(
{Id}				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
{UserId}            INT				NOT NULL	CONSTRAINT {Fk(UserItemTable,UserTable)} FOREIGN KEY REFERENCES {UserTable}({Id}),
{ItemId}            INT				NOT NULL	CONSTRAINT {Fk(UserItemTable,ItemTable)} FOREIGN KEY REFERENCES {ItemTable}({Id}),
);

GO
CREATE PROCEDURE {SelectItemPictureProc} {IdVar} INT
AS
BEGIN
    SELECT {Picture}, {Extension}
    FROM {Schema}.{ItemTable} WHERE {Id}={IdVar}
END

GO
CREATE PROCEDURE {SelectItemProc} {IdVar} INT
AS
BEGIN
    SELECT {Id},
    {Updated},
    {ItemRepository.Type},
    {ItemName},
    {Detail},
    {Extension},
    {Price}
    FROM {Schema}.{ItemTable} WHERE {Id}={IdVar}
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
    JOIN {ItemTable} AS I ON UI.{ItemId}=I.{Id}  WHERE I.{ItemRepository.Type}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}={ItemTypeVar}); 
END

GO  
CREATE PROCEDURE {UpdateUserNickProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {NewNickVar} {StringType}
AS
BEGIN
    UPDATE {Schema}.{UserTable} SET {Nick}={NewNickVar} WHERE {Login}={LoginVar} AND {Password}={PasswordVar};
END

GO  
CREATE PROCEDURE {UpdateUserLoginProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {NewLoginVar} {StringType}
AS
BEGIN
    UPDATE {Schema}.{UserTable} SET {Login}={NewLoginVar} WHERE {Login}={LoginVar} AND {Password}={PasswordVar};
END

GO  
CREATE PROCEDURE {UpdateUserPasswordProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {NewPasswordVar} {StringType}
AS
BEGIN
    UPDATE {Schema}.{UserTable} SET {Password}={NewPasswordVar} WHERE {Login}={LoginVar} AND {Password}={PasswordVar};
END

GO  
CREATE PROCEDURE {UpdateUserEmailProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {NewEmailVar} {StringType}
AS
BEGIN
    UPDATE {Schema}.{UserTable} SET {Email}={NewEmailVar} WHERE {Login}={LoginVar} AND {Password}={PasswordVar};
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
    SELECT {Id} FROM {Schema}.{UserTable} WHERE {Login}={LoginVar} AND {Password}={PasswordVar};
END


GO
CREATE PROCEDURE {UpdateUserAnimationProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    IF {IdVar} IN (SELECT {ItemId} FROM {Schema}.{UserItemTable} WHERE {UserId}=(SELECT {Id} FROM {Schema}.{UserTable} WHERE {Login}={LoginVar} AND {Password}={PasswordVar}))
        UPDATE {Schema}.{UserTable} SET {AnimationId}={IdVar};
END

GO
CREATE PROCEDURE {UpdateUserCheckersProc} {LoginVar} {StringType}, {PasswordVar} {StringType}, {IdVar} INT
AS
BEGIN
    IF {IdVar} IN (SELECT {ItemId} FROM {Schema}.{UserItemTable} WHERE {UserId}=(SELECT {Id} FROM {Schema}.{UserTable} WHERE {Login}={LoginVar} AND {Password}={PasswordVar}))
        UPDATE {Schema}.{UserTable} SET {CheckersId}={IdVar};
END

GO
INSERT INTO ItemType({ItemTypeName}) 
VALUES ('{ItemType.Picture}'),
('{ItemType.Achievement}'),
('{ItemType.CheckersSkin}'),
('{ItemType.Animation}'),
('{ItemType.LootBox}');

INSERT INTO Item({ItemName},
{ItemRepository.Type},
{Detail},
{Extension},
{Picture},
{Price}) 
VALUES 
('Picture 1',
(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.Picture}'),
'First picture detail',
'png',
(SELECT * FROM OPENROWSET(BULK '{path}\{image}\1.png', SINGLE_BLOB) AS D),
100),
('Achievement 1',
(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.Achievement}'),
'First achievement detail',
'png',
(SELECT * FROM OPENROWSET(BULK '{path}\{image}\2.png', SINGLE_BLOB) AS D),
100),
('Checkers 1',
(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.CheckersSkin}'),
'First checkers detail',
'png',
(SELECT * FROM OPENROWSET(BULK '{path}\{image}\3.png', SINGLE_BLOB) AS D),
100),
('Animations 1',
(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.Animation}'),
'First animation detail',
'png',
(SELECT * FROM OPENROWSET(BULK '{path}\{image}\4.png', SINGLE_BLOB) AS D),
100);

INSERT INTO {AchievementTable}({ItemId}) 
(SELECT {Id} FROM {ItemTable} WHERE {ItemRepository.Type}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.Achievement}')); 
INSERT INTO {CheckersTable}({ItemId}) 
(SELECT {Id} FROM {ItemTable} WHERE {ItemRepository.Type}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.CheckersSkin}')); 
INSERT INTO {AnimationTable}({ItemId}) 
(SELECT {Id} FROM {ItemTable} WHERE {ItemRepository.Type}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.Animation}')); 
INSERT INTO {PictureTable}({ItemId}) 
(SELECT {Id} FROM {ItemTable} WHERE {ItemRepository.Type}=(SELECT {Id} FROM {ItemTypeTable} WHERE {ItemTypeName}='{ItemType.Picture}')); 

GO
CREATE VIEW {UserItemExtendedView} AS
SELECT UI.{UserId},U.{Nick}, UI.{ItemId}, I.{Updated}, I.{Detail}, I.{Extension}, I.{ItemName}, 
        I.{Price}, IT.{ItemTypeName}
FROM {UserItemTable} AS UI
JOIN {ItemTable} AS I ON UI.{ItemId} = I.{Id}
JOIN {ItemTypeTable} AS IT ON IT.{Id} = I.{ItemRepository.Type}
JOIN {UserTable} AS U ON UI.{UserId} = U.{Id}

GO
Use Checkers
EXEC CreateUser 'b','b','b','b';
INSERT INTO {UserItemTable}({UserId},{ItemId})
VALUES (
(SELECT TOP 1 {Id} FROM {UserTable}),
(SELECT TOP 1 {ItemId} FROM {AchievementTable}));
");