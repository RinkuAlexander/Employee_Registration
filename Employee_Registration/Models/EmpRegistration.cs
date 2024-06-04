using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Employee_Registration.Models
{
    public class EmpRegistration
    {
        public int Id { get; set; }
        public string EmployeeID { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [Required]
        [Display(Name = "Passport Nationality")]
        public string PassportNationality { get; set; }

        [Required]
        [Display(Name = "Passport Place of Issue")]
        public string PassportPlaceOfIssue { get; set; }

        [Required]
        [Display(Name = "Passport Date of Issue")]
        public DateTime PassportDateOfIssue { get; set; }

        [Required]
        [Display(Name = "Passport Expiry Date")]
        public DateTime PassportExpiryDate { get; set; }

        [Required]
        [Display(Name = "Place of Birth")]
        public string PlaceOfBirth { get; set; }

        [Required]
        [Display(Name = "Visa Id")]
        public string VisaId { get; set; }

        [Required]
        [Display(Name = "Visa Type")]
        public string VisaType { get; set; }

        [Required]
        [Display(Name = "Place Visa Issued")]
        public string PlaceVisaIssued { get; set; }

        [Required]
        [Display(Name = "Visa Nationality")]
        public string VisaNationality { get; set; }

        [Required]
        [Display(Name = "Visa Expiry Date")]
        public DateTime VisaExpiryDate { get; set; }

        [Required]
        [Display(Name = "Basic Salary")]
        public string BasicSalary { get; set; }

        [Required]
        [Display(Name = "Allowance")]
        public string Allowance { get; set; }

        [Required]
        [Display(Name = "Salary Deductions")]
        public string SalaryDeductions { get; set; }

       
        public string NetSalary { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
    
}