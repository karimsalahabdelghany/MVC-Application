using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }    //pk
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length is 5 Chars")]
        public string Name { get; set; }    //not required
        [Range(22, 35, ErrorMessage = "age must be In Range From 22 to 35")]
        public int? Age { get; set; }
      //  [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}[a-zA-Z]{5,10}$",ErrorMessage = "Address Must be like 123 street-city-country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phonenumber { get; set; }
        public DateTime HireDate { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }  //FK
        //FK optional => onDelete => Restrict
        //FK Required => onDelete => Cascade
        [InverseProperty("Employees")]
        public Department Department { get; set; }
        //Navigational property one
    }
}
