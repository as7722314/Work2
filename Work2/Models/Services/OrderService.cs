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

        public List<Order> GetOrderCondition(Index arg)
        {
            CustomerService customerService = new CustomerService();///為了取出顧客名字,所以先產生Customer物件
            List<Order> orders = GetOrder;
            if (arg.OrderID.HasValue)
            {
                orders = orders.Where(m => m.OrderID == arg.OrderID).ToList();
            }
            if (arg.CompanyName != null)
            {
                orders = orders.Where(m => customerService.GetCustomerCondition(m.CustomerID).Contains(arg.CompanyName)).ToList();
                ///透過呼叫方法,傳回顧客名字
            }
            if (arg.EmployeeID.HasValue)
            {
                orders = orders.Where(m => m.EmployeeID == arg.EmployeeID).ToList();
            }
            if (arg.ShipperID.HasValue)
            {
                orders = orders.Where(m => m.ShipperID == arg.ShipperID).ToList();
            }
            if (arg.OrderDate.HasValue)
            {
                orders = orders.Where(m => m.OrderDate == arg.OrderDate).ToList();
            }
            if (arg.RequiredDate.HasValue)
            {
                orders = orders.Where(m => m.RequiredDate == arg.RequiredDate).ToList();
            }
            if (arg.ShipperDate.HasValue)
            {
                orders = orders.Where(m => m.ShipperDate == arg.ShipperDate).ToList();
            }
            return orders;
        }
        public void Del(int orderid)
        {
            int id = GetOrder.FindIndex(m => m.OrderID == orderid);
            GetOrder.RemoveAt(id);
        }
        public void InsertOrder(Order order)
        {
            order.OrderID = GetOrder.Count + 1;///orderid +1 
            GetOrder.Add(order);///新的訂單資料加進去            
        }
        public Order GetOrders(int? orderid)
        {
            Order order = GetOrder.SingleOrDefault(m => m.OrderID == orderid);
            return order;
        }
        public void Update(Order order)
        {
            int id = GetOrder.FindIndex(m => m.OrderID == order.OrderID);
            GetOrder.RemoveAt(id);
            GetOrder.Insert(id, order);
        }
    }
}