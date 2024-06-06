using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Registration.Models
{
    public class DepartSum
    {
        public string DepartmentID { get; set; }  
        public string DepartmentName { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Description { get; set; }
       
        public string Status { get; set; }
    }
}