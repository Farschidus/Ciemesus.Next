print 'inserting into dbo.ApiResources'

set identity_insert dbo.ApiResources on

insert into dbo.ApiResources
    (Id, Description, DisplayName, Enabled, Name)
values
    (1, 'Ciemesus API Application', 'Ciemesus - API', 1, 'Ciemesus.Api');

set identity_insert dbo.ApiResources off
