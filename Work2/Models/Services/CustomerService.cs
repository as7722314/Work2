using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Work2.Models.Services
{
    public class CustomerService
    {
        public static List<Customer> GetCustomer = new List<Customer>()
            {
                new Customer()
                {
                    CustomerID= 1,
                    CompanyName="老三",
                    ContactName="小三",
                    Address = "高雄"
                },
                new Customer()
                {
                    CustomerID= 1,
                    CompanyName="老四",
                    ContactName="小四",
                    Address = "雲林"
                }
            };

        public string GetCustomerCondition(int? id)
        {
            Customer customer = GetCustomer.SingleOrDefault(m => m.CustomerID == id);
            return customer.CompanyName;
        }
        public List<Customer> GetCustomers()
        {
            return GetCustomer;
        }



        public List<SelectListItem> GetCustomerList()
        {
            List<SelectListItem> CustomerList = new List<SelectListItem>();
            foreach (Customer a in GetCustomer)
            {
                CustomerList.Add(new SelectListItem()
                {
                    Text = a.CompanyName,
                    Value = a.CustomerID.ToString()
                });

            }
            return CustomerList;
        }
    }
}