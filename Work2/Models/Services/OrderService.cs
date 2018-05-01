using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private string GetConnStr()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnect"].ConnectionString;
        }
        public DataTable GetOrderCondition(Index arg)
        {
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            String sql = "Select OrderID,CompanyName,OrderDate,ShippedDate from Sales.Orders join Sales.Customers on Sales.Orders.CustomerID = Sales.Customers.CustomerID where ";
            if(arg.OrderID.HasValue)
            {
                sql += "OrderID = @OrderID";
            }
            else
            {
                sql += "OrderID like @OrderID";
            }
            if (!string.IsNullOrWhiteSpace(arg.CompanyName))
            {
                sql += " and CompanyName = @CompanyName";
            }
            if (arg.EmployeeID.HasValue)
            {
                sql += " and EmployeeID = @EmployeeID";
            }
            if (arg.ShipperID.HasValue)
            {
                sql += " and ShipperID = @ShipperID";
            }
            if (arg.OrderDate.HasValue)
            {
                sql += " and OrderDate = @OrderDate";
            }
            if (arg.RequiredDate.HasValue)
            {
                sql += " and RequiredDate = @RequiredDate";
            }
            if (arg.ShipperDate.HasValue)
            {
                sql += " and ShipperedDate = @ShipperDate";
            }
            SqlCommand command = new SqlCommand(sql,conn);
            
            if (arg.OrderID.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@OrderID", arg.OrderID));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@OrderID", "%"));
            }
            if (!string.IsNullOrWhiteSpace(arg.CompanyName))
            {
                command.Parameters.Add(new SqlParameter("@CompanyName", arg.CompanyName));
            }
            if (arg.EmployeeID.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@EmployeeID", arg.EmployeeID));
            }
            if (arg.ShipperID.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@ShipperID", arg.ShipperID));
            }
            if (arg.OrderDate.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@OrderDate", arg.OrderDate));
            }
            if (arg.RequiredDate.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@RequiredDate", arg.RequiredDate));
            }
            if (arg.ShipperDate.HasValue)
            {
                command.Parameters.Add(new SqlParameter("@ShipperDate", arg.ShipperDate));
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            return dataTable;
        }
        public List<SelectListItem> GetOrderDetailList()
        {
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            String sql = "Select ProductID,ProductName from Production.Products";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            List<SelectListItem> orderDetailList = new List<SelectListItem>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                orderDetailList.Add(new SelectListItem()
                {
                    Text = dataTable.Rows[i][1].ToString(),
                    Value = dataTable.Rows[i][0].ToString()
                });
            }
            return orderDetailList;
        }
    }
}