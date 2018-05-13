using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;


namespace Work2.Models.Services
{
    public class OrderService
    {
        private string GetConnStr()
        {   ///取得資料庫連線帳密字串
            return System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnect"].ConnectionString;
        }

        public string Del(int orderid)
        {   
            var messagebox = ""; ///動作訊息
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            String sql = @"Delete from Sales.OrderDetails where OrderID=@OrderID
                           Delete from Sales.Orders where OrderID=@OrderID"; ///先刪除明細再刪除訂單
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add(new SqlParameter("@OrderID", orderid));
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
                messagebox = "刪除成功";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                messagebox = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return messagebox;

        }            
        public void InsertOrder(Order order)
        {
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string ordersql = @"INSERT INTO Sales.Orders (
                                    CustomerID,
                                    EmployeeID,
                                    OrderDate,
                                    RequiredDate,
                                    ShippedDate,
                                    ShipperID,
                                    Freight,
                                    ShipName,
                                    ShipAddress,
                                    ShipCity,
                                    ShipRegion,
                                    ShipPostalCode,
                                    ShipCountry
                                    ) 
                                    VALUES (
                                    @CustomerID,
                                    @EmployeeID, 
                                    @OrderDate, 
                                    @RequiredDate,
                                    @ShippedDate,
                                    @ShipperID, 
                                    @Freight,
                                    @ShipName, 
                                    @ShipAddress,
                                    @ShipCity, 
                                    @ShipRegion,
                                    @ShipPostalCode, 
                                    @ShipCountry
                                    )
                                    select SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(ordersql, conn);
                    command.Parameters.Add(new SqlParameter("@CustomerID", order.CustomerID));
                    command.Parameters.Add(new SqlParameter("@EmployeeID", order.EmployeeID));
                    command.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                    command.Parameters.Add(new SqlParameter("@RequiredDate", order.RequiredDate));
                    if (order.ShipperDate.HasValue)
                    {
                        command.Parameters.Add(new SqlParameter("@ShippedDate", order.ShipperDate));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ShippedDate", DBNull.Value));
                    }
                    command.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperID));
                    command.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                    command.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                    command.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
                    command.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                    if (!string.IsNullOrWhiteSpace(order.ShipRegion))
                    {
                        command.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ShipRegion", DBNull.Value));
                    }
                    if (!string.IsNullOrWhiteSpace(order.CitShipPostalCodey))
                    {
                        command.Parameters.Add(new SqlParameter("@ShipPostalCode", order.CitShipPostalCodey));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ShipPostalCode", DBNull.Value));
                    }
                    command.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
                    conn.Open();///資料庫開始

                    int orderid = Convert.ToInt32(command.ExecuteScalar());   ///取的最新的一筆第一欄第一列=orderid              

                    string detailsql = @"INSERT INTO Sales.OrderDetails (
                                        OrderID,
                                        ProductID,
                                        UnitPrice,
                                        Qty,Discount
                                        ) VALUES(
                                        @OrderID,
                                        @ProductID,
                                        @UnitPrice,
                                        @Qty,
                                        @Discount
                                        )
                                        select SCOPE_IDENTITY()";
                    SqlCommand detailcommand = new SqlCommand(detailsql, conn);
                    for (int i = 0; i < order.OrderDetail.Count; i++)
                    {
                        detailcommand.Parameters.Add(new SqlParameter("@OrderID", orderid));
                        detailcommand.Parameters.Add(new SqlParameter("@ProductID", order.OrderDetail[i].ProductID));
                        detailcommand.Parameters.Add(new SqlParameter("@UnitPrice", order.OrderDetail[i].UnitPrice));
                        detailcommand.Parameters.Add(new SqlParameter("@Qty", order.OrderDetail[i].Qty));
                        detailcommand.Parameters.Add(new SqlParameter("@Discount", "0"));
                        detailcommand.ExecuteScalar();
                        detailcommand.Parameters.Clear();
                    }
                    scope.Complete();
                    scope.Dispose();


                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close(); ///資料庫關閉
            }
        }
        
        public void Update(Order order)
        {
            
        }
        
        public DataTable GetOrderCondition(Index arg)
        {
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            String sql = "Select OrderID,CompanyName,OrderDate,ShippedDate from Sales.Orders join Sales.Customers on Sales.Orders.CustomerID = Sales.Customers.CustomerID where ";
            ///先判斷是否有值才加入條件字串
            if (arg.OrderID.HasValue)
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
            SqlCommand command = new SqlCommand(sql, conn);

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
            DataTable dataTable = ds.Tables[0]; ///***
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
                    Text = dataTable.Rows[i][1].ToString(), ///產品名稱
                    Value = dataTable.Rows[i][0].ToString() ///產品名稱ID
                });
            }
            return orderDetailList;
        }
        public String GetUnitPrice(string arg)
        {
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            String sql = "Select UnitPrice from Production.Products where ProductID = @ProductID";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add(new SqlParameter("@ProductID", arg));
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            return dataTable.Rows[0][0].ToString();
        }
        public Order GetOrders(int orderid)
        {
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            
            String sql = @"Select sales.Orders.OrderID,
                            CustomerID,
                            EmployeeID,
                            OrderDate,
                            RequiredDate,
                            ShippedDate,
                            ShipperID,
                            Freight,
                            ShipName, 
                            ShipAddress,
                            ShipCity,
                            ShipRegion,
                            ShipPostalCode,
                            ShipCountry,
                            ProductID,
                            UnitPrice,
                            Qty,
                            Discount
                           from Sales.Orders join Sales.OrderDetails 
                           on Sales.Orders.OrderID=Sales.OrderDetails.OrderID 
                           where Sales.Orders.OrderID=@OrderID";
            SqlCommand command = new SqlCommand(sql,conn);
            command.Parameters.Add(new SqlParameter("@OrderID", orderid));
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0]; ///***
            List<OrderDetail> orderdetail = new List<OrderDetail>();
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                OrderDetail detail = new OrderDetail(); ///創立一個OrderDetail物件
                detail.OrderID = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                detail.ProductID = Convert.ToInt32(ds.Tables[0].Rows[i][14]);
                detail.UnitPrice = Convert.ToDecimal(ds.Tables[0].Rows[i][15]);
                detail.Qty = Convert.ToInt32(ds.Tables[0].Rows[i][16]);
                detail.Discount = Convert.ToDouble(ds.Tables[0].Rows[i][17]);
                orderdetail.Add(detail);
            }
            Order order = new Order() ///加入orderDetail到Order
            {
                OrderID = Convert.ToInt32(ds.Tables[0].Rows[0][0]),
                CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0][1]),
                EmployeeID = Convert.ToInt32(ds.Tables[0].Rows[0][2]),
                OrderDate = Convert.ToDateTime(ds.Tables[0].Rows[0][3]),
                RequiredDate = Convert.ToDateTime(ds.Tables[0].Rows[0][4]),
                ShipperDate = Convert.ToDateTime(ds.Tables[0].Rows[0][5]),
                ShipperID = Convert.ToInt32(ds.Tables[0].Rows[0][6]),
                Freight = Convert.ToDecimal(ds.Tables[0].Rows[0][7]),
                ShipName = ds.Tables[0].Rows[0][8].ToString(),
                ShipAddress = ds.Tables[0].Rows[0][9].ToString(),
                ShipCity = ds.Tables[0].Rows[0][10].ToString(),
                ShipRegion = ds.Tables[0].Rows[0][11].ToString(),
                CitShipPostalCodey = ds.Tables[0].Rows[0][12].ToString(),
                ShipCountry = ds.Tables[0].Rows[0][13].ToString(),
                OrderDetail = orderdetail
            };
            return order;

        }
    }
}