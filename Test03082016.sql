create database Accounting_030820162
GO

use Accounting_0308201624
create table Customers(
CustomerId int not null primary key IDENTITY(1,1),
Name nvarchar(30) not null,
Customer_Address nvarchar(500) not null,
PhoneNum nvarchar(20) not null)
GO

use Accounting_030820162
create table Orders(
OrderId int not null primary key IDENTITY(1,1),
CustomerId int not null,
Number nvarchar(50) not null,
Amount int not null,
DueTime smalldatetime,
ProcessedTime smalldatetime,
Order_Description nvarchar(1000),
FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId))
GO


use Accounting_030820162
insert into Customers values
('User1','Shevchenka 18','0961234567'),
('User2','Poshtova 7','0961779467'),
('User3','Shuliavska 75','0964400567'),
('User4','Saksaganskogo 71','0961290567'),
('User5','Varash 2','0961234123'),
('User6','Peremogy 2','0961234321')
GO

use Accounting_030820162
insert into Orders values
(1,'12345',20,'20160613 10:34:09 AM','20160308 10:34:09 AM','Description 1'),
(3,'54321',15,'20160717 11:33:09 AM','20160308 10:34:09 AM','Description 2'),
(1,'24689',10,'20160713 11:32:09 AM','20160308 10:34:09 AM','Description 3'),
(1,'13579',70,'20160725 11:24:09 AM','20160308 10:34:09 AM','Description 4'),
(2,'22222',15,'20160727 10:34:09 AM','20160308 10:34:09 AM','Description 5'),
(4,'12345',20,'20160617 10:34:09 AM','20160308 10:34:09 AM','Description 6')
GO
