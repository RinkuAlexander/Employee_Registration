using Employee_Registration.Models;
using Employee_Registration.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Registration.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        private Department _departmentRepo = new Department();
        private SqlConnection con;

        public IEnumerable<SelectListItem> Employees { get; private set; }

        private string Connection()
        {

            string constr = ConfigurationManager.ConnectionStrings["myconn"].ToString();
            con = new SqlConnection(constr);
            return constr;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddDepartment()
        {
            var viewModel = new DepartmentViewModel();
            viewModel.Employees = _departmentRepo.GetEmployees();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddDepartment(DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Assuming _departmentRepo has a method to fetch employee name based on ID
                string employeeName = GetEmployeeName(viewModel.SelectedEmployeeID);

                // Set the fetched employee name to viewModel.EmployeeName
                viewModel.EmployeeName = employeeName;

                //Department obj = new Department();


                var departSum = new DepartSum
                {
                    DepartmentID = viewModel.DepartmentID,
                    DepartmentName = viewModel.DepartmentName,
                    DateOfCreation = viewModel.DateOfCreation,
                    Description = viewModel.Description,
                    Status = viewModel.Status
                };
                string resultSum = _departmentRepo.AddDepartmentSumDetails(departSum);

                // Example of adding DepartDet details
                var departDet = new DepartDet
                {
                    DepartmentID = viewModel.DepartmentID,
                    EmloyeeID = viewModel.SelectedEmployeeID,
                    DateOfJoining = viewModel.DateOfJoining,
                    ContractPeriod = viewModel.ContractPeriod
                };
                string resultDet = _departmentRepo.AddDepartmentDetails(departDet);
                //viewModel.EmployeeName = GetEmployeeName(viewModel.SelectedEmployeeID);

                TempData["result1"] = resultSum; // Or resultDet, depending on your logic
                ModelState.Clear();
                return RedirectToAction("ShowEmployeeDetails");
            }
            else
            {
                // Log model state errors
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }
                viewModel.Employees = _departmentRepo.GetEmployees();
                ModelState.AddModelError("", "Error in saving data");
                return View(viewModel);
            }
        }
        //private IEnumerable<SelectListItem> GetEmployeeSelectList()
        //{
        //    // Fetch list of employees for dropdown
        //    var employees = _departmentRepo.GetEmployees(); // Assuming this method retrieves employees from your repository
        //    var selectList = employees.Select(e => new SelectListItem
        //    //return employees.Select(e => new SelectListItem
        //    {
        //        Text = e.EmployeeName,
        //        Value = e.EmployeeID.ToString()
        //    });
        //    return selectList;
        //}
        private string GetEmployeeName(string employeeId)
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
    }

    //[HttpGet]
    //public ActionResult AddDepartmentSumDetails()
    //{

    //    return View();
    //}

    //[HttpPost]
    //public ActionResult AddDepartmentSumDetails(DepartSum depart)
    //{
    //    depart.DateOfCreation = Convert.ToDateTime(depart.DateOfCreation);


    //    if (ModelState.IsValid)
    //    {
    //        Department obj = new Department();
    //        string result = obj.AddDepartmentSumDetails(depart);
    //        TempData["result1"] = result;
    //        ModelState.Clear();
    //        return RedirectToAction("ShowEmployeeDetails");

    //    }
    //    else
    //    {
    //        ModelState.AddModelError("", "Error in saving data");
    //        return View(depart);
    //    }
    //}

    //[HttpGet]
    //public ActionResult AddDepartmentDetails()
    //{

    //    return View();
    //}

    //HttpPost]
    //public ActionResult AddDepartmentDetails(DepartDet depart)
    //{
    //    depart.DateOfJoining = Convert.ToDateTime(depart.DateOfJoining);


    //    if (ModelState.IsValid)
    //    {
    //        Department obj = new Department();
    //        string result = obj.AddDepartmentDetails(depart);
    //        TempData["result1"] = result;
    //        ModelState.Clear();
    //        return RedirectToAction("ShowEmployeeDetails");

    //    }
    //    else
    //    {
    //        ModelState.AddModelError("", "Error in saving data");
    //        return View(depart);
    //    }
    //}

}