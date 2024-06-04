using Employee_Registration.Models;
using Employee_Registration.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using System.Net;

namespace Employee_Registration.Controllers
{
    public class EmpRegController : Controller
    {
        private SqlConnection con;
        private string Connection()
        {

            string constr = ConfigurationManager.ConnectionStrings["myconn"].ToString();
            con = new SqlConnection(constr);
            return constr;
        }
        public ActionResult Create()
        {
            ViewBag.Nationalities = new SelectList(GetNationalities());
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmpRegistration empreg)
        {
            if (ModelState.IsValid)
            {
                Employee obj = new Employee();
                string result = obj.AddEmployeeDetails(empreg);
                TempData["result1"] = result;
                ModelState.Clear();
                return RedirectToAction("ShowEmployeeDetails");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                ViewBag.Nationalities = new SelectList(GetNationalities());
                return View(empreg);
            }
        }

        private SelectList GetNationalities()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var countryNames = cultures.Select(culture => new RegionInfo(culture.Name))
                                        .OrderBy(info => info.EnglishName)
                                        .Select(info => new SelectListItem
                                        {
                                            Value = info.Name,          // Use the country code as the value
                                    Text = info.EnglishName    // Display the English country name as the text
                                })
                                        .GroupBy(item => item.Text)  // Group by country name
                                        .Select(group => group.First())  // Select the first item from each group (distinct)
                                        .ToList();

            return new SelectList(countryNames, "Value", "Text");
        }



        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddEmployeeDetails()
        {
            ViewBag.Nationalities = GetNationalities();
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployeeDetails(EmpRegistration empreg)
        {
            empreg.PassportDateOfIssue = Convert.ToDateTime(empreg.PassportDateOfIssue);
            empreg.PassportExpiryDate = Convert.ToDateTime(empreg.PassportExpiryDate);
            empreg.VisaExpiryDate = Convert.ToDateTime(empreg.VisaExpiryDate);
            
            if (ModelState.IsValid)
            {
                Employee obj = new Employee();
                string result = obj.AddEmployeeDetails(empreg);
                TempData["result1"] = result;
                ModelState.Clear();
                return RedirectToAction("ShowEmployeeDetails");

            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View(empreg);
            }
        }

        [HttpGet]
        public ActionResult ShowEmployeeDetails(string ID)
        {
            //Employee obj = new Employee();
            //ModelState.Clear();
            //return View(obj.GetEmployeeDetails());
            Employee obj = new Employee();
            var employees = obj.GetEmployeeDetails(ID);
            return View(employees);
        }

        [HttpGet]
        public ActionResult EditEmployee(string ID)
        {
            Employee obj = new Employee();
            EmpRegistration objemployee = new EmpRegistration();
            ViewBag.Nationalities = GetNationalities();
            return View(obj.SelectEmployeebyID(ID));
        }


        
        [HttpPost]
        public ActionResult EditEmployee(EmpRegistration objemployee)
        {
            objemployee.PassportExpiryDate = Convert.ToDateTime(objemployee.PassportExpiryDate);
            objemployee.PassportDateOfIssue = Convert.ToDateTime(objemployee.PassportDateOfIssue);
            objemployee.VisaExpiryDate = Convert.ToDateTime(objemployee.VisaExpiryDate);
            if (ModelState.IsValid)    
            {
                Employee obj = new Employee();   
                string result = obj.EditEmployee(objemployee);
                TempData["result2"] = result;
                ModelState.Clear();   
                return RedirectToAction("ShowEmployeeDetails");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteEmployee(string id)
        {
          Employee obj = new Employee();
            int result = obj.DeleteEmployeeDetails(id);
            TempData["result3"] = result;
            ModelState.Clear();
            return RedirectToAction("ShowEmployeeDetails");
        }


    }

}