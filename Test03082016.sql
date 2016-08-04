create database Accounting_030820162
GO

use Accounting_030820162
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



