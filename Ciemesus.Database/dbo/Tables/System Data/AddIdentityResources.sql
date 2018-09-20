print 'inserting into dbo.IdentityResources'

set identity_insert dbo.IdentityResources on

insert into dbo.IdentityResources
    (Id, Description, DisplayName, Emphasize, Enabled, Name, Required, ShowInDiscoveryDocument)
values
    (1, null, 'Your user identifier', 0, 1, 'openid', 0, 1),
    (2, 'Your user profile information (first name, last name, etc.)', 'User profile', 1, 1, 'profile', 0, 1),
    (3, null, 'Your email address', 1, 1, 'email', 0, 1);

set identity_insert dbo.IdentityResources off