using Employee_Registration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Registration.Models
{
    public class DepartmentViewModel
    {
        // Properties from DepartSum
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        // Properties from DepartDet
        public int SlNo { get; set; }
        public string EmployeeID { get; set; }
        public string SelectedEmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string ContractPeriod { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }

        public List<DepartDet> DepartDetList { get; set; }
        public string SelectedDepartmentID { get; set; }
        public List<EmployeeDetail> EmployeeDetails { get; set; }
        public DepartmentViewModel()
        {
            // Initialize list to avoid null reference
            DepartDetList = new List<DepartDet>();
            EmployeeDetails = new List<EmployeeDetail>();
        }
    }
    public class EmployeeDetail
    {
        public int SlNo { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string ContractPeriod { get; set; }
    }

}