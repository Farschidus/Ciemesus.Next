create table dbo.Teams
(
    TeamId int identity not null,
    TeamName nvarchar(128) not null,
    StartedAt datetime NOT null,
    interval int,
    Pics nvarchar(MAX)
)
go


--primary key
alter table dbo.Teams
    add constraint PK_Teams primary key clustered (TeamId)
go
