using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Work2.Models.Services
{
    public class ShipperService
    {
        public static List<Shipper> shippers = new List<Shipper>()
            {
                new Shipper()
                {
                    ShipperID= 1,
                    CompanyName="國際公司",
                    Phone="0952******"
                },
                new Shipper()
                {
                    ShipperID= 2,
                    CompanyName="大同公司",
                    Phone="0978******"
                }
            };
            
        
        public List<SelectListItem> GetShipperList()
        {            
            List<SelectListItem> shipperlist = new List<SelectListItem>();
            foreach (Shipper a in shippers)
            {
                shipperlist.Add(new SelectListItem()
                {
                    Text = a.CompanyName,
                    Value = a.ShipperID.ToString()
                });
                
            }
            return shipperlist;
        }
    }
}