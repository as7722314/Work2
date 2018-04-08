using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Work2.Models
{
    public class Order
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        ///
        [DisplayName("訂單編號")]
        public int OrderID { get; set; }
        /// <summary>
        /// 客戶編號
        /// </summary>
        /// 
        [DisplayName("客戶編號")]
        public int CustomerID { get; set; }
        /// <summary>
        /// 員工編號
        /// </summary>
        /// 
        [DisplayName("員工編號")]
        public int EmployeeID { get; set; }
        /// <summary>
        /// 訂單日期
        /// </summary>
        /// 
        [DisplayName("訂單日期")]
        [Required()]
        public string OrderDate { get; set; }
        /// <summary>
        /// 需要日期
        /// </summary>
        ///
        [DisplayName("需要日期")]
        [Required()]
        public string RequiredDate { get; set; }
        /// <summary>
        /// 發貨日期
        /// </summary>
        /// 
        [DisplayName("出貨日期")]
        public string ShippedDate { get; set; }
        /// <summary>
        /// 發貨編號
        /// </summary>
        /// 
        [DisplayName("發貨編號")]
        public int ShipperID { get; set; }
        /// <summary>
        /// 貨物
        /// </summary>
        /// 
        [DisplayName("貨物")]
        public string Freight { get; set; }
        /// <summary>
        /// 發貨地址
        /// </summary>
        /// 
        [DisplayName("發貨地址")]
        public string ShipAddress { get; set; }
        /// <summary>
        /// 發貨城市
        /// </summary>
        /// 
        [DisplayName("發貨城市")]
        public string ShipCity { get; set; }
        /// <summary>
        /// 發貨地區
        /// </summary>
        ///
        [DisplayName("發貨地區")]
        public string ShipRegion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [DisplayName("???")]
        public string CitShipPostalCodey { get; set; }
        /// <summary>
        /// 發貨國家
        /// </summary>
        /// 
        [DisplayName("發貨國家")]
        public string ShipCountry { get; set; }
    }
}