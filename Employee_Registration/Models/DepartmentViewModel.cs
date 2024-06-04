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
        public string EmployeeID { get; set; }
        public string SelectedEmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string ContractPeriod { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}