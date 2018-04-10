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
        public List<Order> GetOrders(int? id)
        {
            List<Order> order = GetOrder.Where(m=>m.OrderID==id).ToList();
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
        public void Update(Order order)
        {
            List<Order> orders = GetOrder;
            if (order.CustomerID.HasValue)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.CustomerID = order.CustomerID);
            }
            if (order.EmployeeID.HasValue)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.EmployeeID = order.EmployeeID);
            }
            if (order.OrderDate.HasValue)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.OrderDate = order.OrderDate);
            }
            if (order.RequiredDate.HasValue)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.RequiredDate = order.RequiredDate);
            }
            if (order.ShipperDate.HasValue)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.ShipperDate = order.ShipperDate);
            }
            if (order.ShipperID.HasValue)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.ShipperID = order.ShipperID);
            }
            if (order.Freight.HasValue)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.Freight = order.Freight);
            }
            if (order.ShipAddress != null)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.ShipAddress = order.ShipAddress);
            }
            if (order.ShipCity != null)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.ShipCity = order.ShipCity);
            }
            if (order.ShipCountry != null)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.ShipCountry = order.ShipCountry);
            }
            if (order.ShipRegion != null)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.ShipRegion = order.ShipRegion);
            }
            if (order.CitShipPostalCodey != null)
            {
                orders.Where(m => m.OrderID == order.OrderID).ToList().ForEach(m => m.CitShipPostalCodey = order.CitShipPostalCodey);
            }
        }
    }
}