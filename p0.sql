--DDL

use master;
GO

create PizzaStoreDB;
GO

use PizzaStoreDB;
GO

CREATE SCHEMA [User];
GO
drop table [User].[User];

create table [User].[User](

  UserID int primary key,
  [Name] nvarchar(250) not null,
  LoginID int not null foreign key references [User].[Login](LoginID)

);
GO

create table [User].[Login](

  LoginID int primary key,
  Username nvarchar(250) not null,
  [Password] nvarchar(259) not null

);

CREATE SCHEMA Pizza;
GO

create table Pizza.Pizza(

  PizzaID int primary key,
  CrustID int null foreign key references Pizza.Crust(CrustID),
  SizeID int null foreign key references Pizza.Size(SizeID),
  [Name] nvarchar(250) not null

);

create table Pizza.Crust(

  CrustID int primary key,
  [Name] nvarchar(100) not null

);

create table Pizza.Size(

  SizeID int primary key,
  [Name] nvarchar(100) not null

);

create table Pizza.Topping(

  ToppingID int primary key,
  [Name] nvarchar(250) not null

);

create schema [Order];
go

create table [Order].[Order](

  OrderID int primary key,
  DateOrdered nvarchar(100) not null,
  Placed bit,
  Completed bit

);
go

--Order to Pizza Junction Table
create table [Order].Pizza(

  PizzaOrderID int primary key,
  OrderID int foreign key references [Order].[Order](OrderID),
  PizzaID int foreign key references Pizza.Pizza(PizzaID)

);
go

create schema Store;
go



create table Store.Store(

  StoreID int primary key,
  [Name] nvarchar(250),
  LoginID int foreign key references [User].[Login](LoginID)

);
go

--Store to Order junction table
create table Store.[Order](

  StoreOrderID int primary key,
  StoreID int foreign key references Store.Store(StoreID),
  OrderID int foreign key references [Order].[Order](OrderID)

);
go


--DML


insert into [User].[Login](LoginID, [Username], [Password])
values (1, 'jchionglo','password');

insert into [User].[User](UserID, [Name], LoginID)
values (1, 'Jeremy Chionglo', 1);

insert into [User].[Login](LoginID, [Username], [Password])
values (2, 'oakwood','password');

insert into [Store].[Store](StoreID, [Name], LoginID)
values (1, 'Oakwood', 2);

go

insert into Pizza.Topping(ToppingID, [Name])
values (1, 'pepperoni');
insert into Pizza.Topping(ToppingID, [Name])
values (2, 'ham');
insert into Pizza.Topping(ToppingID, [Name])
values (3, 'sausage');
insert into Pizza.Topping(ToppingID, [Name])
values (4, 'chicken');
insert into Pizza.Topping(ToppingID, [Name])
values (5, 'bacon');
insert into Pizza.Topping(ToppingID, [Name])
values (6, 'spinach');
insert into Pizza.Topping(ToppingID, [Name])
values (7, 'mushroom');
insert into Pizza.Topping(ToppingID, [Name])
values (8, 'pineapple');
insert into Pizza.Topping(ToppingID, [Name])
values (9, 'onion');
insert into Pizza.Topping(ToppingID, [Name])
values (10, 'green pepper');
go

insert into Pizza.Crust(CrustID, [Name])
values (1, 'handtossed');
insert into Pizza.Crust(CrustID, [Name])
values (2, 'thin');
insert into Pizza.Crust(CrustID, [Name])
values (3, 'stuffed');
go

insert into Pizza.Size(SizeID, [Name])
values (1, 'small');
insert into Pizza.Size(SizeID, [Name])
values (2, 'medium');
insert into Pizza.Size(SizeID, [Name])
values (3, 'large');
go
