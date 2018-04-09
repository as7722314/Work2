using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Work2.Models
{
    public class Employee
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        ///
        [DisplayName("員工編號")]
        public int EmployeeID { get; set; }
        /// <summary>
        /// 員工姓
        /// </summary>
        /// 
        [DisplayName("員工姓氏")]
        public string EmployeeFirstName { get; set; }
        /// <summary>
        /// 員工名
        /// </summary>
        /// 
        [DisplayName("員工名字")]
        public string EmployeeLastName { get; set; }
        /// <summary>
        /// 員工名稱
        /// </summary>
        /// 
    }
}