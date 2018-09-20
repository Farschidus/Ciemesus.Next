print 'inserting into dbo.Ciemesuss'

SET IDENTITY_INSERT dbo.Ciemesuss ON

insert into dbo.Ciemesuss
    (CiemesusId, MemberId, TeamId, TakenDate, CiemesusName, Likes, Pics)
values
    (1, 3, 1, '2018-04-14 00:00:00.000', 'Ciemesus Name example 1', 54, '/clientImages/fikas/1.jpg'),
    (2, 2, 1, '2018-04-30 00:00:00.000', 'Ciemesus Name example 2', 43, '/clientImages/fikas/2.jpg'),
    (3, 3, 1, '2018-05-14 00:00:00.000', 'Ciemesus Name example 3', 24, '/clientImages/fikas/3.jpg'),
    (4, 1, 1, '2018-05-30 00:00:00.000', 'Ciemesus Name example 4', 13, '/clientImages/fikas/4.jpg'),
    (5, 2, 1, '2018-06-14 00:00:00.000', 'Ciemesus Name example 5', 42, '/clientImages/fikas/1.jpg'),
    (6, 3, 1, '2018-06-30 00:00:00.000', 'Ciemesus Name example 6', 1, '/clientImages/fikas/2.jpg'),
    (7, 1, 1, '2018-07-14 00:00:00.000', 'Ciemesus Name example 7', 3, '/clientImages/fikas/3.jpg'),
    (8, 2, 1, '2018-07-30 00:00:00.000', 'Ciemesus Name example 8', 2, '/clientImages/fikas/4.jpg'),
    (9, 3, 1, '2018-08-14 00:00:00.000', 'Ciemesus Name example 9', 6, '')

SET IDENTITY_INSERT dbo.Ciemesuss OFF
