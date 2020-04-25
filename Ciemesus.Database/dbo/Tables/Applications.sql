create table dbo.Applications
(
    [Application] nvarchar(50) not null,
    ApplicationName nvarchar(200) not null
)
go

-- primary key
alter table dbo.Applications
    add constraint PK_Applications primary key clustered ([Application])
go
