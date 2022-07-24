create database EmployeeManagement

-----------------------------------------
-------------Admin Table-----------------
----------------------------------------------
create Table AdminTable
(
AdminId int primary key identity(1,1),
FullName varchar(255),
Email varchar(255),
Password varchar(255),
PhoneNumber Bigint
);

Insert into AdminTable values('Admin', 'shubhamthakare@gmail.com', 'shubh@123', '7028873490');

select * from AdminTable

create procedure AdminLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
BEGIN
If(Exists(select * from AdminTable where Email = @Email and Password = @Password))
Begin
select AdminId, FullName,Password, Email, PhoneNumber from AdminTable;
end
Else
Begin
select 2;
End
END;

