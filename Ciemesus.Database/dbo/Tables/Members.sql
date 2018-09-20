create table dbo.Members
(
    MemberId int identity not null,
    UserId nvarchar(450) not null,
    FirstName nvarchar(32) not null,
    LastName nvarchar(32) not null,
    Likes int,
    Pics nvarchar(MAX)
)
go


--primary key
alter table dbo.Members
    add constraint PK_Members primary key clustered (MemberId)
go


-- foreign keys
alter table dbo.Members
	add constraint FK_Members_Users foreign key (UserId) references dbo.Users(Id)
go
