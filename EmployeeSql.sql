create table Emoloyee
(
EmpId int primary key identity(1,1),
FullName varchar(255),
Email varchar(255),
Password varchar(255),
PhoneNumber Bigint,
gender varchar(5),
salary bigint ,
Address varchar(100),

);


Alter proc SpRegister
(
@FullName varchar(255),
@Email varchar(255),
@Password varchar(255),
@PhoneNumber Bigint,
@gender varchar(5),
@salary bigint ,
@Address varchar(100)
)
as
begin 
       insert into Emoloyee (FullName,Email,Password,PhoneNumber,gender,salary,Address)
	   values (@FullName,@Email,@Password,@PhoneNumber,@gender,@salary,@Address);
end;

create procedure Updatebook
(
@EmpId int,
@FullName varchar(255),
@Email varchar(255),
@Password varchar(255),
@PhoneNumber Bigint,
@gender varchar(5),
@salary bigint ,
@Address varchar(100)
)
as
begin
update Emoloyee set 
FullName=@FullName,
Email=@Email,
Password=@Password,
PhoneNumber=@PhoneNumber,
gender=@gender,
salary=@salary,
Address=@Address
where EmpId=@EmpId			
end;

-----Deleteting employee from table---
create procedure Deletebook
(
@EmpId int
)
as
begin
delete from Emoloyee Where EmpId=@EmpId
end;


-----Get All Emoloyee from table---

create proc GetAllEmployee
as 
begin
select * from Emoloyee
end;

create procedure EmployeeLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
BEGIN
select * from Emoloyee where Email = @Email and Password = @Password
end;


create proc EmployeeByEmpId
(
@EmpId int
)
as 
begin
select * from Emoloyee where EmpId=@EmpId
end;