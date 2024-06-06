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
        private string employeeStatus;

        public IEnumerable<SelectListItem> Employees { get; private set; }

        private string GenerateDepartmentID()
        {
            string newDepartmentID = "";
            string year = DateTime.Now.ToString("yy");

            string query = "SELECT TOP 1 DepartmentID FROM Dep_Sum ORDER BY DepartmentID DESC";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    string lastDepartmentID = rdr["DepartmentID"].ToString();
                    string lastId = lastDepartmentID.Substring(7);
                    int newIdNumber = int.Parse(lastId) + 1;
                    newDepartmentID = $"{year}DEP{newIdNumber:D4}";
                }
                else
                {
                    newDepartmentID = $"{year}DEP0001";
                }
                con.Close();
            }
            return newDepartmentID;
        }
        private int GenerateSlNo()
        {
            int newSlNo = 1; // Start from 1 if the table is empty

            string query = "SELECT TOP 1 SLNo FROM Dep_Det ORDER BY SLNo DESC";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    int lastSlNo = Convert.ToInt32(rdr["SLNo"]);
                    newSlNo = lastSlNo + 1;
                }
                con.Close();
            }
            return newSlNo;
        }
        
            public ActionResult Delete(string id)
            {
                _departmentRepo.UpdateDepartmentStatus(id, "Deleted");
                return RedirectToAction("ListDep");
            }
        

        public JsonResult GetNextSlNo()
        {
            int nextSlNo = GenerateSlNo();
            return Json(new { slNo = nextSlNo }, JsonRequestBehavior.AllowGet);
        }

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
        public ActionResult DepEmp()
        {
            var viewModel = new DepartmentViewModel
            {
                // Populate the Departments dropdown list if needed
                // Example: Departments = _departmentRepo.GetDepartments().Select(d => new SelectListItem { Value = d.DepartmentID, Text = d.DepartmentName }),
                DepartDetList = _departmentRepo.GetDepartmentDetails().ToList(),
                Employees = _departmentRepo.GetEmployees()
            };
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult AddDep()
        {
            if (TempData["SuccessMessage"] != null)
            {
                // Get the success message from TempData
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            var viewModel = new DepartSum();
            viewModel.DepartmentID = GenerateDepartmentID(); // Generate DepartmentID
            viewModel.DateOfCreation = DateTime.Now; // Assuming DateOfCreation is set here
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDep(DepartSum viewModel)
        {
            if (ModelState.IsValid)
            {
                // Assuming _departmentRepo.AddDepartment(viewModel); saves the data to the database
                string result = _departmentRepo.AddDepartment(viewModel); // Assuming AddDepartment method returns a result message or ID

                // Check the result and handle accordingly
                if (!string.IsNullOrEmpty(result))
                {
                    TempData["SuccessMessage"] = "Department Created successfully.";
                    // Data saved successfully, redirect to a success page or wherever needed
                    return RedirectToAction("AddDep");
                }
                else
                {
                    // Error occurred while saving data, handle accordingly
                    ModelState.AddModelError("", "Error occurred while saving data.");
                    return View(viewModel);
                }
            }

            // If ModelState is not valid or processing fails, return to the view with the model
            return View(viewModel);
        }
        public ActionResult ListDep()
        {
            // Assuming _departmentRepo.GetDepartments() retrieves department data from the repository
            IEnumerable<DepartSum> departments = _departmentRepo.GetDepartments();

            // Pass the department data to the view
            return View(departments);
        }

        [HttpGet]
        public ActionResult AddDepartment()
        {
            var viewModel = new DepartmentViewModel
            {
                DepartmentID = GenerateDepartmentID(),
                SlNo = GenerateSlNo(),
                DateOfCreation = DateTime.Now,
                Employees = _departmentRepo.GetEmployees(),
                Departments = _departmentRepo.GetDepartments().Select(d => new SelectListItem
                {
                    Value = d.DepartmentID,
                    Text = d.DepartmentID
                })
            };

            return View(viewModel);
        }
        public JsonResult GetDepartmentDetails(string departmentId)
        {
            var department = _departmentRepo.GetDepartmentById(departmentId);
            var formattedDepartment = new
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
                DateOfCreation = department.DateOfCreation != DateTime.MinValue
                             ? department.DateOfCreation.ToString("yyyy-MM-dd")
                             : "",  // Change the format as needed
                Description = department.Description,
                Status = department.Status
            };
            return Json(formattedDepartment, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
public ActionResult AddDepartment(DepartmentViewModel viewModel)
{
    if (ModelState.IsValid)
    {
        var departSum = new DepartSum
        {
            DepartmentID = viewModel.DepartmentID,
            DepartmentName = viewModel.DepartmentName,
            DateOfCreation = viewModel.DateOfCreation,
            Description = viewModel.Description,
            Status = viewModel.Status
        };

        string resultSum = _departmentRepo.AddDepartmentSumDetails(departSum);

        foreach (var employee in viewModel.EmployeeDetails)
        {
            var departDet = new DepartDet
            {
                DepartmentID = viewModel.DepartmentID,
                SLNo = employee.SlNo,
                EmloyeeID = employee.EmployeeID,
                DateOfJoining = employee.DateOfJoining,
                ContractPeriod = employee.ContractPeriod
            };

            string resultDet = _departmentRepo.AddDepartmentDetails(departDet);
        }

        TempData["result1"] = resultSum;
        ModelState.Clear();
        return RedirectToAction("AddDepartment");
    }
    else
    {
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
        [HttpGet]
        public JsonResult GetEmployeeName(string employeeId)
        {
            string employeeName = "";
            string employeeStatus = "";
            string query = "SELECT EmployeeName, Status FROM tbl_Employee WHERE EmployeeID = @EmployeeID";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    employeeName = rdr["EmployeeName"].ToString();
                    employeeStatus = rdr["Status"].ToString();
                }
                con.Close();
            }

            return Json(new { Name = employeeName, Status = employeeStatus }, JsonRequestBehavior.AllowGet);
        }

        private string GetEmployeeNameById(string employeeId)
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