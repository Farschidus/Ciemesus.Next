create table dbo.Products
(
    ProductId int identity(1, 1) not null,
    DispenserType nvarchar(20) not null,
    DesignLine nvarchar(50) not null,
    SensorType nvarchar(50) not null,
    ProductName nvarchar(200) not null,
    NumberOfSensors int not null,
    Color nvarchar(20) null,
    RefillAmount int not null,
    RefillHalfAmount int not null
)
go

--primary key
alter table dbo.Products
    add constraint PK_Products primary key clustered (ProductId)
go

-- foreign keys
alter table dbo.Products
    add constraint FK_Products_DispenserTypes foreign key (DispenserType) references dbo.DispenserTypes(DispenserType)
go

alter table dbo.Products
    add constraint FK_Products_DesignLines foreign key (DesignLine) references dbo.DesignLines(DesignLine)
go

alter table dbo.Products
    add constraint FK_Products_SensorTypes foreign key (SensorType) references dbo.SensorTypes(SensorType)
go

-- indexes
create nonclustered index IX_Products_DispenserType on dbo.Products(DispenserType)
go

create nonclustered index IX_Products_DesignLine on dbo.Products(DesignLine)
go

create nonclustered index IX_Products_SensorType on dbo.Products(SensorType)
go
