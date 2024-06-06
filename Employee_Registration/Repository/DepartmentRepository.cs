using Employee_Registration.Interfaces;
using Employee_Registration.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Employee_Registration.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["myconn"].ToString();
        }

        public string GetLatestDepartmentId()
        {
            string latestDepartmentId = string.Empty;
            string query = "SELECT TOP 1 DepartmentID FROM Departments ORDER BY DepartmentID DESC";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    latestDepartmentId = rdr["DepartmentID"].ToString();
                }
                con.Close();
            }

            return latestDepartmentId;
        }

        public IEnumerable<SelectListItem> GetEmployees()
        {
            List<SelectListItem> employees = new List<SelectListItem>();
            string query = "SELECT EmployeeID, EmployeeName FROM tbl_Employee";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employees.Add(new SelectListItem
                    {
                        Value = rdr["EmployeeID"].ToString(),
                        Text = rdr["EmployeeName"].ToString()
                    });
                }
                con.Close();
            }

            return employees;
        }

        public string AddDepartmentSumDetails(DepartSum depart)
        {
            string result;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand com = new SqlCommand("sp_DepartmentSum", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Query", 1);
                com.Parameters.AddWithValue("@DepartmentName", depart.DepartmentName);
                com.Parameters.AddWithValue("@DateOfCreation", depart.DateOfCreation);
                com.Parameters.AddWithValue("@Description", depart.Description);
                com.Parameters.AddWithValue("@Status", depart.Status);

                con.Open();
                result = com.ExecuteNonQuery().ToString();
                con.Close();
            }
            return result;
        }

        public string AddDepartmentDetails(DepartDet departDet)
        {
            string result;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand com = new SqlCommand("sp_DepartmentDetails", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Query", 1);
                com.Parameters.AddWithValue("@DepartmentID", departDet.DepartmentID);
                com.Parameters.AddWithValue("@EmloyeeID", departDet.EmloyeeID);
                com.Parameters.AddWithValue("@DateOfJoining", departDet.DateOfJoining);
                com.Parameters.AddWithValue("@ContractPeriod", departDet.ContractPeriod);

                con.Open();
                result = com.ExecuteNonQuery().ToString();
                con.Close();
            }
            return result;
        }
    }
}
