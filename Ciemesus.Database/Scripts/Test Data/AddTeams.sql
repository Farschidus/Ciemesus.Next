print 'inserting into dbo.Teams'

SET IDENTITY_INSERT dbo.Teams ON

insert into dbo.Teams
    (TeamId, TeamName, StartedAt, interval, Pics)
values
    (1, 'Ciemesus Team', '2018-04-01 00:00:00.000', 14, null)

SET IDENTITY_INSERT dbo.Teams OFF
