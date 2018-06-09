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
        /// <summary>
        /// 取得資料庫連線帳密字串
        /// </summary>
        /// <returns></returns>
        private string GetConnStr()
        {  
            return System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnect"].ConnectionString;
        }
        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
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
                    command.Parameters.Add(new SqlParameter("@ShippedDate", order.ShipperDate.HasValue ? order.ShipperDate.Value.ToString("yyyy/MM/dd") : ""));                    
                    command.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperID));
                    command.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                    command.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                    command.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
                    command.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                    command.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion ?? ""));
                    command.Parameters.Add(new SqlParameter("@ShipPostalCode", order.CitShipPostalCodey ?? ""));                    
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
                        if (order.OrderDetail[i].ProductID == 0)
                            continue;
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
        /// <summary>
        /// 修改訂單
        /// </summary>
        /// <param name="order"></param>
        public void Update(Order order)
        {
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string updatesql = @"Update Sales.Orders Set 
                                        CustomerID=@CustomerID,
                                        EmployeeID=@EmployeeID,
                                        OrderDate=@OrderDate, 
                                        RequiredDate=@RequiredDate,
                                        ShippedDate=@ShippedDate,
                                        ShipperID=@ShipperID,
                                        Freight=@Freight,
                                        ShipName=@ShipName,
                                        ShipAddress=@ShipAddress,
                                        ShipCity=@ShipCity,
                                        ShipRegion=@ShipRegion,
                                        ShipPostalCode=@ShipPostalCode,
                                        ShipCountry=@ShipCountry
                                        Where OrderID = @OrderID";
                    SqlCommand command = new SqlCommand(updatesql, conn);
                    command.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));
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
                    command.ExecuteNonQuery();

                    string delsql = @"Delete from Sales.OrderDetails where OrderID=@OrderID";
                    SqlCommand dcommand = new SqlCommand(delsql, conn);
                    dcommand.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));                
                    dcommand.ExecuteNonQuery();
                    string insertsql = @"INSERT INTO Sales.OrderDetails (
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
                    SqlCommand detailcommand = new SqlCommand(insertsql, conn);
                    for (int i = 0; i < order.OrderDetail.Count; i++)
                    {
                        detailcommand.Parameters.Add(new SqlParameter("@OrderID", order.OrderID));
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
        /// <summary>
        /// 條件式搜尋
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Index> GetOrderCondition(Index arg)
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
                sql += " and ShippedDate = @ShipperDate";
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
            List<Index> orderdata = new List<Index>();
            foreach(DataRow dr in dataTable.Rows)
            {
                orderdata.Add(new Index()
                {
                    OrderID = Convert.ToInt32(dr["OrderID"]),
                    CompanyName = Convert.ToString(dr["CompanyName"]),
                    OrderDate = Convert.ToDateTime(dr["OrderDate"]),                    
                    ShipperDate = (!String.IsNullOrWhiteSpace(dr["ShippedDate"].ToString())) ? new DateTime? (Convert.ToDateTime(dr["ShippedDate"])) : null
                    
                });
            }            
            return orderdata;
        }
        /// <summary>
        /// 取得產品ID與名稱
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 取得產品價格
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 取得修改頁面的訂單資料
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
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
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add(new SqlParameter("@OrderID", orderid));
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0]; ///***
            List<OrderDetail> detail = new List<OrderDetail>();
            Order order = new Order();///加入orderDetail到Order
            foreach (DataRow i in ds.Tables[0].Rows)
            {
                detail.Add(new OrderDetail
                {
                    OrderID = Convert.ToInt32(i["OrderID"]),
                    ProductID = Convert.ToInt32(i["ProductID"]),
                    UnitPrice = Convert.ToDecimal(i["UnitPrice"]),
                    Qty = Convert.ToInt32(i["Qty"]),
                    Discount = Convert.ToDouble(i["Discount"])
                });
                order.OrderID = Convert.ToInt32(i["OrderID"]);
                order.CustomerID = Convert.ToInt32(i["CustomerID"]);
                order.EmployeeID = Convert.ToInt32(i["EmployeeID"]);
                order.OrderDate = Convert.ToDateTime(i["OrderDate"]);
                order.RequiredDate = Convert.ToDateTime(i["RequiredDate"]);
                order.ShipperDate = (!String.IsNullOrWhiteSpace(i["ShippedDate"].ToString()) ? new DateTime?(Convert.ToDateTime(i["ShippedDate"])) : null);
                order.ShipperID = Convert.ToInt32(i["ShipperID"]);
                order.Freight = Convert.ToDecimal(i["Freight"]);
                order.Freight = Convert.ToDecimal(i["Freight"]);
                order.Freight = Convert.ToDecimal(i["Freight"]);
                order.ShipName = i["ShipName"].ToString();
                order.ShipAddress = i["ShipAddress"].ToString();
                order.ShipCity = i["ShipCity"].ToString();
                order.ShipRegion = i["ShipRegion"].ToString();
                order.CitShipPostalCodey = i["ShipPostalCode"].ToString();
                order.ShipCountry = i["ShipCountry"].ToString();
                order.OrderDetail = detail;
            }
            return order;
        }
    }
}