using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Work2.Models.Services
{
    public class EmployeeService
    {
        public static List<Employee> GetEmployees = new List<Employee>()
        {
            new Employee()
            {
                EmployeeID=1,
                EmployeeFirstName="大",
                EmployeeLastName="雄"
            },
            new Employee()
            {
                EmployeeID=2,
                EmployeeFirstName="小",
                EmployeeLastName="夫"
            }
        };
        public List<SelectListItem> GetEmployeeList()
        {
            List<SelectListItem> employeelist = new List<SelectListItem>();
            foreach (Employee e in GetEmployees)
            {
                employeelist.Add(new SelectListItem()
                {
                    Text = e.EmployeeFirstName + e.EmployeeLastName,
                    Value = e.EmployeeID.ToString()
                });
            }
            return employeelist;
        }
        private string GetConnStr()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnect"].ConnectionString;
        }
        public List<SelectListItem> GetEnameList()
        {
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            String sql = "SELECT EmployeeID,FirstName+LastName as EmployeeName from hr.Employees";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            List<SelectListItem> employeelist = new List<SelectListItem>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                employeelist.Add(new SelectListItem()
                {
                    Text = dataTable.Rows[i][1].ToString(),
                    Value = dataTable.Rows[i][0].ToString()
                });
            }
            return employeelist;
        }
    }
}