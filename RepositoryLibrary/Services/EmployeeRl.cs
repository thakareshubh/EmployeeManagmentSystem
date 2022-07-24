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
    public class EmployeeRl: IEmployeeRl
    {
        private SqlConnection SqlConnection;

        private IConfiguration configuration { get; }

        public EmployeeRl(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        public EmployeeRegisterModel RegisterEmployee(EmployeeRegisterModel emp)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SpRegister", SqlConnection); ;// connection with store procedure


                    SqlConnection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Password", emp.Password);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@gender", emp.gender);
                    cmd.Parameters.AddWithValue("@salary", emp.salary);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                   


                    int result = cmd.ExecuteNonQuery();
                    ///ExecuteNonQuery method is used to execute SQL Command or the storeprocedure performs, INSERT, UPDATE or Delete operations.
                    ///It doesn't return any data from the database.
                    ///Instead, it returns an integer specifying the number of rows inserted, updated or deleted.


                    SqlConnection.Close();
                    if (result != 0)
                    {
                        return emp;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public EmployeeRegisterModel UpdateEmployee(int empId, EmployeeRegisterModel emp)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("Updatebook", SqlConnection); ;// connection with store procedure


                    SqlConnection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId ", empId);
                    cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Password", emp.Password);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@gender", emp.gender);
                    cmd.Parameters.AddWithValue("@salary", emp.salary);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);

                    int result = cmd.ExecuteNonQuery();

                    SqlConnection.Close();

                    if (result != 0)
                    {
                        return emp;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int deleteEmployee(int empId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);



            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("Deletebook", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpId ", empId);


                    SqlConnection.Open();

                    int result = cmd.ExecuteNonQuery();

                    SqlConnection.Close();

                    if (result == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return empId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<GetEmployeeModel> GetAllEmployee()
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);
            List<GetEmployeeModel> empList = new List<GetEmployeeModel>();

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetAllEmployee", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlConnection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            empList.Add(new GetEmployeeModel
                            {
                                EmployeeID = Convert.ToInt32(reader["EmpId"]),
                                FullName = reader["FullName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                PhoneNumber = Convert.ToInt64(reader["PhoneNumber"]),
                                gender = reader["gender"].ToString(),
                                Address = reader["Address"].ToString(),

                                salary = Convert.ToInt32(reader["salary"]),
                               
                            });

                        }
                        SqlConnection.Close();
                        return empList;
                    }


                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddminModel EmployeeLogin(string Email, string Password)
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
                        SqlCommand cmd = new SqlCommand("EmployeeLogin", SqlConnection);
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

                            int EmpId =0;

                            while (sdr.Read())
                            {


                                model.Email = Convert.ToString(sdr["Email"]);
                                model.Password = Convert.ToString(sdr["Password"]);
                                EmpId = Convert.ToInt32(sdr["EmpId"]);



                            }
                            this.SqlConnection.Close();
                            model.Token = this.GenerateSecurityToken(model.Email, EmpId);

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

        public string GenerateSecurityToken(string emailID, int UserId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN"));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.Email, emailID),
                new Claim("UserId", UserId.ToString())
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

        public GetEmployeeModel GetEmployee(int empId)
        {
            SqlConnection = new SqlConnection(this.configuration["ConnectionString:BookStoreConnection"]);

            try
            {
                using (SqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("EmployeeByEmpId", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpId ", empId);


                    SqlConnection.Open();




                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        GetEmployeeModel model = new GetEmployeeModel();
                        while (reader.Read())
                        {
                              empId = Convert.ToInt32(reader["EmpId"]);

                            model.FullName = reader["FullName"].ToString();
                            model.Email = reader["Email"].ToString();
                            model.Password = reader["Password"].ToString();
                            model.PhoneNumber = Convert.ToInt64(reader["PhoneNumber"]);
                            model.gender = reader["gender"].ToString();
                            model.Address = reader["Address"].ToString();

                            model.salary = Convert.ToInt32(reader["salary"]);
                        }
                        SqlConnection.Close();
                        return model;
                    }


                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
