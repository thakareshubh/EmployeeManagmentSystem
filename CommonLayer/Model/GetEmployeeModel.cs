using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class GetEmployeeModel
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long PhoneNumber { get; set; }
        public string gender { get; set; }
        public long salary { get; set; }
        public string Address { get; set; }
    }
}
