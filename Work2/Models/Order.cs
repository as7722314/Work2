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
        /// 訂單Detail
        /// </summary>
        /// 
        public List<OrderDetail> OrderDetails { get; set; }
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
        [Required]
        [DisplayName("客戶名稱")]
        public int? CustomerID { get; set; }
        /// <summary>
        /// 員工編號
        /// </summary>
        /// 
        [Required]
        [DisplayName("員工姓名")]
        public int? EmployeeID { get; set; }
        /// <summary>
        /// 訂單日期
        /// </summary>
        /// 
        [Required]
        [DisplayName("訂單日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 需要日期
        /// </summary>
        ///
        [Required]
        [DisplayName("需要日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? RequiredDate { get; set; }
        /// <summary>
        /// 發貨日期
        /// </summary>
        /// 
        [DisplayName("發貨日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ShipperDate { get; set; }
        /// <summary>
        /// 發貨編號
        /// </summary>
        /// 
        [DisplayName("發貨公司")]
        public int? ShipperID { get; set; }
        /// <summary>
        /// 運費
        /// </summary>
        /// 
        [Range(0,10000)]
        [DisplayName("運費")]
        public decimal? Freight { get; set; }
        /// <summary>
        /// 發貨地址
        /// </summary>
        /// 
        [StringLength(8, ErrorMessage = "字串最多輸入8個字")]
        [DisplayName("發貨地址")]
        public string ShipAddress { get; set; }
        /// <summary>
        /// 發貨城市
        /// </summary>
        /// 
        [StringLength(8, ErrorMessage = "字串最多輸入8個字")]
        [DisplayName("發貨城市")]
        public string ShipCity { get; set; }
        /// <summary>
        /// 發貨地區
        /// </summary>
        ///
        [StringLength(8, ErrorMessage = "字串最多輸入{1}個字")]
        [DisplayName("發貨地區")]
        public string ShipRegion { get; set; }
        /// <summary>
        /// 郵遞區號
        /// </summary>
        /// 
        [StringLength(8, ErrorMessage = "字串最多輸入8個字")]
        [DisplayName("郵遞區號")]
        public string CitShipPostalCodey { get; set; }
        /// <summary>
        /// 發貨國家
        /// </summary>
        /// 
        [StringLength(8, ErrorMessage = "字串最多輸入8個字")]
        [DisplayName("發貨國家")]
        public string ShipCountry { get; set; }
    }
}