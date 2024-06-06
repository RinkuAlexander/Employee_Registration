using Employee_Registration.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Employee_Registration.Interfaces // Adjust the namespace according to your project's structure
{
    public interface IDepartmentRepository
    {
        string GetLatestDepartmentId();
        IEnumerable<SelectListItem> GetEmployees();
        string AddDepartmentSumDetails(DepartSum depart);
        string AddDepartmentDetails(DepartDet departDet);
    }
}
