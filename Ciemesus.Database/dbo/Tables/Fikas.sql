create table dbo.Ciemesuss
(
    CiemesusId int identity not null,
    MemberId int not null,
    TeamId int not null,
    CiemesusName nvarchar(128) not null,
    TakenDate datetime null,
    Likes int,
    Pics nvarchar(MAX)
)
go


--primary key
alter table dbo.Ciemesuss
    add constraint PK_Ciemesuss primary key clustered (CiemesusId)
go


-- foreign keys
alter table dbo.Ciemesuss
	add constraint FK_Ciemesuss_Members foreign key (MemberId) references dbo.Members(MemberId)
go

alter table dbo.Ciemesuss
	add constraint FK_Ciemesuss_Teams foreign key (TeamId) references dbo.Teams(TeamId)
go
