using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminRl
    {
        public AddminModel AdminLogin(string Email, string Password);
    }
}
