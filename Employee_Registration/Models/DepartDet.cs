using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Registration.Models
{
    public class DepartDet
    {
        public int SLNo { get; set; }
        public string DepartmentID { get; set; }
        public string EmloyeeID { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string ContractPeriod { get; set; }
        public string EmployeeID { get; internal set; }
        public string EmployeeName { get; internal set; }
    }
}