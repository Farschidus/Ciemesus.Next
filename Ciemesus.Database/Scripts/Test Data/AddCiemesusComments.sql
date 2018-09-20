print 'inserting into dbo.CiemesusComments'

SET IDENTITY_INSERT dbo.CiemesusComments ON

insert into dbo.CiemesusComments
    (CiemesusCommentId, CiemesusId, MemberId, Comment, Likes, CommentDate)
values
    (1, 1, 2, 'This is a Comment from Farschidus', 24, '2018-04-14 08:30:15.000'),
    (2, 1, 3, 'This is a Comment from Seb Kond', 0, '2018-04-14 09:02:15.000'),
    (3, 1, 2, 'This is a Comment from Farschidus', 2, '2018-04-14 09:12:15.000'),
    (4, 2, 3, 'This is a Comment from Seb Kond', 6, '2018-04-14 09:15:15.000'),
    (5, 3, 1, 'This is a Comment from Admin', 4, '2018-04-15 08:30:15.000')

set identity_insert dbo.CiemesusComments OFF
