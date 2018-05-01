using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Work2.Models.Services
{
    public class ShipperService
    {
        public static List<Shipper> GetShippers = new List<Shipper>()
        {
            new Shipper()
            {
                ShipperID=1,
                CompanyName="大雄公司",
                Phone="0978196729"
            },
            new Shipper()
            {
                ShipperID=2,
                CompanyName="小夫公司",
                Phone="0972883358"
            }
        };
        public List<SelectListItem> GetShipperList()
        {
            List<SelectListItem> shipperlist = new List<SelectListItem>();
            foreach (Shipper s in GetShippers)
            {
                shipperlist.Add(new SelectListItem()
                {
                    Text = s.CompanyName,
                    Value = s.ShipperID.ToString()
                });
            }
            return shipperlist;
        }
        private string GetConnStr()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnect"].ConnectionString;
        }
        public List<SelectListItem> GetSnameList()
        {
            String connStr = GetConnStr();
            SqlConnection conn = new SqlConnection(connStr);
            String sql = "select ShipperID,CompanyName from Sales.Shippers";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable dataTable = ds.Tables[0];
            List<SelectListItem> shipperlist = new List<SelectListItem>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                shipperlist.Add(new SelectListItem()
                {
                    Text = dataTable.Rows[i][1].ToString(),
                    Value = dataTable.Rows[i][0].ToString()
                });
            }
            return shipperlist;
        }
    }
}