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