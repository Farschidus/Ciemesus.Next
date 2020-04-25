create table dbo.ApplicationRoles
(
    [Application] nvarchar(50) not null,
    [Role] nvarchar(50) not null,
    RoleName nvarchar(200) not null
)
go

-- primary key
alter table dbo.ApplicationRoles
    add constraint PK_ApplicationRoles primary key clustered ([Application], [Role])
go

-- foreign key
alter table dbo.ApplicationRoles
    add constraint FK_ApplicationRoles_Applications foreign key ([Application]) references dbo.Applications ([Application])
        on update cascade
go

-- index
create nonclustered index IX_ApplicationRoles_Applications on dbo.Applications ([Application])
go
