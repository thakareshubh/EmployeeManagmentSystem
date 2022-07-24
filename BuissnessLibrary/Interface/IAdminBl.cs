using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuissnessLayer.Interface
{
    public interface IAdminBl
    {
        public AddminModel AdminLogin(string Email, string Password);
    }
}
