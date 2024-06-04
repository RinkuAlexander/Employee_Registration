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
                com.Parameters.AddWithValue("@EmloyeeID", departdet.EmloyeeID);
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

        //private string Connection()
        //{
        //    throw new NotImplementedException();
        //}
    }
}