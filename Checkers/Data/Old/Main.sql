GO
CREATE DATABASE OldCheckers;

GO   
USE OldCheckers;


CREATE TABLE AchievementOptions
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
name		NVARCHAR(30)	NOT NULL,
description	NVARCHAR(250)	NOT NULL	
);


CREATE TABLE EventOptions
(
id		INT				NOT NULL	IDENTITY(2,1)	PRIMARY KEY,
type	NVARCHAR(30)	NOT NULL,
);


CREATE TABLE PictureOptions
(
id			INT 	            NOT NULL    IDENTITY(1,1)	PRIMARY KEY,
name		NVARCHAR(30)		NOT NULL,
description NVARCHAR(250)		NOT NULL
);


CREATE TABLE ItemOptions
(
id			INT				NOT NULL	IDENTITY(1,1) PRIMARY KEY,
name		NVARCHAR(30)	NOT NULL,
description	NVARCHAR(250)	NOT NULL
);


CREATE TABLE Users
(
id					INT				NOT NULL	IDENTITY(1,1) PRIMARY KEY,
login				NVARCHAR(30)	NOT NULL	UNIQUE,
password			NVARCHAR(30)	NOT NULL,
email				NVARCHAR(30)	NOT NULL	UNIQUE,
nick				NVARCHAR(30)	NOT NULL,
rating				INT				NOT NULL	DEFAULT	1000,
currency			INT				NOT NULL	DEFAULT	1000,
picture_id			INT				NOT NULL	DEFAULT 1		REFERENCES PictureOptions(id),
selected_checkers	INT				NOT NULL	DEFAULT 1		REFERENCES ItemOptions(id),
selected_animation	INT				NOT NULL	DEFAULT 1		REFERENCES ItemOptions(id),
last_activity		DATETIME		NOT NULL	DEFAULT GETDATE(),
);


CREATE TABLE Achievements
(
id				INT			NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
user_id			INT			NOT	NULL	REFERENCES Users(id),
achievement_id	INT			NOT NULL	REFERENCES AchievementOptions(id),
);


CREATE TABLE Items
(
id		INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
user_id	INT		NOT NULL	REFERENCES Users(id),
item_id	INT		NOT NULL	REFERENCES ItemOptions(id)
);


CREATE TABLE Games
(
id						INT			NOT NULL	IDENTITY(1,1) PRIMARY KEY,
player1_id				INT			NOT NULL	REFERENCES Users(id),
player2_id				INT			NOT NULL	REFERENCES Users(id),
player1_chekers_id		INT			NOT NULL	REFERENCES ItemOptions(id),
player2_chekers_id		INT			NOT NULL	REFERENCES ItemOptions(id),
player1_animation_id	INT			NOT NULL	REFERENCES ItemOptions(id),
player2_animation_id	INT			NOT NULL	REFERENCES ItemOptions(id),
start_time				DATETIME	NOT NULL,
end_time				DATETIME	NOT NULL,
winner_id				INT			NOT NULL	REFERENCES Users(id),
player1_raiting_change	INT			NOT NULL,
player2_raiting_change	INT			NOT NULL
);



CREATE TABLE GamesProgress
(
id				INT			NOT NULL	IDENTITY(1,1) PRIMARY KEY,
game_id			INT			NOT NULL	REFERENCES Games(id),
action_num		INT			NOT NULL,
actor_id		INT			NOT NULL	REFERENCES Users(id),
action_id		INT			NOT NULL	REFERENCES EventOptions(id),
action_desc		CHAR(12)	NOT NULL,
action_time		TIME		NOT NULL
);


CREATE TABLE Friends
(
id			INT		NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
user_id		INT		NOT NULL	REFERENCES	Users(id),
friend_id	INT		NOT NULL	REFERENCES	Users(id)
);



INSERT INTO AchievementOptions(name,description) VALUES
('Achievement 1','Achievement 1 description'),
('Achievement 2','Achievement 2 description'),
('Achievement 3','Achievement 3 description');


INSERT INTO PictureOptions(name,description) VALUES
('Picture 1','Picture 1 description'),
('Picture 2','Picture 2 description'),
('Picture 3','Picture 3 description');


INSERT INTO ItemOptions(name,description) VALUES
('Item 1','Item 1 description'),
('Item 2','Item 2 description'),
('Item 3','Item 3 description');


INSERT INTO Users(login,password,nick,email) VALUES
('server','server','server','server@example.com'),
('biba','biba','biba','biba@example.com'),
('boba','boba','boba','boba@example.com');


INSERT INTO Achievements(user_id,achievement_id) VALUES
(1,1),
(1,2),
(1,3),
(2,1),
(2,2),
(2,3);


INSERT INTO Items(user_id,item_id) VALUES
(1,1),
(1,2),
(1,3),
(2,1),
(2,2),
(2,3);


INSERT INTO EventOptions(type) VALUES
('GameStart'),
('GameEnd'),
('YourTurn'),
('EnemyTurn'),
('Emote'),
('Move'),
('Remove');


INSERT INTO Friends(user_id,friend_id) VALUES
(2,3),
(3,2);