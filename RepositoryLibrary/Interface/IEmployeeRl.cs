using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IEmployeeRl
    {
        public EmployeeRegisterModel RegisterEmployee(EmployeeRegisterModel emp);
        public EmployeeRegisterModel UpdateEmployee(int empId, EmployeeRegisterModel emp);
        public int deleteEmployee(int empId);
        public List< GetEmployeeModel >GetAllEmployee();

        public AddminModel EmployeeLogin(string Email, string Password);

        public GetEmployeeModel GetEmployee(int empId);


    }
}
