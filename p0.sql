--DDL

use master;
GO

create PizzaStoreDB;
GO

use PizzaStoreDB;
GO

CREATE SCHEMA [User];
GO

create table [User].[User](

  UserID int identity(1,1) primary key,
  firstName nvarchar(250) not null,
  lastName nvarchar(250) not null,
  LoginID int not null foreign key references [User].[Login](LoginID)

);
GO

create table [User].[Login](

  LoginID int identity(1,1) primary key,
  Username nvarchar(250) not null,
  [Password] nvarchar(259) not null

);

CREATE SCHEMA Pizza;
GO

create table Pizza.Pizza(

  PizzaID int identity(1,1) primary key,
  CrustID int null foreign key references Pizza.Crust(CrustID),
  SizeID int null foreign key references Pizza.Size(SizeID),
  [Name] nvarchar(250) not null

);

create table Pizza.Crust(

  CrustID int identity(1,1) primary key,
  [Name] nvarchar(100) not null

);

create table Pizza.Size(

  SizeID int identity(1,1) primary key,
  [Name] nvarchar(100) not null

);

create table Pizza.Topping(

  ToppingID int identity(1,1) primary key,
  [Name] nvarchar(250) not null

);

create schema [Order];
go

create table [Order].[Order](

  OrderID int identity(1,1) primary key,
  DateOrdered nvarchar(100) not null,
  UserID int foreign key references [User].[User](UserID),
  StoreID int foreign key references Store.Store(StoreID),
  Placed bit,
  Completed bit

);
go

--Order to Pizza Junction Table
create table [Order].Pizza(

  PizzaOrderID int identity(1,1) primary key,
  OrderID int foreign key references [Order].[Order](OrderID),
  PizzaID int foreign key references Pizza.Pizza(PizzaID)

);
go

--Pizza to Topping Junction Table
create table Pizza.PizzaTopping(

  PizzaToppingID int identity(1,1) primary key,
  PizzaID int foreign key references Pizza.Pizza(PizzaID),
  ToppingID int foreign key references Pizza.Topping(ToppingID)

);
go

create schema Store;
go



create table Store.Store(

  StoreID int identity(1,1) primary key,
  [Name] nvarchar(250),
  LoginID int foreign key references [User].[Login](LoginID)

);
go

--Store to Order junction table
create table Store.[Order](

  StoreOrderID int identity(1,1) primary key,
  StoreID int foreign key references Store.Store(StoreID),
  OrderID int foreign key references [Order].[Order](OrderID)

);
go


--DML


insert into [User].[Login]([Username], [Password])
values ('jchionglo','password');

insert into [User].[User](firstName, lastName, LoginID)
values ('Jeremy', 'Chionglo', 1);

insert into [User].[Login]([Username], [Password])
values ('oakwood','password');

insert into [Store].[Store]([Name], LoginID)
values ('Oakwood Branch', 2);

insert into [User].[Login]([Username], [Password])
values ('elston','password');

insert into [Store].[Store]([Name], LoginID)
values ('Elston Branch', 3);

go

insert into Pizza.Topping([Name])
values ('pepperoni');
insert into Pizza.Topping([Name])
values ('ham');
insert into Pizza.Topping([Name])
values ('sausage');
insert into Pizza.Topping([Name])
values ('chicken');
insert into Pizza.Topping([Name])
values ('bacon');
insert into Pizza.Topping([Name])
values ('spinach');
insert into Pizza.Topping([Name])
values ('mushroom');
insert into Pizza.Topping([Name])
values ('pineapple');
insert into Pizza.Topping([Name])
values ('onion');
insert into Pizza.Topping([Name])
values ('green pepper');
go

insert into Pizza.Crust([Name])
values ('handtossed');
insert into Pizza.Crust([Name])
values ('thin');
insert into Pizza.Crust([Name])
values ('stuffed');
go

insert into Pizza.Size([Name])
values ('small');
insert into Pizza.Size([Name])
values ('medium');
insert into Pizza.Size([Name])
values ('large');
go

insert into Pizza.Pizza(CrustID, SizeID, Name)
values (1, 2, 'Meat Lovers');
insert into Pizza.Pizza(CrustID, SizeID, Name)
values (1, 2, 'Fred Special');
insert into Pizza.Pizza(CrustID, SizeID, Name)
values (1, 2, 'Jeremy Special');
insert into Pizza.Pizza(CrustID, SizeID, Name)
values (1, 2, 'Deluxe');
go

insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (1,1);
insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (1,2);
insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (1,3);
go

insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (2,2);
insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (2,8);
go

insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (3,5);
insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (3,8);
go

insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (4,1);
insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (4,3);
insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (4,7);
insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (4,10);
insert into Pizza.PizzaTopping(PizzaID, ToppingID)
values (4,9);
go

--Sample Order
insert into [Order].[Order](DateOrdered, UserID, StoreID, Placed, Completed)
values ('07/27/2020 14:36:08', 1, 1, 1, 0)
go

insert into Store.[Order](StoreID, OrderID)
values (1,1)
go

insert into [Order].Pizza(OrderID, PizzaID)
values (1,1);
go

select * from [Order].Pizza;

drop table [Order].Pizza;
drop table Pizza.PizzaTopping;
drop table Store.[Order];
drop table [Order].[Order];
drop table Store.Store;
drop table [User].[User];
drop table [User].[Login];
drop table Pizza.Pizza;
drop table Pizza.Crust;
drop table Pizza.Size;

select * from [Order].[Order];
select * from [Order].Pizza;
select * from Pizza.Crust;
select * from Pizza.Pizza;
select * from Pizza.PizzaTopping;
select * from Pizza.Size;
select * from Pizza.Topping;
select * from Store.[Order];
select * from Store.Store;
select * from [User].[User];
select * from [User].[Login];
go