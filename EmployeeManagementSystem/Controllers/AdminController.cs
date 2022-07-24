using BuissnessLayer.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminBl iadminBl;

        public AdminController(IAdminBl iadminBl)
        {
            this.iadminBl = iadminBl;
        }
        [HttpPost("login")]
        public ActionResult AdminLogin(string Email, string Password)
        {
            try
            {
                var result = this.iadminBl.AdminLogin(Email, Password);
                if (result != null)
                {
                    return this.Ok(new { status = true, message = $"Login Successful", Data = result });

                }
                return this.BadRequest(new  { status = true, message = "Login Failed", Data = result });

            }
            catch
            {

                throw;
            }
        }
    }
}
