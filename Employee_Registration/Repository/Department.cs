using Employee_Registration.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace Employee_Registration.Repository
{
    public class Department
    {
        
        public string AddDepartmentSumDetails(DepartSum depart)
        {
            SqlConnection con = null;
            string result;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString());
                SqlCommand com = new SqlCommand("sp_DepartmentSum", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Query", 1);
                com.Parameters.AddWithValue("@DepartmentName", depart.DepartmentName);
                com.Parameters.AddWithValue("@DateOfCreation", depart.DateOfCreation);
                com.Parameters.AddWithValue("@Description", depart.Description);
                com.Parameters.AddWithValue("@Status", depart.Status);
                
                con.Open();
                result = com.ExecuteNonQuery().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }


        public string AddDepartmentDetails(DepartDet departdet)
        {
            SqlConnection con = null;
            string result;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString());
                SqlCommand com = new SqlCommand("sp_DepartmentDetails", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Query", 1);
                com.Parameters.AddWithValue("@DepartmentID", departdet.DepartmentID);
                com.Parameters.AddWithValue("@SlNo", departdet.SLNo);
                com.Parameters.AddWithValue("@EmployeeID", departdet.EmloyeeID);
                com.Parameters.AddWithValue("@DateOfJoining", departdet.DateOfJoining);
                com.Parameters.AddWithValue("@ContractPeriod", departdet.ContractPeriod);

                con.Open();
                result = com.ExecuteNonQuery().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        public string AddDepartment(DepartSum depart)
        {
            string resultSum = AddDepartmentSumDetails(depart);
            

            // Check if both operations were successful
            if (!string.IsNullOrEmpty(resultSum))
            {
                // Both operations succeeded
                return "Department added successfully.";
            }
            else
            {
                // Error occurred while saving data
                return "";
            }
        }

        public IEnumerable<DepartSum> GetDepartments()
        {
            List<DepartSum> departments = new List<DepartSum>();
            string query = "SELECT DepartmentID, DepartmentName, DateOfCreation, Description, Status FROM Dep_Sum";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DepartSum department = new DepartSum
                    {
                        DepartmentID = rdr["DepartmentID"].ToString(),
                        DepartmentName = rdr["DepartmentName"].ToString(),
                        DateOfCreation = Convert.ToDateTime(rdr["DateOfCreation"]),
                        Description = rdr["Description"].ToString(),
                        Status = rdr["Status"].ToString()
                    };
                    departments.Add(department);
                }
                con.Close();
            }

            return departments;
        }

        public IEnumerable<SelectListItem> GetEmployees()
        {
            List<SelectListItem> Employees = new List<SelectListItem>();
            string query = "SELECT EmployeeID, EmployeeName FROM tbl_Employee";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employees.Add(new SelectListItem
                    {
                        Value = rdr["EmployeeID"].ToString(),
                        Text = rdr["EmployeeID"].ToString()
                    });
                }
                con.Close();
            }

            return Employees;
        }


        public DepartSum GetDepartmentById(string departmentId)
        {
            DepartSum department = null;
            string query = "SELECT DepartmentID, DepartmentName, DateOfCreation,Description, Status FROM Dep_Sum WHERE DepartmentID = @DepartmentID";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    try
                    {
                        // Log the columns available in the result set
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            System.Diagnostics.Debug.WriteLine($"Column: {rdr.GetName(i)}, Value: {rdr[i]}");
                        }

                        department = new DepartSum
                        {
                            DepartmentID = rdr["DepartmentID"].ToString(),
                            DepartmentName = rdr["DepartmentName"].ToString(),
                            DateOfCreation = Convert.ToDateTime(rdr["DateOfCreation"]),
                            Description = rdr["Description"].ToString(),
                            Status = rdr["Status"].ToString()
                        };
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        // Log the exception details for debugging
                        System.Diagnostics.Debug.WriteLine($"IndexOutOfRangeException: {ex.Message}");
                        throw;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No rows found for the given DepartmentID.");
                }
                con.Close();
            }
            return department;
        }
        public void UpdateDepartmentStatus(string departmentId, string status)
        {
            string query = "UPDATE Dep_Sum SET Status = @Status WHERE DepartmentID = @DepartmentID";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public string GetEmployeeName(string employeeId)
        {
            string employeeName = "";
            string query = "SELECT EmployeeName FROM tbl_Employee WHERE EmployeeID = @EmployeeID";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    employeeName = rdr["EmployeeName"].ToString();
                }
                con.Close();
            }
            return employeeName;
        }
        public IEnumerable<DepartDet> GetDepartmentDetails()
        {
            List<DepartDet> departDets = new List<DepartDet>();
            string query = @"SELECT d.SLNo, d.DepartmentID, d.EmployeeID, e.EmployeeName, d.DateOfJoining, d.ContractPeriod 
                             FROM Dep_Det d
                             JOIN tbl_Employee e ON d.EmployeeID = e.EmployeeID";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DepartDet departDet = new DepartDet
                    {
                        SLNo = Convert.ToInt32(rdr["SLNo"]),
                        DepartmentID = rdr["DepartmentID"].ToString(),
                        EmployeeID = rdr["EmployeeID"].ToString(),
                        EmployeeName = rdr["EmployeeName"].ToString(),
                        DateOfJoining = Convert.ToDateTime(rdr["DateOfJoining"]),
                        ContractPeriod = rdr["ContractPeriod"].ToString()
                    };
                    departDets.Add(departDet);
                }
                con.Close();
            }

            return departDets;
        }

        //private string Connection()
        //{
        //    throw new NotImplementedException();
        //}
    }

}