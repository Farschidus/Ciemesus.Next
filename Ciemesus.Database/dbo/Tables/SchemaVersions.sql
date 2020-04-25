create table dbo.SchemaVersions
(
    Id int identity(1, 1) not null,
    ScriptName nvarchar(255) not null,
    Applied datetime not null
)
go

alter table dbo.SchemaVersions
    add constraint PK_SchemaVersions primary key clustered (Id)
go
