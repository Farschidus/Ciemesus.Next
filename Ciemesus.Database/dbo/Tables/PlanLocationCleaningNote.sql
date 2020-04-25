create table dbo.PlanLocationCleaningNote
(
    CleaningNoteId int not null,
    PlanId int not null,
    LocationId int not Null
)
go

-- primary keys
alter table dbo.PlanLocationCleaningNote
    add constraint PK_PlanLocationCleaningNote primary key clustered (CleaningNoteId, PlanId, LocationId)
go

-- foreign keys
alter table dbo.PlanLocationCleaningNote
    add constraint FK_PlanLocationCleaningNote_CleaningNotes foreign key (CleaningNoteId) references dbo.CleaningNotes(CleaningNoteId)
        on delete cascade
go

alter table dbo.PlanLocationCleaningNote
    add constraint FK_PlanLocationCleaningNote_Plans foreign key (PlanId) references dbo.Plans(PlanId)
        on delete cascade
go

alter table dbo.PlanLocationCleaningNote
    add constraint FK_PlanLocationCleaningNote_Locations foreign key (LocationId) references dbo.Locations(LocationId)
        on delete cascade
go
