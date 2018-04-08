using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Work2.Models.Services
{
    public class EmployeeService
    {
        public List<Employee> GetEmployee()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    EmployeeId = 1,
                    EmployeeFirstName="蔡",
                    EmployeeLastName="文成"
                },
                new Employee()
                {
                    EmployeeId = 2,
                    EmployeeFirstName="黃",
                    EmployeeLastName="正瑋"
                }
            };
            return employees;
        }
        public List<String> GetName()
        {
            List<Employee> namedata = GetEmployee();
            List<string> fullname = new List<string>();

            foreach (Employee a in namedata)
            {
                fullname.Add(a.EmployeeFirstName + a.EmployeeLastName);
            }



            return fullname;
        }
    }
}