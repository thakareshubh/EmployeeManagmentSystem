using BuissnessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuissnessLayer.Services
{
    public class EmployeeBl: IEmployeeBl
    {
        private readonly IEmployeeRl iEmployeeRl;

        public EmployeeBl(IEmployeeRl iEmployeeRl)
        {
            this.iEmployeeRl = iEmployeeRl;
        }

        public int deleteEmployee(int empId)
        {
            try
            {
                return this.iEmployeeRl.deleteEmployee( empId);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public AddminModel EmployeeLogin(string Email, string Password)
        {
            try
            {
                return this.iEmployeeRl.EmployeeLogin( Email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetEmployeeModel> GetAllEmployee()
        {
            try
            {
                return this.iEmployeeRl.GetAllEmployee();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetEmployeeModel GetEmployee(int empId)
        {
            try
            {
                return this.iEmployeeRl.GetEmployee(empId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmployeeRegisterModel RegisterEmployee(EmployeeRegisterModel emp)
        {
            try
            {
                return this.iEmployeeRl.RegisterEmployee(emp);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmployeeRegisterModel UpdateEmployee(int empId, EmployeeRegisterModel emp)
        {
            try
            {
                return this.iEmployeeRl.UpdateEmployee(empId,emp);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
