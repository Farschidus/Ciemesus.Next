print 'inserting into dbo.Members'

SET IDENTITY_INSERT dbo.Members ON

insert into dbo.Members
    (MemberId, UserId, FirstName, LastName, Likes, Pics)
VALUES
    (1, '2E9646C4-8CF6-46FF-BAD6-50B6B9C5CE8E', 'Super', 'Admin', 37, '/clientImages/profiles/default.jpg'),
    (2, 'D9C4C07F-02B4-44F7-A64E-E0D7CC3604C5', 'Farschidus', 'Ghavanini', 5505, '/clientImages/profiles/farschidus.jpg'),
    (3, '495840D2-1F8F-40A5-AEDC-42BAAEAEEB5F', 'Seb', 'Kond', 0, '/clientImages/profiles/Seb.kond.jpg')

SET IDENTITY_INSERT dbo.Members OFF
