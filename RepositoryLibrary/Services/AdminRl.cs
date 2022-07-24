using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRl : IAdminRl
    {
        private SqlConnection SqlConnection;

        private IConfiguration configuration { get; }

        public AdminRl(IConfiguration configuration)
        {
            this.configuration = configuration;
          
        }
        public AddminModel AdminLogin(string Email, string Password)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);
            try
            {
                if (Email == null || Password == null)
                {
                    return null;
                }
                else
                {
                    using (SqlConnection)
                    {
                        SqlCommand cmd = new SqlCommand("AdminLogin", SqlConnection);
                        { cmd.CommandType = CommandType.StoredProcedure; }

                        AddminModel model = new AddminModel();

                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Password", Password);

                        SqlConnection.Open();

                        SqlDataReader sdr = cmd.ExecuteReader();
                        ///ExecuteReader method is used to execute a SQL Command or storedprocedure returns a set of rows from the database.
                        ///


                        if (sdr.HasRows)
                        {
                            ///The HasRows property returns information about the current result set.

                            int AdminId = 1;

                            while (sdr.Read())
                            {


                                model.Email = Convert.ToString(sdr["Email"]);
                                model.Password = Convert.ToString(sdr["Password"]);
                                AdminId = Convert.ToInt32(sdr["AdminId"]);



                            }
                            this.SqlConnection.Close();
                            model.Token = this.GenerateSecurityToken(model.Email, AdminId);

                            return model;

                        }

                        else
                        {
                            this.SqlConnection.Close();
                            return null;
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateSecurityToken(string emailID, int AdminId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN"));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Email, emailID),
                new Claim("AdminId", AdminId.ToString())
            };
            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
