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