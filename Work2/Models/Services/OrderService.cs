using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Work2.Models.Services
{
    public class OrderService
    {
        public static List<Order> GetOrder = new List<Order>()
        {
                new Order(){
                    OrderID = 1,
                    CustomerID = 1,
                    EmployeeID = 1,
                    OrderDate = new DateTime(2018,4,8),
                    RequiredDate = new DateTime(2018,4,10),
                    ShipperDate = new DateTime(2018,4,9),
                    ShipperID = 1,
                    Freight = 100,
                    ShipAddress = "雲林縣北港鎮",
                    ShipCity = "雲林縣",
                    ShipRegion = "北港鎮",
                    CitShipPostalCodey = "651",
                    ShipCountry = "台灣"

                },new Order(){
                    OrderID = 2,
                    CustomerID = 2,
                    EmployeeID = 2,
                    OrderDate = new DateTime(2018,4,8),
                    RequiredDate = new DateTime(2018,4,10),
                    ShipperDate = new DateTime(2018,4,9),
                    ShipperID = 2,
                    Freight = 5000,
                    ShipAddress = "高雄市楠梓區",
                    ShipCity = "高雄市",
                    ShipRegion = "楠梓區",
                    CitShipPostalCodey = "801",
                    ShipCountry = "台灣"
                }

        };
        public Order GetOrders(int id)
        {
            Order order = GetOrder.SingleOrDefault(m => m.OrderID == id);
            return order;
        }
        public List<Order> GetOrderCondition(Index arg)
        {
            CustomerService customerService = new CustomerService();
            IEnumerable<Order> orders = GetOrder;
            if (arg.OrderID.HasValue)
            {
                orders = orders.Where(m => m.OrderID == arg.OrderID);
            }
            if (arg.CompanyName != null)
            {
                orders = orders.Where(m => customerService.GetCustomerCondition(m.CustomerID).Contains(arg.CompanyName));
            }
            if (arg.EmployeeID.HasValue)
            {
                orders = orders.Where(m => m.EmployeeID == arg.EmployeeID);
            }
            if (arg.ShipperID.HasValue)
            {
                orders = orders.Where(m => m.ShipperID == arg.ShipperID);
            }
            if (arg.OrderDate.HasValue)
            {
                orders = orders.Where(m => m.OrderDate == arg.OrderDate);
            }
            if (arg.RequiredDate.HasValue)
            {
                orders = orders.Where(m => m.RequiredDate == arg.RequiredDate);
            }
            if (arg.ShipperDate.HasValue)
            {
                orders = orders.Where(m => m.ShipperDate == arg.ShipperDate);
            }
            return orders.ToList();
        }
        public void InsertOrder(Order order)
        {
            order.OrderID = GetOrder.Count + 1;
            GetOrder.Add(order);
        }
        public void Del(int orderid)
        {
            Order order = GetOrder.SingleOrDefault(m => m.OrderID == orderid);
            GetOrder.Remove(order);
        }
    }
}