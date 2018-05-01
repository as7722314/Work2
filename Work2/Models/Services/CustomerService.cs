using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Work2.Models.Services
{
    public class CustomerService
    {
        public static List<Customer> GetCustomers = new List<Customer>()
        {
            new Customer()
            {
                CustomerID=1,
                CompanyName="大雄公司",
                ContactName="老雄",
                Address="0978196729"
            },
            new Customer()
            {
                CustomerID=2,
                CompanyName="小夫公司",
                ContactName="老夫",
                Address="0972883358"
            }
        };

        public List<SelectListItem> GetCustomerList()
        {
            List<SelectListItem> costomerlist = new List<SelectListItem>();
            foreach (Customer c in GetCustomers)
            {
                costomerlist.Add(new SelectListItem()
                {
                    Text = c.CompanyName,
                    Value = c.CustomerID.ToString()
                });
            }
            return costomerlist;
        }
        public string GetCustomerCondition(int? id)
        {
            Customer customer = GetCustomers.SingleOrDefault(m => m.CustomerID == id);
            return customer.CompanyName;
        }
    }
}