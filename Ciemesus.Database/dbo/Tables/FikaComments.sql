create table dbo.CiemesusComments
(
    CiemesusCommentId int identity not null,
    CiemesusId int not null,
    MemberId int not null,
    Comment nvarchar(512) not null,
    Likes int,
    CommentDate datetime NOT null
)
go


--primary key
alter table dbo.CiemesusComments
    add constraint PK_CiemesusComments primary key clustered (CiemesusCommentId)
go


-- foreign keys
alter table dbo.CiemesusComments
	add constraint FK_CiemesusComments_Ciemesuss foreign key (CiemesusId) references dbo.Ciemesuss(CiemesusId)
go

alter table dbo.CiemesusComments
	add constraint FK_CiemesusComments_Members foreign key (MemberId) references dbo.Members(MemberId)
go


