print 'inserting into dbo.IdentityClaims'

insert into dbo.IdentityClaims
    (IdentityResourceId, Type)
values
    (1, 'sub'),
    (2, 'name'),
    (2, 'family_name'),
    (2, 'given_name'),
    (2, 'family_name'),
    (2, 'middle_name'),
    (2, 'nickname'),
    (2, 'preferred_username'),
    (2, 'profile'),
    (2, 'picture'),
    (2, 'website'),
    (2, 'gender'),
    (2, 'birthdate'),
    (2, 'zoneinfo'),
    (2, 'locale'),
    (2, 'updated_at'),
    (3, 'email'),
    (3, 'email_verified');
