create table dbo.Users
(
    UserId int identity(1,1) not null,
    [Name] nvarchar(256) not null,
    Email nvarchar(256) null,
    Username nvarchar(256) null,
    IdentityProviderUserId uniqueidentifier null,
    IsActive bit not null,
    CanCreateApiKeys bit not null, 
    Locale nvarchar(100) null
)
go

-- primary key
alter table dbo.Users
    add constraint PK_Users primary key clustered (UserId)
go

-- constraint

alter table dbo.Users
    add constraint DF_Users_IsActive default 0 for IsActive
go

alter table dbo.Users
    add constraint DF_Users_CanCreateApiKeys default 0 for CanCreateApiKeys
go

alter table dbo.Users
    add constraint OneIsSet_Email_Username check (Email is not null or Username is not null)
go

-- index
create unique nonclustered index IX_UQ_Users_Email on dbo.Users (Email)
    where Email is not null
go

create unique nonclustered index IX_UQ_Users_Username on dbo.Users (Username)
    where Username is not null
go

create unique nonclustered index IX_UQ_Users_IdentityProviderUserId on dbo.Users (IdentityProviderUserId)
    where IdentityProviderUserId is not null
go
