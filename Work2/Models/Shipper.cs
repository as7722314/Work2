using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Work2.Models
{
    public class Shipper
    {
        /// <summary>
        /// 發貨人編號
        /// </summary>
        ///
        [DisplayName("發貨人編號")]
        public int ShipperID { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        /// 
        [DisplayName("公司名稱")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        /// 
        [DisplayName("電話")]
        public string Phone { get; set; }
    }
}