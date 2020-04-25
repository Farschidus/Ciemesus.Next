create table dbo.LocationPeopleStatus
(
    LocationId int not null,
    LowUrgencyThreshold int not null,
    HighUrgencyThreshold int not null,
    LocationStatus nvarchar(6) null,
    LocationStatusUpdatedAt datetime null
)
go

-- primary key
alter table dbo.LocationPeopleStatus
    add constraint PK_LocationPeopleStatus primary key clustered (LocationId)
go

-- foreign key
alter table dbo.LocationPeopleStatus
    add constraint FK_LocationPeopleStatus_Locations foreign key (LocationId) references dbo.Locations(LocationId)
go

alter table dbo.LocationPeopleStatus
    add constraint FK_LocationPeopleStatus_LocationStatuses foreign key (LocationStatus) references dbo.LocationStatuses(LocationStatus)
go

-- index
create nonclustered index IX_LocationPeopleStatus_LocationId on dbo.LocationPeopleStatus(LocationId)
go

create nonclustered index IX_LocationPeopleStatus_LocationStatus on dbo.LocationPeopleStatus(LocationStatus)
go
