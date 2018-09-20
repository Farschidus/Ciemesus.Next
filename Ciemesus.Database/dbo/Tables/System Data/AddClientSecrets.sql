print 'inserting into dbo.AddClientSecrets'

insert into dbo.ClientSecrets
    (ClientId, Description, Expiration, Type, Value)
values
    (1, 'Check Seed Script', null, 'SharedSecret', 'Vuh7vqpngRp7eFRDEPyPAj0IHa+Cvl5laoK0PMJVSwE=');
