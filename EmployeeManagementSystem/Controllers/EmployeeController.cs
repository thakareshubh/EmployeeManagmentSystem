using BuissnessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EmployeeManagementSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBl iEmployeeBl;

        public EmployeeController(IEmployeeBl iEmployeeBl)
        {
            this.iEmployeeBl = iEmployeeBl;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("RegisterEmployee")]
        public ActionResult RegisterEmployee(EmployeeRegisterModel emp)
        {
            try
            {
                var result = this.iEmployeeBl.RegisterEmployee( emp);
                if (result != null)
                {
                    return this.Ok(new  { status = true, message = $"Register Successful", Data = result });

                }
                return this.BadRequest(new { status = true, message = $"Register Failed", Data = result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("UpdateEmployee")]
        public ActionResult UpdateEmployee(int empId, EmployeeRegisterModel emp)
        {
            try
            {
                var result = this.iEmployeeBl.UpdateEmployee(empId, emp);
                if (result != null)
                {
                    return this.Ok(new { status = true, message = $"Register Successful", Data = result });

                }
                return this.BadRequest(new { status = true, message = $"Register Failed", Data = result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetAllEmployee")]
        public ActionResult GetAllEmployee()
        {
            try
            {
                var result = this.iEmployeeBl.GetAllEmployee( );
                if (result != null)
                {
                    return this.Ok(new { status = true, message = $"Get all emp Successful", Data = result });

                }
                return this.BadRequest(new { status = true, message = $"Get employee Failed", Data = result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetEmployee")]
        public ActionResult GetEmployee()
        {
            try
            {
                int empId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.iEmployeeBl.GetEmployee( empId);
                if (result != null)
                {
                    return this.Ok(new { status = true, message = $"Get all emp Successful", Data = result });

                }
                return this.BadRequest(new { status = true, message = $"Get employee Failed", Data = result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("deleteEmployee")]
        public ActionResult deleteEmployee(int empId)
        {
            try
            {
                var result = this.iEmployeeBl.deleteEmployee( empId);
                if (result != 0)
                {
                    return this.Ok(new { status = true, message = $"Get all emp Successful", Data = result });

                }
                return this.BadRequest(new { status = true, message = $"Get employee Failed", Data = result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("EmployeeLogin")]
        public ActionResult EmployeeLogin(string Email, string Password)
        {
            try
            {
                var result = this.iEmployeeBl.EmployeeLogin(Email,  Password);
                if (result != null)
                {
                    return this.Ok(new { status = true, message = $"login  Successful", Data = result });

                }
                return this.BadRequest(new { status = true, message = $"login Failed", Data = result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
