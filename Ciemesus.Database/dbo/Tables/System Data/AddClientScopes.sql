print 'inserting into dbo.ClientScopes'

insert into dbo.ClientScopes
    (ClientId, Scope)
values
    (1, 'Ciemesus.Api.Read'),
    (1, 'profile'),
    (1, 'openid'),
    (1, 'email');
