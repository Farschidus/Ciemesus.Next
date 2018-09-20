create table dbo.TeamMembers
(
    TeamId int not null,
    MemberId int not null,
    MemberOrder int
)
go


--primary key
alter table dbo.TeamMembers
    add constraint PK_TeamMembers primary key clustered (TeamId, MemberId)
go


-- foreign keys
alter table dbo.TeamMembers
	add constraint FK_TeamMembers_Teams foreign key (TeamId) references dbo.Teams(TeamId)
    on delete cascade
go

alter table dbo.TeamMembers
	add constraint FK_TeamMembers_Members foreign key (MemberId) references dbo.Members(MemberId)
    on delete cascade
go
