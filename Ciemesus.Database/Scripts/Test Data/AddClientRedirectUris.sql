print 'inserting into dbo.ClientRedirectUris'

insert into dbo.ClientRedirectUris
    (ClientId, RedirectUri)
values
    (1, 'http://localhost:3000/signin-oidc');
