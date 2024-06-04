using Employee_Registration.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Employee_Registration.Repository
{
    public class Employee
    {
       

        public string AddEmployeeDetails(EmpRegistration empreg)
        {
            SqlConnection con = null;
            string result;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString());
                SqlCommand com = new SqlCommand("sp_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Query", 1);
                com.Parameters.AddWithValue("@EmployeeName", empreg.EmployeeName);
                com.Parameters.AddWithValue("@PassportNumber", empreg.PassportNumber);
                com.Parameters.AddWithValue("@PassportNationality", empreg.PassportNationality);
                com.Parameters.AddWithValue("@PassportPlaceOfIssue", empreg.PassportPlaceOfIssue);
                com.Parameters.AddWithValue("@PassportDateOfIssue", empreg.PassportDateOfIssue);
                com.Parameters.AddWithValue("@PassportExpiryDate", empreg.PassportExpiryDate);
                com.Parameters.AddWithValue("@PlaceOfBirth", empreg.PlaceOfBirth);
                com.Parameters.AddWithValue("@VisaId", empreg.VisaId);
                com.Parameters.AddWithValue("@VisaType", empreg.VisaType);
                com.Parameters.AddWithValue("@PlaceVisaIssued", empreg.PlaceVisaIssued);
                com.Parameters.AddWithValue("@VisaNationality", empreg.VisaNationality);
                com.Parameters.AddWithValue("@VisaExpiryDate", empreg.VisaExpiryDate);
                com.Parameters.AddWithValue("@BasicSalary", empreg.BasicSalary);
                com.Parameters.AddWithValue("@Allowance", empreg.Allowance);
                com.Parameters.AddWithValue("@SalaryDeductions", empreg.SalaryDeductions);
                com.Parameters.AddWithValue("@NetSalary", empreg.NetSalary);
                com.Parameters.AddWithValue("@Status", empreg.Status);
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
                con.Close();
            }

        }

        //public List<EmpRegistration> GetEmployeeDetails()
        //{
        //    SqlConnection con = null;
        //    DataSet ds = null;
        //    List<EmpRegistration> employeelist = null;
        //    try
        //    {
        //        con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString());
        //        SqlCommand com = new SqlCommand("sp_Employee", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@Query", 2);
        //        com.Parameters.AddWithValue("@Id", null);
        //        com.Parameters.AddWithValue("@EmployeeId", null);
        //        com.Parameters.AddWithValue("@EmployeeName", null);
        //        com.Parameters.AddWithValue("@PassportNumber", null);
        //        com.Parameters.AddWithValue("@PassportNationality", null);
        //        com.Parameters.AddWithValue("@@PassportPlaceOfIssue", null);
        //        com.Parameters.AddWithValue("@PassportDateOfIssue", null);
        //        com.Parameters.AddWithValue("@PassportExpiryDate", null);
        //        com.Parameters.AddWithValue("@PlaceOfBirth", null);
        //        com.Parameters.AddWithValue("@VisaId", null);
        //        com.Parameters.AddWithValue("@VisaType", null);
        //        com.Parameters.AddWithValue("@PlaceVisaIssued", null);
        //        com.Parameters.AddWithValue("@VisaNationality", null);
        //        com.Parameters.AddWithValue("@VisaExpiryDate", null);
        //        com.Parameters.AddWithValue("@BasicSalary", null);
        //        com.Parameters.AddWithValue("@Allowance", null);
        //        com.Parameters.AddWithValue("@SalaryDeductions", null);
        //        com.Parameters.AddWithValue("@NetSalary", null);
        //        com.Parameters.AddWithValue("@Status", null);
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = com;
        //        ds = new DataSet();
        //        da.Fill(ds);
        //        employeelist = new List<EmpRegistration>();
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            EmpRegistration eobj = new EmpRegistration();
        //            eobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
        //            eobj.EmployeeID = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
        //            eobj.EmployeeName = ds.Tables[0].Rows[i]["EmployeeName"].ToString();
        //            eobj.PassportNumber = ds.Tables[0].Rows[i]["PassportNumber"].ToString();
        //            eobj.PassportNationality = ds.Tables[0].Rows[i]["PassportNationality"].ToString();
        //            eobj.PassportPlaceOfIssue = ds.Tables[0].Rows[i]["PassportPlaceOfIssue"].ToString();
        //            eobj.PassportDateOfIssue = Convert.ToDateTime(ds.Tables[0].Rows[i]["PassportDateOfIssue"].ToString());
        //            eobj.PassportExpiryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["PassportExpiryDate"].ToString());
        //            eobj.PlaceOfBirth = ds.Tables[0].Rows[i]["PlaceOfBirth"].ToString();
        //            eobj.VisaId = ds.Tables[0].Rows[i]["VisaId"].ToString();
        //            eobj.VisaType = ds.Tables[0].Rows[i]["VisaType"].ToString();
        //            eobj.PlaceVisaIssued = ds.Tables[0].Rows[i]["PlaceVisaIssued"].ToString();
        //            eobj.VisaNationality = ds.Tables[0].Rows[i]["VisaNationality"].ToString();
        //            eobj.VisaExpiryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["VisaExpiryDate"].ToString());
        //            eobj.BasicSalary = ds.Tables[0].Rows[i]["BasicSalary"].ToString();
        //            eobj.Allowance = ds.Tables[0].Rows[i]["Allowance"].ToString();
        //            eobj.SalaryDeductions = ds.Tables[0].Rows[i]["SalaryDeductions"].ToString();
        //            eobj.NetSalary = ds.Tables[0].Rows[i]["NetSalary"].ToString();
        //            eobj.Status = ds.Tables[0].Rows[i]["Status"].ToString();
        //            employeelist.Add(eobj);
        //        }

        //        return employeelist;
        //    }
        //    catch
        //    {
        //        return employeelist;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        public List<EmpRegistration> GetEmployeeDetails(string id)
        {
            List<EmpRegistration> employeelist = new List<EmpRegistration>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                try
                {
                    using (SqlCommand com = new SqlCommand("sp_Employee", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@Query", 2);

                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                EmpRegistration eobj = new EmpRegistration
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    EmployeeID = row["EmployeeID"].ToString(),
                                    EmployeeName = row["EmployeeName"].ToString(),
                                    PassportNumber = row["PassportNumber"].ToString(),
                                    PassportNationality = row["PassportNationality"].ToString(),
                                    PassportPlaceOfIssue = row["PassportPlaceOfIssue"].ToString(),
                                    PassportDateOfIssue = Convert.ToDateTime(row["PassportDateOfIssue"]),
                                    PassportExpiryDate = Convert.ToDateTime(row["PassportExpiryDate"]),
                                    PlaceOfBirth = row["PlaceOfBirth"].ToString(),
                                    VisaId = row["VisaId"].ToString(),
                                    VisaType = row["VisaType"].ToString(),
                                    PlaceVisaIssued = row["PlaceVisaIssued"].ToString(),
                                    VisaNationality = row["VisaNationality"].ToString(),
                                    VisaExpiryDate = Convert.ToDateTime(row["VisaExpiryDate"]),
                                    BasicSalary = row["BasicSalary"].ToString(),
                                    Allowance = row["Allowance"].ToString(),
                                    SalaryDeductions = row["SalaryDeductions"].ToString(),
                                    NetSalary = row["NetSalary"].ToString(),
                                    Status = row["Status"].ToString()
                                };

                                employeelist.Add(eobj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optionally log the error
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return employeelist;
        }

        public string EditEmployee(EmpRegistration empreg)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString());
                SqlCommand com = new SqlCommand("sp_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Query", 3);
                com.Parameters.AddWithValue("@Id", empreg.Id);
                com.Parameters.AddWithValue("@EmployeeID", empreg.EmployeeID);
                com.Parameters.AddWithValue("@EmployeeName", empreg.EmployeeName);
                com.Parameters.AddWithValue("@PassportNumber", empreg.PassportNumber);
                com.Parameters.AddWithValue("@PassportNationality", empreg.PassportNationality);
                com.Parameters.AddWithValue("@PassportPlaceOfIssue", empreg.PassportPlaceOfIssue);
                com.Parameters.AddWithValue("@PassportDateOfIssue", empreg.PassportDateOfIssue);
                com.Parameters.AddWithValue("@PassportExpiryDate", empreg.PassportExpiryDate);
                com.Parameters.AddWithValue("@PlaceOfBirth", empreg.PlaceOfBirth);
                com.Parameters.AddWithValue("@VisaId", empreg.VisaId);
                com.Parameters.AddWithValue("@VisaType", empreg.VisaType);
                com.Parameters.AddWithValue("@PlaceVisaIssued", empreg.PlaceVisaIssued);
                com.Parameters.AddWithValue("@VisaNationality", empreg.VisaNationality);
                com.Parameters.AddWithValue("@VisaExpiryDate", empreg.VisaExpiryDate);
                com.Parameters.AddWithValue("@BasicSalary", empreg.BasicSalary);
                com.Parameters.AddWithValue("@Allowance", empreg.Allowance);
                com.Parameters.AddWithValue("@SalaryDeductions", empreg.SalaryDeductions);
                com.Parameters.AddWithValue("@NetSalary", empreg.NetSalary);
                com.Parameters.AddWithValue("@Status", empreg.Status);


                con.Open();
                com.ExecuteNonQuery(); // ExecuteNonQuery() is used for INSERT, UPDATE, DELETE operations
                result = "Success"; // Assuming the update operation was successful
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine("Error: " + ex.Message);
                return result = "Failed"; // Return a failure indication
            }
            finally
            {
                con.Close();
            }
        }

        //        con.Open();
        //        result = com.ExecuteScalar().ToString();
        //        return result;
        //    }

        //    catch
        //    {
        //        return result = "";

        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //}

        public EmpRegistration SelectEmployeebyID(String EmpId)
        {
            SqlConnection con = null;
            DataSet ds = null;
            EmpRegistration eobj = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString());
                SqlCommand com = new SqlCommand("sp_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Query", 4);
                com.Parameters.AddWithValue("@Id", EmpId);
                com.Parameters.AddWithValue("@EmployeeID", null);
                com.Parameters.AddWithValue("@EmployeeName", null);
                com.Parameters.AddWithValue("@PassportNumber", null);
                com.Parameters.AddWithValue("@PassportNationality", null);
                com.Parameters.AddWithValue("@PassportPlaceOfIssue", null);
                com.Parameters.AddWithValue("@PassportDateOfIssue", null);
                com.Parameters.AddWithValue("@PassportExpiryDate", null);
                com.Parameters.AddWithValue("@PlaceOfBirth", null);
                com.Parameters.AddWithValue("@VisaId", null);
                com.Parameters.AddWithValue("@VisaType", null);
                com.Parameters.AddWithValue("@PlaceVisaIssued", null);
                com.Parameters.AddWithValue("@VisaNationality", null);
                com.Parameters.AddWithValue("@VisaExpiryDate", null);
                com.Parameters.AddWithValue("@BasicSalary", null);
                com.Parameters.AddWithValue("@Allowance", null);
                com.Parameters.AddWithValue("@SalaryDeductions", null);
                com.Parameters.AddWithValue("@NetSalary", null);
                com.Parameters.AddWithValue("@Status", null);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = com;
                ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    eobj = new EmpRegistration();
                    eobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    eobj.EmployeeID = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
                    eobj.EmployeeName = ds.Tables[0].Rows[i]["EmployeeName"].ToString();
                    eobj.PassportNumber = ds.Tables[0].Rows[i]["PassportNumber"].ToString();
                    eobj.PassportNationality = ds.Tables[0].Rows[i]["PassportNationality"].ToString();
                    eobj.PassportPlaceOfIssue = ds.Tables[0].Rows[i]["PassportPlaceOfIssue"].ToString();
                    eobj.PassportDateOfIssue = Convert.ToDateTime(ds.Tables[0].Rows[i]["PassportDateOfIssue"].ToString());
                    eobj.PassportExpiryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["PassportExpiryDate"].ToString());
                    eobj.PlaceOfBirth = ds.Tables[0].Rows[i]["PlaceOfBirth"].ToString();
                    eobj.VisaId = ds.Tables[0].Rows[i]["VisaId"].ToString();
                    eobj.VisaType = ds.Tables[0].Rows[i]["VisaType"].ToString();
                    eobj.PlaceVisaIssued = ds.Tables[0].Rows[i]["PlaceVisaIssued"].ToString();
                    eobj.VisaNationality = ds.Tables[0].Rows[i]["VisaNationality"].ToString();
                    eobj.VisaExpiryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["VisaExpiryDate"].ToString());
                    eobj.BasicSalary = ds.Tables[0].Rows[i]["BasicSalary"].ToString();
                    eobj.Allowance = ds.Tables[0].Rows[i]["Allowance"].ToString();
                    eobj.SalaryDeductions = ds.Tables[0].Rows[i]["SalaryDeductions"].ToString();
                    eobj.NetSalary = ds.Tables[0].Rows[i]["NetSalary"].ToString();
                    eobj.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                }
                return eobj;
            }
            catch
            {
                return eobj;
            }
            finally
            {
                con.Close();
            }
        }

        public int DeleteEmployeeDetails(string id)
        {
            SqlConnection con = null;
            int result = 0;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString());
                SqlCommand com = new SqlCommand("sp_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                com.Parameters.AddWithValue("@Query", 5);

                con.Open();
                result = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }


        //public int DeleteEmployeeDetails(String ID)
        //{
        //    SqlConnection con = null;
        //    int result;
        //    try
        //    {
        //        con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString());
        //        SqlCommand com = new SqlCommand("sp_Employee", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@Id", ID);
        //        //com.Parameters.AddWithValue("@EmployeeID", null);
        //        //com.Parameters.AddWithValue("@EmployeeName", null);
        //        //com.Parameters.AddWithValue("@PassportNumber", null);
        //        //com.Parameters.AddWithValue("@PassportNationality", null);
        //        //com.Parameters.AddWithValue("@PassportPlaceOfIssue", null);
        //        //com.Parameters.AddWithValue("@PassportDateOfIssue", null);
        //        //com.Parameters.AddWithValue("@PassportExpiryDate", null);
        //        //com.Parameters.AddWithValue("@PlaceOfBirth", null);
        //        //com.Parameters.AddWithValue("@VisaId", null);
        //        //com.Parameters.AddWithValue("@VisaType", null);
        //        //com.Parameters.AddWithValue("@PlaceVisaIssued", null);
        //        //com.Parameters.AddWithValue("@VisaNationality", null);
        //        //com.Parameters.AddWithValue("@VisaExpiryDate", null);
        //        //com.Parameters.AddWithValue("@BasicSalary", null);
        //        //com.Parameters.AddWithValue("@Allowance", null);
        //        //com.Parameters.AddWithValue("@SalaryDeductions", null);
        //        //com.Parameters.AddWithValue("@NetSalary", null);
        //        //com.Parameters.AddWithValue("@Status", null);
        //        com.Parameters.AddWithValue("@Query", 5);
        //        con.Open();
        //        result = com.ExecuteNonQuery();
        //        return result;
        //    }
        //    catch
        //    {
        //        return result = 0;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

    }
}