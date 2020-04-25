create table dbo.LocationStatuses
(
    LocationStatus nvarchar(6) not null
)
go

-- primary key
alter table dbo.LocationStatuses
    add constraint PK_LocationStatuses primary key clustered (LocationStatus)
go
