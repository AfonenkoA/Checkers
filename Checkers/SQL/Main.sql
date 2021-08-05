CREATE TABLE AchievementOptions
(
id			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
name		NVARCHAR(30)	NOT NULL,
description	NVARCHAR(250)	NOT NULL	
);

CREATE TABLE EventOptions
(
id		INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY,
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