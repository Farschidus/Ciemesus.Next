create table dbo.Calendar
(
    [Date] date not null
)
go

-- primary key
alter table dbo.Calendar
    add constraint PK_Calendar primary key clustered ([Date])
go

