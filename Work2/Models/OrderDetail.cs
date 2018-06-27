using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Work2.Models
{
    public class OrderDetail
    {
        public int? OrderID { get; set; }
        //public int ProductID { get; set; }
        public List<ProductList> ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public double? Discount { get; set; }
    }
}