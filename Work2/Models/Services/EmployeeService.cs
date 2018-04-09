using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Work2.Models.Services
{
    public class EmployeeService
    {     

       
        public static List<Employee> Employees = new List<Employee>()
            {
                new Employee()
                {
                    EmployeeID = 1,
                    EmployeeFirstName="蔡",
                    EmployeeLastName="文成"
                },
                new Employee()
                {
                    EmployeeID = 2,
                    EmployeeFirstName="黃",
                    EmployeeLastName="正瑋"
                }
            };
            
        
        public List<SelectListItem> GetNameList()
        {            
            List<SelectListItem> namelist = new List<SelectListItem>();
            foreach (Employee a in Employees)
            {
                namelist.Add(new SelectListItem()
                {
                    Text = a.EmployeeFirstName + a.EmployeeLastName,
                    Value = a.EmployeeID.ToString()
                });
                
            }
            return namelist;
        }
    }
}