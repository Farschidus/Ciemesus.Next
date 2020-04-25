create table dbo.Timezones
(
    TimezoneId nvarchar(50) not null,
    BaseUtcOffsetInMinutes int not null,
    DisplayName nvarchar(100) not null
)
go

--primary key
alter table dbo.Timezones
    add constraint PK_Timezones primary key clustered (TimezoneId)
go
