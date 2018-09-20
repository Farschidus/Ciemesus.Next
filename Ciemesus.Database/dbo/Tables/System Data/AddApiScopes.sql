print 'inserting into dbo.ApiScopes'

insert into dbo.ApiScopes
    (ApiResourceId, Description, DisplayName, Emphasize, Name, Required, ShowInDiscoveryDocument)
values
    (1, 'Ciemesus API read access', 'Ciemesus API - Read', 0, 'Ciemesus.Api.Read', 0, 1);
