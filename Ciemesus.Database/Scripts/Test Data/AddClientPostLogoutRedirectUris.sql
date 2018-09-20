print 'inserting into dbo.ClientPostLogoutRedirectUris'

insert into dbo.ClientPostLogoutRedirectUris
    (ClientId, PostLogoutRedirectUri)
values
    (1, 'http://localhost:3000/signout-oidc');
