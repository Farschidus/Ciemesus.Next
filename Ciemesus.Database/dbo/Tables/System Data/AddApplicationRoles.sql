print 'inserting into dbo.ApplicationRoles'

insert into dbo.ApplicationRoles
    ([Role], RoleName, [Application])
values
    ('SuperAdministrator', 'Super Administrator', 'AdminOffice'),
    ('Administrator', 'Administrator', 'AdminOffice'),
    ('PublicAdministrator', 'Public Administrator', 'PublicView'),
    ('Visitor', 'Visitor', 'PublicView')
