create table dbo.UserApplicationRoles
(
    UserId int not null,
    [Application] nvarchar(50) not null,
    [Role] nvarchar(50) not null
)
go

-- primary keys
alter table dbo.UserApplicationRoles
    add constraint PK_UserApplicationRoles primary key clustered (UserId, [Application], [Role])
go

-- foreign keys
alter table dbo.UserApplicationRoles
    add constraint FK_UserApplicationRoles_Users foreign key (UserId) references dbo.Users (UserId)
        on delete cascade
go

alter table dbo.UserApplicationRoles
    add constraint FK_UserApplicationRoles_ApplicationRoles foreign key ([Application], [Role])
        references dbo.ApplicationRoles ([Application], [Role])
            on delete cascade
go

-- index
create unique index IX_Users_Applications on dbo.UserApplicationRoles (UserId, [Application])
go
