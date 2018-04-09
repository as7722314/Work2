using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Work2.Models
{
    public class Index
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        ///
        [DisplayName("訂單編號")]
        public int? OrderID { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        /// 
        [DisplayName("客戶名稱")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 員工姓名
        /// </summary>
        /// 
        [DisplayName("員工")]
        public int? EmployeeID { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        /// 
        [DisplayName("出貨公司")]
        public int? ShipperID { get; set; }
        /// <summary>
        /// 訂單日期
        /// </summary>
        /// 
        [DisplayName("訂單日期")]
        [Required()]
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 需要日期
        /// </summary>
        ///
        [DisplayName("需要日期")]
        [Required()]
        public DateTime? RequiredDate { get; set; }
        /// <summary>
        /// 發貨日期
        /// </summary>
        /// 
        [DisplayName("出貨日期")]
        public DateTime? ShipperDate { get; set; }
    }
}